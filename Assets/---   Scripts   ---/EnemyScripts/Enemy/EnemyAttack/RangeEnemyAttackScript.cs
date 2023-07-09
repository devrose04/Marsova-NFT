using System;
using System.Collections;
using ______Scripts______.EnemyScripts.Enemy.Enemy;
using EnemyScripts.AIScripts;
using EnemyScripts.Enemy.EnemyAttack;
using ObjectsScripts;
using PlayerScripts;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace EnemyScripts.Enemy
{
    public class RangeEnemyAttackScript : MonoBehaviour
    {
        private AIScript __AIScript;

        [SerializeField] private GameObject EnemyBullet;
        private GameObject Enemy;
        private GameObject Player;

        [SerializeField] private GameObject shotPoint;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClipFiredBullet;

        private void Awake()
        {
            Enemy = this.gameObject;
            Player = GameObject.Find("Player");
            __AIScript = Enemy.GetComponent<AIScript>();
        }

        public void StopAndAttack(GameObject enemy)
        {
            __AIScript.isEnemyAttackToPlayer = true;

            StartCoroutine(BulletIsCreated(enemy));
        }

        IEnumerator BulletIsCreated(GameObject enemy)
        {
            float hitTimeRange = enemy.GetComponent<EnemyScript>().hitTimeRange;
            yield return new WaitForSeconds(hitTimeRange); // Burda Dolum süresi zamanı

            if (enemy != null)
            {
                _audioSource.PlayOneShot(_audioClipFiredBullet);

                EnemyScript _enemyScript = enemy.GetComponent<EnemyScript>();

                ParticleSystem hitEffect = _enemyScript.HitEffect; // Enemy'nin vuruş Efektini burdan alıyoruz. 
                float damages = _enemyScript.damage;
                float knockBackPower = _enemyScript.knockBackPower;

                // buraya mermiyi oluştur ve Player'a yolla

                // Quaternion rotation = Quaternion.LookRotation(Player.transform.position - transform.position);

                GameObject CreatedBullet = Instantiate(EnemyBullet, shotPoint.transform.position, Quaternion.identity, null); // mermiyi oluşturdum
                Destroy(CreatedBullet, 5f);

                // merminin bilgilerini aktardım.
                EnemyBulletScript _enemyBulletScript = CreatedBullet.GetComponent<EnemyBulletScript>();
                _enemyBulletScript.damages = damages;
                _enemyBulletScript.knockBackPower = knockBackPower;
                _enemyBulletScript.hitEffect = hitEffect;

                __AIScript.isEnemyAttackToPlayer = false;
            }
        }

        public void SuportEnemyStop(GameObject enemy)
        {
            float SuportTimeRange = enemy.GetComponent<EnemyScript>().hitTimeRange;
            Rigidbody2D RB2 = enemy.GetComponent<Rigidbody2D>();

            if (SuportTimeRange > 1000) // eger bu if koşulu truse ise, Suport Enemy'sidir
                RB2.velocity = new Vector2(0, 0);
        }
    }
}
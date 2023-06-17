using System;
using System.Collections;
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
            yield return new WaitForSeconds(hitTimeRange);
            if (enemy != null)
            {
                EnemyScript _enemyScript = enemy.GetComponent<EnemyScript>();

                ParticleSystem hitEffect = _enemyScript.HitEffect; // Enemy'nin vuruş Efektini burdan alıyoruz. 
                float damages = _enemyScript.damage;
                float knockBackPower = _enemyScript.knockBackPower;

                // buraya mermiyi oluştur ve Player'a yolla

                GameObject CreatedBullet = Instantiate(EnemyBullet, this.transform.position, Quaternion.identity, null); // mermiyi oluşturdum
                Destroy(CreatedBullet,5f);
                
                // merminin bilgilerini aktardım.
                EnemyBulletScript _enemyBulletScript = CreatedBullet.GetComponent<EnemyBulletScript>();
                _enemyBulletScript.damages = damages;
                _enemyBulletScript.knockBackPower = knockBackPower;
                _enemyBulletScript.hitEffect = hitEffect;


                __AIScript.isEnemyAttackToPlayer = false;
            }
        }
    }
}
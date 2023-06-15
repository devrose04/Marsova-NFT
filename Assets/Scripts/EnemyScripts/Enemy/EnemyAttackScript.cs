using System;
using System.Collections;
using EnemyScripts.AIScripts;
using ObjectsScripts;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts.Enemy
{
    public class EnemyAttackScript : MonoBehaviour
    {
        [SerializeField] float enemyAttackRadius;
        [SerializeField] LayerMask playerLayer;

        private AIScript __AIScript;
        private Calculations __Calculations;
        private PlayerScript __PlayerScript;

        private GameObject Enemy;
        private GameObject Player;

        private void Awake()
        {
            Enemy = this.gameObject;
            Player = GameObject.Find("Player");
            __AIScript = Enemy.GetComponent<AIScript>();
            __Calculations = Player.GetComponent<Calculations>();
            __PlayerScript = Player.GetComponent<PlayerScript>();
        }

        public void StopAndAttack(GameObject other)
        {
            __AIScript.isEnemyAttackToPlayer = true; // bunu bir yerde kullanmıyorum şuan

            StartCoroutine(AttackReaction(other));
        }

        IEnumerator AttackReaction(GameObject other)
        {
            float hitTimeRange = other.GetComponent<EnemyScript>().hitTimeRange;
            yield return new WaitForSeconds(hitTimeRange);
            if (other != null)
            {
                Collider2D[] _Player = Physics2D.OverlapCircleAll(other.transform.position, enemyAttackRadius, playerLayer);
                // print("Vurdugun kişi sayısı:" + hitEnemies.Length); // bunu canvasa yazdır.

                EnemyScript _enemyScript = other.GetComponent<EnemyScript>();

                ParticleSystem hitEffect = _enemyScript.HitEffect; // Enemy'nin vuruş Efektini burdan alıyoruz. 
                float damages = _enemyScript.damage;
                float knockBackPower = _enemyScript.knockBackPower;

                foreach (var player in _Player)
                {
                    Vector2 directionToPlayer;
                    float angle;

                    var result = __Calculations.CalculationsAboutToObject(other, player); // Enemy'lerin vuruş yaptıgı yerin verileri hesaplanır.
                    (directionToPlayer, angle) = result;

                    if (angle == 0) // attackAngle == 0 ise, sadece önüne vuruyor. // attackAngle == 180 ise hem önüne, hem arkasına vuruyor.
                    {
                        HitToPlayer(directionToPlayer, hitEffect, damages, knockBackPower);
                        _enemyScript.HitToPlayerTimer = 0; // zamanı burdan sıfırlamak lazım yoksa hata veriyor.
                    }
                }

                __AIScript.isEnemyAttackToPlayer = false;
            }
        }

        void HitToPlayer(Vector2 directionToPlayer, ParticleSystem hitEffect, float damages, float knockBackPower) // Player dmg alıyor   
        {
            ParticleSystem effect = Instantiate(hitEffect, Player.transform); // Effekti Player üzerinde uyguluyor
            Destroy(effect.gameObject, 5f);

            __PlayerScript.TakeDamage(damages, directionToPlayer, knockBackPower);
        }
    }
}
using System.Collections;
using EnemyScripts;
using ObjectsScripts;
using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess
// ReSharper disable Unity.PreferNonAllocApi

namespace PlayerScripts.SwordScripts
{
    public class SwordScript : MonoBehaviour
    {
        [SerializeField] public float swordDamage;
        [SerializeField] float swordRange;
        [SerializeField] float attackAngle;
        [SerializeField] public ParticleSystem hitEffect;
        [SerializeField] LayerMask enemyLayer;

        private SwordSkilsScript __SwordSkilsScript;
        private Calculations __Calculations;    

        private Vector2 directionToEnemy;
        private float angle;

        public bool isAttack = false;

        private GameObject Player;
        public Rigidbody2D RB2;

        // ReSharper disable Unity.PerformanceAnalysis
        private void Awake()
        {
            Player = this.gameObject;
            RB2 = Player.GetComponent<Rigidbody2D>();
            __SwordSkilsScript = Player.GetComponent<SwordSkilsScript>();
            __Calculations = Player.GetComponent<Calculations>();
        }

        public void SwordAttack(float dmgPower, float KnockBackPower, bool isJumpit, float moveOnTime, float HittingAllAngle)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(this.transform.position, swordRange, enemyLayer);
            // print("Vurdugun kişi sayısı:" + hitEnemies.Length); // bunu canvasa yazdır.
            StartCoroutine(__SwordSkilsScript.AttackAndMoveOn(moveOnTime, RB2));

            foreach (var enemy in hitEnemies) // yuvarlagın içindeki vurulan Enemylere teker teker bakar.
            {
                var result = __Calculations.CalculationsAboutToEnemy(enemy); // swordun çarpıtıgı Enemylerin verileri hesaplanır.
                (directionToEnemy, angle) = result;
                // Açı 60 derecenin içindeyse Enemy'e hasar ver
                if (angle <= attackAngle * HittingAllAngle) // isHittingAll == 1 ise, 60 derecelik bir açıya vurur. Yani sadece önüne vurur.
                {
                    // isHittingAll == 4 ise 240 derecelik bir açıya vurur. Oda arkasında vurmasını sağlar.
                    TakeDamages(enemy, isJumpit, dmgPower, KnockBackPower);
                }
            }
        }

        void TakeDamages(Collider2D enemy, bool isJumpit, float dmgPower, float KnockBackPower)
        {
            if (isJumpit) // Enemyi zıplatır
            {
                Rigidbody2D EnemyRB2 = enemy.gameObject.GetComponent<Rigidbody2D>();
                EnemyRB2.AddForce(new Vector2(EnemyRB2.velocity.x, Random.Range(25f, 35f)), ForceMode2D.Impulse);
            }

            ParticleSystem _hitEffect = Instantiate(hitEffect, enemy.transform.position, Quaternion.identity);
            Destroy(_hitEffect.gameObject, 2f);

            EnemyScript _enemyScript = enemy.GetComponent<EnemyScript>();
            if (enemy != null)
                _enemyScript.TakeDamages(swordDamage * dmgPower, directionToEnemy * KnockBackPower);
        }

        void OnDrawGizmosSelected() // Player'ın vuruş menzilini gösterir
        {
            // Kılıç menzilini görselleştirme
            Gizmos.color = Color.red;

            Gizmos.DrawSphere((Vector2)this.transform.position, swordRange);
        }
    }
}
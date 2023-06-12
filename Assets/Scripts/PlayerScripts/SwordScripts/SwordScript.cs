using System.Collections;
using EnemyScripts;
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
        
        private Vector2 enemyPosition;
        private Vector2 directionToEnemy;
        private Vector2 playerDirection;
        private Vector2 roundedDirectionToEnemy;
        private Vector2 clampedDirectionToEnemy;
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
        }

        public void SwordAttack(float dmgPower , float KnockBackPower,bool isJumpit, float moveOnTime, float HittingAllAngle)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(this.transform.position, swordRange, enemyLayer);
            // print("Vurdugun kişi sayısı:" + hitEnemies.Length); // bunu canvasa yazdır.
            StartCoroutine(__SwordSkilsScript.AttackAndMoveOn(moveOnTime,RB2));
            
            foreach (var enemy in hitEnemies)   // yuvarlagın içindeki vurulan Enemylere teker teker bakar.
            {
                Calculations(enemy);    // swordun çarpıtıgı Enemylerin verileri hesaplanır. (asıl önemli olan angle verisidir.)
                // Açı 60 derecenin içindeyse Enemy'e hasar ver
                if (angle <= attackAngle * HittingAllAngle)    // isHittingAll == 1 ise, 60 derecelik bir açıya vurur. Yani sadece önüne vurur.
                {                                           // isHittingAll == 4 ise 240 derecelik bir açıya vurur. Oda arkasında vurmasını sağlar.
                    TakeDamages(enemy);
                }
            }
            
            void Calculations(Collider2D enemy) // temel olarak açı verisini hesaplar.
            {
                // Düşmanın pozisyonunu al
                enemyPosition = enemy.transform.position;

                // 1-) Düşmanın pozisyonunun hangi yönde oldugunu buluyor.
                directionToEnemy = (enemyPosition - (Vector2)transform.position).normalized;  // 1 , -1 arasında bir değer (x,y)
                
                // 2-) X ve Y bileşenlerini yuvarlayarak tam sayı değerlere dönüştürüyoruz
                roundedDirectionToEnemy = new Vector2(Mathf.Round(directionToEnemy.x), Mathf.Round(directionToEnemy.y)); 

                // 3-) 1 veya -1 değerlerini almak için vektörün uzunluğunu 1'e sınırlıyoruz   // ya 1 veya -1 alır başka bişi almaz.
                clampedDirectionToEnemy = Vector2.ClampMagnitude(roundedDirectionToEnemy, 1f);    // bunu KnocBack uygularken ya tam sağa yada tam sola dogru uygulamak için oluşturduk.
                
                // Player'ın yönünü al
                playerDirection = transform.right;  // 1 veya -1
                
                // Aradaki açıyı hesapla
                angle = Vector2.Angle(playerDirection, directionToEnemy);

                if (clampedDirectionToEnemy.x == 0) // Bazen Enemy Player'ın tam içine giriyor ve bu 0 değerini alıyor. Bu kod onun önüne geçiyor
                    clampedDirectionToEnemy.x = 1;
            }

            void TakeDamages(Collider2D enemy)
            {
                if (isJumpit) // Enemyi zıplatır
                {
                    Rigidbody2D EnemyRB2 = enemy.gameObject.GetComponent<Rigidbody2D>();
                    EnemyRB2.AddForce(new Vector2(EnemyRB2.velocity.x, Random.Range(25f,35f)),ForceMode2D.Impulse);  
                }
                    
                ParticleSystem _hitEffect = Instantiate(hitEffect, enemy.transform.position,Quaternion.identity);
                Destroy(_hitEffect.gameObject,2f);
                    
                EnemyScript _enemyScript = enemy.GetComponent<EnemyScript>();
                if (enemy != null)
                    _enemyScript.TakeDamages( swordDamage * dmgPower, clampedDirectionToEnemy * KnockBackPower);
            }
            
            void OnDrawGizmosSelected()  // bu nedense çalışmadı
            {
                // Kılıç menzilini görselleştirme
                Gizmos.color = Color.red;
                
                Gizmos.DrawSphere((Vector2)this.transform.position,swordRange);
            }
        }
    }
}
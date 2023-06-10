using System;
using System.Collections;
using EnemyScripts;
using UnityEngine;
// ReSharper disable Unity.InefficientPropertyAccess
// ReSharper disable Unity.PreferNonAllocApi

namespace PlayerScripts
{
    public class P_SwordScript : MonoBehaviour
    {
        [SerializeField] public float swordDamage;
        [SerializeField] float swordRange;
        [SerializeField] float attackAngle;
        [SerializeField] LayerMask enemyLayer;
        [SerializeField] public ParticleSystem hitEffect;
        
        public bool isAttack = false;
        
        private GameObject Player;
        private Rigidbody2D RB2;
        // ReSharper disable Unity.PerformanceAnalysis
        private void Awake()
        {
            Player = this.gameObject;
            RB2 = Player.GetComponent<Rigidbody2D>();
        }

        public void SwordAttack(float dmgPower , float KnockBackPower,bool isJumpit, float moveOnTime, float HittingAllAngle)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(this.transform.position, swordRange, enemyLayer);
            print("Vurdugun kişi sayısı:" + hitEnemies.Length);
            StartCoroutine(AttackAndMoveOn(moveOnTime));
            foreach (var enemy in hitEnemies)
            {
                // Düşmanın pozisyonunu al
                Vector2 enemyPosition = enemy.transform.position;

                // Düşmanın pozisyonunun hangi yönde oldugunu buluyor.
                Vector2 directionToEnemy = (enemyPosition - (Vector2)transform.position).normalized;  // 1 , -1 arası değer (x,y)
                
                // Player'ın yönünü al
                Vector2 playerDirection = transform.right;  // 1 veya -1
                
                // Aradaki açıyı hesapla
                float angle = Vector2.Angle(playerDirection, directionToEnemy);
               
                // Açı 60 derecenin içindeyse hasar ver
                if (angle <= attackAngle * HittingAllAngle)    // isHittingAll == 1 ise, 60 derecelik bir açıya vurur. Yani sadece önüne vurur.
                {                                           // isHittingAll == 4 ise 240 derecelik bir açıya vurur. Oda arkasında vurmasını sağlar.
                    if (isJumpit) // Enemyi zıplatır
                    {
                        Rigidbody2D EnemyRB2 = enemy.gameObject.GetComponent<Rigidbody2D>();
                        EnemyRB2.AddForce(new Vector2(EnemyRB2.velocity.x, 30f),ForceMode2D.Impulse);  
                    }
                    
                    ParticleSystem _hitEffect = Instantiate(hitEffect, enemy.transform.position,Quaternion.identity);
                    Destroy(_hitEffect.gameObject,2f);
                    
                    EnemyScript _enemyScript = enemy.GetComponent<EnemyScript>();
                    if (enemy != null)
                        _enemyScript.TakeDamages( swordDamage * dmgPower, directionToEnemy * KnockBackPower);
                }
            }

            void OnDrawGizmosSelected()  // bu nedense çalışmadı
            {
                // Kılıç menzilini görselleştirme
                Gizmos.color = Color.red;
                
                Gizmos.DrawSphere((Vector2)this.transform.position,swordRange);
            }

            IEnumerator AttackAndMoveOn(float _moveOnTime)   // vurdutkan sonr bir adım öne gidiyor ve 0.35 sn hareket edemiyor.
            {
                isAttack = true;
                float PushPower = 250f;
                if (RB2.gravityScale == 1)  // eger Player havada ise daha az itme kuvveti uygulasın
                    PushPower = 100f;
                
                switch (Player.transform.rotation.y)
                {
                    case 1:// sola bakıyor
                        RB2.AddForce(new Vector2(-PushPower,RB2.velocity.y),ForceMode2D.Impulse);
                        break;
                    
                    case 0: // sağa bakıyor
                        RB2.AddForce(new Vector2(PushPower,RB2.velocity.y),ForceMode2D.Impulse);
                        break;
                }

                yield return new WaitForSeconds(_moveOnTime);    // Player kaç sn sonra hareket etmye başlasın, onun zamanı
                                                                 // Combo vuruşlarında 3 vuruş var ise sadece ilk vuruşu baz alıcaktır ve ilk vuruşta: isAttack = false; olcaktır. 
                isAttack = false;                                
            }
        }
        public void SwordAttack1()
        {
            SwordAttack(0.8f , 0.8f,false,1f,1f);
        }        
        public void SwordAttack2()
        {
            SwordAttack(1.2f ,1,false,1f,1f);
        }        
        public void SwordAttack3()   
        {
            SwordAttack(1.5f ,1.2f,true,1f,1f);
        }

        public void HittingAll1()
        {
            SwordAttack(0.8f,1f,true,0.5f,4);
        }
        public void HittingAll2()
        {  
            SwordAttack(1.2f,2.5f,false,0.5f,4);
        }
        
    }
}
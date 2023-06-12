using System;
using System.Collections;
using EnemyScripts;
using GameManagerScript;
using GameManagerScript.SkillsDetails;
using GameManagerScript.SkillsScripts;
using PlayerScripts.SwordScripts;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerScripts
{
    public class DmgColliderScript : MonoBehaviour
    {
        // private EnemyScript _enemyScript;
        [SerializeField] ParticleSystem BloodEffect;
        [SerializeField] ParticleSystem BlueEffect;
        [SerializeField] ParticleSystem BlackBlueEffect;
        
        private GameObject Player;
        private GameObject GameManager;
        
        private PlayerScript __PlayerScript;
        private SkillsScript __SkillsScript;
        private DashAttackDetails __DashAttackDetails;
        
        private float health;
        private float canUseDashHitTimer;

        private void Awake()
        {
            Player = this.gameObject;
            GameManager = GameObject.Find("GameManager");
            __PlayerScript = Player.GetComponent<PlayerScript>();
            __SkillsScript = GameManager.GetComponent<SkillsScript>();
            __DashAttackDetails = GameManager.GetComponent<DashAttackDetails>();
        }

        private void OnTriggerStay2D(Collider2D other)  // Enemy Player'a deger ise Player'ın canını götür.
        {       // other: Enemy oluyor.
            var _isEnemyLayer = other.gameObject.layer == LayerMask.NameToLayer("Enemy");
            float HitToPlayerTimer = 0;
            float AgainHitToPlayerTime = 0;
            
            Vector2 directionToEnemy = (other.transform.position - transform.position).normalized;
            
            if (other.gameObject.GetComponent<EnemyScript>() != null)
            {
                HitToPlayerTimer = other.gameObject.GetComponent<EnemyScript>().HitToPlayerTimer;
                AgainHitToPlayerTime = other.gameObject.GetComponent<EnemyScript>().hitTimeRange;
                canUseDashHitTimer = other.gameObject.GetComponent<EnemyScript>().canUseDashHitTimer;   // bunu burdan çagırma
            }
            
            if (__SkillsScript.isDashAtackUse)     // eğer Enemy Playera vurmuş ise time = 0 lanacaktır. ve o if koşulu çalışmayacak.
                HitToPlayerTimer = 10;                      // onun için Dash Skilini kullandıgımda time = 10 yapıyorumki, if koşulları çalışabilsin.
            
            if (_isEnemyLayer && HitToPlayerTimer >= AgainHitToPlayerTime)  // bu Enemy'nin Player'a bir süre aralıgında vurmasını sağlar
            {
                CollisionIsActive(other, directionToEnemy);  
            }
        }
        private void CollisionIsActive(Collider2D other, Vector2 directionToEnemy)  // bu Dash kullanıldıgında dmg olmasın diye yazılmıştır.
        {
            switch (__SkillsScript.isDashAtackUse)
            {
                case false: // Dash kullanılmadıysa Player dmg alabilir.
                    HitToPlayer(other, directionToEnemy);
                    break;
                
                case true: // eger Dash kullanıldı ise Player bir süre dmg alamaz.
                    if (canUseDashHitTimer > 1f && other.gameObject != null)  // Dash Vurmak için bu var __SkillsScript.isDashAtackUse == TRUE oldugunda çalışır
                    {       // dashHitTime olmasının nedeni: onu kullanmazsam hep Dash ile vuruyor olmasıdır. Yani alttaki kodun hep çalışıyor olmasına sebep oluyor.
                        other.gameObject.GetComponent<EnemyScript>().canUseDashHitTimer = 0;
                        StartCoroutine(__DashAttackDetails.HitToEnemy(other,directionToEnemy)); // DashAtack çalışsın
                    }
                    break;
            }
        }
        
        void HitToPlayer(Collider2D other, Vector2 directionToEnemy)    // Player dmg alıyor
        {
            ParticleSystem Effect = other.gameObject.GetComponent<EnemyScript>().HitEffect; // Hangi Effekti oldugunu alıyor
            ParticleSystem effect = Instantiate(Effect, Player.transform);  // Effekti Player üzerinde uyguluyor
            Destroy(effect.gameObject,5f);
            
            float dmg = other.gameObject.GetComponent<EnemyScript>().damage;
            __PlayerScript.TakeDamage(dmg, directionToEnemy);

            other.gameObject.GetComponent<EnemyScript>().HitToPlayerTimer = 0;   // zamanı burdan sıfırlamak lazım yoksa hata veriyor.
        }
        
 
    }
}

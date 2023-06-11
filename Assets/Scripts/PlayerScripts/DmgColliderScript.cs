using System;
using System.Collections;
using EnemyScripts;
using GameManagerScript;
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
        private PlayerScript __PlayerScript;
        private SkillsScript __SkillsScript;
        private float health;
        private float dmgTime;
        private float dashHitTime;
        private float alongTime;

        private void Awake()
        {
            Player = this.gameObject;
            __PlayerScript = Player.GetComponent<PlayerScript>();
            __SkillsScript = GameObject.Find("GameManager").GetComponent<SkillsScript>();
        }

        private void OnTriggerStay2D(Collider2D other)  // Enemy Player'a deger ise Player'ın canını götür.
        {       // other: Enemy oluyor.
            var _isEnemyLayer = other.gameObject.layer == LayerMask.NameToLayer("Enemy");
        
            
            Vector2 directionToEnemy = (other.transform.position - transform.position).normalized;
            
            if (other.gameObject.GetComponent<EnemyScript>() != null)
            {
                dmgTime = other.gameObject.GetComponent<EnemyScript>().dmgTime;
                alongTime = other.gameObject.GetComponent<EnemyScript>().hitTimeRange;
                dashHitTime = other.gameObject.GetComponent<EnemyScript>().dashHitTime;
            }
            
            if (__SkillsScript.isDashAtackUse)     // eğer Enemy Playera vurmuş ise time = 0 lanacaktır. ve o if koşulu çalışmayacak.
                dmgTime = 10;                      // onun için Tumble Skilini kullandıgımda time = 10 yapıyorumki, if koşulları çalışabilsin.
            
            if (_isEnemyLayer && dmgTime >= alongTime)
            {
                CollisionIsActive(other, directionToEnemy);     // burda bi çelişki var
            }
        }
        private void CollisionIsActive(Collider2D other, Vector2 directionToEnemy)  // bu Dash kullanıldıgında dmg olmasın diye yazılmıştır.
        {
            if (__SkillsScript.isDashAtackUse == false)     // DashSkill kullanılıyorsa, Player dmg almasın diye bu if koşulu var.
            {
                    HitToPlayer(other, directionToEnemy);
            }
            else if (dashHitTime > 1f && other.gameObject != null)  // Dash Vurmak için bu var __SkillsScript.isDashAtackUse == TRUE oldugunda çalışır
            {       // dashHitTime olmasının nedeni: onu kullanmazsam hep Dash ile vuruyor olmasıdır. Yani alttaki kodun hep çalışıyor olmasına sebep oluyor.
                other.gameObject.GetComponent<EnemyScript>().dashHitTime = 0;
                StartCoroutine(HitToEnemy(other,directionToEnemy)); // DashAtack çalışsın
            }
        }
        
        void HitToPlayer(Collider2D other, Vector2 directionToEnemy)    // Player dmg alıyor
        {
            ParticleSystem Effect = other.gameObject.GetComponent<EnemyScript>().HitEffect; // Hangi Effekti oldugunu alıyor
            ParticleSystem effect = Instantiate(Effect, Player.transform);  // Effekti Player üzerinde uyguluyor
            Destroy(effect.gameObject,5f);
            
            float dmg = other.gameObject.GetComponent<EnemyScript>().damage;
            __PlayerScript.TakeDamage(dmg, directionToEnemy);

            other.gameObject.GetComponent<EnemyScript>().dmgTime = 0;   // zamanı burdan sıfırlamak lazım yoksa hata veriyor.
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator HitToEnemy(Collider2D other, Vector2 directionToEnemy)     // Enemy dmg alıyor     // dash vuruş
        {
            other.gameObject.GetComponent<EnemyScript>().dmgTime = 10;   // zamana 10 veriyorum çünkü, if koşulu içindeki HiToPlayer hemen çalışabililir duruma geçsin.
            EnemyScript __EnemyScript = other.gameObject.GetComponent<EnemyScript>();
            float dmg = Player.GetComponent<SwordScript>().swordDamage/2; // %50 daha az dmg vurur ama 4 kere vurar
            
            if (other != null) HitAndEffect(other,dmg,directionToEnemy,__EnemyScript);
            
            yield return new WaitForSeconds(0.12f);
            if (other != null) HitAndEffect(other,dmg,directionToEnemy,__EnemyScript);
            
            yield return new WaitForSeconds(0.15f);
            if (other != null) HitAndEffect(other,dmg,directionToEnemy,__EnemyScript);
            
            yield return new WaitForSeconds(0.18f);
            if (other != null) HitAndEffect(other,dmg,directionToEnemy,__EnemyScript);
        }

        void HitAndEffect(Collider2D other,float dmg,Vector2 directionToEnemy, EnemyScript __EnemyScript)
        {
            __EnemyScript.TakeDamages(dmg,directionToEnemy);
            ParticleSystem Effect = Player.GetComponent<SwordScript>().hitEffect; // Hangi Effekti oldugunu alıyor    
            ParticleSystem effect = Instantiate(Effect, other.gameObject.transform);
            Destroy(effect.gameObject,5f);
        }
    }
}

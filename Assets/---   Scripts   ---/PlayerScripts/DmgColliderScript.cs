using System;
using System.Collections;
using ______Scripts______.EnemyScripts.Enemy.Enemy;
using ______Scripts______.GameManagerScript.SkillsScripts;
using EnemyScripts;
using EnemyScripts.AIScripts;
using EnemyScripts.Enemy;
using EnemyScripts.Enemy.EnemyAttack;
using GameManagerScript;
using GameManagerScript.SkillsDetails;
using GameManagerScript.SkillsScripts;
using ObjectsScripts;
using PlayerScripts.SwordScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

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

        private SkillsScript __SkillsScript;
        private DashAttackDetails __DashAttackDetails;
        private Calculations __Calculations;

        private float health;
        private float canUseDashHitTimer;

        private void Awake()
        {
            Player = this.gameObject;
            GameManager = GameObject.Find("GameManager");
            __Calculations = Player.GetComponent<Calculations>();
            __SkillsScript = GameManager.GetComponent<SkillsScript>();
            __DashAttackDetails = GameManager.GetComponent<DashAttackDetails>();
        }

        private void OnTriggerStay2D(Collider2D other) // Enemy Player'a deger ise Player'ın canını götür.
        {
            // other: Enemy oluyor.
            var _isEnemyLayer = other.gameObject.layer == LayerMask.NameToLayer("Enemy");
            float HitToPlayerTimer = 0;
            float AgainHitToPlayerTime = 0;

            if (other.gameObject.CompareTag("EnemyBullet"))
            {
                Collider2D PlayerCollider2D = Player.GetComponent<Collider2D>();
                
                EnemyBulletScript _enemyBulletScript = other.GetComponent<EnemyBulletScript>();
                _enemyBulletScript.BulletIsTouchThePlayer(PlayerCollider2D);
            }

            if (other.gameObject.GetComponent<EnemyScript>() != null)
            {
                HitToPlayerTimer = other.gameObject.GetComponent<EnemyScript>().HitToPlayerTimer;
                AgainHitToPlayerTime = other.gameObject.GetComponent<EnemyScript>().hitTimeRange;
                canUseDashHitTimer = other.gameObject.GetComponent<EnemyScript>().canUseDashHitTimer; // bunu burdan çagırma
            }

            if (__SkillsScript.isDashAtackUse) // eğer Enemy Playera vurmuş ise time = 0 lanacaktır. ve o if koşulu çalışmayacak.
                HitToPlayerTimer = 10; // onun için Dash Skilini kullandıgımda time = 10 yapıyorumki, if koşulları çalışabilsin.
            
            if (_isEnemyLayer && HitToPlayerTimer >= AgainHitToPlayerTime) // bu Enemy'nin Player'a bir süre aralıgında vurmasını sağlar
            {
                var result = __Calculations.CalculationsAboutToObject(Player, other); // Enemy'nin sağında mı solunda mı oldugunu hesaplar.
                Vector2 directionToEnemy = result.Item1;
            
                CollisionIsActive(other, directionToEnemy);
            }
        }

        private void CollisionIsActive(Collider2D other, Vector2 directionToEnemy) // bu Dash kullanıldıgında dmg olmasın diye yazılmıştır.
        {
            switch (__SkillsScript.isDashAtackUse)
            {
                case false: // Dash kullanılmadıysa Player dmg alabilir.
                        /// Burası boşş Burası Enemy ile Player çarpışır ise çalışacaktır.
                    break;

                case true: // eger Dash kullanıldı ise Player bir süre dmg alamaz.
                    if (canUseDashHitTimer > 1f && other.gameObject != null) // Dash Vurmak için bu var __SkillsScript.isDashAtackUse == TRUE oldugunda çalışır
                    {
                        // dashHitTime olmasının nedeni: onu kullanmazsam hep Dash ile vuruyor olmasıdır. Yani alttaki kodun hep çalışıyor olmasına sebep oluyor.
                        other.gameObject.GetComponent<EnemyScript>().canUseDashHitTimer = 0;
                        StartCoroutine(__DashAttackDetails.DashAtackToEnemy(other, directionToEnemy)); // DashAtack çalışsın
                    }

                    break;
            }
        }

       
    }
}
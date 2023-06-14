using System;
using System.Collections;
using EnemyScripts;
using EnemyScripts.AIScripts;
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

        [SerializeField] GameObject images;

        [SerializeField] float enemyAttackRadius;
        [SerializeField] LayerMask playerLayer;

        private GameObject Player;
        private GameObject GameManager;

        private PlayerScript __PlayerScript;
        private SkillsScript __SkillsScript;
        private DashAttackDetails __DashAttackDetails;
        private Calculations __Calculations;


        private float health;
        private float canUseDashHitTimer;


        private void Awake()
        {
            Player = this.gameObject;
            GameManager = GameObject.Find("GameManager");
            __PlayerScript = Player.GetComponent<PlayerScript>();
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
                    AIScript __AIScript = other.GetComponent<AIScript>();

                    if (!__AIScript.isEnemyAttackToPlayer)
                        StopAndAttack(other, __AIScript);
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

        void StopAndAttack(Collider2D other, AIScript __AIScript)
        {
            __AIScript.isEnemyAttackToPlayer = true;

            StartCoroutine(AttackReaction(other, __AIScript));
        }

        IEnumerator AttackReaction(Collider2D other, AIScript __AIScript)
        {
            float hitTimeRange = other.GetComponent<EnemyScript>().hitTimeRange;
            images.gameObject.SetActive(true); // Todo: geçici bu
            yield return new WaitForSeconds(hitTimeRange);
            images.gameObject.SetActive(false); // Todo: geçici bu
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

                    var result = __Calculations.CalculationsAboutToObject(other.gameObject, player); // Enemy'lerin vuruş yaptıgı yerin verileri hesaplanır.
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
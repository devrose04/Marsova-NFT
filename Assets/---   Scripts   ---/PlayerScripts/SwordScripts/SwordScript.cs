using ______Scripts______.EnemyScripts.Enemy.Enemy;
using EnemyScripts.Enemy;
using ObjectsScripts;
using PlayerScripts.SwordScripts;
using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess
// ReSharper disable Unity.PreferNonAllocApi

namespace ______Scripts______.PlayerScripts.SwordScripts
{
    public class SwordScript : MonoBehaviour
    {
        [SerializeField] public float swordDamage;
        [SerializeField] float playerSwordRadius;
        [SerializeField] public ParticleSystem hitEffect;
        [SerializeField] LayerMask enemyLayer;

        private SwordSkilsScript __SwordSkilsScript;
        private Calculations __Calculations;

        private Vector2 directionToEnemy;
        private float angle;

        public bool isAttack = false;

        private GameObject Player;
        public Rigidbody2D RB2;

        private Coroutine myCoroutine;

        private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClipHit;

        // ReSharper disable Unity.PerformanceAnalysis
        private void Awake()
        {
            Player = this.gameObject;
            RB2 = Player.GetComponent<Rigidbody2D>();
            _audioSource = Player.GetComponent<AudioSource>();
            __SwordSkilsScript = Player.GetComponent<SwordSkilsScript>();
            __Calculations = Player.GetComponent<Calculations>();
        }

        public void SwordAttack(float dmgPower, float KnockBackPower, bool isJumpit, float moveOnTime, bool isJustHitFrontArea)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(this.transform.position, playerSwordRadius, enemyLayer);
            // print("Vurdugun kişi sayısı:" + hitEnemies.Length); // bunu canvasa yazdır.
            if (myCoroutine != null)
                StopCoroutine(myCoroutine);
            myCoroutine = StartCoroutine(__SwordSkilsScript.AttackAndMoveOn(moveOnTime, RB2));

            float attackAngle = isJustHitFrontArea ? 0 : 180; // burda Sadece önüne vurdugu bir skilmi yoksa, arkasınada vurdugu bir skill mi oldugunun ayarını yapıyoruz.

            foreach (var enemy in hitEnemies) // yuvarlagın içindeki vurulan Enemylere teker teker bakar.
            {
                var result = __Calculations.CalculationsAboutToObject(Player, enemy); // swordun çarpıtıgı Enemylerin verileri hesaplanır.
                (directionToEnemy, angle) = result;

                if (angle <= attackAngle) // attackAngle == 0 ise, sadece önüne vuruyor. // attackAngle == 180 ise hem önüne, hem arkasına vuruyor.
                {
                    TakeDamages(enemy, isJumpit, dmgPower, KnockBackPower);
                }
            }
        }

        void TakeDamages(Collider2D enemy, bool isJumpit, float dmgPower, float KnockBackPower)
        {
            ParticleSystem _hitEffect = Instantiate(hitEffect, enemy.transform.position, Quaternion.identity);
            Destroy(_hitEffect.gameObject, 2f);

            EnemyScript _enemyScript = enemy.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                _audioSource.PlayOneShot(_audioClipHit);
                _enemyScript.TakeDamages(swordDamage * dmgPower, directionToEnemy * KnockBackPower, isJumpit);
            }
        }

        // void OnDrawGizmosSelected() // Player'ın vuruş menzilini gösterir
        // {
        //     // Kılıç menzilini görselleştirme
        //     Gizmos.color = Color.red;
        //
        //     Gizmos.DrawSphere((Vector2)this.transform.position, playerSwordRadius);
        // }
    }
}
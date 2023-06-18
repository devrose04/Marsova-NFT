using EnemyScripts.Enemy.Enemy;
using EnemyScripts.OwnScript;
using UnityEngine;

namespace EnemyScripts.Enemy
{
    public class EnemyScript : MonoBehaviour // Bu Scripte dışardan public ile kullanılacak kodlar olucak. 
    {
        // Enemy GameObjelerin hepsini GameManagerdan Update ile çagırıyorum.
        public float speed;
        public float health;
        public float damage;
        public float hitTimeRange; // vuruş yapma sıklıgının süresi.
        public float attackRadius;
        public float knockBackPower;
        public bool isAttackinRange;
        private bool isItFly;
        
        (float, float, float, float, float, float, bool, bool) OwnInformations;

        [SerializeField] public ParticleSystem OwnEffect; // bu kendi Effecti, boş olsada olur
        [SerializeField] public ParticleSystem HitEffect; // bu vuruş effecti
        [SerializeField] private float effectCreationTimer; // effecti gerçekleştirme sürem
        private float effectCreationTime; // Efekt kaç sn de bir tekrarlansın

        private string Tag;

        private GameObject Enemy;
        private Rigidbody2D RB2;

        private ICustomScript __ICustomScript;
        private EnemyKnockBackScript __EnemyKnockBackScript;

        public float HitToPlayerTimer = 10f; // 10 verme nedenim bir hatayı önlediğinden.   // en son ne zaman vuruş yaptıgının verisini tutuyor.
        public float canUseDashHitTimer = 10f; // bu, burayla alakalı degil ama DeltaTimeUp() fonksiyonu için buraya yazdım. 

        private void Awake()
        {
            Enemy = this.gameObject;
            Tag = Enemy.tag;
            RB2 = Enemy.GetComponent<Rigidbody2D>();
            effectCreationTime = effectCreationTimer;

            __ICustomScript = Enemy.GetComponent<ICustomScript>(); // burda kendi özel oluşturdugumuz Scripti buluyoruz ve onu çagırıyoruz. 
            __EnemyKnockBackScript = Enemy.GetComponent<EnemyKnockBackScript>();

            if (__ICustomScript != null)
                OwnInformations = __ICustomScript.OwnInformations(); // Bu Obejini kendi bilgilerini buraya geçiriyoruz. Her Enemyinin farklı aralıkta bilgileri var.

            speed = OwnInformations.Item1;
            health = OwnInformations.Item2;
            damage = OwnInformations.Item3;
            hitTimeRange = OwnInformations.Item4;
            attackRadius = OwnInformations.Item5;
            knockBackPower = OwnInformations.Item6;
            isAttackinRange = OwnInformations.Item7;
            isItFly = OwnInformations.Item8;
        }

        public void MYFixedUpdate()
        {
        }

        public void TakeDamages(float dmg, Vector2 directionToEnemy, bool isJumpit)
        {
            if (isItFly == false) // eger uçmuyor ise Knockback uygulansın
                StartCoroutine(__EnemyKnockBackScript.KnockBack(directionToEnemy, RB2 , isJumpit));
            
            health -= dmg;
            // print($"<color=yellow>Enemy Health:</color>" + health);
            if (health <= 0)
            {
                if (this.gameObject.GetComponent<SalyangozScript>() != null)
                {
                    this.gameObject.GetComponent<SalyangozScript>().TakeHeal();
                }
                Destroy(this.gameObject);
            }
        }

        public void CreateOwnEffect()
        {
            if (OwnEffect != null && effectCreationTimer >= effectCreationTime)
            {
                //hepsinin kendi özel OwnEffekti oldugu için her Effektin tekrarlanma süresi farklıdır, o yüzden burda Timerlar var
                effectCreationTimer = 0;
                ParticleSystem Effect = Instantiate(OwnEffect, Enemy.transform);
                Destroy(Effect.gameObject, 10f);
            }
        }

        public void DeltaTimeUp() // bunu ayrı bir Scripte oluştur oraya koy
        {
            HitToPlayerTimer += Time.deltaTime;
            effectCreationTimer += Time.deltaTime;
            canUseDashHitTimer += Time.deltaTime;
        }
    }
}
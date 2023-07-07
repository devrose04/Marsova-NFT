using ______Scripts______.Canvas.Enemy;
using ______Scripts______.EnemyScripts.OwnScript;
using ______Scripts______.UIScripts.Canvas.Player;
using EnemyScripts;
using EnemyScripts.Enemy.Enemy;
using EnemyScripts.OwnScript;
using PlayerScripts.Player;
using UnityEngine;

namespace ______Scripts______.EnemyScripts.Enemy.Enemy
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
        public bool isItFly;
        public float suportArmor;
        public float maxSuportArmor = 50;
        public int score;

        (float, float, float, float, float, float, bool, bool, float, int) OwnInformations;

        [SerializeField] public ParticleSystem OwnEffect; // bu kendi Effecti, boş olsada olur
        [SerializeField] public ParticleSystem HitEffect; // bu vuruş effecti
        [SerializeField] private ParticleSystem DieEffect;
        [SerializeField] private ParticleSystem SalyangozEffect;
        [SerializeField] private float effectCreationTimer; // effecti gerçekleştirme sürem
        private float effectCreationTime; // Efekt kaç sn de bir tekrarlansın

        private string Tag;

        private GameObject Enemy;
        private Rigidbody2D RB2;
        private GameObject Player;

        private PlayerScript _playerScript;
        private ICustomScript __ICustomScript;
        private EnemyKnockBackScript __EnemyKnockBackScript;
        private EnemyHealthBar _enemyHealthBar;
        private PlayerScore _playerScore;

        private AudioSource _audioSource;
        private AudioClip _audioClipDeadth;

        public float HitToPlayerTimer = 10f; // 10 verme nedenim bir hatayı önlediğinden.   // en son ne zaman vuruş yaptıgının verisini tutuyor.
        public float canUseDashHitTimer = 10f; // bu, burayla alakalı degil ama DeltaTimeUp() fonksiyonu için buraya yazdım. 

        private void Awake()
        {
            Enemy = this.gameObject;
            Player = GameObject.Find("Player");
            Tag = Enemy.tag;
            RB2 = Enemy.GetComponent<Rigidbody2D>();
            effectCreationTime = effectCreationTimer;

            __ICustomScript = Enemy.GetComponent<ICustomScript>(); // burda kendi özel oluşturdugumuz Scripti buluyoruz ve onu çagırıyoruz. 
            __EnemyKnockBackScript = Enemy.GetComponent<EnemyKnockBackScript>();
            _enemyHealthBar = Enemy.GetComponent<EnemyHealthBar>();
            _playerScript = Player.GetComponent<PlayerScript>();
            _playerScore = Player.GetComponent<PlayerScore>();

            _audioSource = Player.GetComponent<AudioSource>();
            _audioClipDeadth = _playerScript.audioClipDeadth;

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
            suportArmor = OwnInformations.Item9;
            score = OwnInformations.Item10;
        }

        public void MYFixedUpdate()
        {
        }

        public void TakeDamages(float dmg, Vector2 directionToEnemy, bool isJumpit)
        {
            // if (isItFly == false) // eger uçmuyor ise Knockback uygulansın
            StartCoroutine(__EnemyKnockBackScript.KnockBack(directionToEnemy, RB2, isJumpit));

            // print(suportArmor);
            // print(health);

            if (suportArmor <= 0)
            {
                health -= dmg;
                _enemyHealthBar.ChangeHealthBar();
            }
            else
            {
                suportArmor -= dmg;
                _enemyHealthBar.ChangeArmorBar();
            }

            // print($"<color=yellow>Enemy Health:</color>" + health);
            if (health <= 0)
            {
                _audioSource.PlayOneShot(_audioClipDeadth);
                _playerScript.totalScore += score;
                _playerScore.ScoreUpdate();

                if (Enemy.CompareTag("Salyangoz"))
                {
                    Enemy.GetComponent<SalyangozScript>().TakeHeal();
                    ParticleSystem effect = Instantiate(SalyangozEffect, Enemy.transform.position, Quaternion.identity);
                    Destroy(effect.gameObject, 3f);
                }

                if (Enemy.CompareTag("Ahtapot"))
                    Enemy.GetComponent<AhtapaotScript>().DestroyThisParentGameObject();

                if (DieEffect != null)
                {
                    ParticleSystem _effect = Instantiate(DieEffect, Enemy.transform.position, Quaternion.identity);
                    Destroy(_effect.gameObject, 3f);
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

        public void UpSuportArmor()
        {
            if (suportArmor <= maxSuportArmor)
                suportArmor += 3;
            else if (suportArmor > maxSuportArmor)
                suportArmor = maxSuportArmor;
            else
                suportArmor = 0;
        }
    }
}
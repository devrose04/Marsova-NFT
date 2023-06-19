using System.Collections;
using EnemyScripts.Enemy;
using EnemyScripts.OwnScript;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts.AIScripts
{
    public class AIScript : MonoBehaviour
    {
        public bool isKnockBackNotActive = true;
        public bool isEnemySeePlayer;
        public bool isWaitingInTheBase;
        public bool isRight = true;
        public bool isEnemyAttackToPlayer = false;

        private int JustOneTimeWork;

        public float moveSpeed;
        private float distance; // Player ile Enemy arasoındaki mesafeyi ölçer 

        private Transform Player; // Hedef
        private GameObject Enemy;
        private Rigidbody2D RB2;

        private EnemyScript __EnemyScript;
        private AISkillsScript __AISkillsScript;
        private DmgColliderScript __DmgColliderScript;
        private NearEnemyAttackScript _nearEnemyAttackScript; //Todo: Enemy objesi yakından vuruyorsa __RangeEnemyAttackScript'ini kaldır.
        private RangeEnemyAttackScript __RangeEnemyAttackScript; //Todo: Enemy objesi uzaktan vuruyorsa _nearEnemyAttackScript'ini kaldır.

        private Vector2 direction; // bu Enemy'e karşılık Player hangi yönde onu bulur.   -1 ise Enemy Player'ın sağında
        public Vector2 startingPosition; // Başlangıç pozisyonu
        public Vector2 basePosition; // Base pozisyonu

        [SerializeField] float baseRange; // Serbest Yürüyüş alanının uzunlugu

        private void Awake()
        {
            Player = GameObject.Find("Player").transform;
            Enemy = this.gameObject;
            RB2 = this.gameObject.GetComponent<Rigidbody2D>();

            __EnemyScript = Enemy.GetComponent<EnemyScript>();
            __AISkillsScript = Enemy.GetComponent<AISkillsScript>();
            _nearEnemyAttackScript = Enemy.GetComponent<NearEnemyAttackScript>();
            __RangeEnemyAttackScript = Enemy.GetComponent<RangeEnemyAttackScript>();

            isKnockBackNotActive = true;
        }

        private void Start()
        {
            moveSpeed = Enemy.GetComponent<EnemyScript>().speed;
            startingPosition.x = transform.position.x;
            basePosition = startingPosition + new Vector2(baseRange, 0); // Base'in genişligini ayarlıyorum.
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void MYFixedUpdate() // bunu GameManagerda çagırıyorum.
        {
            direction = (Player.position - transform.position).normalized; // Player Enemy'nin hangi tarafında onu hesaplar
            distance = Vector2.Distance(Player.position, transform.position); // Player ile Enemy arasoındaki mesafeyi ölçer

            SpecialFunctions1();

            if (distance < 1.5f && !__EnemyScript.isAttackinRange) // Enemy Player'ın dibine geldiginde, Enemy dursun. ve vursun
                NearAttackToPlayer();

            else if (distance < 10) // 10 metre içinde görüyorsa ve Enemy vuruş hareketi yapmıyor ise hareket edicektir
            {
                if (distance < 7 && __EnemyScript.isAttackinRange) // Enemy menzilli ise vursun.
                    RangeAttackToPlayer();
                else
                    GoPlayerPosition(); //eger Enemy uzaktan vurmuyor ise Player'ın dibine git.

                SpecialFunctions2(); // slime - giant vb. Scriptlerin kullanıldıgı yer
                EnemyLookingToPlayer();
            }
            else if (distance >= 10)
            {
                EnemyDontLookingToPlayer();
                if (isWaitingInTheBase == false) // Base'in içinde beklemiyor ise hareket etsin
                {
                    __AISkillsScript.MoveOwnBase(distance, moveSpeed, startingPosition, basePosition, isRight, baseRange);
                    JustOneTimeWork = 1;
                }
                else if (JustOneTimeWork == 1) // JustOneTimeWork bir kez çalışmasını saglıyor
                {
                    // ilerde bunun gibi farklı Bekleme şeyleride yaparsın.
                    JustOneTimeWork = 0;
                    StartCoroutine(__AISkillsScript.StopMoveAndLookAround(isWaitingInTheBase, distance)); // durur ve etrafına bakar.
                }
            }
        }

        void GoPlayerPosition() // Player'a doğru hareket etme
        {
            if (isKnockBackNotActive && !Enemy.CompareTag("Bee"))
                RB2.velocity = new Vector2(direction.x * moveSpeed, RB2.velocity.y); // direction.x 1 veya -1 dir
            else if (isKnockBackNotActive && Enemy.CompareTag("Bee"))
                RB2.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed); // direction.x 1 veya -1 dir
        }

        void EnemyLookingToPlayer()
        {
            if (direction.x < 0) // Enemy Player'ın sağında ise çalışır     // direction.x < 0  :  Enemy Player'ın sağında olunca -1 olur
                Enemy.transform.rotation = new Quaternion(0, 1, 0, 0); // baktıgı yöne çevirir
            else if (direction.x > 0) // Enemy Player'ın solunda ise çalışır
                Enemy.transform.rotation = new Quaternion(0, 0, 0, 0); // baktıgı yöne çevirir

            isEnemySeePlayer = true;
        }

        void EnemyDontLookingToPlayer()
        {
            if (direction.x < 0 && isEnemySeePlayer) // Enemy Player'ın sağında ise çalışır 1 kez çalışır ve yönünü Player'ın zıttına döndürür.
                Enemy.transform.rotation = new Quaternion(0, 0, 0, 0); // baktıgı yöne çevirir
            else if (direction.x > 0 && isEnemySeePlayer) // Enemy Player'ın solunda ise çalışır  1 kez çalışır ve yönünü Player'ın zıttına döndürür.
                Enemy.transform.rotation = new Quaternion(0, 1, 0, 0); // baktıgı yöne çevirir

            isEnemySeePlayer = false;
        }

        void SpecialFunctions2()
        {
            if (Enemy.CompareTag("Dogs")) // eger Dogs ise zıplasın
                Enemy.GetComponent<SlimeScript>().Jump();

            if (Enemy.CompareTag("Salyangoz")) // salyangoz ise dursun.
                Enemy.GetComponent<EnemyScript>().speed = 0;
        }

        void SpecialFunctions1()
        {
        }

        void NearAttackToPlayer()
        {
            if (!isEnemyAttackToPlayer) // bu if koşulunun nedeni hep çalışıp döngüye girmesin diye.
                _nearEnemyAttackScript.StopAndAttack(this.gameObject);
        }

        void RangeAttackToPlayer() // todo: __EnemyAttackScript'in ismini degiştir, bir tane daah Script aç, onada uzaktan vuran fonksiyonarı yaz.
        {
            if (!isEnemyAttackToPlayer)
                __RangeEnemyAttackScript.StopAndAttack(Enemy);
        }
    }
}
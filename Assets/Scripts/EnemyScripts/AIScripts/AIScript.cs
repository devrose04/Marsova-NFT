using System.Collections;
using EnemyScripts.OwnScript;
using UnityEngine;

namespace EnemyScripts.AIScripts
{
    public class AIScript : MonoBehaviour
    {
        public bool isKnockBackNotActive;

        private bool isEnemySeePlayer;
        public bool isWaitingInTheBase;
        public bool isRight = true;

        private int JustOneTimeWork;

        public float moveSpeed;
        private float distance; // Player ile Enemy arasoındaki mesafeyi ölçer 

        private Transform Player; // Hedef
        private GameObject Enemy;
        private Rigidbody2D RB2;

        private EnemyScript __EnemyScript;
        private AISkillsScript __AISkillsScript;

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

            if (distance < 10) // 10 metre içinde görüyorsa hareket edicektir
            {
                GoPlayerPosition();
                if (distance < 0.8f) // Enemy Player'ın dibine geldiginde, Enemy dursun.
                    RB2.velocity = new Vector2(0, 0);
                SpecialFunctions(); // Dogs - Zombi vb. Scriptlerin kullanıldıgı yer
                EnemyLookingToPlayer();
            }
            else
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
            if (isKnockBackNotActive)
                RB2.velocity = new Vector2(direction.x * moveSpeed, RB2.velocity.y); // direction.x 1 veya -1 dir
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

        void SpecialFunctions()
        {
            if (Enemy.CompareTag("Dogs")) // eger Dogs ise zıplasın
                Enemy.GetComponent<DogsScript>().Jump();
            if (Enemy.CompareTag("Skeletons") && __EnemyScript.health <= 0)
                Enemy.GetComponent<SkeletonsScript>().ReBorn();
        }
    }
}
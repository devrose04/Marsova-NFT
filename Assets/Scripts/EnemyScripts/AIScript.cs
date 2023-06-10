using System.Collections;
using System.Threading.Tasks;
using EnemyScripts.OwnScript;
using UnityEngine;

namespace EnemyScripts
{
    public class AIScript : MonoBehaviour
    {
        private bool isWaitingInTheBase;
        private bool isEnemySeePlayer;
        private int JustOneTimeWork;
        
        private Vector2 direction;  // bu Enemy'e karşılık Player hangi yönde onu bulur.   -1 ise Enemy Player'ın sağında
        private float distance;  // Player ile Enemy arasoındaki mesafeyi ölçer 
        
        private Transform Player; // Hedef
        private GameObject Enemy;
        private EnemyScript __EnemyScript;
        private Rigidbody2D rb;

        private Vector2 startingPosition; // Başlangıç pozisyonu
        private Vector2 basePosition; // Base pozisyonu

        [SerializeField] float baseRange; // Serbest Yürüyüş alanının uzunlugu
        private float moveSpeed; 
        bool isRight = true;
        private void Awake()
        {
            Player = GameObject.Find("Player").transform;
            Enemy = this.gameObject;
            rb = this.gameObject.GetComponent<Rigidbody2D>();
            __EnemyScript = Enemy.GetComponent<EnemyScript>();
        }

        private void Start()
        {
            moveSpeed = Enemy.GetComponent<EnemyScript>().speed;
            startingPosition.x = transform.position.x;
            basePosition = startingPosition + new Vector2(baseRange,0); // Base'in genişligini ayarlıyorum.
           
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void MYFixedUpdate()  // bunu GameManagerda çagırıyorum.
        {
            direction = (Player.position - transform.position).normalized;  // Player Enemy'nin hangi tarafında onu hesaplar
            distance = Vector2.Distance(Player.position, transform.position);   // Player ile Enemy arasoındaki mesafeyi ölçer
            
            if (distance < 10)  // 10 metre içinde görüyorsa hareket edicektir
            {
                GoPlayerPosition();
                if (distance < 0.8f)    // Enemy Player'ın dibine geldiginde, Enemy dursun.
                    rb.velocity = new Vector2(0, 0);
                if (Enemy.CompareTag("Dogs"))  // eger Dogs ise zıplasın
                    Enemy.GetComponent<DogsScript>().Jump();
                if (Enemy.CompareTag("Skeletons") && __EnemyScript.health <= 0)
                    Enemy.GetComponent<SkeletonsScript>().ReBorn();

                if (direction.x < 0) // Enemy Player'ın sağında ise çalışır     // direction.x < 0  :  Enemy Player'ın sağında olunca -1 olur
                    Enemy.transform.rotation = new Quaternion(0, 1, 0, 0);  // baktıgı yöne çevirir
                else if (direction.x > 0)   // Enemy Player'ın solunda ise çalışır
                    Enemy.transform.rotation = new Quaternion(0, 0, 0, 0);  // baktıgı yöne çevirir

                isEnemySeePlayer = true;
            }
            else
            {
                if (direction.x < 0 && isEnemySeePlayer) // Enemy Player'ın sağında ise çalışır 1 kez çalışır ve yönünü Player'ın zıttına döndürür.
                    Enemy.transform.rotation = new Quaternion(0, 0, 0, 0);  // baktıgı yöne çevirir
                else if (direction.x > 0 && isEnemySeePlayer)   // Enemy Player'ın solunda ise çalışır  1 kez çalışır ve yönünü Player'ın zıttına döndürür.
                    Enemy.transform.rotation = new Quaternion(0, 1, 0, 0);  // baktıgı yöne çevirir

                isEnemySeePlayer = false;
                if (isWaitingInTheBase == false)
                {
                    MoveOwnBase();
                    JustOneTimeWork = 1;
                }
                else if (JustOneTimeWork == 1)  // JustOneTimeWork bir kez çalışmasını saglıyor
                {                               // ilerde bunun gibi farklı Bekleme şeyleride yaparsın.
                    JustOneTimeWork = 0;
                    StartCoroutine(StopMoveAndLookAround());
                }
            }
        }

        void GoPlayerPosition()  // Player'a doğru hareket etme
        {
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);  // direction.x 1 ve ya -1 dir
        }
 
        void MoveOwnBase()
        {
            if (Random.Range(1,500) == 2 && distance < 15)  // Player 15m Enemyye yakınsa bir ihtimal StopMoveAndLookAround fonksiyonunu çalıştırabilir.
            {
                isWaitingInTheBase = true;
            }
            
            var _position = transform.position;
        
            startingPosition.y = _position.y;  // bunu ve altındakini koymaz isem, hep spawn oldugu -y position'a göre işlem yapıyor.
            basePosition.y = _position.y;
            // alttaki kod: Base'in içinde hareket ettirir.
            _position = Vector2.MoveTowards(_position, basePosition, moveSpeed / 2.5f * Time.deltaTime);

            transform.position = _position;
            
            if (Vector2.Distance(transform.position, basePosition) < 0.1f) // Base'in en sol ve en sağına ulaştığında diger yere yöneltir
            {
                if (isRight)
                {
                    isRight = false;
                    basePosition = startingPosition - new Vector2(baseRange, 0);
                    Enemy.transform.rotation = new Quaternion(0,1 , 0, 0);
                }
                else
                {
                    isRight = true;
                    basePosition = startingPosition + new Vector2(baseRange, 0);
                    Enemy.transform.rotation = new Quaternion(0,0 , 0, 0);
                }
            }
        }

        IEnumerator StopMoveAndLookAround()
        {
            int oppositeRotation = 0; 
            int directionRotation = 1; 
            if (Enemy.transform.rotation.y == 0)    // eger sağa bakıyor ise çalışır
            {
                oppositeRotation = 1;  // eger sağa bakıyorsa ilk sola bakmasını istiyorum.
                directionRotation = 0;
            }

            isWaitingInTheBase = true;
            
            yield return new WaitForSeconds(1f);
            Enemy.transform.rotation = new Quaternion(0,oppositeRotation , 0, 0);      // bu zıttına kafasını çeviriyor
            yield return isPlayerCloseBreakit();
            
            yield return new WaitForSeconds(1f);
            Enemy.transform.rotation = new Quaternion(0, directionRotation, 0, 0);     // bu normal asıl baktıgı yere baktırıyor.
            yield return isPlayerCloseBreakit();
            
            yield return new WaitForSeconds(1f);
            Enemy.transform.rotation = new Quaternion(0, oppositeRotation, 0, 0);      // bu gine zıttına 
            yield return isPlayerCloseBreakit();
            
            yield return new WaitForSeconds(1f);
            Enemy.transform.rotation = new Quaternion(0, directionRotation, 0, 0);     // bu asıl baktıgı yöne bakıyor ve yürümeye başlıyor.
            yield return isPlayerCloseBreakit();
            
            yield return new WaitForSeconds(1f);
            yield return isPlayerCloseBreakit();
            
            isWaitingInTheBase = false;
           
        }

        IEnumerator isPlayerCloseBreakit()
        {
            if (distance < 10)
            {
                isWaitingInTheBase = false;
                yield break;
            }
        }
    }
}

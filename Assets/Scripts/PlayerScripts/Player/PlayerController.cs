using System.Threading;
using EnemyScripts.Enemy;
using GameManagerScript.SkillsScripts;
using PlayerScripts.PlayerLaserAbout;
using PlayerScripts.PlayerLaserAbout.Drone;
using PlayerScripts.SwordScripts;
using UIScripts;
using UnityEngine;

// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable RedundantCheckBeforeAssignment
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace PlayerScripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private SkillsScript __SkillsScript;
        private SkillsManager __SkillsManager;
        private SwordScript __SwordScript;
        private PlayerScript __PlayerScript;
        private SkillsDataScript __SkillsData;
        private IsGroundTouchScript __isGroundTouch;
        private StartShipAttack _startShipAttack;
        private DroneScript _droneScript;
        private EnemyDetector _enemyDetector;

        [SerializeField] private GameObject PlayerLaserBullet;

        private float LaserTimer = 1; // todo:  Laserle alakalı bir Script aç ve Oraya Mermi miktarını vb. şeyleride yaz 

        private GameObject GameManager;
        private GameObject Player;
        private Rigidbody2D RB2;
        private GameObject Weapon;
        private Transform ShotPoint;
        private GameObject Drone;

        private float speedSabit;
        private float speed;
        private float speedAmount;

        private void Awake()
        {
            Player = GameObject.Find("Player");
            Weapon = GameObject.Find("Weapon");
            Drone = GameObject.Find("Drone");
            RB2 = Player.GetComponent<Rigidbody2D>();
            GameManager = GameObject.Find("GameManager");
            ShotPoint = Weapon.transform.Find("shotPoint");
            _enemyDetector = Drone.GetComponent<EnemyDetector>();
            _droneScript = Drone.GetComponent<DroneScript>();
            __SwordScript = Player.GetComponent<SwordScript>();
            __PlayerScript = Player.GetComponent<PlayerScript>();
            _startShipAttack = Weapon.GetComponent<StartShipAttack>();
            __SkillsScript = GameManager.GetComponent<SkillsScript>();
            __SkillsData = GameManager.GetComponent<SkillsDataScript>();
            __SkillsManager = GameManager.GetComponent<SkillsManager>();
            __isGroundTouch = Player.transform.Find("IsGrounTouch").GetComponent<IsGroundTouchScript>(); // IsGrounTouch sonunda d yok
        }

        private void Start()
        {
            speed = Player.GetComponent<PlayerScript>().speed;
            speedSabit = speed;
        }

        public void MYFixedUpdate() // GameManagerdan çagırıyorum
        {
            __SkillsData.SkilsCoolDownTime(); // Skillerin kullanılabilir hale geçip geçmedigin kontrol eder.     


            if (__PlayerScript.isKnockbacked || __SkillsScript.isMoveSkilsUse) // Bu kod Player hit yediginde ve dodge, tumble vs attıgında: hareket etmesini ve zıplamasını engeliyecektir.
                return;


            if (Input.GetButton("Horizontal") && __SwordScript.isAttack == false) // sağ ve sol yönlerine gitmek için
            {
                Walking();
            }


            if (__SkillsScript.isArmorFrameUse) // bu ArmorFrame kullandıgında zıplamasını engelliyecektir
                return;


            if (Input.GetKey(KeyCode.W) && __isGroundTouch.isGroundTouchBool) // zıplamak
            {
                Jump();
            }

            if (Input.GetKey(KeyCode.C)) // JetPack
            {
                __SkillsScript.JetPack();
                Run();
            }
        }

        public void MYUpdate() // GameManagerdan çagırıyorum
        {
            _startShipAttack.MYUpdate();
            _droneScript.MYUpdate();
            _enemyDetector.MYUpdate();

            if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space)) // havaya zıplatıp alan vurma skili
            {
                // Aşagıda yaptıgın şey Skilli kullanıyor ve onun zaman verisini burdaki __SkillsData.HittingAllCanUse1'e atıyor.
                __SkillsManager.HittingAll1_manager();

                __SkillsManager.HittingAll2_manager();
            }
            else if (Input.GetKeyDown(KeyCode.Space)) // 3 lü vuruş combo
            {
                __SkillsManager.SwordAttack1_manager();

                __SkillsManager.SwordAttack2_and_SwordAttack3_manager();
            }

            if (Input.GetKeyDown(KeyCode.X)) // ArmorFrame Skill
            {
                __SkillsManager.ArmorFrame_manager();
            }

            LaserTimer += Time.deltaTime;
            if (Input.GetMouseButton(0) && LaserTimer > 0.2f) // Laser silahı
            {
                GameObject Laser = Instantiate(PlayerLaserBullet, ShotPoint.position, transform.rotation);

                Destroy(Laser, 5f);
                LaserTimer = 0;
            }

            if (__PlayerScript.isKnockbacked || __SkillsScript.isMoveSkilsUse || __SkillsScript.isArmorFrameUse) // Bu kod Player hit yediginde ve dodge, dashatack vs attıgında: hareket etmesini engeliyecektir.
                return; // bunun altındaki kodları etkiler.


            if (Input.GetButtonUp("Horizontal")) // DashAttack Skill
            {
                __SkillsManager.DashAtack_manager();
            }


            if (Input.GetKeyDown(KeyCode.Q)) // q ile sola Dodge atıyor
            {
                __SkillsManager.DodgeSkils_q_manager();
            }

            if (Input.GetKeyDown(KeyCode.E)) // e ile sağa Dodge atıyor
            {
                __SkillsManager.DodgeSkils_e_manager();
            }
        }

        void Walking()
        {
            speed = Player.GetComponent<PlayerScript>().speed; // bunu yazma nedenim: ArmorFrame gibi oyun içinde hızı azaltacak faktörleri uygulayabilmek için
            // if (__SkillsScript.isArmorFrameUse == false) // bu Shifte basınca hızlı koşmaydı
            //     Run();
            speedAmount = speed * Time.deltaTime;
            RB2.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speedAmount, RB2.velocity.y);

            if (Input.GetAxisRaw("Horizontal") == -1)
                Player.transform.rotation = new Quaternion(0, 180, 0, 0);
            else if (Input.GetAxisRaw("Horizontal") == 1)
                Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        void Run()
        {
            if (Input.GetKey(KeyCode.C)) // hızlı koşma
            {
                if (speed != speedSabit * 1.8f)
                    speed = speedSabit * 1.8f;
            }
            else
            {
                speed = speedSabit;
            }
        }

        void Jump()
        {
            RB2.velocity = new Vector2(RB2.velocity.x, 5);
        }
    }

    // -*-Todo: -Uğur-

    // ***Todo: Yapılacaklar:


    // Todo: Solid prensipleri ile kodu daha okunaklı yap 
    // ---


    // ***Todo: Belki Yapılabilir:

    // Todo: Eror:   Knockback yerinde hepsini farklı güçte itmesi lazım ama öyle olmuyor.
    // Todo: SwordAndAttack fonksiyonuna ilerde bunları ekleyebiliriz:     bool isJumpit, float dmgPower, float KnockBackPower,
    // *Todo: DmgCollider daki HitToPlayerTimer vb. kullanılmayan parametreleri sil
    // Todo:

    // ---


    // ***Todo: Yapılanlar 11:

    // Todo: Dron oluşturuldu, tüm kodları yazıldı(Player'ın yanına gelip Enemylere Elektirik vuruyor)
    // Todo: StarShip oluşturuldu, tüm kodları yazıldı(uzaydan mermi yagdırabiliyoruz.)
    // Todo: Pley Buller geliştirildi.
    // todo: EnemyDetector oluşturudlu.
    // ---
}
using System.Collections;
using ______Scripts______.GameManagerScript.SkillsScripts;
using ______Scripts______.PlayerScripts.PlayerLaserAbout;
using ______Scripts______.PlayerScripts.PlayerLaserAbout.Drone;
using ______Scripts______.PlayerScripts.SwordScripts;
using ______Scripts______.UIScripts.Canvas.Buttons;
using GameManagerScript.SkillsScripts;
using PlayerScripts.Player;
// using PlayerScripts.PlayerLaserAbout;
using PlayerScripts.SwordScripts;
using UIScripts;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable RedundantCheckBeforeAssignment
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace ______Scripts______.PlayerScripts.Player
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
        private PlayerAnimations _playerAnimations;
        private GameManager _gameManagerScript;

        [SerializeField] private GameObject PlayerLaserBullet;

        private float LaserTimer = 1;
        public float LaserCD = 0.2f;
        int _count = 0;
        private GameObject _GameManager;
        private GameObject Player;
        private Rigidbody2D RB2;
        private GameObject StarShip;
        private Transform ShotPoint;
        private GameObject Drone;

        private float speedSabit;
        private float speed;
        private float speedAmount;

        [SerializeField] private AudioSource _audioSourceStartShip;
        [SerializeField] private AudioClip _audioClipLaser;

        [SerializeField] private AudioSource _audioSourceMove;
        [SerializeField] private AudioClip _audioClipMove1;
        [SerializeField] private AudioClip _audioClipMove2;

        [SerializeField] private AudioSource _audioSourceJetPack;
        [SerializeField] private AudioClip _audioClipJetPack;

        [SerializeField] private StartButton _startButton;

        private void Awake()
        {
            Player = GameObject.Find("Player");
            StarShip = GameObject.Find("StarShip");
            Drone = GameObject.Find("Drone");
            RB2 = Player.GetComponent<Rigidbody2D>();
            _GameManager = GameObject.Find("GameManager");
            ShotPoint = StarShip.transform.Find("shotPoint");
            _gameManagerScript = _GameManager.GetComponent<GameManager>();
            _enemyDetector = Drone.GetComponent<EnemyDetector>();
            _droneScript = Drone.GetComponent<DroneScript>();
            __SwordScript = Player.GetComponent<SwordScript>();
            __PlayerScript = Player.GetComponent<PlayerScript>();
            _playerAnimations = Player.GetComponent<PlayerAnimations>();
            _startShipAttack = StarShip.GetComponent<StartShipAttack>();
            __SkillsScript = _GameManager.GetComponent<SkillsScript>();
            __SkillsData = _GameManager.GetComponent<SkillsDataScript>();
            __SkillsManager = _GameManager.GetComponent<SkillsManager>();
            __isGroundTouch = Player.transform.Find("IsGrounTouch").GetComponent<IsGroundTouchScript>(); // IsGrounTouch sonunda d yok
        }

        private void Start()
        {
            speed = Player.GetComponent<PlayerScript>().speed;
            speedSabit = speed;
        }

        public void MYFixedUpdate() // GameManagerdan çagırıyorum
        {
            if (_startButton.isGameStart == false) // oyun başlamamışsa çalışacak.
            {
                _startButton.StopPlayer(); // oyun başlamadıgı için karakter gemini içinde sabit durması lazım
                return;
            }

            if (__PlayerScript.isKnockbacked || __SkillsScript.isMoveSkilsUse || __PlayerScript.isHeDead == true) // Bu kod Player hit yediginde ve dodge, tumble vs attıgında: hareket etmesini ve zıplamasını engeliyecektir.
                return;


            if (Input.GetButton("Horizontal") && __SwordScript.isAttack == false) // sağ ve sol yönlerine gitmek için
            {
                Walking();
                // _playerAnimations.SetBoolParameter("isHeWalking", true);
            }
            else
            {
                // _playerAnimations.SetBoolParameter("isHeWalking", false);
            }


            if (__SkillsScript.isArmorFrameUse) // bu ArmorFrame kullandıgında zıplamasını engelliyecektir
                return;


            if (Input.GetKey(KeyCode.W) && __isGroundTouch.isGroundTouchBool) // zıplamak
            {
                Jump();
            }

            if (Input.GetKey(KeyCode.W)) // JetPack
            {
                __SkillsScript.JetPack();
                if (_count == 0 && __SkillsScript.JetPackFuel >= 0.2f)
                {
                    _count = 1;
                    _audioSourceJetPack.PlayOneShot(_audioClipJetPack);
                }
                else if (__SkillsScript.JetPackFuel < 0.2f)
                    _audioSourceJetPack.Stop();
            }
            else
            {
                _count = 0;
                _audioSourceJetPack.Stop();
            }
        }

        public void MYUpdate() // GameManagerdan çagırıyorum
        {
            __SkillsData.SkilsCoolDownTime(); // Skillerin kullanılabilir hale geçip geçmedigin kontrol eder.     

            _startShipAttack.MYUpdate();
            _droneScript.MYUpdate();
            _enemyDetector.MYUpdate();
            _playerAnimations.GroundSlameAnim(RB2);
            _playerAnimations.idleAnim(RB2);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _gameManagerScript.EscExitMenu();
            }

            if (_startButton.isGameStart == false) // oyun başlamamışsa çalışacak.
                return;

            if (RB2.gravityScale == 10 || __PlayerScript.isHeDead == true) // ArmorFrame kullanıldıgında, hava da iken çalışacak.  Bu havadayekn vuruş yapmasın diye koydum
                return; // bunun altındaki kodları etkiler.


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
                if (RB2.gravityScale == 1) // havada ise çalışsın
                {
                    __SkillsManager.ArmorFrame_manager();
                    _audioSourceJetPack.Stop();
                }
            }

            LaserTimer += Time.deltaTime;
            if (Input.GetMouseButton(0) && LaserTimer > LaserCD && _startShipAttack.SpaceShipAttackIsActive) // Laser silahı
            {
                // _audioSourceStartShip.PlayDelayed(0.5f);
                _audioSourceStartShip.PlayOneShot(_audioClipLaser);

                GameObject Laser = Instantiate(PlayerLaserBullet, ShotPoint.position, transform.rotation);
                Destroy(Laser, 5f);

                _startShipAttack.amountOfBullets--;

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

            _playerAnimations.JetPackAnimation(RB2, __SwordScript.isAttack);
        }

        void Walking()
        {
            speed = Player.GetComponent<PlayerScript>().speed; // bunu yazma nedenim: ArmorFrame gibi oyun içinde hızı azaltacak faktörleri uygulayabilmek için
            // if (__SkillsScript.isArmorFrameUse == false) // bu Shifte basınca hızlı koşmaydı
            MovedSpeedUp();
            speedAmount = speed * Time.deltaTime;
            RB2.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speedAmount, RB2.velocity.y);

            if (RB2.gravityScale == 0) // sadece yerde oldugunda çalışsın
            {
                _WalkSound();
                // StartCoroutine("WalkSound");
                _playerAnimations.ChangeAnimationState("Run");
            }

            if (Input.GetAxisRaw("Horizontal") == -1)
                Player.transform.rotation = new Quaternion(0, 180, 0, 0);
            else if (Input.GetAxisRaw("Horizontal") == 1)
                Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        void MovedSpeedUp() // jetpack kullanıldıgında çalışır
        {
            if (Input.GetKey(KeyCode.W)) // hızlı koşma
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

        // IEnumerator WalkSound()
        // {
        //     WaitForSeconds Wait = new WaitForSeconds(0.15f);
        //
        //     _audioSource.PlayOneShot(_audioClipMove1);
        //     yield return Wait;
        //     _audioSource.PlayOneShot(_audioClipMove2);
        //     yield return Wait;
        // }

        int count = 1;

        void _WalkSound()
        {
            float volume = _audioSourceMove.volume;
            _audioSourceMove.volume = volume / 2f;
            _audioSourceMove.pitch = 3f;
            if (_audioSourceMove.isPlaying == false)
            {
                if (count == 1)
                {
                    _audioSourceMove.PlayOneShot(_audioClipMove1);
                    count = 0;
                }
                else if (count == 0)
                {
                    _audioSourceMove.PlayOneShot(_audioClipMove2);
                    count = 1;
                }
            }

            _audioSourceMove.volume = volume;
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


    // ***Todo: Yapılanlar 20:

    // ---
}
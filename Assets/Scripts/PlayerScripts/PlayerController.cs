using System;
using GameManagerScript;
using UIScripts;
using UnityEngine;
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable RedundantCheckBeforeAssignment
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        private GameObject GameManager;
        private SkillsScript __SkillsScript;
        private ButtonScript __ButtonScript;
        private P_SwordScript __pSwordScript;
        private PlayerScript __PlayerScript;
        private GameObject Player;
        private Rigidbody2D RB2;
        private IsGroundTouchScript __isGroundTouch;
    
        private float speedSabit;
        private float speed;
        private float speedAmount;
        private float _time;
        
        // *** Bu alltaki şeyler Data Verileridir. En son ne zaman vuruş yaptıgının veya o sklinin Dolum süresinin verisini burda tutar.
        // ***  1-) lastNamePressAvailableTime    2-) lastNamePressTime     Aşagıdakiler Bu 2 sinin kısaltımı şekilde yazılmıştır.
        
        // ***  1-) lastNamePressAvailableTime = Skilin en son ne zaman kullanıldıgının zaman verisini tutar
        // ***  2-) lastNamePressTime = En son basıldıgı zamanın verisini tutar
        
        // private float LSwordKPT1;              // bu bir Buttona 2 veya 3 kere arka arka basıp basmadıgımızın, verisini tutuyor.
        private float LSwordKPAvailableT1;        // bu butonun dolum süresinin verisini tutuyor.
        
        private float LSwordKPT2;                 // bu bir Buttona 2 veya 3 kere arka arka basıp basmadıgımızın, verisini tutuyor.
        private float LSwordKPAvailableT2;        // bu butonun dolum süresinin verisini tutuyor.
        
        // private float LHittingAllKPT1;         // bu bir Buttona 2 veya 3 kere arka arka basıp basmadıgımızın, verisini tutuyor.
        private float LHittingAllKPAvailableT1;   // bu butonun dolum süresinin verisini tutuyor.
        
        private float LHittingAllKPT2;            // bu bir Buttona 2 veya 3 kere arka arka basıp basmadıgımızın, verisini tutuyor.
        private float LHittingAllKPAvailableT2;   // bu butonun dolum süresinin verisini tutuyor.
        
        // private float lastDodgeKeyPressTime;   // bu bir Buttona 2 veya 3 kere arka arka basıp basmadıgımızın, verisini tutuyor.
        private float LDodgeKPAvailableT;         // bu butonun dolum süresinin verisini tutuyor.
        
        private float LDashAtackKPT;              // bu bir Buttona 2 veya 3 kere arka arka basıp basmadıgımızın, verisini tutuyor.
        private float LDashAtackKPAvailableT;     // bu butonun dolum süresinin verisini tutuyor.
        
        // private float LArmorFrameKPT;          // bu bir Buttona 2 veya 3 kere arka arka basıp basmadıgımızın, verisini tutuyor.
        private float LArmorFrameKPAvailableT;    // bu butonun dolum süresinin verisini tutuyor.
        
        private void Awake()
        {
            Player = GameObject.Find("Player");
            GameManager = GameObject.Find("GameManager");
            RB2 = Player.GetComponent<Rigidbody2D>();
            __pSwordScript = Player.GetComponent<P_SwordScript>();
            __PlayerScript = Player.GetComponent<PlayerScript>();
            __ButtonScript = GameManager.GetComponent<ButtonScript>();
            __SkillsScript = GameManager.GetComponent<SkillsScript>();
            __isGroundTouch = Player.transform.Find("IsGrounTouch").GetComponent<IsGroundTouchScript>();    // IsGrounTouch sonunda d yok
        }

        private void Start()
        {
            speed = Player.GetComponent<PlayerScript>().speed;
            speedSabit = speed;
        }

        private void FixedUpdate()
        {
            SkilsCoolDownTime();   // Skillerin kullanılabilir hale geçip geçmedigin kontrol eder.     
    
            if (__PlayerScript.isKnockbacked || __SkillsScript.isMoveSkilsUse)   // Bu kod Player hit yediginde ve dodge, tumble vs attıgında: hareket etmesini ve zıplamasını engeliyecektir.
                return;
            
            if (Input.GetButton("Horizontal") && __pSwordScript.isAttack == false)  // sağ ve sol yönlerine gitmek için
            {
                speed = Player.GetComponent<PlayerScript>().speed;  // bunu yazma nedenim: ArmorFrame gibi oyun içinde hızı azaltacak faktörleri uygulayabilmek için
                if (__SkillsScript.isArmorFrameUse == false)
                    Run();
                speedAmount = speed * Time.deltaTime;
                RB2.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speedAmount, RB2.velocity.y);

                if (Input.GetAxisRaw("Horizontal") == -1)
                    Player.transform.rotation = new Quaternion(0, 180, 0, 0);
                else if (Input.GetAxisRaw("Horizontal") == 1)
                    Player.transform.rotation = new Quaternion(0, 0, 0, 0);
            }

            if (__SkillsScript.isArmorFrameUse)     // bu ArmorFrame kullandıgında zıplamasını engelliyecektir
                return;
                
            if (Input.GetKey(KeyCode.W) && __isGroundTouch.isGroundTouchBool)  // zıplamak
            {
                RB2.velocity = new Vector2(RB2.velocity.x, 5);
            }
        }

        private void Update()   
        {
            if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space)) // havaya zıplatıp alan vurma skili
            {
                LHittingAllKPAvailableT1 =
                    __ButtonScript.ButtonAvailable_A(__pSwordScript.HittingAll1, 3f, LHittingAllKPAvailableT1);
                
                (LHittingAllKPT2, LHittingAllKPAvailableT2) =
                    __ButtonScript.ManyPressButton(null,__pSwordScript.HittingAll2, null,  1f, 2, LHittingAllKPT2, 3f, LHittingAllKPAvailableT2);
            }
            else if (Input.GetKeyDown(KeyCode.Space))  // vuruş
            {
                // Aşagıda yaptıgın şey Skilli kullanıyor ve onun dolum süresini burdaki lastSwordKeyPressAvailableTime'e atıyor.
                LSwordKPAvailableT1 = 
                    __ButtonScript.ButtonAvailable_A(__pSwordScript.SwordAttack1, 1.5f, LSwordKPAvailableT1);
                
                (LSwordKPT2, LSwordKPAvailableT2) =
                    __ButtonScript.ManyPressButton( __pSwordScript.SwordAttack2,__pSwordScript.SwordAttack3, null,1.5f, 3, LSwordKPT2, 1.5f,LSwordKPAvailableT2);
            }
            
            if (Input.GetKeyDown(KeyCode.X))    // ArmorFrame Skill
            {
                LArmorFrameKPAvailableT =
                    __ButtonScript.ButtonAvailable_C(__SkillsScript.ArmorFrame,10f, LArmorFrameKPAvailableT);
            }

            
            if (__PlayerScript.isKnockbacked || __SkillsScript.isMoveSkilsUse || __SkillsScript.isArmorFrameUse)   // Bu kod Player hit yediginde ve dodge, dashatack vs attıgında: hareket etmesini engeliyecektir.
                return;     // bunun altındaki kodları etkiler.

            if (Input.GetButtonUp("Horizontal"))    // bunu burdan bool ile ayarlıyıp, FixedUpdaten çagırabiliriz.
            {   // Dash Atack
                (LDashAtackKPT, LDashAtackKPAvailableT) =  
                    __ButtonScript.ManyPressButton(null,null,__SkillsScript.DashAtack, 0.4f,3 ,LDashAtackKPT,5f,LDashAtackKPAvailableT);
            }
            
            if (Input.GetKeyDown(KeyCode.Q))    // q ile sola Dodge atıyor
            {
                LDodgeKPAvailableT =
                    __ButtonScript.ButtonAvailable_C(__SkillsScript.DodgeSkils_q, 3f, LDodgeKPAvailableT);
            }
            if (Input.GetKeyDown(KeyCode.E))    // e ile sağa Dodge atıyor
            {
                LDodgeKPAvailableT =
                    __ButtonScript.ButtonAvailable_C(__SkillsScript.DodgeSkils_e, 3f, LDodgeKPAvailableT);
            }
        }

        void Run()
        {
            if (Input.GetKey(KeyCode.LeftShift))  // hızlı koşma
            {
                if (speed != speedSabit * 1.8f)
                    speed = speedSabit * 1.8f;
            }
            else
            {
                speed = speedSabit;
            }
        }

        private void SkilsCoolDownTime()    // burdan Canvasta Skillerin kullanılabilir hale geçip geçmediginin, işleyen bir fonksiyon.
        {
            // 3f ler 1.5f ler 10f ler skillerin dolum süresini gösteriyor.
            _time += Time.deltaTime;
            
            if (_time > 3f + LHittingAllKPAvailableT1)  // HittingAll_1 Skils
            {
                // print("HittingAll1 Skili Kullanılabilir");   
            }
            else if (_time > 3f + LHittingAllKPAvailableT2)  // HittingAll_2 Skils
            {
                // print("HittingAll2 Skili Kullanılabilir");   
            }

            if (_time > 1.5f + LSwordKPAvailableT1) // SwordAtack_1 Skils
            {
                // print("SwordAttack_1 Skili Kullanılabilir");   
            }
            else if (_time > 1.5f + LSwordKPAvailableT2) // SwordAtack_2 ve SwordAtack_3 Skils
            {
                // print("SwordAttack_2 ve SwordAttack_3 Skili Kullanılabilir");   // burda SwordAttack_2'nin dolum süresini alamıyorum ondan: Tasarım kısmında aralında bir köprü görevi görür gibi bir hissiyat vermek lazım.
            }

            if (_time > 10f + LArmorFrameKPAvailableT)  // ArmorFrame Skils
            {
                // print("ArmorFrame Skili kullanılabilir");
            }

            if (_time > 5f + LDashAtackKPAvailableT)    // DashAtack Skils
            {
                // print("DashAtack Skili kullanılabilir");
            }

            if (_time > 3f + LDodgeKPAvailableT)
            {
                // print("Dodge Skili kullanılabilir");
            }

        }
    }
    // -Todo: -Uğur-
    
    // *Todo: Yapılacaklar:
    // Todo: Error:  gizmoz niye çalışmıyor ona bak,
    // Todo: PlayerController'dan SkilsCoolDownTime ve Zaman verilerini al başka bir Scripte aktar. Orda dursun ve ordan çagır kullan.
    // Todo: Enemy geldigi gibi çarparak dmg vurmasın. Onun özel bir IEnumator ile vuruş şeklini oluştur. Ve onu kullan.
    // ---
    
    // *Todo: Belki Yapılabilir:
    // Todo:  if (_time > 3f + LHittingAllKPAvailableT1)    Bu Scripteki 72. satırdaki koda Skillerin dolsun süresini tutan bir float oluşturabilirsin.
    // Todo: StopMoveAndLookAround Fonksiyonuna kafalarının üstüne bir ünlem koyabiliriz.
    // ---
    
    // *Todo: Yapılanlar 3:
    // Todo: AI'da MyOwnBase'de IEnumerotör işlemleri entegre ettim.
    // Todo: AI'da StopMoveAndLookAround Fonksiyonunu oluşturdum.
    // Todo: AI'da Enemy'nin baktıgı yönlerin hepsini düzelttim.
    // Todo: Tüm çevresine vuran bir skil yaptım. S ve Space tuşuna basınca çalışıyor.Combosuda var.
    // Todo: Skillerin kullanılabilir hale geçip geçmedigini işleyen bir fonksiyon yazdım.
    // ---

}

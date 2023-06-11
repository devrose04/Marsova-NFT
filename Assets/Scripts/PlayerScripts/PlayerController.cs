using System;
using GameManagerScript;
using GameManagerScript.SkillsScripts;
using PlayerScripts.SwordScripts;
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
        private SkillsController __SkillsController;
        private SwordScript __SwordScript;
        private PlayerScript __PlayerScript;
        private SkillsDataScript __SkillsData;
        private IsGroundTouchScript __isGroundTouch;
        private GameObject Player;
        private Rigidbody2D RB2;
    
        private float speedSabit;
        private float speed;
        private float speedAmount;

        private void Awake()
        {
            Player = GameObject.Find("Player");
            RB2 = Player.GetComponent<Rigidbody2D>();
            GameManager = GameObject.Find("GameManager");
            __SwordScript = Player.GetComponent<SwordScript>();
            __PlayerScript = Player.GetComponent<PlayerScript>();
            __SkillsScript = GameManager.GetComponent<SkillsScript>();
            __SkillsData = GameManager.GetComponent<SkillsDataScript>();
            __SkillsController = GameManager.GetComponent<SkillsController>();
            __isGroundTouch = Player.transform.Find("IsGrounTouch").GetComponent<IsGroundTouchScript>();    // IsGrounTouch sonunda d yok
        }

        private void Start()
        {
            speed = Player.GetComponent<PlayerScript>().speed;
            speedSabit = speed;
        }

        private void FixedUpdate()
        { 
            __SkillsData.SkilsCoolDownTime();   // Skillerin kullanılabilir hale geçip geçmedigin kontrol eder.     
    
            if (__PlayerScript.isKnockbacked || __SkillsScript.isMoveSkilsUse)   // Bu kod Player hit yediginde ve dodge, tumble vs attıgında: hareket etmesini ve zıplamasını engeliyecektir.
                return;
            
            if (Input.GetButton("Horizontal") && __SwordScript.isAttack == false)  // sağ ve sol yönlerine gitmek için
            {
                Walking();
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
                // Aşagıda yaptıgın şey Skilli kullanıyor ve onun zaman verisini burdaki __SkillsData.HittingAllCanUse1'e atıyor.
                __SkillsData.HittingAllCanUse1 = __SkillsController.HittingAll1_ctrl();

                (__SkillsData.HittingAll2, __SkillsData.HittingAllCanUse2) = __SkillsController.HittingAll2_ctrl();
            }
            else if (Input.GetKeyDown(KeyCode.Space))  // 3 lü vuruş combo
            {
                
                __SkillsData.SwordCanUse1 = __SkillsController.SwordAttack1_ctrl();

                (__SkillsData.Sword2, __SkillsData.SwordCanUse2) = __SkillsController.SwordAttack2_and_SwordAttack3_ctrl();
            }
            
            if (Input.GetKeyDown(KeyCode.X))  // ArmorFrame Skill
            {
                __SkillsData.ArmorFrameCanUse = __SkillsController.ArmorFrame_ctrl();
            }

            if (__PlayerScript.isKnockbacked || __SkillsScript.isMoveSkilsUse || __SkillsScript.isArmorFrameUse)   // Bu kod Player hit yediginde ve dodge, dashatack vs attıgında: hareket etmesini engeliyecektir.
                return;     // bunun altındaki kodları etkiler.

            if (Input.GetButtonUp("Horizontal"))  // DashAttack Skill
            {
                (__SkillsData.DashAttack, __SkillsData.DashAttackCanUse) = __SkillsController.DashAtack_ctrl();
            }
            
            if (Input.GetKeyDown(KeyCode.Q))    // q ile sola Dodge atıyor
            {
                __SkillsData.DodgeCanUse = __SkillsController.DodgeSkils_q_ctrl();
            }
            if (Input.GetKeyDown(KeyCode.E))    // e ile sağa Dodge atıyor
            {
                __SkillsData.DodgeCanUse = __SkillsController.DodgeSkils_e_ctrl();
            }
        }

        void Walking()
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

    }
    // -Todo: -Uğur-
    
    // *Todo: Yapılacaklar:
    
    // Todo: Error:  gizmoz niye çalışmıyor ona bak,
    // Todo: Enemy geldigi gibi çarparak dmg vurmasın. Onun özel bir IEnumator ile vuruş şeklini oluştur. Ve onu kullan.
    // Todo: bazı if yerine Switch case kullanılabilirmi bak.
    // ---
    
    // *Todo: Belki Yapılabilir:
    
    // Todo: GamaManager her saniye degil. saniyede 1 kere çalışsınki pc daha az yorulsun.
    // Todo: DmgCollider daha okunaklı olabilir.    (Enemy'lerin vuruş şeklini degiştirdikten sonra bunu yap)
    
    // Todo: Hep tekrar tekrar tanımlayıp kullandıgım şeyleri GameManager da static olarak tanımla ve hep ordan çek.
    // Todo: Skillerin Datasını tuttugun Scripte statik kullanmak verimli olurmu, ona bak.
    // Todo: Time.delta'lı şeyleri static olarak kullansam daha mı verimli olur, ona bak.
    
    // Todo: Scriptlerin en başındaki yerleri düzenlicem.
    // Todo: PlayerController daki FixedUpda ve Update'i bir tane fonksiyona koy ve GameManagerdan çagırmayı dene.
    // Todo: GameManagerda OwnEffect yerinde InvokeRepeating fonksiyonunu kullanarak daha kısa ve öz yazabilirsin.
    
    // Todo: Invoke metodlarını kullanarak oyun mekanigine güzel eklentiler yapabilirsin.   (uygula bunu)
    
    // Todo: time dedigin yerlere Timer de.
    // Todo: GameManager Update fonksiyonun üstüne Timer oluşturdugum gibi diger Scriptleri Fonksiyonlarada onu yap
    // ---
    
    // *Todo: Yapılanlar 4:
    
    // Todo: PlayerController'dan SkillsDataScripti oluşturdum ve Zaman verilerini ordan alıp PlayerController'a aktarıyorum.
    // Todo: Skilleri bir SkillsController oluşturdum ve ordan fonksiyona koydum ordan Controlerdan çagırıp işlem yapıyorum (daha okunaklı oldu)
    // Todo: Kodun çogu Scriptlerini düzenledim daha okunaklı yaptım.
    // Todo: AISkillsScripts oluşturdum ve AISkill kodlarını oraya aktardım (daha okunaklı oldu)
    // Todo: SwordSkills ve SwordController oluşturdum. (daha okunaklı oldu)
    // ---

}

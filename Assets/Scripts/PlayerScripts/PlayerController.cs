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
        private SkillsScript __SkillsScript;
        private SkillsController __SkillsController;
        private SwordScript __SwordScript;
        private PlayerScript __PlayerScript;
        private SkillsDataScript __SkillsData;
        private IsGroundTouchScript __isGroundTouch;
       
        private GameObject GameManager;
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

        public void MYFixedUpdate() // GameManagerdan çagırıyorum
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

        public void MYUpdate()     // GameManagerdan çagırıyorum
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
    
    // Todo: Error:  gizmoz niye çalışmıyor ona bak,
    
    // *Todo: Yapılacaklar:
    
    
    // Todo: ortada ki çizgiyi kaldır.
    // Todo: Player Controlerdaki Skiller fonksiyona konsun.
    
    // Todo: a-) Enemy geldigi gibi çarparak dmg vurmasın. Onun özel bir geçikmeli İnvoke fonks. ile vuruş şeklini oluştur. Ve onu kullan.
    // Todo: a-) Enemy'e değdiginde direk canı gitmesin, 0.5f 1f arasında bir süre ile Enemy o alandaki yere vuruş yapsın.
    // Todo: Dash'in KnockBackni ayarla.
    
    // ---
    
    
    // *Todo: Belki Yapılabilir:
    
    

    // Todo: SwordAtack fonksiyonuna girilen parametleri TakesDamages fonksiyonun içinde kullanıyoruz ama orda parametre olarak girmemişiz? böyle çalışıyormu. oluyorsa diger Scriptlerede uygula
    // ---
    
    
    // *Todo: Yapılanlar 5:
    
    // Todo: GamaManager her saniye degil. saniyede 1 kere çalıştırıyorum ki pc daha az yorulsun diye.
    // Todo: PlayerController daki FixedUpdat ve Update'i bir tane fonksiyona koydum ve GameManagerdan çagırdım.
    // Todo: bazı yerlerde time dedigim yerleri Timer dedim.
    // Todo: EnemyScripte CretedEffectk fonksiyonunu düzenledim.
    // Todo: Scriptlerin en başındaki yerleri düzenledim.
    // Todo: bazı yerlerde if else yerine Switch case olarak değiştirdim.
    // Todo: a-) Dash vuruş mekanigini değiştirdim. ve başka bir yere taşıdım.
    // Todo: a-) DmgCollider daha okunaklı olabilir. 
    // Todo: KnockBack'i tamamne degiştirdim ve başka özellikler ekeldim.
    // Todo: Yeni bir KnockBack diye Script oluşturdum. ve oraya taşıdım.
    // ---

}

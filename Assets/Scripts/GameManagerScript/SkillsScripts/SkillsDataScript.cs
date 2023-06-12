using UnityEngine;
using UnityEngine.Serialization;

namespace GameManagerScript.SkillsScripts
{
    public class SkillsDataScript : MonoBehaviour
    {
        private float timer;

        // *** Bu alltaki şeyler Data Verileridir. En son ne zaman tuşa bastığının veya o sklinin Dolum süresinin verisini burda tutar.
        // ***  1-) lastPressTime    2-) lastPressCanUseTime     Aşagıdakiler Bu 2 sinin kısaltımı şekilde yazılmıştır.
        
        // ***  1-) lastPressTime = En son basıldıgı zamanın verisini tutar     // 2 3 kere bir tuşa arka arka basınca kullanılacak veride bu
        // ***  2-) lastPressCanUseTime = Skilin en son ne zaman kullanıldıgının zaman verisini tutar
        
        // private float Sword1;                      // bu En son butona basıldıgı zamanın verisini tutar  
        public float SwordCanUse1;                    // bu Butonun en son ne zaman aktif olarak kullanıldıgının verisini tutuyor.
        [SerializeField] public float SwordCD1;       // bu butonun dolum süresinin verisini tutuyor.
        
        public float Sword2;                          // ""
        public float SwordCanUse2;                    // ""
        [SerializeField] public float SwordCD2;       // ""

        // private float HittingAll1;                 // ""  
        public float HittingAllCanUse1;               // ""
        [SerializeField] public float HittingAllCD1;  // ""

        public float HittingAll2;                     // ""
        public float HittingAllCanUse2;               // ""
        [SerializeField] public float HittingAllCD2;  // ""

        // private float Dodge;                       // ""
        public float DodgeCanUse;                     // ""
        [SerializeField] public float DodgeCD;        // ""

        public float DashAttack;                      // ""
        public float DashAttackCanUse;                // ""
        [SerializeField] public float DashAtackCD;    // ""

        // private float ArmorFrame;                  // ""
        public float ArmorFrameCanUse;                // ""
        [SerializeField] public float ArmorFrameCD;   // ""

        
        public void SkilsCoolDownTime() // burdan Canvasta Skillerin kullanılabilir hale geçip geçmediginin, işleyen bir fonksiyon.
        {
            // 3f ler 1.5f ler 10f ler skillerin dolum süresini gösteriyor.
            timer += Time.deltaTime;

            if (timer > HittingAllCD1 + HittingAllCanUse1) // HittingAll_1 Skils
            {
                // print("HittingAll1 Skili Kullanılabilir");   
            }
            else if (timer > HittingAllCD2 + HittingAllCanUse2) // HittingAll_2 Skils
            {
                // print("HittingAll2 Skili Kullanılabilir");   
            }

            if (timer > SwordCD1 + SwordCanUse1) // SwordAtack_1 Skils
            {
                // print("SwordAttack_1 Skili Kullanılabilir");   
            }
            else if (timer > SwordCD2 + SwordCanUse2) // SwordAtack_2 ve SwordAtack_3 Skils
            {
                // print("SwordAttack_2 ve SwordAttack_3 Skili Kullanılabilir");   // burda SwordAttack_2'nin dolum süresini alamıyorum ondan: Tasarım kısmında aralında bir köprü görevi görür gibi bir hissiyat vermek lazım.
            }

            if (timer > ArmorFrameCD + ArmorFrameCanUse) // ArmorFrame Skils
            {
                // print("ArmorFrame Skili kullanılabilir");
            }

            if (timer > DashAtackCD + DashAttackCanUse) // DashAtack Skils
            {
                // print("DashAtack Skili kullanılabilir");
            }

            if (timer > DodgeCD + DodgeCanUse)
            {
                // print("Dodge Skili kullanılabilir");
            }
        }

    }
}

using UnityEngine;
using UnityEngine.Serialization;

namespace GameManagerScript.SkillsScripts
{
    public class SkillsDataScript : MonoBehaviour
    {
        [SerializeField] private float timer;

        // *** Bu alltaki şeyler Data Verileridir. En son ne zaman tuşa bastığının veya o sklinin Dolum süresinin verisini burda tutar.
        // ***  1-) lastPressTime    2-) lastPressCanUseTime     Aşagıdakiler Bu 2 sinin kısaltımı şekilde yazılmıştır.

        // ***  1-) lastPressTime = En son basıldıgı zamanın verisini tutar     // 2 3 kere bir tuşa arka arka basınca kullanılacak veride bu
        // ***  2-) lastPressCanUseTime = Skilin en son ne zaman kullanıldıgının zaman verisini tutar

        [SerializeField] private GameObject ArmorFrameButton;
        [SerializeField] private GameObject DashButton;
        [SerializeField] private GameObject DodgeButton;

        [SerializeField] private GameObject SwordAttack_1Button;
        [SerializeField] private GameObject SwordAttack_2Button;

        [SerializeField] private GameObject HittingAll_1Button;
        [SerializeField] private GameObject HittingAll_2Button;

        // private float Sword1;                      // bu En son butona basıldıgı zamanın verisini tutar  
        public float SwordCanUse1; // bu Butonun en son ne zaman aktif olarak kullanıldıgının verisini tutuyor.
        [SerializeField] public float SwordCD1; // bu butonun dolum süresinin verisini tutuyor.

        public float Sword2; // ""
        public float SwordCanUse2; // ""
        [SerializeField] public float SwordCD2; // ""

        // private float HittingAll1;                 // ""  
        public float HittingAllCanUse1; // ""
        [SerializeField] public float HittingAllCD1; // ""

        public float HittingAll2; // ""
        public float HittingAllCanUse2; // ""
        [SerializeField] public float HittingAllCD2; // ""

        // private float Dodge;                       // ""
        public float DodgeCanUse; // ""
        [SerializeField] public float DodgeCD; // ""

        public float DashAttack; // ""
        public float DashAttackCanUse; // ""
        [SerializeField] public float DashAtackCD; // ""

        // private float ArmorFrame;                  // ""
        public float ArmorFrameCanUse; // ""
        [SerializeField] public float ArmorFrameCD; // ""


        public void SkilsCoolDownTime() // burdan Canvasta Skillerin kullanılabilir hale geçip geçmediginin, işleyen bir fonksiyon.
        {
            // 3f ler 1.5f ler 10f ler skillerin dolum süresini gösteriyor.
            timer += Time.deltaTime;

            if (timer > HittingAllCD1 + HittingAllCanUse1) // HittingAll_1 Skils
                HittingAll_1Button.SetActive(true);
            else
                HittingAll_1Button.SetActive(false);


            if (timer > HittingAllCD2 + HittingAllCanUse2 && HittingAll_1Button.activeSelf == true) // HittingAll_2 Skils
                HittingAll_2Button.SetActive(true);
            else
                HittingAll_2Button.SetActive(false);


            if (timer > SwordCD1 + SwordCanUse1) // SwordAtack_1 Skils
                SwordAttack_1Button.SetActive(true);
            else
                SwordAttack_1Button.SetActive(false);


            if (timer > SwordCD2 + SwordCanUse2 && SwordAttack_1Button.activeSelf == true) // SwordAtack_2 ve SwordAtack_3 Skils
                SwordAttack_2Button.SetActive(true);
            else
                SwordAttack_2Button.SetActive(false);


            if (timer > ArmorFrameCD + ArmorFrameCanUse) // ArmorFrame Skils
                ArmorFrameButton.SetActive(true);
            else
                ArmorFrameButton.SetActive(false);


            if (timer > DashAtackCD + DashAttackCanUse) // DashAtack Skils
                DashButton.SetActive(true);
            else
                DashButton.SetActive(false);


            if (timer > DodgeCD + DodgeCanUse)
                DodgeButton.SetActive(true);
            else
                DodgeButton.SetActive(false);
        }
    }
}
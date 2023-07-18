using ______Scripts______.PlayerScripts.SwordScripts;
using UnityEngine;
using UnityEngine.Events;

namespace ______Scripts______.Upgrade.All_Upgrade
{
    public class UpDmg : MonoBehaviour, IUpgrade_Chose_1
    {
        [SerializeField] private SwordScript _swordScript;

        private string MainSentece;
        private string ExtraSentecen;

        void CheckText()
        {
            MainSentece = "Increase the player's sword attack power by 3.";
            ExtraSentecen = $"{_swordScript.swordDamage} --> {_swordScript.swordDamage + 3}";
        }

        void UppDamges()
        {
            _swordScript.swordDamage += 3;
        }

        public (string, string, UnityAction) Upgrade()
        {
            CheckText();
            return (MainSentece, ExtraSentecen, UppDamges);
        }
    }
}
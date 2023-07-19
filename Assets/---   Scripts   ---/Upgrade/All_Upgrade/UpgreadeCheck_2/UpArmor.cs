using PlayerScripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace ______Scripts______.Upgrade.All_Upgrade.UpgreadeCheck_2
{
    public class UpArmor : MonoBehaviour, IUpgrade_Chose_2
    {
        [SerializeField] private PlayerScript _playerScript;

        private string MainSentece;
        private string ExtraSentecen;

        void CheckText()
        {
            MainSentece = "Increase the Player's Armor by 10%.";
            ExtraSentecen = $"{_playerScript.ExtraArmor}% --> {_playerScript.ExtraArmor + 10}%";
        }

        void UppArmor()
        {
            _playerScript.ExtraArmor += 10;
        }

        public (string, string, UnityAction) Upgrade()
        {
            CheckText();
            return (MainSentece, ExtraSentecen, UppArmor);
        }
    }
}
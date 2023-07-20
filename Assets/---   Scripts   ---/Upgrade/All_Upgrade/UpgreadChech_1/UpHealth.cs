using ______Scripts______.PlayerScripts.PlayerLaserAbout;
using ______Scripts______.UIScripts.Canvas.Player;
using PlayerScripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace ______Scripts______.Upgrade.All_Upgrade.UpgreadChech_1
{
    public class UpHealth : MonoBehaviour, IUpgrade_Chose_1
    {
        [SerializeField] private PlayerScript _playerScript;
        [SerializeField] private HealthBarScript _healthBarScript;

        private string MainSentece;
        private string ExtraSentecen;

        void CheckText()
        {
            MainSentece = "Permanently increase the player's health by 30.";
            ExtraSentecen = $"{_healthBarScript.maxHealth} --> {_healthBarScript.maxHealth + 30}"; //todo: Bar yerindede güncellemeyi uygula
        }

        void UppHealth()
        {
            _playerScript.health += 30;
            _healthBarScript.UpgreadHealth();
        }

        public (string, string, UnityAction) Upgrade()
        {
            CheckText();
            return (MainSentece, ExtraSentecen, UppHealth);
        }
    }
}
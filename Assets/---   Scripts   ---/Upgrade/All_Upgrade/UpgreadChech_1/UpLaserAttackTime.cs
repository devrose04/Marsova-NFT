using ______Scripts______.PlayerScripts.Player;
using ______Scripts______.PlayerScripts.PlayerLaserAbout.Drone;
using UnityEngine;
using UnityEngine.Events;

namespace ______Scripts______.Upgrade.All_Upgrade.UpgreadChech_1
{
    public class UpLaserAttackTime : MonoBehaviour, IUpgrade_Chose_1
    {
        [SerializeField] private PlayerController _playerController;

        private string MainSentece;
        private string ExtraSentecen;

        void CheckText()
        {
            MainSentece = "Decrease the Laser's firing frequency by 0.2 seconds.";
            ExtraSentecen = $"{_playerController.LaserCD} --> {_playerController.LaserCD - 0.02f}";
        }

        void DownAttackCD()
        {
            _playerController.LaserCD -= 0.02f;
        }

        public (string, string, UnityAction) Upgrade()
        {
            CheckText();
            return (MainSentece, ExtraSentecen, DownAttackCD);
        }
    }
}
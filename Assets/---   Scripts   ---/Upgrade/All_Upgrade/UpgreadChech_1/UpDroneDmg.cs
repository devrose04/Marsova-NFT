using ______Scripts______.PlayerScripts.PlayerLaserAbout.Drone;
using UnityEngine;
using UnityEngine.Events;

namespace ______Scripts______.Upgrade.All_Upgrade.UpgreadChech_1
{
    public class UpDroneDmg : MonoBehaviour, IUpgrade_Chose_1
    {
        [SerializeField] private EnemyDetector _enemyDetector;

        private string MainSentece;
        private string ExtraSentecen;

        void CheckText()
        {
            MainSentece = "Increase the Drone's damage by 2.";
            ExtraSentecen = $"{_enemyDetector.DroneDmg} --> {_enemyDetector.DroneDmg + 2}";
        }

        void UppDroneDmg()
        {
            _enemyDetector.DroneDmg += 2;
        }

        public (string, string, UnityAction) Upgrade()
        {
            CheckText();
            return (MainSentece, ExtraSentecen, UppDroneDmg);
        }
    }
}
using ______Scripts______.PlayerScripts.PlayerLaserAbout.Drone;
using UnityEngine;
using UnityEngine.Events;

namespace ______Scripts______.Upgrade.All_Upgrade.UpgreadeCheck_2
{
    public class UpDroneAttackRadius : MonoBehaviour, IUpgrade_Chose_2
    {
        [SerializeField] private EnemyDetector _enemyDetector;

        private string MainSentece;
        private string ExtraSentecen;

        void CheckText()
        {
            MainSentece = "The drone's attack radius increases by 2.";
            ExtraSentecen = $"{_enemyDetector.detectionRadius} --> {_enemyDetector.detectionRadius + 2}";
        }

        void UppDroneDmg()
        {
            _enemyDetector.detectionRadius += 2;
        }

        public (string, string, UnityAction) Upgrade()
        {
            CheckText();
            return (MainSentece, ExtraSentecen, UppDroneDmg);
        }
    }
}
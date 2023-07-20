using ______Scripts______.PlayerScripts.PlayerLaserAbout.Drone;
using UnityEngine;
using UnityEngine.Events;

namespace ______Scripts______.Upgrade.All_Upgrade.UpgreadeCheck_2
{
    public class UpDroneAttackTime_ : MonoBehaviour, IUpgrade_Chose_2
    {
        [SerializeField] private EnemyDetector _enemyDetector;

        private string MainSentece;
        private string ExtraSentecen;

        void CheckText()
        {
            MainSentece = "Reduce the Drone's attack frequency by 0.2 seconds.";
            ExtraSentecen = $"{_enemyDetector.timeLimit} --> {_enemyDetector.timeLimit - 0.2f}";
        }

        void DownAttackCD()
        {
            _enemyDetector.timeLimit = Mathf.Round(_enemyDetector.timeLimit - 0.2f); // yuvarlıyoruz
        }

        public (string, string, UnityAction) Upgrade()
        {
            CheckText();
            return (MainSentece, ExtraSentecen, DownAttackCD);
        }
    }
}
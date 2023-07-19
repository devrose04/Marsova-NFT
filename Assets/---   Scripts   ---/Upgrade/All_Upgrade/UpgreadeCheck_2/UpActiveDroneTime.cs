using ______Scripts______.PlayerScripts.PlayerLaserAbout;
using ______Scripts______.PlayerScripts.PlayerLaserAbout.Drone;
using UnityEngine;
using UnityEngine.Events;

namespace ______Scripts______.Upgrade.All_Upgrade.UpgreadeCheck_2
{
    public class UpActiveDroneTime : MonoBehaviour, IUpgrade_Chose_2
    {
        [SerializeField] private DroneScript _droneScript;

        private string MainSentece;
        private string ExtraSentecen;

        void CheckText()
        {
            MainSentece = "Increase the active attack duration of the Drone by 2.";
            ExtraSentecen = $"{_droneScript.ActiveDroneTime} --> {_droneScript.ActiveDroneTime + 2f}";
        }

        void UppDroneActiveTime()
        {
            _droneScript.ActiveDroneTime += 2f;
        }

        public (string, string, UnityAction) Upgrade()
        {
            CheckText();
            return (MainSentece, ExtraSentecen, UppDroneActiveTime);
        }
    }
}
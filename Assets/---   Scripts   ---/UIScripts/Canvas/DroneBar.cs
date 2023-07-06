using System;
using ______Scripts______.PlayerScripts.PlayerLaserAbout.Drone;
using UnityEngine;
using UnityEngine.UI;

namespace ______Scripts______.UIScripts.Canvas
{
    public class DroneBar : MonoBehaviour
    {
        private GameObject Drone;
        private DroneScript _droneScript;

        [SerializeField] private Slider _droneBar;

        private void Awake()
        {
            Drone = this.gameObject;
            _droneScript = Drone.GetComponent<DroneScript>();
        }

        public void DroneBarFonk()
        {
            _droneBar.value = _droneScript.ActiveDroneTime - _droneScript.DroneStartAttackTimer;
            _droneBar.maxValue = _droneScript.ActiveDroneTime;
        }
    }
}
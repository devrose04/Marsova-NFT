using System;
using PlayerScripts.Player;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace ______Scripts______.Canvas.Player
{
    public class HealthBarScript : MonoBehaviour
    {
        [SerializeField] private GameObject Bar;
        private GameObject Player;

        private Slider _slider;
        private PlayerScript _playerScript;

        private void Start()
        {
            Player = this.gameObject;

            _slider = Bar.GetComponent<Slider>();
            _playerScript = Player.GetComponent<PlayerScript>();

            _slider.maxValue = _playerScript.health;
            _slider.value = _playerScript.health;
        }

        public void ChangeHealthBar()
        {
            _slider.value = _playerScript.health;
        }
    }
}
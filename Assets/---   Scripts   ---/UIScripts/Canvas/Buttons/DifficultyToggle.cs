using PlayerScripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class DifficultyToggle : MonoBehaviour
    {
        [SerializeField] private Toggle Easy;
        [SerializeField] private Toggle Normal;
        [SerializeField] private Toggle Hard;

        [SerializeField] private PlayerScript _playerScript;

        public void EasyFonk()
        {
            Normal.isOn = false;
            Hard.isOn = false;
            _playerScript.health = 200;
        }

        public void NormalFonk()
        {
            Easy.isOn = false;
            Hard.isOn = false;
            _playerScript.health = 120;
        }

        public void HardFonk()
        {
            Easy.isOn = false;
            Normal.isOn = false;
            _playerScript.health = 60;
        }
    }
}
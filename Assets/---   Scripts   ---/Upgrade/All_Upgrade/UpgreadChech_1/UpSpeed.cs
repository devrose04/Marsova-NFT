using PlayerScripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace ______Scripts______.Upgrade.All_Upgrade
{
    public class UpSpeed : MonoBehaviour, IUpgrade_Chose_1
    {
        [SerializeField] private PlayerScript _playerScript;

        private string MainSentece;
        private string ExtraSentecen;

        void CheckText()
        {
            MainSentece = "It increases the Player's movement speed by 20.";
            ExtraSentecen = $"{_playerScript.speed} --> {_playerScript.speed + 20}";
        }

        void UppSpead()
        {
            _playerScript.speed += 20;
        }

        public (string, string, UnityAction) Upgrade()
        {
            CheckText();
            return (MainSentece, ExtraSentecen, UppSpead);
        }
    }
}
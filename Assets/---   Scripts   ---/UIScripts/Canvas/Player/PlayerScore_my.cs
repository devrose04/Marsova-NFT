using System;
using ______Scripts______.Upgrade;
using PlayerScripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace ______Scripts______.UIScripts.Canvas.Player
{
    public class PlayerScore_my : MonoBehaviour
    {
        [SerializeField] private Text _text;

        private GameObject Player;
        private PlayerScript _playerScript;

        [SerializeField] private GameObject Block2;
        [SerializeField] private GameObject Block3;

        [SerializeField] private UpController _upController;

        int Product = 1;

        private void Awake()
        {
            Player = this.gameObject;
            _playerScript = Player.GetComponent<PlayerScript>();
        }

        public void ScoreUpdate()
        {
            _text.text = _playerScript.totalScore.ToString();

            if (_playerScript.totalScore >= 50 * Product)
            {
                _upController.ShowUpdateMenu();
                Product += 1;
            }

            if (_playerScript.totalScore >= 1000)
                Block3.SetActive(true);
            else if (_playerScript.totalScore >= 300)
                Block2.SetActive(true);
        }
    }
}
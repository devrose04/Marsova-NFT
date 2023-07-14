using System;
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

        private void Awake()
        {
            Player = this.gameObject;
            _playerScript = Player.GetComponent<PlayerScript>();
        }

        public void ScoreUpdate()
        {
            _text.text = _playerScript.totalScore.ToString();

            if (_playerScript.totalScore >= 300)
                Block3.SetActive(true);
            else if (_playerScript.totalScore >= 100)
                Block2.SetActive(true);
        }
    }
}
using PlayerScripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private GameObject MainMenu;
        [SerializeField] private GameObject Player;

        [SerializeField] private PlayerScript _playerScript;


        public void StartGame()
        {
            _playerScript.totalScore = 0;
            MainMenu.SetActive(false);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Player.transform.position = new Vector3(10, 0, 0);
            // todo: Player'ın pozisyonunu belirli yere taşı.
        }
    }
}
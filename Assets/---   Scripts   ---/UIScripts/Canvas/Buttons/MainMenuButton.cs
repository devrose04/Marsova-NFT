using PlayerScripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class MainMenuButton : MonoBehaviour
    {
        [SerializeField] private GameObject SettingMenu;
        [SerializeField] private GameObject GameOver;

        [SerializeField] private PlayerScript _playerScript;

        public void OpenMainMenu()
        {
            _playerScript.totalScore = 0;
            SettingMenu.SetActive(false);
            GameOver.SetActive(false);
            Time.timeScale = 1;
            // Player.transform.position = new Vector3(20, 0, 0); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // Player'ı ışınlar 
        }
    }
}
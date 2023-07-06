using PlayerScripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class ReStartButton : MonoBehaviour
    {
        [SerializeField] private GameObject GameOverimages;
        [SerializeField] private GameObject SettingMenu;
        [SerializeField] private GameObject MainMenu;

        [SerializeField] private GameObject Player;
        [SerializeField] private PlayerScript _playerScript;

        public void RestartGame()
        {
            _playerScript.totalScore = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameOverimages.SetActive(false);
            SettingMenu.SetActive(false);
            MainMenu.SetActive(true);
            Time.timeScale = 1;
            // Player.transform.position = new Vector3(10, 0, 0); // todo: ReStrart ve Start'ta hata var düzelt onu
        }
    }
}
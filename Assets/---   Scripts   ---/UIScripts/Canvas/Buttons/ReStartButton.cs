using ______Using_Assets______.DataBaseAssets.dreamlo;
using ______Using_Assets______.DataBaseAssets.dreamlo.MyScript;
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

        [SerializeField] private dreamloLeaderBoard _dreamloLeaderBoard;
        [SerializeField] private ScoreSender _scoreSender;

        [SerializeField] private PlayerShowOwnScore _playerShowOwnScore;
        [SerializeField] private SaveData _saveData;

        public void RestartGame()
        {
            StartCoroutine(_scoreSender.SendScore(PlayerPrefs.GetString("NickName"), _playerScript.totalScore));
            _playerShowOwnScore.ChangeOwnHighScore();
            _playerShowOwnScore.ChangeOwnNickName();
            _saveData.SendScore();

            _playerScript.totalScore = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            // Bunlar altta oldugu için çalışmaz
            // ### 
            GameOverimages.SetActive(false);
            SettingMenu.SetActive(false);
            MainMenu.SetActive(true);
            Time.timeScale = 1;
            // ###     

            // Player.transform.position = new Vector3(10, 0, 0); // todo: ReStrart ve Start'ta hata var düzelt onu
        }
    }
}
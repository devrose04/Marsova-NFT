using UnityEditor.SceneManagement;
using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons.OpenButton
{
    public class OpenNickName : MonoBehaviour
    {
        [SerializeField] private StartButton _startButtonScript;
        [SerializeField] private GameObject NickNameMenu;
        [SerializeField] private GameObject MainMenu;

        public void StarButtonNickName()
        {
            if (PlayerPrefs.HasKey("NickName") == true)
            {
                _startButtonScript.StartGame();
            }
            else
            {
                MainMenu.SetActive(false);
                NickNameMenu.SetActive(true);
            }
        }
    }
}
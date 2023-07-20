using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class CloseLeaderBoardMain : MonoBehaviour
    {
        [SerializeField] private GameObject LeaderBoard;
        [SerializeField] private GameObject MainMenu;
        [SerializeField] private GameObject SettingMenu;

        [SerializeField] private StartButton _startButton;

        void _CloseLeaderBoardMain()
        {
            if (_startButton.isGameStart == false)
            {
                LeaderBoard.SetActive(false);
                MainMenu.SetActive(true);
            }
        }

        void _CloseLeaderBoardSetting()
        {
            if (_startButton.isGameStart == true)
            {
                LeaderBoard.SetActive(false);
                SettingMenu.SetActive(true);
            }
        }


        public void CloseLeaderBoard()
        {
            _CloseLeaderBoardSetting();
            _CloseLeaderBoardMain();
        }
    }
}
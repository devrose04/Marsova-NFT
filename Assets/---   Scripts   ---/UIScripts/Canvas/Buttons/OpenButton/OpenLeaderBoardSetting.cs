using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class OpenLeaderBoardSetting : MonoBehaviour
    {
        [SerializeField] private GameObject SettingMenu;
        [SerializeField] private GameObject LeaderBoard;

        public void OpenLeaderBoard()
        {
            SettingMenu.SetActive(false);
            LeaderBoard.SetActive(true);
        }
    }
}

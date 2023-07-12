using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons.CloseButton
{
    public class CloseLeaderBoardSetting : MonoBehaviour
    {
        [SerializeField] private GameObject LeaderBoardSetting;
        [SerializeField] private GameObject SettingMenu;

        public void CloseLeaderBoard()
        {
            LeaderBoardSetting.SetActive(false);
            SettingMenu.SetActive(true);
        }
    }
}
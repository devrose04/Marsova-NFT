using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class CloseLeaderBoardMain : MonoBehaviour
    {
        [SerializeField] private GameObject LeaderBoardMain;
        [SerializeField] private GameObject MainMenu;

        public void CloseLeaderBoard()
        {
            LeaderBoardMain.SetActive(false);
            MainMenu.SetActive(true);
        }
    }
}
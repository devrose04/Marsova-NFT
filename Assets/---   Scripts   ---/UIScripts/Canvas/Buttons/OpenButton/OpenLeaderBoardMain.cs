using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class OpenLeaderBoardMain : MonoBehaviour
    {
        [SerializeField] private GameObject MainMenu;
        [SerializeField] private GameObject LeaderBoard;

        public void OpenLeaderBoard()
        {
            MainMenu.SetActive(false);
            LeaderBoard.SetActive(true);
        }
    }
}

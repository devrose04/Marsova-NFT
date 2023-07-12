using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class CloseDifficultyMenu : MonoBehaviour
    {
        [SerializeField] private GameObject difficultyMenu;
        [SerializeField] private GameObject mainMenu;

        public void CloseMenu()
        {
            difficultyMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}
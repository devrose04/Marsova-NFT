using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class DifficultyButton : MonoBehaviour
    {
        [SerializeField] private GameObject difficultyButton;
        [SerializeField] private GameObject MainMenu;

        public void OppenDiffucltyMenu()
        {
            difficultyButton.SetActive(true);
            MainMenu.SetActive(false);
        }
    }
}
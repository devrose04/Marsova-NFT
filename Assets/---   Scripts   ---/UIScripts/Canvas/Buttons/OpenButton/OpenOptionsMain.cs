using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class OpenOptionsMain : MonoBehaviour
    {
        [SerializeField] private GameObject MainMenu;
        [SerializeField] private GameObject OptionsMenu;

        public void OpenOptions()
        {
            MainMenu.SetActive(false);
            OptionsMenu.SetActive(true);
        }
    }
}
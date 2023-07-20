using UnityEngine;
using UnityEngine.Serialization;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class CloseAudioMenuMain : MonoBehaviour
    {
        [SerializeField] private GameObject MainMenu;
        [SerializeField] private GameObject OptionsMenu;

        public void CloseAudioMenu()
        {
            MainMenu.SetActive(true);
            OptionsMenu.SetActive(false);
        }
    }
}
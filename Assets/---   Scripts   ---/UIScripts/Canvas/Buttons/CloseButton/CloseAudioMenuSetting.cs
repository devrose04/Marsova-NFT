using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class CloseAudioMenuSetting : MonoBehaviour
    {
        [SerializeField] private GameObject SettingMenu;
        [SerializeField] private GameObject OptionsMenu;

        public void CloseAudioMenu()
        {
            SettingMenu.SetActive(true);
            OptionsMenu.SetActive(false);
        }
    }
}
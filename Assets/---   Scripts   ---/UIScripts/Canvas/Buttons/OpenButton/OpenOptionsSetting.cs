using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class OpenOptionsSetting : MonoBehaviour
    {
        [SerializeField] private GameObject SettingMenu;
        [SerializeField] private GameObject OptionsMenu;

        public void OpenOptions()
        {
            SettingMenu.SetActive(false);
            OptionsMenu.SetActive(true);
        }
    }
}
using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class DeadSettingButton : MonoBehaviour
    {
        [SerializeField] private GameObject deadSettingButton;
        [SerializeField] private GameObject GameOver;
        [SerializeField] private GameObject SettingButton;

        public void OpenDeadSettingButton()
        {
            deadSettingButton.SetActive(true);
            GameOver.SetActive(false);
            SettingButton.SetActive(true);
        }
    }
}
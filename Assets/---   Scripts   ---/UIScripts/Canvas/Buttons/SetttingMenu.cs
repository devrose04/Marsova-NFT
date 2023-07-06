using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class SetttingMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _settingMenu;

        public void OpenPauseMenu()
        {
            _settingMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
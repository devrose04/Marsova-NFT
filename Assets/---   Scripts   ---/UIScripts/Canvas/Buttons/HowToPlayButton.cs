using System;
using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class HowToPlayButton : MonoBehaviour
    {
        [SerializeField] private GameObject HowToPlayMenu;

        private int _PlayerPrefs;
        private bool isOpenMenu = false;

        private void Awake()
        {
            if (!PlayerPrefs.HasKey("ShowHowToPlayCount"))
                PlayerPrefs.SetInt("ShowHowToPlayCount", 1);

            _PlayerPrefs = PlayerPrefs.GetInt("ShowHowToPlayCount");
        }

        void OpenHowToPlayMenu()
        {
            HowToPlayMenu.SetActive(true);
            isOpenMenu = true;
        }

        void CloseHowToPlayMenu()
        {
            HowToPlayMenu.SetActive(false);
            isOpenMenu = false;
        }

        public void StartGameOpenHowToPlayMenu()
        {
            _PlayerPrefs++;
            PlayerPrefs.SetInt("ShowHowToPlayCount", _PlayerPrefs);

            if (_PlayerPrefs <= 5 && isOpenMenu == false)
            {
                OpenHowToPlayMenu();

                Invoke("CloseHowToPlayMenu", 15f);
            }
        }

        public void ClickButtonOpenHowToPlayMenu()
        {
            if (isOpenMenu == false)
            {
                OpenHowToPlayMenu();

                Invoke("CloseHowToPlayMenu", 30f);
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts.Player;
using UnityEngine;
using UnityEngine.Serialization;

public class CloseSettingMenu : MonoBehaviour
{
    [SerializeField] private GameObject SettingMenu;
    [SerializeField] private GameObject GameOver;
    [SerializeField] private PlayerScript _playerScript;

    public void _ClosePauseMenu()
    {
        SettingMenu.SetActive(false);
        Time.timeScale = 1;
        if (_playerScript.health <= 0)
        {
            GameOver.SetActive(true);
        }
    }
}
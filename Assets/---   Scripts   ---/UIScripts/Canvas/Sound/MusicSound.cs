using JetBrains.Annotations;
using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas
{
    public class MusicSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource1;
        [SerializeField] private AudioClip _audioClip1;

        [SerializeField] private AudioSource _audioSource2;
        [SerializeField] private AudioClip _audioClip2;

        [SerializeField] private GameObject SettingMenu;
        [SerializeField] private GameObject MainMenu;
        [SerializeField] private GameObject GameOver;
        [SerializeField] private GameObject DifficultyMenu;
        [SerializeField] private GameObject OptinsMenuSetting;
        [SerializeField] private GameObject OptinsMenuMain;

        public void MyUpdate()
        {
            if (SettingMenu.activeSelf == false && MainMenu.activeSelf == false && GameOver.activeSelf == false && DifficultyMenu.activeSelf == false && OptinsMenuMain.activeSelf == false && OptinsMenuSetting.activeSelf == false)
            {
                if (_audioSource2.isPlaying == false)
                {
                    _audioSource1.Stop();
                    _audioSource2.PlayOneShot(_audioClip2);
                }
            }
            else
            {
                if (_audioSource1.isPlaying == false)
                {
                    _audioSource2.Stop();
                    _audioSource1.PlayOneShot(_audioClip1);
                }
            }
        }
    }
}
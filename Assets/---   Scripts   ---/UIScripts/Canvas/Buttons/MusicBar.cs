using System;
using ______Scripts______.UIScripts.Canvas.Sound;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class MusicBar : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource1;
        [SerializeField] private AudioSource _audioSource2;

        [SerializeField] private Slider _MusicBarSetting;
        [SerializeField] private Slider _MusicBarMain;

        [SerializeField] private MusicDataManager musicDataManager;

        private void Start()
        {
            _MusicBarSetting.value = musicDataManager.volume;
            _MusicBarMain.value = musicDataManager.volume;
            
            _audioSource1.volume = musicDataManager.volume;
            _audioSource2.volume = musicDataManager.volume;
        }

        public void ChangeMusicSoundSetting()
        {
            musicDataManager.SetVolume(_MusicBarSetting.value);
            _audioSource1.volume = _MusicBarSetting.value;
            _audioSource2.volume = _MusicBarSetting.value;
        }

        public void ChangeMusicSoundMain()
        {
            musicDataManager.SetVolume(_MusicBarMain.value);
            _audioSource1.volume = _MusicBarMain.value;
            _audioSource2.volume = _MusicBarMain.value;
        }
    }
}
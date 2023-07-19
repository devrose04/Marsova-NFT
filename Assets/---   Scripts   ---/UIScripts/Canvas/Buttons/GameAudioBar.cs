using System;
using ______Scripts______.UIScripts.Canvas.Sound;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class GameAudioBar : MonoBehaviour
    {
        [SerializeField] public AudioSource[] audioSources;

        [SerializeField] public Slider GameAudioSliderSetting;
        [SerializeField] public Slider GameAudioSliderMain;

        [SerializeField] private SoundDataManager _soundDataManager;

        [SerializeField] private AudioSource DroneAuidoSource;

        private void Start()
        {
            GameAudioSliderMain.value = _soundDataManager.volume;
            GameAudioSliderSetting.value = _soundDataManager.volume;

            foreach (var audioSource in audioSources)
            {
                audioSource.volume = _soundDataManager.volume;
                ChaneDroneEllectricVolume(audioSource);
            }
        }

        public void ChangeVolumeSetting()
        {
            float volume = GameAudioSliderSetting.value;

            _soundDataManager.SetVolume(volume);
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume = volume;
                ChaneDroneEllectricVolume(audioSource);
            }
        }

        public void ChangeVolumeMain()
        {
            float volume = GameAudioSliderMain.value;

            _soundDataManager.SetVolume(volume);
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume = volume;
                ChaneDroneEllectricVolume(audioSource);
            }
        }

        void ChaneDroneEllectricVolume(AudioSource _audioSource) // this is fixed a bugg
        {
            if (DroneAuidoSource == _audioSource)
            {
                float _volume = _audioSource.volume;
                _audioSource.volume = _volume / 3;
            }
        }
    }
}
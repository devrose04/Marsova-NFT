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

        private void Start()
        {
            GameAudioSliderMain.value = _soundDataManager.volume;
            GameAudioSliderSetting.value = _soundDataManager.volume;

            foreach (var audioSource in audioSources)
                audioSource.volume = _soundDataManager.volume;
        }

        public void ChangeVolumeSetting()
        {
            float volume = GameAudioSliderSetting.value;

            _soundDataManager.SetVolume(volume);
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume = volume;
            }
        }

        public void ChangeVolumeMain()
        {
            float volume = GameAudioSliderMain.value;

            _soundDataManager.SetVolume(volume);
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume = volume;
            }
        }
    }
}
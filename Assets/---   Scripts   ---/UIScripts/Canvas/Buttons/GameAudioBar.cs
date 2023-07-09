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

        public void ChangeVolumeSetting()
        {
            float volume = GameAudioSliderSetting.value;

            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume = volume;
            }
        }

        public void ChangeVolumeMain()
        {
            float volume = GameAudioSliderMain.value;

            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume = volume;
            }
        }
    }
}
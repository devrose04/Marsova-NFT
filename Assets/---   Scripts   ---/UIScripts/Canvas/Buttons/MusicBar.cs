using UnityEngine;
using UnityEngine.UI;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class MusicBar : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource1;
        [SerializeField] private AudioSource _audioSource2;

        [SerializeField] private Slider _MusicBarSetting;
        [SerializeField] private Slider _MusicBarMain;

        public void ChangeMusicSoundSetting()
        {
            _audioSource1.volume = _MusicBarSetting.value;
            _audioSource2.volume = _MusicBarSetting.value;
        }

        public void ChangeMusicSoundMain()
        {
            _audioSource1.volume = _MusicBarMain.value;
            _audioSource2.volume = _MusicBarMain.value;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace ______Scripts______.UIScripts.Canvas.Sound
{
    public class SoundDataManager : MonoBehaviour
    {
        public float defaultVolume = 0.5f;
        public float volume;

        [SerializeField] private Slider _SoundBarSetting;
        [SerializeField] private Slider _SoundBarMain;

        private void Awake()
        {
            // Kaydedilen ses seviyesini kontrol etmek için PlayerPrefs'ten değeri oku
            if (PlayerPrefs.HasKey("SoundVolume"))
            {
                volume = PlayerPrefs.GetFloat("SoundVolume");
            }
            else
            {
                // Kaydedilen bir ses seviyesi yoksa varsayılan değeri kullan
                volume = defaultVolume;
            }


            // Ses seviyesini uygula
            ApplyVolume();
        }

        private void ApplyVolume()
        {
            // Ses seviyesini uygula (örneğin, AudioSource'lara atanabilir)
            Debug.Log("SoundVolume: " + volume);
        }

        public void SetVolume(float newVolume)
        {
            _SoundBarSetting.value = volume;
            _SoundBarMain.value = volume;

            // Yeni ses seviyesini kaydet
            volume = newVolume;
            PlayerPrefs.SetFloat("SoundVolume", volume);

            // Ses seviyesini uygula
            ApplyVolume();
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace ______Scripts______.UIScripts.Canvas.Sound
{
    public class MusicDataManager : MonoBehaviour
    {
        public float defaultVolume = 0.5f;
        public float volume;

        [SerializeField] private Slider _MusicBarSetting;
        [SerializeField] private Slider _MusicBarMain;

        private void Awake()
        {
            // Kaydedilen ses seviyesini kontrol etmek için PlayerPrefs'ten değeri oku
            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                volume = PlayerPrefs.GetFloat("MusicVolume");
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
            // Debug.Log("MusicVolume: " + volume);
        }

        public void SetVolume(float newVolume)
        {
            _MusicBarSetting.value = volume;
            _MusicBarMain.value = volume;

            // Yeni ses seviyesini kaydet
            volume = newVolume;
            PlayerPrefs.SetFloat("MusicVolume", volume);

            // Ses seviyesini uygula
            ApplyVolume();
        }
    }
}
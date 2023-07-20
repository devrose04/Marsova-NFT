using System;
using PlayerScripts.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ______Scripts______.UIScripts.Canvas.Buttons
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private GameObject MainMenu;
        [SerializeField] private GameObject Player;

        [SerializeField] private PlayerScript _playerScript;

        [SerializeField] private GameObject CrashPlayerVehicle;
        public bool isGameStart = false;

        [SerializeField] private GameObject _Text1;
        [SerializeField] private GameObject _Text2;

        private Rigidbody2D RB2;

        [SerializeField] private Transform CrashShip;
        [SerializeField] private Transform ExploisonHere;

        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _audioClipTeleport;
        [SerializeField] private AudioClip _audioClipFire;
        [SerializeField] private AudioClip _audioClipElectir;
        [SerializeField] private AudioClip _audioClipWind;
        [SerializeField] private AudioClip _audioClipWater;
        [SerializeField] private AudioClip _audioClipBlock;

        [SerializeField] private ParticleSystem _LongTileFire;
        [SerializeField] private ParticleSystem _BigExplosion;
        [SerializeField] private ParticleSystem _BigExplosion2;
        [SerializeField] private ParticleSystem _TileExplosion;
        [SerializeField] private ParticleSystem _Ellectric;
        [SerializeField] private ParticleSystem _electircBall;

        private void Awake()
        {
            RB2 = Player.GetComponent<Rigidbody2D>();

            // PlayerPrefs.DeleteKey("NickName"); // bug: bunu gerektiginde kullan
        }

        public void StartGame() // bunu OpenNickName.cs te çagırıyorum
        {
            CrashPlayerVehicle.SetActive(true);
            RB2.gravityScale = 1;
            _playerScript.totalScore = 0;
            MainMenu.SetActive(false);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Player.transform.position = new Vector3(6.5f, -3, 0);
            isGameStart = true;

            _audioSource.PlayOneShot(_audioClipTeleport);
            _audioSource.PlayOneShot(_audioClipElectir);

            Invoke("Explosion1", 1f);
            Invoke("Explosion5", 1.8f);
            Invoke("Explosion2", 3.2f);
            Invoke("Explosion2", 4.8f);

            Invoke("OpenText1", 5f); //text

            Invoke("Explosion3", 5f);
            Invoke("Explosion4", 6f);
            Invoke("Explosion3", 7f);
            Invoke("Explosion4", 8f);

            Invoke("Explosion6", 8.8f);

            Invoke("Explosion3", 10f);
            Invoke("Explosion4", 11.5f);
            Invoke("Explosion3", 13f);

            Invoke("CloseText1", 13.8f); // text
            Invoke("OpenText2", 14.6f); //text

            Invoke("Explosion6", 13.8f);

            Invoke("Explosion4", 15f);
            Invoke("Explosion3", 17f);
            Invoke("Explosion4", 19f);

            Invoke("Explosion6", 20.2f);

            Invoke("Explosion3", 22f);
            Invoke("Explosion4", 25f);

            Invoke("CloseText2", 25f); // text

            Invoke("Explosion3", 27f);
            Invoke("Explosion4", 29f);

            Invoke("Explosion3", 32f);
            Invoke("Explosion4", 34f);

            Invoke("Explosion3", 38f);
            Invoke("Explosion4", 40f);

            Invoke("Explosion3", 44f);
            Invoke("Explosion4", 47f);
        }

        public void StopPlayer()
        {
            CrashPlayerVehicle.SetActive(false);
            RB2.gravityScale = 0;
            RB2.velocity = new Vector2(0, 0);
        }

        void Explosion1()
        {
            _audioSource.PlayOneShot(_audioClipWind);

            ParticleSystem _particleSystem = Instantiate(_BigExplosion2, CrashShip.position, Quaternion.identity);
            Destroy(_particleSystem.gameObject, 5f);
        }

        void Explosion2()
        {
            _audioSource.PlayOneShot(_audioClipBlock);
            ParticleSystem _particleSystem = Instantiate(_LongTileFire, ExploisonHere.position, Quaternion.identity);
            Destroy(_particleSystem.gameObject, 5f);
        }

        void Explosion3()
        {
            ParticleSystem _particleSystem = Instantiate(_Ellectric, ExploisonHere.position, Quaternion.identity);
            Destroy(_particleSystem.gameObject, 3f);
        }

        void Explosion4()
        {
            ParticleSystem _particleSystem = Instantiate(_electircBall, CrashShip.position, Quaternion.identity);
            Destroy(_particleSystem.gameObject, 3f);
        }

        void Explosion5()
        {
            _audioSource.PlayOneShot(_audioClipWater);

            ParticleSystem _particleSystem = Instantiate(_BigExplosion, CrashShip.position, Quaternion.identity);
            Destroy(_particleSystem.gameObject, 5f);
        }

        void Explosion6()
        {
            _audioSource.PlayOneShot(_audioClipFire);

            ParticleSystem _particleSystem = Instantiate(_TileExplosion, CrashShip.position, Quaternion.identity);
            Destroy(_particleSystem.gameObject, 3f);
        }

        void OpenText1()
        {
            _Text1.SetActive(true);
        }

        void OpenText2()
        {
            _Text2.SetActive(true);
        }

        void CloseText1()
        {
            _Text1.SetActive(false);
        }

        void CloseText2()
        {
            _Text2.SetActive(false);
        }
    }
}
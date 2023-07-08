using ______Scripts______.EnemyScripts.Enemy.Enemy;
using EnemyScripts.Enemy;
using EnemyScripts.Enemy.SuportSkills_Ahtapot;
using ObjectsScripts;
using UnityEngine;

namespace PlayerScripts.PlayerLaserAbout
{
    public class PlayerBullet : MonoBehaviour
    {
        private GameObject Bullet;
        private GameObject Player;
        private Rigidbody2D RB2;
        private Transform ShotPoint;
        private GameObject StarShip;

        private EnemyScript __EnemyScript;
        private Calculations _calculations;
        [SerializeField] private ParticleSystem LaserHitEffeckt;

        private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClipTocuhEnemy;
        [SerializeField] private AudioClip _audioClipTocuhShield;

        private float damages;

        private void Awake()
        {
            Player = GameObject.Find("Player");
            StarShip = GameObject.Find("StarShip");
            Bullet = this.gameObject;
            RB2 = Bullet.GetComponent<Rigidbody2D>();
            damages = Random.Range(8f, 12f);
            _calculations = Player.GetComponent<Calculations>();

            _audioSource = Player.GetComponent<AudioSource>();
            ShotPoint = StarShip.transform.Find("shotPoint");

            FiredBullet();
        }

        public void BulletIsTouchTheEnemy(GameObject enemy)
        {
            _audioSource.PlayOneShot(_audioClipTocuhEnemy);

            Collider2D _enemy = enemy.GetComponent<Collider2D>();
            var result = _calculations.CalculationsAboutToObject(this.gameObject, _enemy); // Enemy'lerin vuruş yaptıgı yerin verileri hesaplanır.
            Vector2 directionToEnemy = result.Item1;

            ParticleSystem CreatedHitEffect = Instantiate(LaserHitEffeckt, _enemy.transform.position, Quaternion.identity); // hitEffeckti oluşturduk
            Destroy(CreatedHitEffect.gameObject, 5f);

            __EnemyScript = _enemy.GetComponent<EnemyScript>();

            __EnemyScript.TakeDamages(damages, directionToEnemy / 4, false);
            Destroy(Bullet);
        }

        void FiredBullet() // burda Mermi hangi yöne bakıyorsa o tarafa dogru ateşleniyor.
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Bullet.transform.right = mousePosition - (Vector2)Bullet.transform.position;
            RB2.AddForce(ShotPoint.right * 30f, ForceMode2D.Impulse);
        }

        private void OnTriggerStay2D(Collider2D other) // Mermi Ground'a çarpar ise yok olur
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                Destroy(Bullet);
            }

            if (other.gameObject.CompareTag("Shield"))
            {
                _audioSource.PlayOneShot(_audioClipTocuhShield);

                SuportShield Script = other.GetComponent<SuportShield>();
                Script.TakeDamagesShield(damages);
                Script.CreateHitEffect(Bullet);

                Destroy(Bullet);
            }
        }
    }
}
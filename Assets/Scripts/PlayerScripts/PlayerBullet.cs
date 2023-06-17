using EnemyScripts.Enemy;
using ObjectsScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerScripts
{
    public class PlayerBullet : MonoBehaviour
    {
        private GameObject Bullet;
        private GameObject Player;
        private Rigidbody2D RB2;
        private Transform ShotPoint;
        private GameObject Weapon;

        private EnemyScript __EnemyScript;
        private Calculations _calculations;
        [SerializeField] private ParticleSystem LaserHitEffeckt;

        private float damages;

        private void Awake()
        {
            Player = GameObject.Find("Player");
            Weapon = GameObject.Find("Weapon");
            Bullet = this.gameObject;
            RB2 = Bullet.GetComponent<Rigidbody2D>();
            damages = Random.Range(8f, 12f);
            _calculations = Player.GetComponent<Calculations>();

            ShotPoint = Weapon.transform.Find("shotPoint");

            FiredBullet();
        }

        public void BulletIsTouchTheEnemy(GameObject enemy)
        {
            Collider2D _enemy = enemy.GetComponent<Collider2D>();
            var result = _calculations.CalculationsAboutToObject(this.gameObject, _enemy); // Enemy'lerin vuruş yaptıgı yerin verileri hesaplanır.
            Vector2 directionToEnemy = result.Item1;

            ParticleSystem CreatedHitEffect = Instantiate(LaserHitEffeckt, _enemy.transform); // hitEffeckti oluşturduk
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
        }
    }
}
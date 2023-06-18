using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerScripts.PlayerLaserAbout.Drone
{
    public class DroneScript : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform; // Takip edilecek karakterin Transform bile�eni

        private GameObject Drone;
        private Rigidbody2D RB2;
        private GameObject Player;

        private float distance;
        private Vector2 direction;

        private float DroneSkilCD = 10;
        private float DroneCDTimer = 10; // 30 verme nedenim oyun başladıgı gibi, skili kullanabiliyor olsun diye

        private float ActiveDroneTime = 6.5f;
        public float DroneStartAttackTimer = 0;

        private EnemyDetector _enemyDetector;

        public GameObject DroneLookThisObject;

        private bool DroneSkillIsReadToUse = false;
        public bool EnemiesAreThere = false;

        private void Awake()
        {
            Drone = this.gameObject;
            RB2 = Drone.GetComponent<Rigidbody2D>();
            _enemyDetector = Drone.GetComponent<EnemyDetector>();
            Player = GameObject.Find("Player");
            DroneLookThisObject = Player;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void MYUpdate()
        {
            if (DroneSkillIsReadToUse == true)
            {
                LookingTheEnemy();
                GoPlayerPossition();
            }

            DroneSkilCDFilling();
        }

        void LookingTheEnemy()
        {
            if (DroneLookThisObject == null)
            {
                DroneLookThisObject = Player;
            }

            Vector2 difference = DroneLookThisObject.transform.position - transform.position;

            if (difference.x > 0)
                this.GetComponent<SpriteRenderer>().flipY = false;
            else
                this.GetComponent<SpriteRenderer>().flipY = true;

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

            // this.gameObject.transform.position = new Vector2(playerTransform.position.x + 2, playerTransform.position.y + 3);
        }

        void GoPlayerPossition()
        {
            Calculations();

            if (distance < 4)
            {
                _enemyDetector.DroneIsReadyToAttack = true;
                RB2.velocity = new Vector2(direction.x * 3, 0); // Drone Player'a yakınsa yavaşça yanında harekt ediyor

                DroneSeeEnemyAndAttackHim();
            }
            else
            {
                RB2.velocity = new Vector2(direction.x * 5, direction.y * 5); // Drone Player'ın yanına geliyor
            }

            // RB2.AddForce(new Vector2(direction.x, RB2.velocity.y), ForceMode2D.Impulse); // direction.x 1 veya -1 dir
        }

        void DroneSkilCDFilling()
        {
            // print(" CD Timer: " + DroneCDTimer);

            if (DroneCDTimer < DroneSkilCD && DroneSkillIsReadToUse == false) // bu skilin dolum süresini ayarlicak
                DroneCDTimer += Time.deltaTime;
            else if (DroneCDTimer >= DroneSkilCD) // 30 sn ye geçtikten sonar skil kullanmaya hazır.
                DroneSkillIsReadToUse = true;
        }

        void GoToSky()
        {
            _enemyDetector.DroneIsReadyToAttack = false; // Silahları çalışmasın
            DroneSkillIsReadToUse = false; // skil artık kullılabilir degil
            DroneCDTimer = 0; // Skilin dolum süresi sayaçı çalışmaya başlar.
            DroneStartAttackTimer = 0;
            _enemyDetector.JustOneTimeWork = 0;
            _enemyDetector.ClearList();

            // print("DİSTANCE: " + distance);
            if (distance < 10)
                RB2.velocity = new Vector2(5, 5); // Drone Gökyüzüne dogru gidiyor
        }

        void DroneSeeEnemyAndAttackHim()
        {
            if (EnemiesAreThere == true)
                DroneStartAttackTimer += Time.deltaTime;

            if (DroneStartAttackTimer >= ActiveDroneTime) // 5 saniye Player'ın yanında durduktan sonra gitsin.
                GoToSky();
        }

        void Calculations()
        {
            direction = (playerTransform.position - Drone.transform.position).normalized; // Player Enemy'nin hangi tarafında onu hesaplar
            distance = Vector2.Distance(playerTransform.position, transform.position); // Player ile Drone arasoındaki mesafeyi ölçer
        }
    }
}
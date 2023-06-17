using UnityEngine;

namespace PlayerScripts.PlayerLaserAbout.Drone
{
    public class DroneScript : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform; // Takip edilecek karakterin Transform bile�eni

        private GameObject Drone;
        private Rigidbody2D RB2;

        private float distance;
        private Vector2 direction;
        
        private void Awake()
        {
            Drone = this.gameObject;
            RB2 = Drone.GetComponent<Rigidbody2D>();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void MYUpdate()
        {
            LookingTheEnemy();
            GoPlayerPossition();
        }

        void LookingTheEnemy()
        {
            Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // todo: Camera.main yerine en yakın Eneym'nin pozisyonu olucak.

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
            direction = (playerTransform.position - Drone.transform.position).normalized; // Player Enemy'nin hangi tarafında onu hesaplar
            distance = Vector2.Distance(playerTransform.position, transform.position); // Player ile Drone arasoındaki mesafeyi ölçer

            if (distance < 4)
            {
                RB2.velocity = new Vector2(direction.x * 3, 0); // Drone Player'a yakınsa yavaşça yanında harekt ediyor
                // Time'ı burda başlat
            }
            else
            {
                RB2.velocity = new Vector2(direction.x * 5, direction.y * 5);   // Drone Player'ın yanına geliyor
            }

            // RB2.AddForce(new Vector2(direction.x, RB2.velocity.y), ForceMode2D.Impulse); // direction.x 1 veya -1 dir
        }

        void GoToSky()
        {
            // bu Skilin süresi bitince çalışsın.
        }
    }
}
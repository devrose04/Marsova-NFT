using ______Scripts______.UIScripts.Canvas;
using UnityEngine;

namespace ______Scripts______.PlayerScripts.PlayerLaserAbout
{
    public class StartShipAttack : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform; // Takip edilecek karakterin Transform bile�eni

        private GameObject Ship;
        private Rigidbody2D RB2;

        private float distance;
        private Vector2 direction;

        public float ShipReloadTimeCD = 20;
        public float ShipReloadTheBulletTimer = 20; // oyun başladıgında skilli direk kulana bilsin diye 20 verdim.
        public bool SpaceShipAttackIsActive = true;

        public int amountOfBullets = 30;
        public int extraAmountOfBullets = 0;

        public int totalBullets;

        private StarShipBar _starShipBar;
        private StartShipBulletAmount _startShipBulletAmount;

        public float extraBullletDamges = 10; // this for UpdateChoce

        private void Awake()
        {
            Ship = this.gameObject;
            RB2 = Ship.GetComponent<Rigidbody2D>();
            _starShipBar = Ship.GetComponent<StarShipBar>();
            _startShipBulletAmount = Ship.GetComponent<StartShipBulletAmount>();

            totalBullets = amountOfBullets + extraAmountOfBullets;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void MYUpdate()
        {
            LookingTheMousePosition();
            WalkingTheSky();
            ShipBulletReload();
        }

        void LookingTheMousePosition()
        {
            Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (difference.x > 0)
                this.GetComponent<SpriteRenderer>().flipY = false;
            else
                this.GetComponent<SpriteRenderer>().flipY = true;

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

            // this.gameObject.transform.position = new Vector2(playerTransform.position.x + 2, playerTransform.position.y + 3);
        }

        void Calculation()
        {
            // RB2.velocity = new Vector2(playerTransform.position.x, playerTransform.position.y + 40);
        }

        int count = 0;

        void WalkingTheSky()
        {
            direction = (playerTransform.position - Ship.transform.position); // Player Enemy'nin hangi tarafında onu hesaplar
            distance = Vector2.Distance(playerTransform.position, transform.position); // Player ile Drone arasoındaki mesafeyi ölçer

            // direction = direction.normalized;

            // float power = 1f;
            // if (RB2.velocity.magnitude < 50) // çok fazla dışa savrulmasını engelliyor
            //     power = 0.2f;


            if ((Ship.transform.localPosition.x <= -40 || Ship.transform.localPosition.x >= 40) && count == 1)
            {
                RB2.velocity = new Vector2(0, 0);
                count = 0;
            }
            else if ((Ship.transform.localPosition.x > -40 || Ship.transform.localPosition.x < 40) && count == 0)
                count = 1;

            RB2.AddForce(new Vector2(direction.x, RB2.velocity.y), ForceMode2D.Impulse); // direction.x 1 veya -1 dir
        }

        void ShipBulletReload()
        {
            _starShipBar.StartShipFonk();
            _startShipBulletAmount.StartShipBulletFonk();

            if (amountOfBullets == 29 + extraAmountOfBullets) // bu başta, ShipReloadTheBulletTimer'a 20 verdigimiz için oluşturulmuş bir if tir.
            {
                ShipReloadTheBulletTimer = 0;
            }

            if (ShipReloadTheBulletTimer > ShipReloadTimeCD) // mermileri 20 sn de bir fullüyor
            {
                SpaceShipAttackIsActive = true;
                amountOfBullets = 30 + extraAmountOfBullets;
            }
            else if (amountOfBullets != 30 + extraAmountOfBullets)
                ShipReloadTheBulletTimer += Time.deltaTime;


            if (amountOfBullets == 0) // mermi 0 ise ateş edemesin.
                SpaceShipAttackIsActive = false;
        }
    }
}
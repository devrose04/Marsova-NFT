using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace ______Scripts______.UIScripts
{
    public class MeteorMove : MonoBehaviour
    {
        // mantıgı şu: eger RotationPower yüksek geldi ise hızlı gitsin.
        private GameObject Meteor;
        private Rigidbody2D RB2;

        [SerializeField] private Transform SpawnPosition; // spawn yapıcak yer
        [SerializeField] private Transform StartGamePosition;
        [SerializeField] private Transform EndPosition;

        private float Start_Z_Rotation; // kaç derece dönük şekilde başlayacagını ayarlar
        private float Start_Y_Position; // en sağda Y ekseninde başladıgı yer
        private float Start_X_Position; // oyun başladıgında rasgele 1 kere bir yerde spawn eder.
        private float MovingDirection_y; // 3'ü geçmemesi lazım yukarı veya aşagı gitmesine yarıyacak
        private float RotationPower; // kendi kendine dönme hızı
        private float MovingPower_x; // bu itme gücü
        private int RotateDirection; // %50 ihtimalle ters yönde dönmesini sağlar

        private void Awake()
        {
            Meteor = this.gameObject;
            RB2 = Meteor.GetComponent<Rigidbody2D>();

            RandomCounts();
        }

        private void Start()
        {
            AwakeMeteor();

            StartGameSpawn_x_position();
            InvokeRepeating("ReSpawnMeteors", 15f, 2f);
        }

        void RandomCounts()
        {
            Start_Z_Rotation = Random.Range(0f, 360f);
            Start_Y_Position = Random.Range(-12f, 12f); // eger yukarda başladı ise y'i neğatif ver
            Start_X_Position = Random.Range(-24f, 8);
            RotationPower = Random.Range(6f, 16f); // bu ne kadar hızlı ise MovingPower_x de hızlı olacak  
            RotateDirection = Random.Range(1, 3);
        }

        public void AwakeMeteor()
        {
            Which_y_DirectionGo(); // MovingPower_y = Random.Range(-3f, 3f);
            x_Moving_Power(); // MovingPower_x = Random.Range(10f, 45f);

            MeteorMoveing();
            MeteorRotaion();
        }

        void Which_y_DirectionGo() // eger yukarıda başladı ise MovingDirection_y negatif alması lazımki aşagı gidebilsin. vs. vs.
        {
            if (10f < Start_Y_Position)
                MovingDirection_y = Random.Range(-3f, -2.2f);
            else if (8f < Start_Y_Position)
                MovingDirection_y = Random.Range(-2.2f, -1.4f);
            else if (6f < Start_Y_Position)
                MovingDirection_y = Random.Range(-1.4f, -0.6f);
            else if (4f < Start_Y_Position)
                MovingDirection_y = Random.Range(-1f, 1f);
            else if (0f < Start_Y_Position)
                MovingDirection_y = Random.Range(-1.5f, 1.5f);
            else if (-4f < Start_Y_Position)
                MovingDirection_y = Random.Range(-1f, 1f);
            else if (-6f < Start_Y_Position)
                MovingDirection_y = Random.Range(1.4f, 0.6f);
            else if (-8f < Start_Y_Position)
                MovingDirection_y = Random.Range(2.2f, 1.4f);
            else if (-10f < Start_Y_Position)
                MovingDirection_y = Random.Range(3f, 2.2f);
            else if (-12f <= Start_Y_Position)
                MovingDirection_y = Random.Range(3f, 2.2f);
            else // bu else'i belki bir hata verir ise yazdım 
                MovingDirection_y = Random.Range(-3f, 3f);

            MovingDirection_y = MovingDirection_y / 10;
        }

        void x_Moving_Power() // eger hızlı dönüyor ise hızlı hareket ediyor demektir, onu sağlıyor.
        {
            float Böl = 40;

            if (14f < RotationPower)
                MovingPower_x = Random.Range(39f, 45f) / Böl;
            else if (12 < RotationPower)
                MovingPower_x = Random.Range(33f, 39f) / Böl;
            else if (10 < RotationPower)
                MovingPower_x = Random.Range(27f, 33f) / Böl;
            else if (8 < RotationPower)
                MovingPower_x = Random.Range(21f, 27f) / Böl;
            else if (6 <= RotationPower)
                MovingPower_x = Random.Range(15f, 21f) / Böl;
            else // bu else'i belki bir hata verir ise yazdım
                MovingPower_x = Random.Range(10f, 45) / Böl;
        }

        void MeteorMoveing()
        {
            RB2.velocity = new Vector2(MovingPower_x, MovingDirection_y); // meteora hız verir
            Meteor.transform.rotation = Quaternion.Euler(0f, 0f, Start_Z_Rotation); // metoranun hangi dönük bozüsyonda başlayacagı ayarlarnır
            Meteor.transform.position += new Vector3(0f, Start_Y_Position); // meteor hangi y pozisyonda başlanacagı ayarlanır
        }

        void MeteorRotaion()
        {
            float RotationPowerLess = RotationPower;
            // RotationPowerLess = RotationPowerLess;

            if (RotateDirection == 1) // %50 ihtimalle - ile çarpar, buda farklı yönde dönmesini sağlar
                RotationPowerLess = -RotationPowerLess;

            RB2.AddTorque(RotationPowerLess);
        }

        void StartGameSpawn_x_position() // bunu sadece oyun başladıgında kullan.
        {
            Meteor.transform.position = new Vector3(StartGamePosition.position.x + Start_X_Position, Start_Y_Position);
        }

        void StartSpawn_x_position() // bunu Meteor en sağa gittiginde, solda spawn olmasına sağlıyacak
        {
            Meteor.transform.position = new Vector3(SpawnPosition.position.x, Start_Y_Position);
        }

        public void ReSpawnMeteors()
        {
            if (EndPosition.position.x < Meteor.transform.position.x)
            {
                RandomCounts();
                AwakeMeteor();
                StartSpawn_x_position();
            }
        }
    }
}
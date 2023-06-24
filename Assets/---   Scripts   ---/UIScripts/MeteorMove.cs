using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ______Scripts______.UIScripts
{
    public class MeteorMove : MonoBehaviour
    {
        // mantıgı şu: eger RotationPower yüksek geldi ise hızlı gitsin.
        private GameObject Meteor;

        [SerializeField] private GameObject StartPositionGameObject; // spawn yapıcak yer
        [SerializeField] private GameObject EndPositionGameObject; // yok olucak yer

        private float Start_Z_Rotation; // kaç derece dönük şekilde başlayacagını ayarlar
        private float Start_Y_Position; // en sağda Y ekseninde başladıgı yer
        private float MovingPower_y; // 3'ü geçmemesi lazım yukarı veya aşagı gitmesine yarıyacak
        private float RotationPower; // kendi kendine dönme hızı
        private float MovingPower_x; // bu itme gücü

        private void Awake()
        {
            Meteor = this.gameObject;
            Start_Z_Rotation = Random.Range(0f, 360f);
            Start_Y_Position = Random.Range(-5f, 5f); // eger yukarda başladı ise y'i neğatif ver

            MovingPower_y = Random.Range(-3f, 3f);

            RotationPower = Random.Range(2f, 16f); // bu ne kadar hızlı ise MovingPower_x de hızlı olacak  
            MovingPower_x = Random.Range(10f, 45f);
        }

        // void Which_y_DirectionGo() // todo: burda kaldın
        // {
        //     if (3f < Start_Y_Position && Start_Y_Position <= 5f)
        //     {
        //         MovingPower_y = Random.Range(-3f, -1f);
        //     }
        //     else if (0 < Start_Y_Position && Start_Y_Position <= 3)
        //     {
        //         MovingPower_y =   
        //     }
        //     else if (expr)
        //     {
        //         
        //     }
        // }
        //

        //  Meteordun Rototion Z'si random bir sayıda başlasın,  ve Rasgele degerler ile dönsün
        //  Rasgele bir hızla ilerlesin. Rasgele bir yöne dogru ilerlsein,
        //  ilerledigi yer hep sağdan sola olsun ve belirli dar bir açıda ilersein

        //  2 tane farklı GameObesi oluştur, biri objenin oluşcagı yeri ayarlasın digeri, oraya degdiginde yok olsun
        //  spwan yapıcak nesen sabit dursun
    }
}
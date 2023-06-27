using System;
using UnityEngine;

namespace UIScripts
{
    public class CharracterFollow : MonoBehaviour
    {
        // [SerializeField] private float smoothSpeed;
        // [SerializeField] private Transform target; // Takip edilecek nesne
        // [SerializeField] private Vector3 offset; // Kamera ve karakter arasındaki mesafe
        //
        // private Vector3 initialPosition; // Başlangıç pozisyonu
        //
        // private bool isInitialPositionSet = false; // Başlangıç pozisyonunun ayarlandığı kontrol flag'i
        //
        // void Start()
        // {
        //     initialPosition = transform.position; // Başlangıç pozisyonunu kaydet
        // }

        // void LateUpdate()
        // {
        //     Vector3 desiredPosition;
        //
        //     switch (isInitialPositionSet)
        //     {
        //         case true:
        //             // Başlangıç pozisyonuna yavaşça geçiş yap
        //             desiredPosition = initialPosition;
        //             transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        //
        //             // Başlangıç pozisyonuna yakın bir konuma geldiğinde flag'i true yap
        //             if (Vector3.Distance(transform.position, initialPosition) <= 0.05f)
        //             {
        //                 transform.position = initialPosition; // Tam olarak başlangıç pozisyonuna yerleştir
        //                 isInitialPositionSet = true;
        //             }
        //
        //             break;
        //
        //         case false:
        //             desiredPosition = target.position + offset;
        //             Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        //             transform.position = smoothedPosition; // Kamera pozisyonu güncellenir
        //             break;
        //     }
        // }

        public Transform player; // Oyuncu objesinin referansı
        private float smoothSpeed = 0.7f; // Kamera takip yumuşaklığı
        public Vector3 offset; // Kamera ile oyuncu arasındaki mesafe

        private Vector3 velocity = Vector3.zero;

        private Rigidbody2D RB2;

        private void Awake()
        {
            player = GameObject.Find("Player").transform;
            RB2 = player.GetComponent<Rigidbody2D>();
        }

        private void LateUpdate()
        {
            float magnitude = RB2.velocity.magnitude;

            if (magnitude < 7)
                smoothSpeed = 0.7f;
            else if (magnitude < 8)
                smoothSpeed = 0.5f;
            else if (magnitude < 9)
                smoothSpeed = 0.4f;
            else if (magnitude < 10)
                smoothSpeed = 0.3f;
            else if (magnitude < 11)
                smoothSpeed = 0.2f;
            else if (magnitude < 12)
                smoothSpeed = 0.1f;


            // Fare imleci pozisyonunu al
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Fare imlecinin x ve y pozisyonları arasındaki farkları al
            float deltaX = mousePosition.x - player.position.x;
            float deltaY = mousePosition.y - player.position.y;

            // Oyuncunun pozisyonunu alarak kamerayı takip etme
            Vector3 desiredPosition = player.position + offset;

            // Fare imlecinin x ve y pozisyonlarına göre kamerayı kaydırma
            desiredPosition.x += Mathf.Sign(deltaX) * 1.8f; // İstenilen kayma miktarını belirleyebilirsiniz
            desiredPosition.y += Mathf.Sign(deltaY) * 0.8f; // İstenilen kayma miktarını belirleyebilirsiniz

            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
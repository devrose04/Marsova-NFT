using UnityEngine;

namespace UIScripts
{
    public class CharracterFollow : MonoBehaviour
    {
        [SerializeField] private Transform target; // Takip edilecek nesne
        [SerializeField]private float smoothSpeed;
        [SerializeField]private Vector3 offset; // Kamera ve karakter arasındaki mesafe
    
        private Vector3 initialPosition; // Başlangıç pozisyonu
        private bool isInitialPositionSet = false; // Başlangıç pozisyonunun ayarlandığı kontrol flag'i

        void Start()
        {
            initialPosition = transform.position; // Başlangıç pozisyonunu kaydet
        }

        void LateUpdate()
        {
            Vector3 desiredPosition;

            if (!isInitialPositionSet)
            {
                // Başlangıç pozisyonuna yavaşça geçiş yap
                desiredPosition = initialPosition;
                transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

                // Başlangıç pozisyonuna yakın bir konuma geldiğinde flag'i true yap
                if (Vector3.Distance(transform.position, initialPosition) <= 0.05f)
                {
                    transform.position = initialPosition; // Tam olarak başlangıç pozisyonuna yerleştir
                    isInitialPositionSet = true;
                }
            }
            else
            {
                desiredPosition = target.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                transform.position = smoothedPosition; // Kamera pozisyonu güncellenir
            }
        }
    }
}
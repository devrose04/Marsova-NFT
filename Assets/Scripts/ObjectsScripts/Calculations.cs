using UnityEngine;

namespace ObjectsScripts
{
    public class Calculations : MonoBehaviour
    {
        public (Vector2, float) CalculationsAboutToEnemy(Collider2D enemy) // temel olarak açı verisini ve clampedDirectionToEnemy şeyleri hesaplar.
        {
            // Düşmanın pozisyonunu al
            Vector2 enemyPosition = enemy.transform.position;

            // 1-) Düşmanın pozisyonunun hangi yönde oldugunu buluyor.
            Vector2 approximatelyDirectionToEnemy = (enemyPosition - (Vector2)transform.position).normalized; // 1 , -1 arasında bir değer (x,y)

            // 2-) aşagı da  X ve Y bileşenlerini yuvarlayarak tam sayı değerlere dönüştürüyoruz (ya 1 ayda -1 olacak şekilde)

            approximatelyDirectionToEnemy.y = 0;

            if (approximatelyDirectionToEnemy.x < 0)
                approximatelyDirectionToEnemy.x = -1;
            else
                approximatelyDirectionToEnemy.x = 1;


            Vector2 roundedDirectionToEnemy = new Vector2((approximatelyDirectionToEnemy.x), (approximatelyDirectionToEnemy.y));

            // 3-) 1 veya -1 değerlerini almak için vektörün uzunluğunu 1'e sınırlıyoruz   // ya 1 veya -1 alır başka bişi almaz.
            Vector2 directionToEnemy = Vector2.ClampMagnitude(roundedDirectionToEnemy, 1f); // bunu KnocBack uygularken ya tam sağa yada tam sola dogru uygulamak için oluşturduk.

            // Player'ın yönünü al
            Vector2 playerDirection = transform.right; // 1 veya -1

            // Aradaki açıyı hesapla
            float angle = Vector2.Angle(playerDirection, approximatelyDirectionToEnemy);

            return (directionToEnemy, angle);
        }
    }
}
using UnityEngine;

namespace ObjectsScripts
{
    public class Calculations : MonoBehaviour
    {
        public (Vector2, float) CalculationsAboutToObject(GameObject thisAttackingObject, Collider2D target) // temel olarak açı verisini ve clampedDirectionToEnemy şeyleri hesaplar.
        {
            // Düşmanın pozisyonunu al
            Vector2 targetPosition = target.transform.position;
            Vector2 attackingPosition = thisAttackingObject.transform.position;

            // 1-) Düşmanın pozisyonunun hangi yönde oldugunu buluyor.   
            Vector2 approximatelyDirectionToTarget = (targetPosition - attackingPosition).normalized; // 1 , -1 arasında bir değer (x,y)

            // 2-) aşagı da  X ve Y bileşenlerini yuvarlayarak tam sayı değerlere dönüştürüyoruz (ya 1 ayda -1 olacak şekilde)

            approximatelyDirectionToTarget.y = 0;

            if (approximatelyDirectionToTarget.x < 0)
                approximatelyDirectionToTarget.x = -1;
            else
                approximatelyDirectionToTarget.x = 1;


            Vector2 roundedDirectionToTarget = new Vector2((approximatelyDirectionToTarget.x), (approximatelyDirectionToTarget.y));

            // 3-) 1 veya -1 değerlerini almak için vektörün uzunluğunu 1'e sınırlıyoruz   // ya 1 veya -1 alır başka bişi almaz.
            Vector2 directionToTarget = Vector2.ClampMagnitude(roundedDirectionToTarget, 1f); // bunu KnocBack uygularken ya tam sağa yada tam sola dogru uygulamak için oluşturduk.

            // Saldıran Objen'nin yönünü al
            Vector2 attackingDirection = thisAttackingObject.transform.right; // 1 veya -1

            // Aradaki açıyı hesapla
            float attackAngle = Vector2.Angle(attackingDirection, approximatelyDirectionToTarget);

            // print("Calculation içi: " + directionToTarget);

            return (directionToTarget, attackAngle);
        }
    }
}
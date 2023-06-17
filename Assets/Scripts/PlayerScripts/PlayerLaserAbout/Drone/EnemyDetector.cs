using System.Collections.Generic;
using EnemyScripts.Enemy;
using UnityEngine;

namespace PlayerScripts.PlayerLaserAbout.Drone
{
    public class EnemyDetector : MonoBehaviour
    {
        public float detectionRadius = 8; //Canvastan değiştir 
        private List<GameObject> detectedEnemies = new List<GameObject>();

        [SerializeField] private ParticleSystem BigElectric;
        [SerializeField] private ParticleSystem SmaleElectric;

        private float time;

        public void MYUpdate()
        {
            CalculationsWhichEnemiesAround();
            time += Time.deltaTime;
            if (time > 2)
            {
                time = 0;
                AttackingDrone();
            }
        }

        void CalculationsWhichEnemiesAround()
        {
            // Enemy katmanındaki objeleri algılamak için bir küre yayılımı kullanın
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, LayerMask.GetMask("Enemy"));

            // Algılanan her bir obje için işlem yapın
            foreach (Collider2D collider in hitColliders)
            {
                GameObject enemy = collider.gameObject;

                // Eğer daha önce algılanmamışsa listeye ekle
                if (!detectedEnemies.Contains(enemy))
                {
                    detectedEnemies.Add(enemy);
                }
            }

            // Algılanan objeleri kontrol edin ve listeden çıkarın
            for (int i = detectedEnemies.Count - 1; i >= 0; i--)
            {
                GameObject enemy = detectedEnemies[i];  /// todo: Nulll Objecy hatası verdi Burda kaldın, en son: Dronun Vuruş yapmasını sagladım.

                // Eğer obje hala algılanıyorsa ve 10 metrelik alandan çıkmışsa listeden çıkar
                if (Vector2.Distance(transform.position, enemy.transform.position) > detectionRadius)
                {
                    detectedEnemies.RemoveAt(i);
                }
            }
        }

        void AttackingDrone()
        {
            foreach (var enemy in detectedEnemies)
            {
                ParticleSystem _smaleElectric = Instantiate(SmaleElectric, enemy.transform.position, transform.rotation);
                Destroy(_smaleElectric.gameObject, 3f);

                enemy.GetComponent<EnemyScript>().TakeDamages(10, new Vector2(0, 0), false);
            }

            ParticleSystem _bigElectric = Instantiate(BigElectric, this.transform.position, transform.rotation);
            Destroy(_bigElectric.gameObject, 3f);
        }

        void OnDrawGizmosSelected()
        {
            // Gizmos üzerinde algılama yarıçapını göstermek için bir çember çizin
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}
using System;
using ______Scripts______.Canvas.Enemy;
using EnemyScripts.Enemy;
using UnityEngine;

namespace ______Scripts______.EnemyScripts.Enemy.EnemySkills.SuportSkills_Ahtapot
{
    public class HealBullet : MonoBehaviour
    {
        public GameObject TargetEnemy;
        private Rigidbody2D RB2;

        private void Start()
        {
            RB2 = this.gameObject.GetComponent<Rigidbody2D>();
            InvokeRepeating("HealBulletGoToEnemyPossition", 0f, 0.5f); // todo: ilerde burda oluşan bir bug vardı, onu oyuna entegre edebilirsin.
        }

        public void HealBulletGoToEnemyPossition()
        {
            if (TargetEnemy != null)
            {
                Vector2 DirectionToTarget = (TargetEnemy.transform.position - this.gameObject.transform.position).normalized; // 1 , -1 arasında bir değer (x,y)
                FiredBullet(DirectionToTarget);
            }
            else
                Destroy(this.gameObject);
        }

        void FiredBullet(Vector2 directionToTarget)
        {
            float bulletSpeed = 4;

            // Mermiyi hedefe doğru hareket ettir
            Vector2 yon = TargetEnemy.transform.position - transform.position;
            yon.Normalize();
            RB2.velocity = yon * bulletSpeed;

            // Merminin baktıgı yer hedefe dogru bakar
            float angle = Mathf.Atan2(yon.y, yon.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == TargetEnemy) // HealBullet Enemy'e ulaştıgında armor'ı versin
            {
                TargetEnemy.GetComponent<EnemyScript>().UpSuportArmor();
                other.GetComponent<EnemyHealthBar>().ChangeArmorBar();

                Destroy(this.gameObject);
            }
        }
    }
}
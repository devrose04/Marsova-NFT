using System;
using UnityEngine;

namespace EnemyScripts.Enemy.EnemySkills.SuportSkills_Ahtapot
{
    public class SuportGivesArmor : MonoBehaviour
    {
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private GameObject HealBulletPrefabs;
        private float ActionRadius;

        private void Awake()
        {
            InvokeRepeating("LookingWhichEnemiesAround", 0f, 1.5f);
        }

        void LookingWhichEnemiesAround()
        {
            Collider2D[] Enemies = Physics2D.OverlapCircleAll(this.transform.position, ActionRadius, enemyLayer);
            // print("Vurdugun kişi sayısı:" + hitEnemies.Length); // bunu canvasa yazdır.

            foreach (var enemy in Enemies) // yuvarlagın içindeki olan Enemylere teker teker bakar.
            {
                HealBulletGoToEnemyPossition(enemy.gameObject);
            }
        }

// todo: yapman gereken: Enemy objlerine dogru mermi oluştur ve yolla
        void HealBulletGoToEnemyPossition(GameObject enemy)
        {
            Vector2 DirectionToTarget = (enemy.transform.position - this.gameObject.transform.position).normalized; // 1 , -1 arasında bir değer (x,y)
            //
            // HealBulletPrefabs.transform.right = mousePosition - (Vector2)HealBulletPrefabs.transform.position; // todo: buraya instantion ettigin bullet'ı yaz
            // RB2.AddForce(ShotPoint.right * 30f, ForceMode2D.Impulse);
        }
    }
}
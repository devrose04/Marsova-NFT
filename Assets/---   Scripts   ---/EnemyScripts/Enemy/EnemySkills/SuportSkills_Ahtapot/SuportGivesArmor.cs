using System;
using ______Scripts______.EnemyScripts.Enemy.EnemySkills.SuportSkills_Ahtapot;
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
            ActionRadius = 8;
            InvokeRepeating("LookingWhichEnemiesAround", 0f, 3f);
        }

        void LookingWhichEnemiesAround()
        {
            Collider2D[] Enemies = Physics2D.OverlapCircleAll(this.transform.position, ActionRadius, enemyLayer);
            // print("Vurdugun kişi sayısı:" + hitEnemies.Length); // bunu canvasa yazdır.

            foreach (var enemy in Enemies) // yuvarlagın içindeki olan Enemylere teker teker bakar.
            {
                WhichGoingToEnemy(enemy.gameObject);
            }
        }

        void WhichGoingToEnemy(GameObject enemy)
        {
            GameObject HealBullet = Instantiate(HealBulletPrefabs, this.transform.position, Quaternion.identity);
            HealBullet.GetComponent<HealBullet>().TargetEnemy = enemy;
        }
    }
}
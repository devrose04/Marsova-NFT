using UnityEngine;

namespace EnemyScripts.OwnScript
{
    public class ZombiesScript : MonoBehaviour, ICustomScript
    {
        private float speed;
        private float health;
        private float damage;
        private float hitTimeRange;

        private GameObject Enemy;

        private void Awake()
        {
            Enemy = this.gameObject;
        }


        public (float, float, float, float) OwnInformations()
        {
            speed = Random.Range(0f, 0f); //1.8f, 2.2f 
            health = Random.Range(100f, 140f);
            damage = Random.Range(18f, 24f);
            hitTimeRange = Random.Range(2f, 2.7f);
            return (speed, health, damage, hitTimeRange);
        }
    }
}
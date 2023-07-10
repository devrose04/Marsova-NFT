using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyScripts.OwnScript
{
    public class GiantScript : MonoBehaviour, ICustomScript
    {
        private float speed;
        private float health;
        private float damage;
        private float hitTimeRange;
        private float attackRadius;
        private float knockBackPower;
        private bool isAttackinRange;
        private bool isItFly;
        private float suportArmor;
        private int score;

        public (float, float, float, float, float, float, bool, bool, float, int) OwnInformations()
        {
            speed = Random.Range(2.5f, 3.2f);
            health = Random.Range(100f, 140f);
            damage = Random.Range(15f, 18f);
            hitTimeRange = Random.Range(1.1f, 1.4f);
            attackRadius = Random.Range(2.5f, 3f);
            knockBackPower = Random.Range(1.2f, 1.4f);
            isAttackinRange = false;
            isItFly = false;
            suportArmor = 0;
            score = 10;
            return (speed, health, damage, hitTimeRange, attackRadius, knockBackPower, isAttackinRange, isItFly, suportArmor, score);
        }
    }
}
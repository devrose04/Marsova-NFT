using UnityEngine;

namespace EnemyScripts.OwnScript
{
    public class WhiteScript : MonoBehaviour, ICustomScript
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

        public (float, float, float, float, float, float ,bool, bool, float,int) OwnInformations()
        {
            speed = Random.Range(2f, 2.4f);
            health = Random.Range(45f, 60f); 
            damage = Random.Range(8f, 12f);
            hitTimeRange = Random.Range(2.2f, 3f);
            attackRadius = Random.Range(2.2f, 2.6f);    // bu kullanılmıyor
            knockBackPower = Random.Range(0f, 0f);
            isAttackinRange = true;
            isItFly = true;
            suportArmor = 0;
            score = 5;
            return (speed, health, damage, hitTimeRange, attackRadius, knockBackPower, isAttackinRange, isItFly, suportArmor, score);
        }
    }
}

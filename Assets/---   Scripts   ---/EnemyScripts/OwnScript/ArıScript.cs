using UnityEngine;

// ReSharper disable UseNameofExpression

namespace EnemyScripts.OwnScript
{
    public class ArıScript : MonoBehaviour, ICustomScript // iskeletler öldükten sonra tek vuruşla canla doguyorlar, aslında bu bir hara ama özellik olarak kalsın dedim
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

        public (float, float, float, float, float, float, bool, bool, float) OwnInformations()
        {
            speed = Random.Range(2.7f, 3.1f);
            health = Random.Range(45f, 60f);
            damage = Random.Range(8f, 12f);
            hitTimeRange = Random.Range(2.2f, 3f);
            attackRadius = Random.Range(2.2f, 2.6f); // bu kullanılmıyor
            knockBackPower = Random.Range(1f, 1.2f);
            isAttackinRange = false;
            isItFly = true;
            suportArmor = 0;
            return (speed, health, damage, hitTimeRange, attackRadius, knockBackPower, isAttackinRange, isItFly, suportArmor);
        }
    }
}
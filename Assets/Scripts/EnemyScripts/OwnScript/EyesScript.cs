using UnityEngine;

// ReSharper disable UseNameofExpression

namespace EnemyScripts.OwnScript
{
    public class EyesScript : MonoBehaviour, ICustomScript // iskeletler öldükten sonra tek vuruşla canla doguyorlar, aslında bu bir hara ama özellik olarak kalsın dedim
    {
        public bool isHeLive = true;

        private float speed;
        private float health;
        private float damage;
        private float hitTimeRange;
        private float attackRadius;
        private float knockBackPower;
        private bool isAttackinRange;

        private GameObject Enemy;
        private Rigidbody2D RB2;

        private void Awake()
        {
            Enemy = this.gameObject;
            RB2 = this.gameObject.GetComponent<Rigidbody2D>();
            Invoke("LayerName", Random.Range(3, 6)); // iskeletlerin doğuş hızı
        }

        void LayerName()
        {
            Enemy.layer = LayerMask.NameToLayer("Enemy");
        }

        public (float, float, float, float, float, float ,bool) OwnInformations()
        {
            speed = Random.Range(2f, 2.4f);
            health = Random.Range(45f, 60f); 
            damage = Random.Range(8f, 12f);
            hitTimeRange = Random.Range(0.8f, 1f);
            attackRadius = Random.Range(2.2f, 2.6f);    // bu kullanılmıyor
            knockBackPower = Random.Range(1f, 1.2f);
            isAttackinRange = true;
            return (speed, health, damage, hitTimeRange, attackRadius, knockBackPower, isAttackinRange);
        }
    }
}
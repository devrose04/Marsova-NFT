using UnityEngine;

// ReSharper disable UseNameofExpression

namespace EnemyScripts.OwnScript
{
    public class SkeletonsScript : MonoBehaviour, ICustomScript  // iskeletler öldükten sonra tek vuruşla canla doguyorlar, aslında bu bir hara ama özellik olarak kalsın dedim
    {
        public bool isHeLive = true;
        
        private float speed;
        private float health;
        private float damage;
        private float hitTimeRange;
        
        private GameObject Enemy;
        private Rigidbody2D RB2;

        private void Awake()
        {
            Enemy = this.gameObject;
            RB2 = this.gameObject.GetComponent<Rigidbody2D>();
            Invoke("LayerName",Random.Range(3,6));  // iskeletlerin doğuş hızı
        }
    
        public void ReBorn()
        {
            if (isHeLive)
            {
                isHeLive = false;
                Enemy = Instantiate(Enemy, Enemy.transform.position,Quaternion.identity);
                Enemy.layer = LayerMask.NameToLayer("Default");
            }
        
        }
        void LayerName()
        {
            Enemy.layer = LayerMask.NameToLayer("Enemy");
        }
        
        public (float, float, float, float) OwnInformations()
        {
            speed = Random.Range(2f, 2.4f);
            health = Random.Range(45f,60f);
            damage = Random.Range(15f, 20f);
            hitTimeRange = Random.Range(1.8f, 2.4f);
            return (speed,health,damage,hitTimeRange);
        }
    }
}

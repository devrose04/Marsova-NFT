using UIScripts;
using UnityEngine;
using Random = UnityEngine.Random;


namespace EnemyScripts.OwnScript
{
    public class SlimeScript : MonoBehaviour, ICustomScript // bu Scripte sadece Dogs'a özel kodlar olucak.
    {
        private float speed;
        private float health;
        private float damage;
        private float hitTimeRange;
        private float attackRadius;
        private float knockBackPower;
        private bool isAttackinRange;
        private bool isItFly;

        private Rigidbody2D RB2;

        private IsGroundTouchScript __isGroundTouch;

        private void Awake()
        {
            RB2 = this.gameObject.GetComponent<Rigidbody2D>();
            __isGroundTouch = gameObject.GetComponentInChildren<IsGroundTouchScript>();
        }

        public void Jump()
        {
            if (__isGroundTouch.isGroundTouchBool)
                RB2.velocity = new Vector2(RB2.velocity.x, Random.Range(3, 6));
        }

        public (float, float, float, float, float, float, bool, bool) OwnInformations()
        {
            speed = Random.Range(3f, 4.5f);
            health = Random.Range(30f, 45f); 
            damage = Random.Range(5f, 8f);
            hitTimeRange = Random.Range(0.4f, 0.6f);
            attackRadius = Random.Range(1.5f, 1.8f);
            knockBackPower = Random.Range(0.4f, 0.6f);
            isAttackinRange = false;
            isItFly = false;
            return (speed, health, damage, hitTimeRange, attackRadius, knockBackPower, isAttackinRange, isItFly);
        }
    }
}
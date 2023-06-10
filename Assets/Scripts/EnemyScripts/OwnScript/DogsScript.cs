using UIScripts;
using UnityEngine;
using Random = UnityEngine.Random;


namespace EnemyScripts.OwnScript
{
    public class DogsScript : MonoBehaviour,ICustomScript  // bu Scripte sadece Dogs'a özel kodlar olucak.
    {
        private float speed;
        private float health;
        private float damage;
        private float hitTimeRange;
        
        private GameObject Enemy;
        private Rigidbody2D RB2;
        private IsGroundTouchScript __isGroundTouch;

        private void Awake()
        {
            Enemy = this.gameObject;
            RB2 = this.gameObject.GetComponent<Rigidbody2D>();
            __isGroundTouch = gameObject.GetComponentInChildren<IsGroundTouchScript>();
        }

        public void Jump()
        {
            if (__isGroundTouch.isGroundTouchBool)
                RB2.velocity = new Vector2(RB2.velocity.x, Random.Range(3,6));
        }
        
        public (float, float, float, float) OwnInformations()
        {
            speed = Random.Range(3f, 4.5f);
            health = Random.Range(30f,45f);
            damage = Random.Range(8f, 12f);
            hitTimeRange = Random.Range(1f, 1.5f);
            return (speed,health,damage,hitTimeRange);
        }
    }

}

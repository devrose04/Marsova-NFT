using System;
using ObjectsScripts;
using PlayerScripts;
using PlayerScripts.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace EnemyScripts.Enemy.EnemyAttack
{
    public class EnemyBulletScript : MonoBehaviour
    {
        private GameObject Bullet;
        private GameObject Player;

        private Rigidbody2D RB2;

        private PlayerScript __PlayerScript;
        private Calculations _calculations;

        public float damages;
        public float knockBackPower;

        public ParticleSystem hitEffect;

        private void Awake()
        {
            Player = GameObject.Find("Player");
            Bullet = this.gameObject;

            RB2 = Bullet.GetComponent<Rigidbody2D>();

            __PlayerScript = Player.GetComponent<PlayerScript>();
            _calculations = Player.GetComponent<Calculations>();

            FiredBullet();
        }

        public void BulletIsTouchThePlayer(Collider2D player)
        {
            var result = _calculations.CalculationsAboutToObject(this.gameObject, player); // Enemy'lerin vuruş yaptıgı yerin verileri hesaplanır.
            Vector2 directionToPlayer = result.Item1;

            ParticleSystem CreatedHitEffect = Instantiate(hitEffect, Player.transform); // hitEffeckti oluşturduk
            Destroy(CreatedHitEffect.gameObject, 5f);

            __PlayerScript.TakeDamage(damages, directionToPlayer, knockBackPower);
            Destroy(Bullet);
        }

        void FiredBullet()
        {
            // Düşmanın pozisyonunu al
            Vector2 targetPosition = Player.transform.position;
            Vector2 attackingPosition = Bullet.transform.position;

            // 1-) Düşmanın pozisyonunun hangi yönde oldugunu buluyor.   
            Vector2 approximatelyDirectionToTarget = (targetPosition - attackingPosition).normalized; // 1 , -1 arasında bir değer (x,y)

            float bulletSpeed = 220;

            RB2.AddForce(new Vector2(approximatelyDirectionToTarget.x * bulletSpeed, approximatelyDirectionToTarget.y * bulletSpeed));
        }

        private void OnTriggerStay2D(Collider2D other)  // Mermi Ground'a çarpar ise yok olur
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                Destroy(Bullet);
            }
        }
    }
}
using System;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts.Enemy.Enemy
{
    public class EnemyDmgCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerBuller"))
            {
                PlayerBullet __PlayerBullet = other.GetComponent<PlayerBullet>();
                __PlayerBullet.BulletIsTouchTheEnemy(this.gameObject);
            }
        }
    }
}
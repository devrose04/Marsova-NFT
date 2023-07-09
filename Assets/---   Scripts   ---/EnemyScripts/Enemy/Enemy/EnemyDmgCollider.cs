using ______Scripts______.PlayerScripts.PlayerLaserAbout;
using PlayerScripts.PlayerLaserAbout;
using UnityEngine;

namespace ______Scripts______.EnemyScripts.Enemy.Enemy
{
    public class EnemyDmgCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerBullet"))
            {
                PlayerBullet __PlayerBullet = other.GetComponent<PlayerBullet>();
                __PlayerBullet.BulletIsTouchTheEnemy(this.gameObject);
            }
        }
    }
}
using System;
using UnityEngine;

namespace ______Scripts______.EnemyScripts.Enemy.EnemyAttack
{
    public class EnemyWeaponScript : MonoBehaviour
    {
        private GameObject Player;

        private void Awake()
        {
            Player = GameObject.Find("Player");
        }

        private void Update()
        {
            LookingTheMousePosition();
        }

        void LookingTheMousePosition()
        {
            Vector2 difference = Player.transform.position - transform.position;

            if (difference.x > 0)
                this.GetComponent<SpriteRenderer>().flipY = false;
            else
                this.GetComponent<SpriteRenderer>().flipY = true;

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

            // this.gameObject.transform.position = new Vector2(playerTransform.position.x + 2, playerTransform.position.y + 3);
        }
    }
}
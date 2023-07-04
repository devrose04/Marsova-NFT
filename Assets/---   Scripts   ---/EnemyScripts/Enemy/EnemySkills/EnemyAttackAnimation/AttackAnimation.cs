using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace ______Scripts______.EnemyScripts.Enemy.EnemySkills.EnemyAttackAnimation
{
    public class AttackAnimation : MonoBehaviour
    {
        private GameObject Enemy;

        private float Origin_Scale_x;
        private float Origin_Scale_y;

        private void Awake()
        {
            Enemy = this.gameObject;

            Origin_Scale_x = Enemy.transform.localScale.x;
            Origin_Scale_y = Enemy.transform.localScale.y;
        }

        public IEnumerator EnemyAttackAnimation()
        {
            float Origin_Rotation_y = Enemy.transform.rotation.y;
            Origin_Rotation_y = Origin_Rotation_y * 180;

            Enemy.transform.rotation = Quaternion.Euler(0, Origin_Rotation_y, 12);
            Enemy.transform.localScale = new Vector3(Origin_Scale_x + 0.5f, Origin_Scale_y + 0.5f);
            yield return new WaitForSeconds(0.04f);

            Enemy.transform.rotation = Quaternion.Euler(0, Origin_Rotation_y, 25);
            yield return new WaitForSeconds(0.04f);

            Enemy.transform.localScale = new Vector3(Origin_Scale_x + 0.8f, Origin_Scale_y + 0.8f);
            yield return new WaitForSeconds(0.05f);

            Enemy.transform.rotation = Quaternion.Euler(0, Origin_Rotation_y, 10);
            yield return new WaitForSeconds(0.05f);

            Enemy.transform.localScale = new Vector3(Origin_Scale_x + 0.5f, Origin_Scale_y + 0.5f);
            yield return new WaitForSeconds(0.04f);

            Enemy.transform.rotation = Quaternion.Euler(0, Origin_Rotation_y, -8);
            yield return new WaitForSeconds(0.04f);

            Enemy.transform.rotation = Quaternion.Euler(0, Origin_Rotation_y, -15);
            Enemy.transform.localScale = new Vector3(Origin_Scale_x + 0.2f, Origin_Scale_y + 0.2f);
            yield return new WaitForSeconds(0.03f);

            Enemy.transform.rotation = Quaternion.Euler(0, Origin_Rotation_y, -5);
            yield return new WaitForSeconds(0.03f);

            Enemy.transform.rotation = Quaternion.Euler(0, Origin_Rotation_y, 0);
            Enemy.transform.localScale = new Vector3(Origin_Scale_x, Origin_Scale_y);
        }
    }
}
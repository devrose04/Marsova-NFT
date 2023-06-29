using System;
using System.Collections;
using ______Scripts______.PlayerScripts.SwordScripts;
using EnemyScripts;
using EnemyScripts.Enemy;
using PlayerScripts.SwordScripts;
using UnityEngine;

namespace GameManagerScript.SkillsDetails
{
    public class DashAttackDetails : MonoBehaviour
    {
        private GameObject Player;

        private void Awake()
        {
            Player = GameObject.Find("Player");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator DashAtackToEnemy(Collider2D other, Vector2 directionToEnemy) // Enemy dmg alıyor     // dash vuruş
        {
            other.gameObject.GetComponent<EnemyScript>().HitToPlayerTimer = 10; // zamana 10 veriyorum çünkü, if koşulu içindeki HiToPlayer hemen çalışabililir duruma geçsin.
            EnemyScript __EnemyScript = other.gameObject.GetComponent<EnemyScript>();
            float dmg = Player.GetComponent<SwordScript>().swordDamage / 2; // %50 daha az dmg vurur ama 4 kere vurar

            if (other != null) HitAndEffect(other, dmg, directionToEnemy, __EnemyScript);

            yield return new WaitForSeconds(0.12f);
            if (other != null) HitAndEffect(other, dmg, directionToEnemy, __EnemyScript);

            yield return new WaitForSeconds(0.15f);
            if (other != null) HitAndEffect(other, dmg, directionToEnemy, __EnemyScript);

            yield return new WaitForSeconds(0.18f);
            if (other != null) HitAndEffect(other, dmg, directionToEnemy, __EnemyScript);
        }

        private void HitAndEffect(Collider2D other, float dmg, Vector2 directionToEnemy, EnemyScript __EnemyScript)
        {
            __EnemyScript.TakeDamages(dmg, directionToEnemy, false);
            ParticleSystem Effect = Player.GetComponent<SwordScript>().hitEffect; // Hangi Effekti oldugunu alıyor    
            ParticleSystem effect = Instantiate(Effect, other.gameObject.transform);
            Destroy(effect.gameObject, 5f);
        }
    }
}
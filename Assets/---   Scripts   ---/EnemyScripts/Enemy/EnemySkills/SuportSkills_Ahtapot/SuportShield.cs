using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyScripts.Enemy.SuportSkills_Ahtapot
{
    public class SuportShield : MonoBehaviour
    {
        [SerializeField] private GameObject Shield;
        [SerializeField] private GameObject Target_enemy;
        [SerializeField] private ParticleSystem HitShieldEffect;
        [SerializeField] GameObject ShieldEffect;

        public float ShieldHeal;

        private void Awake()
        {
            ShieldHeal = Random.Range(80, 100); // her mermi 5 vuruyor
            InvokeRepeating("ShieldGoEnemyUpPossition", 0f, 0.5f);
        }

        public void TakeDamagesShield(float damages)
        {
            ShieldHeal -= damages;

            if (ShieldHeal <= 0)
            {
                ShieldEffect.SetActive(false); // Kalkan Effektini kapat
                Destroy(this.gameObject);
            }
        }

        public void CreateHitEffect(GameObject Bullet)
        {
            ParticleSystem effect = Instantiate(HitShieldEffect, Bullet.transform.position, Quaternion.identity);
            Destroy(effect.gameObject, 2f);
        }

        void ShieldGoEnemyUpPossition()
        {
            Shield.transform.position = Target_enemy.transform.position + new Vector3(0, 5f, 0);
        }
    }
}
using System;
using ______Scripts______.Canvas.Player;
using PlayerScripts.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyScripts.OwnScript
{
    public class SalyangozScript : MonoBehaviour, ICustomScript
    {
        private float speed;
        private float health;
        private float damage;
        private float hitTimeRange;
        private float attackRadius;
        private float knockBackPower;
        private bool isAttackinRange;
        private bool isItFly;
        private float suportArmor;

        private GameObject Player;
        private HealthBarScript _healthBarScript;

        private void Awake()
        {
            Player = GameObject.Find("Player");
            _healthBarScript = Player.GetComponent<HealthBarScript>();
        }

        public (float, float, float, float, float, float, bool, bool, float) OwnInformations()
        {
            speed = Random.Range(0.5f, 0.7f);
            health = Random.Range(80f, 100f);
            damage = Random.Range(0f, 0f);
            hitTimeRange = Random.Range(2f, 3f);
            attackRadius = Random.Range(0f, 0f); // bu kullanılmıyor
            knockBackPower = Random.Range(0f, 0f);
            isAttackinRange = false;
            isItFly = false;
            suportArmor = 0;
            return (speed, health, damage, hitTimeRange, attackRadius, knockBackPower, isAttackinRange, isItFly, suportArmor);
        }

        public void TakeHeal()
        {
            PlayerScript _script = Player.GetComponent<PlayerScript>();
            _script.health += 35;
            _healthBarScript.ChangeHealthBar();
            if (_script.health > 100)
            {
                _script.health = 100;
            }
        }
        // todo: EnemyScriptten isEnemySeePlayer bunu çekerke Animasyonunu ona göre çalıştırırsın. 
    }
}
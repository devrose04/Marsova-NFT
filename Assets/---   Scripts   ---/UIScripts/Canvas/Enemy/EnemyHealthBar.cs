using System;
using ______Scripts______.EnemyScripts.Enemy.Enemy;
using EnemyScripts.Enemy;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;


namespace ______Scripts______.Canvas.Enemy
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider HealthBar;
        [SerializeField] private Slider ArmorBar;

        private GameObject Enemy;
        private EnemyScript _enemyScript;

        private void Start()
        {
            Enemy = this.gameObject;
            _enemyScript = Enemy.GetComponent<EnemyScript>();

            HealthBar.maxValue = _enemyScript.health;
            HealthBar.value = _enemyScript.health;

            ArmorBar.maxValue = _enemyScript.maxSuportArmor;
            ArmorBar.value = _enemyScript.suportArmor;
        }

        public void ChangeHealthBar()
        {
            HealthBar.value = _enemyScript.health;
        }

        public void ChangeArmorBar()
        {
            ArmorBar.value = _enemyScript.suportArmor;
        }
    }
}
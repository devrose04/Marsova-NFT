using EnemyScripts;
using UnityEngine;

namespace ______Scripts______.EnemyScripts.OwnScript
{
    public class AhtapaotScript : MonoBehaviour, ICustomScript
    {
        // todo: Bu Suport olucak ve Etrafındaki dostlarıne Heal vericek. Bu nu SuportHeal diye bir değişken oluştur onu arttır.
        // todo: VE 2. özelligi,üstünde bir katman olsun ve o katman havadan gelen mermileri engellesin. 5 6 merermiden sonra kırılsın ve yok olsun.

        private float speed;
        private float health;
        private float damage;
        private float hitTimeRange;
        private float attackRadius;
        private float knockBackPower;
        private bool isAttackinRange;
        private bool isItFly;
        private float suportArmor;
        private int score;

        [SerializeField] private GameObject ParentObject;

        public (float, float, float, float, float, float, bool, bool, float, int) OwnInformations()
        {
            speed = Random.Range(2.7f, 3.1f);
            health = Random.Range(60f, 75f);
            damage = Random.Range(0f, 0f);
            hitTimeRange = Random.Range(10000f, 10001f); // vuruş yapamasın diye yüksek
            attackRadius = Random.Range(0f, 0f); // bu bazısında kullanılmıyor
            knockBackPower = Random.Range(0f, 0f);
            isAttackinRange = true; // not attack this object
            isItFly = true;
            suportArmor = 0;
            score = 20;
            return (speed, health, damage, hitTimeRange, attackRadius, knockBackPower, isAttackinRange, isItFly, suportArmor, score);
        }

        public void DestroyThisParentGameObject() // Armor ile Ahtapot 1 tane GameObjesinde birlikte duruyor, bu fonk. onu yok eder.
        {
            Destroy(ParentObject);
        }
        
    }
}
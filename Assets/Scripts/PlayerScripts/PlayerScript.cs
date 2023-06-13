using System.Collections;
using GameManagerScript;
using GameManagerScript.SkillsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerScript : MonoBehaviour
    {
        private GameObject Player;
        private Rigidbody2D RB2;

        private SkillsScript __SkillsScript;

        [SerializeField] public float speed;
        [SerializeField] public float health;
        [SerializeField] public float armor; // armor 10 ise %10 hasar azaltır. max 100 olur
        [SerializeField] float knockbackForce;

        public bool isKnockbacked = false; // Player'ın yürüme skillerini kullanamaması için oluşturdum

        private void Awake()
        {
            Player = GameObject.Find("Player");
            RB2 = Player.GetComponent<Rigidbody2D>();
            __SkillsScript = GameObject.Find("GameManager").GetComponent<SkillsScript>();
        }

        public void TakeDamage(float dmg, Vector2 clampedDirectionToEnemy)
        {
            dmg = dmg * (0.01f * (100 - armor)); // 0.01 yazma nedenim: 100'ü 0.01 ile çarparsak 1 elder ederiz. Yani %1 ini elde ederiz.
            health -= dmg;
            // print($"<color=green>Player Health:</color>" + health);
            if (health <= 0)
            {
                Destroy(this.gameObject);
                print($"<color=red>GAME OVER</color>");
                Time.timeScale = 0; // oyun bitti
            }

            if (__SkillsScript.isArmorFrameUse == false) // ArmorFrame Skili kullanıldıgına Knock.back'i deActive etmek için bu if koşulunu koydum
                StartCoroutine(DoKnockback(clampedDirectionToEnemy));
        }

        private IEnumerator DoKnockback(Vector2 clampedDirectionToEnemy) // Bu Player'ın KnockBack'i dir
        {
            isKnockbacked = true;
            print(clampedDirectionToEnemy);
            Vector2 knockbackVector = new Vector2(-clampedDirectionToEnemy.x * knockbackForce, clampedDirectionToEnemy.y);
            RB2.AddForce(knockbackVector, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.35f);

            isKnockbacked = false;
        }
    }
}
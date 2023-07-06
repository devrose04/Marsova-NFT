using System.Collections;
using ______Scripts______.Canvas.Player;
using ______Scripts______.GameManagerScript.SkillsScripts;
using ______Scripts______.PlayerScripts.Player;
using GameManagerScript.SkillsScripts;
using PlayerScripts.SwordScripts;
using UIScripts;
using UnityEngine;

namespace PlayerScripts.Player
{
    public class PlayerScript : MonoBehaviour
    {
        private GameObject Player;
        private Rigidbody2D RB2;
        private IsGroundTouchScript _isGroundTouchScript;

        [SerializeField] private ParticleSystem TouchGroundEffect;
        [SerializeField] private GameObject GameOverimages;

        private SkillsScript __SkillsScript;
        private SwordController _swordController;
        private PlayerAnimations _playerAnimations;
        private CapsuleCollider2D _capsuleCollider2D;
        private GameObject IsGrounTouch;
        private SpriteRenderer _spriteRenderer;
        private HealthBarScript _healthBarScript;

        private Coroutine _coroutine;

        [SerializeField] public float speed;
        [SerializeField] public float health;
        [SerializeField] public float armor; // armor 10 ise %10 hasar azaltır. max 100 olur
        public int totalScore = 0;
        [SerializeField] private Transform touchGrand;
        private float knockbackForce = 750;

        public bool isHeDead;
        public bool isKnockbacked = false; // Player'ın yürüme skillerini kullanamaması için oluşturdum

        private void Awake()
        {
            Player = GameObject.Find("Player");
            RB2 = Player.GetComponent<Rigidbody2D>();
            __SkillsScript = GameObject.Find("GameManager").GetComponent<SkillsScript>();
            _isGroundTouchScript = Player.GetComponentInChildren<IsGroundTouchScript>();
            IsGrounTouch = _isGroundTouchScript.gameObject;
            _swordController = Player.GetComponent<SwordController>();
            _playerAnimations = Player.GetComponent<PlayerAnimations>();
            _capsuleCollider2D = Player.GetComponent<CapsuleCollider2D>();
            _spriteRenderer = Player.GetComponent<SpriteRenderer>();
            _healthBarScript = Player.GetComponent<HealthBarScript>();
        }

        public void TakeDamage(float dmg, Vector2 directionToPlayer, float knockBackPower)
        {
            TakeDamagesTransactions();

            dmg = dmg * (0.01f * (100 - armor)); // 0.01 yazma nedenim: 100'ü 0.01 ile çarparsak 1 elder ederiz. Yani %1 ini elde ederiz.
            health -= dmg;
            _healthBarScript.ChangeHealthBar();
            // print($"<color=green>Player Health:</color>" + health);
            if (health <= 0)
            {
                isHeDead = true;
                _playerAnimations.ChangeAnimationState("Die");
                _playerAnimations.SetBoolParameter("isHeDead", true);

                // Destroy(this.gameObject);
                print($"<color=red>GAME OVER</color>");
                GameOverimages.SetActive(true);

                _capsuleCollider2D.size = new Vector2(0.13f, 0.2f);
                IsGrounTouch.SetActive(false);
                Invoke("GameOver", 3.5f);
            }

            if (__SkillsScript.isArmorFrameUse == false) // ArmorFrame Skili kullanıldıgına Knock.back'i deActive etmek için bu if koşulunu koydum
                StartCoroutine(DoKnockback(directionToPlayer, knockBackPower));
        }

        private IEnumerator DoKnockback(Vector2 directionToPlayer, float knockBackPower) // Bu Player'ın KnockBack'i dir
        {
            isKnockbacked = true;

            if (RB2.gravityScale == 1)
                knockbackForce = 450;
            else
                knockbackForce = 750;

            Vector2 knockbackVector = new Vector2(directionToPlayer.x * knockbackForce * knockBackPower, directionToPlayer.y);
            RB2.AddForce(knockbackVector, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.35f);

            isKnockbacked = false;
        }

        public void CreateTouchTheGroundEffeckt()
        {
            if (__SkillsScript.isArmorFrameUse == true && _isGroundTouchScript.isGroundTouchBool == true && __SkillsScript.justOneTimeWork == 1)
            {
                ParticleSystem _effect = Instantiate(TouchGroundEffect, touchGrand.position, Quaternion.identity);
                Destroy(_effect.gameObject, 3f);
                __SkillsScript.justOneTimeWork = 0;
                _swordController.ArmorFrameAttack();
            }
        }

        void GameOver()
        {
            Time.timeScale = 0; // oyun bitti
        }

        IEnumerator TakeDamagesAnimation()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            Color color_half = _spriteRenderer.color;
            color_half.a = 0.5f;

            Color color_full = _spriteRenderer.color;
            color_full.a = 1f;

            yield return wait;
            _spriteRenderer.color = color_half;
            yield return wait;
            _spriteRenderer.color = color_full;

            yield return wait;
            _spriteRenderer.color = color_half;
            yield return wait;
            _spriteRenderer.color = color_full;

            yield return wait;
            _spriteRenderer.color = color_half;
            yield return wait;
            _spriteRenderer.color = color_full;
        }

        void TakeDamagesTransactions()
        {
            Color color_full = _spriteRenderer.color;
            color_full.a = 1f;

            _spriteRenderer.color = color_full;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(TakeDamagesAnimation());
        }
    }
}
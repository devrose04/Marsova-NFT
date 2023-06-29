using System.Collections;
using ______Scripts______.PlayerScripts.Player;
using PlayerScripts.Player;
using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace ______Scripts______.GameManagerScript.SkillsScripts
{
    public class SkillsScript : MonoBehaviour
    {
        public bool isMoveSkilsUse = false;
        public bool isDashAtackUse = false;
        public bool isArmorFrameUse = false;
        private bool isDodgeUse = false;

        private GameObject Player;
        private Rigidbody2D RB2;

        private PlayerScript __PlayerScript;
        private PlayerAnimations _playerAnimations;

        public int justOneTimeWork;

        [SerializeField] public float JetPackFuel;
        [SerializeField] private ParticleSystem JetPackEffect;

        private void Awake()
        {
            Player = GameObject.Find("Player");
            RB2 = Player.GetComponent<Rigidbody2D>();
            __PlayerScript = Player.GetComponent<PlayerScript>();
            _playerAnimations = Player.GetComponent<PlayerAnimations>();
        }

        public IEnumerator DashAtack() // Bu ileri atılıp saldırma skili
        {
            Quaternion lookingRotation = Player.transform.rotation.normalized;
            isMoveSkilsUse = true;
            isDashAtackUse = true;
            // _playerAnimations.SetBoolParameter("isDashAtackUse",isDashAtackUse);
            _playerAnimations.ChangeAnimationState("DashAttack");

            int pushPower = 800;
            float time = 1f;
            if (RB2.gravityScale == 0) // zemine degili ise çalışır
            {
                time = 0.7f;
                pushPower = 1200;
            }

            if (lookingRotation.w == 1) // sağa bakıyor
                RB2.AddForce(new Vector2(pushPower, RB2.velocity.y), ForceMode2D.Impulse);
            if (lookingRotation.y == 1) // sola bakıyor
                RB2.AddForce(new Vector2(-pushPower, RB2.velocity.y), ForceMode2D.Impulse);

            yield return new WaitForSeconds(time);
            isMoveSkilsUse = false;
            isDashAtackUse = false;
            // _playerAnimations.SetBoolParameter("isDashAtackUse",isDashAtackUse);
            _playerAnimations.ChangeAnimationState("idle");
        }

        public IEnumerator DodgeSkils_q() // q ile sola Dodge atıyor    bu kodu ilerde diger  dodge kodu ile birleştire bilirisin.
        {
            isMoveSkilsUse = true;
            isDodgeUse = true;
            // _playerAnimations.SetBoolParameter("isDodgeUse", isDodgeUse);
            _playerAnimations.ChangeAnimationState("Dodge");

            int pushPower = 28;
            if (RB2.gravityScale == 1) // zemine degmiyor ise çalışır
                pushPower = 13;

            RB2.velocity = new Vector2(-pushPower, RB2.velocity.y);
            Player.transform.rotation = new Quaternion(0, 180, 0, 0);

            yield return new WaitForSeconds(0.50f);

            isMoveSkilsUse = false;
            isDodgeUse = false;
            // _playerAnimations.SetBoolParameter("isDodgeUse", isDodgeUse);
            _playerAnimations.ChangeAnimationState("idle");
        }

        public IEnumerator DodgeSkils_e() // e ile sağa Dodge atıyor
        {
            isMoveSkilsUse = true;
            isDodgeUse = true;
            // _playerAnimations.SetBoolParameter("isDodgeUse", isDodgeUse);
            _playerAnimations.ChangeAnimationState("Dodge");

            int pushPower = 28;
            if (RB2.gravityScale == 1) // zemine degmiyor ise çalışır
                pushPower = 13;

            RB2.velocity = new Vector2(pushPower, RB2.velocity.y);
            Player.transform.rotation = new Quaternion(0, 0, 0, 0);

            yield return new WaitForSeconds(0.50f);

            isMoveSkilsUse = false;
            isDodgeUse = false;
            // _playerAnimations.SetBoolParameter("isDodgeUse", isDodgeUse);
            _playerAnimations.ChangeAnimationState("idle");
        }

        public IEnumerator ArmorFrame() // Player 4.5 saniyeligine daha az hasar alır.
        {
            // Zombi Yeniçeri Özelligi
            justOneTimeWork = 1;
            isArmorFrameUse = true; // todo: armor frame kullanıdlgıdan ve yere ilk degene kadar 1. animasyn çalışsın
            isMoveSkilsUse = true;

            RB2.gravityScale = 10;

            float realArmor = __PlayerScript.armor; // gerçek Armor
            float armorFrameArmor; // bu Skilin verdigi Armor

            float realSpeed = __PlayerScript.speed;

            if (realArmor >= 60f)
                armorFrameArmor = 95f;

            else if (realArmor >= 40f)
                armorFrameArmor = 85f;

            else
                armorFrameArmor = 75f;

            __PlayerScript.armor = realArmor; // armor'ı bi anda artırıyor ve zamanla düşürüyor.
            __PlayerScript.speed = realSpeed * (1 - 0.6f); // speed'i bi anlık düşüyor ve zamanla artırıyor.
            yield return new WaitForSeconds(1f);
            isMoveSkilsUse = false;
            yield return new WaitForSeconds(2f);

            RB2.gravityScale = 0;
            isArmorFrameUse = false; // karakter hareket edebilir artık.
            __PlayerScript.armor = armorFrameArmor - 10f;
            __PlayerScript.speed = realSpeed * (1 - 0.3f);
            yield return new WaitForSeconds(1.5f);

            __PlayerScript.armor = armorFrameArmor - 20f;
            __PlayerScript.speed = realSpeed * (1 - 0.1f);
            yield return new WaitForSeconds(1f);

            __PlayerScript.armor = realArmor;
            __PlayerScript.speed = realSpeed;
        }

        public void JetPack()
        {
            if (RB2.velocity.y < 5 && JetPackFuel > 0)
            {
                JetPackFuel -= Time.deltaTime;
                RB2.AddForce(new Vector2(RB2.velocity.x, 50), ForceMode2D.Impulse);
                if (JetPackFuel > 0.2)
                {
                    ParticleSystem Effect = Instantiate(JetPackEffect, Player.transform);
                    Destroy(Effect.gameObject, 1.2f);
                }
            }
        }
    }
}
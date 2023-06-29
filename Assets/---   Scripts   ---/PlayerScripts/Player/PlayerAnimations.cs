using System;
using System.Collections;
using ______Scripts______.GameManagerScript.SkillsScripts;
using ______Scripts______.PlayerScripts.SwordScripts;
using GameManagerScript.SkillsScripts;
using UnityEngine;

namespace ______Scripts______.PlayerScripts.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator playerAnimator;
        private SwordScript _swordScript;
        private SkillsScript _skillsScript;

        private GameObject Player;
        private GameObject GameManager;

        private string currentState;

        private void Awake()
        {
            Player = this.gameObject;
            GameManager = GameObject.Find("GameManager");
            playerAnimator = Player.GetComponent<Animator>();
            _swordScript = Player.GetComponent<SwordScript>();
            _skillsScript = GameManager.GetComponent<SkillsScript>();
        }

        public void SetBoolParameter(string parameterName, bool value)
        {
            if (currentState != "Die")
            {
                playerAnimator.SetBool(parameterName, value);
            }
        }

        public void GroundSlameAnim(Rigidbody2D RB2)
        {
            if (currentState != "Die")
            {
                bool isSmashUseSecondPart = playerAnimator.GetBool("isSmashUseSecondPart");

                if (RB2.gravityScale == 10) // 10 ise ArmorFrame çalışmıştır demek
                {
                    ChangeAnimationState("SmashFirstPart");
                    SetBoolParameter("isSmashUseFirstPart", true);
                }
                else if (isSmashUseSecondPart == false && RB2.gravityScale == 0) // Player'zemine degmiş olur
                {
                    SetBoolParameter("isSmashUseSecondPart", true);
                    SetBoolParameter("isSmashUseFirstPart", false);
                }
                else
                    SetBoolParameter("isSmashUseSecondPart", false);
            }
        }

        // public IEnumerator SwordAnimations(string parameterName)
        // {
        //     SetBoolParameter(parameterName, true);
        //     yield return new WaitForSeconds(0.3f); // 0.3 sn sonra bitsin
        //     SetBoolParameter(parameterName, false);
        // }

        public void ChangeAnimationState(string newState)
        {
            if (currentState != "Die")
            {
                if (currentState == newState) // aynı animasyon ise çalışmasın
                    return;

                playerAnimator.Play(newState);

                currentState = newState;
            }
        }

        public void idleAnim(Rigidbody2D RB2)
        {
            if (currentState != "Die")
            {
                if (RB2.velocity.magnitude <= 1f && RB2.gravityScale == 0 && _swordScript.isAttack == false && _skillsScript.isArmorFrameUse == false)
                    ChangeAnimationState("idle");
            }
        }

        public void JetPackAnimation(Rigidbody2D RB2, bool isAttacking)
        {
            if (RB2.gravityScale == 1 && isAttacking == false && _skillsScript.JetPackFuel <= 0.2f)
            {
                print("düşüyor");
                // düşüyor
            }
            else if (RB2.gravityScale == 1 && isAttacking == false && _skillsScript.JetPackFuel > 0.2f)
            {
                print("uçuyor");
                // uçuyor
            }
        }
    }
}
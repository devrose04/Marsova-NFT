using System;
using System.Collections;
using UnityEngine;

namespace ______Scripts______.PlayerScripts.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator playerAnimator;

        private void Awake()
        {
            playerAnimator = this.gameObject.GetComponent<Animator>();
        }

        public void SetBoolParameter(string parameterName, bool value)
        {
            playerAnimator.SetBool(parameterName, value);
        }

        public void GroundSlameAnim(Rigidbody2D RB2)
        {
            bool isGroundSlameUseSecondPart = playerAnimator.GetBool("isGroundSlameUseSecondPart");

            if (RB2.gravityScale == 10) // 10 ise ArmorFrame çalışmıştır demek
                SetBoolParameter("isGroundSlameUseFirstPart", true);
            else if (isGroundSlameUseSecondPart == false && RB2.gravityScale == 0)  // Player'zemine degmiş olur
            {
                SetBoolParameter("isGroundSlameUseSecondPart", true);
                SetBoolParameter("isGroundSlameUseFirstPart", false);
            }
            else
                SetBoolParameter("isGroundSlameUseSecondPart", false);
        }

        public IEnumerator SwordAnimations(string parameterName) 
        {
            SetBoolParameter(parameterName, true);
            yield return new WaitForSeconds(0.3f); // 0.3 sn sonra bitsin
            SetBoolParameter(parameterName, false);
        }
    }
}
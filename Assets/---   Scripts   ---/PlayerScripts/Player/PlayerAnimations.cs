using System;
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
            playerAnimator.SetBool(parameterName,value);
        }
        
    }
}

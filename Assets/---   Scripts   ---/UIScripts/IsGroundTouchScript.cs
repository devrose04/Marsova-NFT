using System;
using ______Scripts______.PlayerScripts.Player;
using GameManagerScript.SkillsScripts;
using PlayerScripts.Player;
using UnityEngine;

namespace UIScripts
{
    public class IsGroundTouchScript : MonoBehaviour
    {
        public bool isGroundTouchBool;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
                isGroundTouchBool = true;

            if (this.gameObject.GetComponentInParent<PlayerScript>() != null)
            {
                this.gameObject.GetComponentInParent<PlayerScript>().CreateTouchTheGroundEffeckt();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
                isGroundTouchBool = false;
        }
    }
}
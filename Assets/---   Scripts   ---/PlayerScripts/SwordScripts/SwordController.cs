using System;
using ______Scripts______.PlayerScripts.Player;
using UnityEngine;

namespace PlayerScripts.SwordScripts
{
    public class SwordController : MonoBehaviour
    {
        private GameObject Player;

        private SwordScript __SwordScript;
        private PlayerAnimations _playerAnimations;

        private void Awake()
        {
            Player = this.gameObject;
            __SwordScript = Player.GetComponent<SwordScript>();
            _playerAnimations = Player.GetComponent<PlayerAnimations>();
        }

        public void SwordAttack1()
        {
            __SwordScript.SwordAttack(0.8f, 0.6f, false, 0.8f, true);
            StartCoroutine(_playerAnimations.SwordAnimations("isUseSwordAttack1"));
        }

        public void SwordAttack2()
        {
            __SwordScript.SwordAttack(1.2f, 0.7f, false, 0.8f, true);
            StartCoroutine(_playerAnimations.SwordAnimations("isUseSwordAttack2"));
        }

        public void SwordAttack3()
        {
            __SwordScript.SwordAttack(1.5f, 0.8f, true, 0.8f, false);
            StartCoroutine(_playerAnimations.SwordAnimations("isUseSwordAttack3"));
        }

        public void HittingAll1()
        {
            __SwordScript.SwordAttack(0.8f, 0.6f, true, 0.5f, false);
            StartCoroutine(_playerAnimations.SwordAnimations("isUseSwordAttack4"));
        }

        public void HittingAll2()
        {
            __SwordScript.SwordAttack(1.2f, 1.5f, false, 0.5f, false);
            StartCoroutine(_playerAnimations.SwordAnimations("isUseSwordAttack5"));
        }
    }
}
using System;
using UnityEngine;

namespace PlayerScripts.SwordScripts
{
    public class SwordController : MonoBehaviour
    {
        private GameObject Player;

        private SwordScript __SwordScript;

        private void Awake()
        {
            Player = this.gameObject;
            __SwordScript = Player.GetComponent<SwordScript>();
        }

        public void SwordAttack1()
        {
            __SwordScript.SwordAttack(0.8f, 0.6f, false, 0.8f, true);
        }

        public void SwordAttack2()
        {
            __SwordScript.SwordAttack(1.2f, 0.7f, false, 0.8f, true);
        }

        public void SwordAttack3()
        {
            __SwordScript.SwordAttack(1.5f, 0.8f, true, 0.8f, false);
        }

        public void HittingAll1()
        {
            __SwordScript.SwordAttack(0.8f, 0.3f, true, 0.5f, false);
        }

        public void HittingAll2()
        {
            __SwordScript.SwordAttack(1.2f, 1.5f, false, 0.5f, false);
        }
    }
}
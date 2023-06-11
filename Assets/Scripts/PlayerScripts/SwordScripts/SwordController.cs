using System;
using UnityEngine;

namespace PlayerScripts.SwordScripts
{
    public class SwordController : MonoBehaviour
    {
        private SwordScript __SwordScript;
        private GameObject Player;

        private void Awake()
        {
            Player = this.gameObject;
            __SwordScript = Player.GetComponent<SwordScript>();
        }

        public void SwordAttack1()
        {
            __SwordScript.SwordAttack(0.8f , 0.8f,false,0.8f,1f);
        }        
        public void SwordAttack2()
        {
            __SwordScript.SwordAttack(1.2f ,1,false,0.8f,1f);
        }        
        public void SwordAttack3()   
        {
            __SwordScript.SwordAttack(1.5f ,1.2f,true,0.8f,1f);
        }

        public void HittingAll1()
        {
            __SwordScript.SwordAttack(0.8f,1f,true,0.5f,4);
        }
        public void HittingAll2()
        {  
            __SwordScript.SwordAttack(1.2f,2.5f,false,0.5f,4);
        }

    }
}

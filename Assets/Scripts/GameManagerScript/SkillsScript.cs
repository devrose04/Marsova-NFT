using System;
using System.Collections;
using PlayerScripts;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable Unity.InefficientPropertyAccess
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GameManagerScript
{
    public class SkillsScript : MonoBehaviour
    {
        public bool isMoveSkilsUse = false;
        public bool isDashAtackUse = false;
        public bool isArmorFrameUse = false;

        private GameObject Player;
        private Rigidbody2D RB2;
        private CapsuleCollider2D capsuleCollider2D;
        private PlayerScript __PlayerScript; 

        private void Awake()
        {
            Player = GameObject.Find("Player");
            RB2 = Player.GetComponent<Rigidbody2D>();
            capsuleCollider2D = Player.GetComponent<CapsuleCollider2D>();
            __PlayerScript = Player.GetComponent<PlayerScript>();
        }

        public IEnumerator DashAtack()     // Bu ileri atılıp saldırma skili
        {
            Quaternion lookingRotation = Player.transform.rotation.normalized;
            isMoveSkilsUse = true;
            isDashAtackUse = true;
            int pushPower = 800;
            float time = 1f;
            if (RB2.gravityScale == 0)  // zemine degili ise çalışır
            {
                time = 0.7f;
                pushPower = 1200;
            }
            
            if (lookingRotation.w == 1) // sağa bakıyor
                RB2.AddForce(new Vector2( pushPower,RB2.velocity.y),ForceMode2D.Impulse);
            if (lookingRotation.y == 1) // sola bakıyor
                RB2.AddForce(new Vector2( -pushPower,RB2.velocity.y),ForceMode2D.Impulse);
            
            yield return new WaitForSeconds(time);
            isMoveSkilsUse = false;
            isDashAtackUse = false;
        }
        
        public IEnumerator DodgeSkils_q()     // q ile sola Dodge atıyor    bu kodu ilerde diger  dodge kodu ile birleştire bilirisin.
        {
            isMoveSkilsUse = true;

            int pushPower = 20;
            if (RB2.gravityScale == 1)  // zemine degmiyor ise çalışır
                pushPower = 6;

            RB2.velocity = new Vector2(-pushPower, RB2.velocity.y);
            
            yield return new WaitForSeconds(0.35f);
            isMoveSkilsUse = false;
        }  
        public IEnumerator DodgeSkils_e()     // e ile sağa Dodge atıyor
        {
            isMoveSkilsUse = true;

            int pushPower = 20;
            if (RB2.gravityScale == 1)  // zemine degmiyor ise çalışır
                pushPower = 6; 

            RB2.velocity = new Vector2(pushPower, RB2.velocity.y);
            
            yield return new WaitForSeconds(0.35f);
            isMoveSkilsUse = false;
        }

        public IEnumerator ArmorFrame()     // Player 4.5 saniyeligine daha az hasar alır.
        {   // Zombi Yeniçeri Özelligi
            isArmorFrameUse = true;
            
            float realArmor = __PlayerScript.armor; // gerçek Armor
            float armorFrameArmor;   // bu Skilin verdigi Armor
             
            float realSpeed = __PlayerScript.speed; 
            
            if (realArmor >= 60f)
                armorFrameArmor = 95f;
            
            else if (realArmor >= 40f)
                armorFrameArmor = 85f;
            
            else
                armorFrameArmor = 75f;
            
            __PlayerScript.armor = realArmor;   // armor'ı bi anda artırıyor ve zamanla düşürüyor.
            __PlayerScript.speed = realSpeed * ( 1 - 0.8f); // speed'i bi anlık düşüyor ve zamanla artırıyor.
            yield return new WaitForSeconds(2f);
            
            __PlayerScript.armor = armorFrameArmor - 10f;
            __PlayerScript.speed = realSpeed * ( 1 - 0.7f);
            yield return new WaitForSeconds(1.5f);
            
            __PlayerScript.armor = armorFrameArmor - 20f;
            __PlayerScript.speed = realSpeed * (1 - 0.6f);
            yield return new WaitForSeconds(1f);
            
            __PlayerScript.armor = realArmor;
            __PlayerScript.speed = realSpeed;
            
            isArmorFrameUse = false;
        }
    }
}

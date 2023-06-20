using System;
using System.Collections;
using UnityEngine;

namespace PlayerScripts.SwordScripts
{
    public class SwordSkilsScript : MonoBehaviour
    {
        private GameObject Player;

        private SwordScript __SwordScript;

        private void Awake()
        {
            Player = this.gameObject;
            __SwordScript = Player.GetComponent<SwordScript>();
        }

        public IEnumerator AttackAndMoveOn(float _moveOnTime, Rigidbody2D RB2) // vurdutkan sonr bir adım öne gidiyor ve 0.35 sn hareket edemiyor.
        {
            __SwordScript.isAttack = true;
            float PushPower = 250f;
            if (RB2.gravityScale == 1) // eger Player havada ise daha az itme kuvveti uygulasın
                PushPower = 100f;

            switch (Player.transform.rotation.y)
            {
                case 1: // sola bakıyor
                    __SwordScript.RB2.AddForce(new Vector2(-PushPower, RB2.velocity.y), ForceMode2D.Impulse);
                    break;

                case 0: // sağa bakıyor
                    __SwordScript.RB2.AddForce(new Vector2(PushPower, RB2.velocity.y), ForceMode2D.Impulse);
                    break;
            }

            yield return new WaitForSeconds(_moveOnTime); // Player kaç sn sonra hareket etmye başlasın, onun zamanı
            // Combo vuruşlarında 3 vuruş var ise sadece ilk vuruşu baz alıcaktır ve ilk vuruşta: isAttack = false; olcaktır. 
            __SwordScript.isAttack = false;
        }
    }
}
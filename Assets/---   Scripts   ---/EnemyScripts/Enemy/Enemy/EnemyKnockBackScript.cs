using System.Collections;
using EnemyScripts.AIScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyScripts.Enemy.Enemy
{
    public class EnemyKnockBackScript : MonoBehaviour
    {
        private AIScript __AIScript;

        private void Awake()
        {
            __AIScript = this.gameObject.GetComponent<AIScript>();
        }

        public IEnumerator KnockBack(Vector2 directionToEnemy, Rigidbody2D RB2, bool isJumpit)
        {
            __AIScript.isKnockBackNotActive = false;

            if (isJumpit) // Enemyi zıplatır
            {
                float jumpPower;
                if (RB2.gravityScale == 1)
                    jumpPower = Random.Range(18f, 20f);
                else
                    jumpPower = Random.Range(25f, 35f);

                RB2.AddForce(new Vector2(RB2.velocity.x, jumpPower), ForceMode2D.Impulse);
            }

            float duration = 1.2f; // Döngünün çalışma süresi
            float elapsedTime = 0f; // Geçen süre
            float pushPower = Random.Range(170f, 220f);

            while (elapsedTime <= duration)
            {
                if (RB2.velocity.magnitude < 6) // bazen çok fazla AddForce uyguluyor, bu if koşulu o hatayı engelliyor.
                    RB2.AddForce(new Vector2(directionToEnemy.x * pushPower, directionToEnemy.y), ForceMode2D.Force);

                yield return new WaitForSeconds(0.02f);
                if (pushPower >= 20)
                {
                    pushPower -= 5; // yavaşça uygulanan kuvveti azaltıyorum. 
                }
                else
                {
                    StartCoroutine(StartMove());
                    elapsedTime += duration;
                }

                elapsedTime += 0.02f; // Geçen süreyi güncelle
            }

            __AIScript.isKnockBackNotActive = true;
        }

        private IEnumerator StartMove() // Enemy'nin hızı 0 dan hızı 1'e gelene kadar yavaşca artacak
        {
            float realSpeed = __AIScript.moveSpeed;

            float _moveSpeed = 0;

            const float duration = 1.2f; // Döngünün çalışma süresi
            float elapsedTime = 0f; // Geçen süre

            while (elapsedTime <= duration)
            {
                _moveSpeed += 0.1f;

                if (_moveSpeed <= realSpeed) // hızı 0.1 den başlayarak yavaşca 1'e kadar artacak.
                    __AIScript.moveSpeed = _moveSpeed;
                else
                    elapsedTime += duration;

                yield return new WaitForSeconds(0.02f);

                elapsedTime += 0.02f; // Geçen süreyi güncelle
            }

            __AIScript.moveSpeed = realSpeed;
        }
    }
}
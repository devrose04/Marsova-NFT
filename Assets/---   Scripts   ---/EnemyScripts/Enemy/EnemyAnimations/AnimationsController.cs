using ______Scripts______.EnemyScripts.Enemy.EnemyAnimationsScripts;
using EnemyScripts.AIScripts;
using EnemyScripts.OwnScript;
using UnityEngine;

namespace ______Scripts______.EnemyScripts.Enemy.EnemyAnimations
{
    public class AnimationsController : MonoBehaviour
    {
        public void EnemyAnimations(GameObject enemy)
        {
            AIScript __AIScript = enemy.GetComponent<AIScript>();

            if (enemy.CompareTag("Giant"))
            {
                GiantAnimations _giantAnimations = enemy.gameObject.GetComponent<GiantAnimations>();
                _giantAnimations.SetBoolParameter("isWaitingInTheBase", __AIScript.isWaitingInTheBase);
            }
            else if (enemy.CompareTag("Smale"))
            {
                SmaleAnimations _smaleAnimations = enemy.gameObject.GetComponent<SmaleAnimations>();
                _smaleAnimations.SetBoolParameter("isWaitingInTheBase", __AIScript.isWaitingInTheBase);
            }
        }

        public void SalyangozTurtleActive(GameObject enemy)
        {
            AIScript __AIScript = enemy.GetComponent<AIScript>();

            if (enemy.CompareTag("Salyangoz"))
            {
                SalyangozAnimations _salyangozAnimations = enemy.gameObject.GetComponent<SalyangozAnimations>();
                _salyangozAnimations.SetBoolParameter("isEnemySeePlayer", __AIScript.isEnemySeePlayer);
            }
        }

        public void AnimationSpeedUp(GameObject enemy, bool isEnemySeePlayer)
        {
            if (enemy.GetComponent<Animator>() != null)
            {
                Animator _animator = enemy.GetComponent<Animator>();

                if (isEnemySeePlayer == true)
                    _animator.speed = 1f;
                else // EnemyPlayer'ı görmüyor ise çalışır
                    _animator.speed = 0.8f;
            }
        }
    }
    //todo: distance 10 dan fazla ise yavaş hareket etsin
}
using UnityEngine;

namespace ______Scripts______.EnemyScripts.Enemy.EnemyAnimations
{
    public class SalyangozAnimations : MonoBehaviour
    {
        private Animator salyangozAnimator;

        private void Awake()
        {
            salyangozAnimator = this.gameObject.GetComponent<Animator>();
        }

        public void SetBoolParameter(string parameterName, bool value)
        {
            salyangozAnimator.SetBool(parameterName,value);
        }
    }
}

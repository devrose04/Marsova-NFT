using UnityEngine;

namespace ______Scripts______.EnemyScripts.Enemy.EnemyAnimationsScripts
{
    public class SmaleAnimations : MonoBehaviour
    {
        private Animator smaleAnimator;

        private void Awake()
        {
            smaleAnimator = this.gameObject.GetComponent<Animator>();
        }

        public void SetBoolParameter(string parameterName, bool value)
        {
            smaleAnimator.SetBool(parameterName,value);
        }
    }
}

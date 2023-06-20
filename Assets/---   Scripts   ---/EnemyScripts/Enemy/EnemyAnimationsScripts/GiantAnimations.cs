using System;
using UnityEngine;

namespace ______Scripts______.EnemyScripts.Enemy.EnemyAnimationsScripts
{
    public class GiantAnimations : MonoBehaviour
    {
        private Animator giantAnimator;

        private void Awake()
        {
            giantAnimator = this.gameObject.GetComponent<Animator>();
        }

        public void SetBoolParameter(string parameterName, bool value)
        {
            giantAnimator.SetBool(parameterName,value);
        }
    }
}

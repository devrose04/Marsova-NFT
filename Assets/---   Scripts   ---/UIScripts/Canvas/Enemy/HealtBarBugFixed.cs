using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Enemy
{
    public class HealtBarBugFixed : MonoBehaviour
    {
        private GameObject Bar;

        private Vector3 originalScale;

        private float Scale_x;
        private float Scale_y;
        private float Scale_z;

        private void Awake()
        {
            Bar = this.gameObject;
            originalScale = Bar.transform.localScale;

            Scale_x = originalScale.x;
            Scale_y = originalScale.y;
            Scale_z = originalScale.z;
        }

        public void TurnRight()
        {
            Bar.transform.localScale = new Vector3(Scale_x, Scale_y, Scale_z);
        }

        public void TurnLeft()
        {
            Bar.transform.localScale = new Vector3(-Scale_x, -Scale_y, -Scale_z);
        }
    }
}
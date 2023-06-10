using UnityEngine;

namespace UIScripts
{
    public class IsGroundTouchScript : MonoBehaviour
    {
        public bool isGroundTouchBool;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
                isGroundTouchBool = true;
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
                isGroundTouchBool = false;
        }

    }
}

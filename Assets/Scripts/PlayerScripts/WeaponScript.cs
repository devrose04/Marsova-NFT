using UnityEngine;

namespace PlayerScripts
{
    public class WeaponScript : MonoBehaviour
    {
        [SerializeField] float offset;
        [SerializeField] private Transform player; // Takip edilecek karakterin Transform bile�eni

        public void MYUpdate() 
        {
            Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (difference.x > 0)
                this.GetComponent<SpriteRenderer>().flipY = false;
            else
                this.GetComponent<SpriteRenderer>().flipY = true;

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

            this.gameObject.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.25f);
        }
    }
}
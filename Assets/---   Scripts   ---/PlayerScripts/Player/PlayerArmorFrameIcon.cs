using UnityEngine;

namespace ______Scripts______.PlayerScripts.Player
{
    public class PlayerArmorFrameIcon : MonoBehaviour
    {
        [SerializeField] private GameObject ArmorUppIcon;
        [SerializeField] private GameObject JetPackOffIcon;

        public void ArmorUppIconActive()
        {
            ArmorUppIcon.SetActive(true);
        }

        public void ArmorUppIconDeAcive()
        {
            ArmorUppIcon.SetActive(false);
        }


        public void JetPackOffIconActive()
        {
            JetPackOffIcon.SetActive(true);
        }

        public void JetPackOffIconDeActive()
        {
            JetPackOffIcon.SetActive(false);
        }
    }
}
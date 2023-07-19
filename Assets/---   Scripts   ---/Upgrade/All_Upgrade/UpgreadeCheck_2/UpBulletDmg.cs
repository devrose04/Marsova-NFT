using ______Scripts______.PlayerScripts.PlayerLaserAbout;
using UnityEngine;
using UnityEngine.Events;

namespace ______Scripts______.Upgrade.All_Upgrade.UpgreadeCheck_2
{
    public class UpBulletDmg : MonoBehaviour, IUpgrade_Chose_2
    {
        [SerializeField] private StartShipAttack _startShipAttack;

        private string MainSentece;
        private string ExtraSentecen;

        void CheckText()
        {
            MainSentece = "Increase the damage of the Space Laser bullet by 4.";
            ExtraSentecen = $"{_startShipAttack.extraBullletDamges} --> {_startShipAttack.extraBullletDamges + 4}";
        }

        void UppDamges()
        {
            _startShipAttack.extraBullletDamges += 4;
        }

        public (string, string, UnityAction) Upgrade()
        {
            CheckText();
            return (MainSentece, ExtraSentecen, UppDamges);
        }
    }
}
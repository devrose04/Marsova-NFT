using ______Scripts______.PlayerScripts.PlayerLaserAbout;
using ______Scripts______.PlayerScripts.SwordScripts;
using UnityEngine;
using UnityEngine.Events;

namespace ______Scripts______.Upgrade.All_Upgrade.UpgreadChech_1
{
    public class UpBulletAmount : MonoBehaviour, IUpgrade_Chose_1
    {
        [SerializeField] private StartShipAttack _startShipAttack;

        private string MainSentece;
        private string ExtraSentecen;

        void CheckText()
        {
            MainSentece = "Increase the quantity of Space Laser by 8.";
            ExtraSentecen = $"{_startShipAttack.totalBullets} --> {_startShipAttack.totalBullets + 8}";
        }

        void UppDamges()
        {
            _startShipAttack.extraAmountOfBullets += 8;
        }

        public (string, string, UnityAction) Upgrade()
        {
            CheckText();
            return (MainSentece, ExtraSentecen, UppDamges);
        }
    }
}
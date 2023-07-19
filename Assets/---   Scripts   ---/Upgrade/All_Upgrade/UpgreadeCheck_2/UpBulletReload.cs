using ______Scripts______.PlayerScripts.PlayerLaserAbout;
using UnityEngine;
using UnityEngine.Events;

namespace ______Scripts______.Upgrade.All_Upgrade.UpgreadeCheck_2
{
    public class UpBulletReload : MonoBehaviour, IUpgrade_Chose_2
    {
        [SerializeField] private StartShipAttack _startShipAttack;

        private string MainSentece;
        private string ExtraSentecen;

        void CheckText()
        {
            MainSentece = "Reduce the recharge time of the Space Laser by 4.";
            ExtraSentecen = $"{_startShipAttack.ShipReloadTimeCD} --> {_startShipAttack.ShipReloadTimeCD - 4}";
        }

        void DownCD()
        {
            _startShipAttack.ShipReloadTimeCD -= 4;
        }

        public (string, string, UnityAction) Upgrade()
        {
            CheckText();
            return (MainSentece, ExtraSentecen, DownCD);
        }
    }
}
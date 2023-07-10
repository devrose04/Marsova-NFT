using ______Scripts______.PlayerScripts.PlayerLaserAbout;
using PlayerScripts.PlayerLaserAbout;
using UnityEngine;
using UnityEngine.UI;

namespace ______Scripts______.UIScripts.Canvas
{
    public class StarShipBar : MonoBehaviour
    {
        private GameObject StartShip;
        private StartShipAttack _startShipAttack;

        [SerializeField] private Slider _startShipBar;

        private void Awake()
        {
            StartShip = this.gameObject;
            _startShipAttack = StartShip.GetComponent<StartShipAttack>();
        }

        public void StartShipFonk()
        {
            if (_startShipAttack.amountOfBullets == 30) // bu if koşulu bir hatayı önlediginden yaptım.
                _startShipBar.value = _startShipAttack.ShipReloadTimeCD;
            else
            {
                _startShipBar.value = _startShipAttack.ShipReloadTimeCD - _startShipAttack.ShipReloadTheBulletTimer;
                _startShipBar.maxValue = _startShipAttack.ShipReloadTimeCD;
            }
        }
    }
}
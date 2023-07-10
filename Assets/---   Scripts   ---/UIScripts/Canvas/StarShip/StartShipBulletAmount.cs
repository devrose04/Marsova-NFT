using System;
using ______Scripts______.PlayerScripts.PlayerLaserAbout;
using PlayerScripts.PlayerLaserAbout;
using UnityEngine;
using UnityEngine.UI;

namespace ______Scripts______.UIScripts.Canvas
{
    public class StartShipBulletAmount : MonoBehaviour
    {
        private GameObject StartShip;
        private StartShipAttack _startShipAttack;

        [SerializeField] private Text _text;

        [SerializeField] private GameObject Block2;
        [SerializeField] private GameObject Block3;

        private void Awake()
        {
            StartShip = this.gameObject;
            _startShipAttack = StartShip.GetComponent<StartShipAttack>();
        }

        public void StartShipBulletFonk()
        {
            _text.text = _startShipAttack.amountOfBullets.ToString();

            if (_startShipAttack.amountOfBullets <= 10)
                Block2.SetActive(false);
            else if (_startShipAttack.amountOfBullets <= 20)
                Block3.SetActive(false);
            else
            {
                Block2.SetActive(true);
                Block3.SetActive(true);
            }
        }
    }
}
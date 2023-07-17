using ______Scripts______.PlayerScripts.SwordScripts;
using UnityEngine;
using UnityEngine.UI;

namespace ______Scripts______.SelectUpgrade.All_Upgrades
{
    public class UpgreadPlayerDamages : MonoBehaviour //Chose_1
    {
        private string MainSentence;
        private string extraSentece;

        [SerializeField] private Button _button;

        [SerializeField] private Text _senteceText;
        [SerializeField] private Text _extraSenteceText;

        [SerializeField] private GameObject UpgreadeMenu;

        [SerializeField] private SwordScript _swordScript;

        void Start()
        {
            _button.onClick.AddListener(_UpgreadPlayerDamages);
        }

        public void CheckText() // ekran açıldıgında textlte yükle
        {
            MainSentence = "Increase the damage of the player's sword strikes by 3.";
            extraSentece = $"{_swordScript.swordDamage} --> {_swordScript.swordDamage + 3}";

            _senteceText.text = MainSentence;
            _extraSenteceText.text = extraSentece;
        }

        public void _UpgreadPlayerDamages()
        {
            _swordScript.swordDamage += 3;
            UpgreadeMenu.SetActive(false);
        }
    }
}
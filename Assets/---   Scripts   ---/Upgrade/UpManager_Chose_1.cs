using System;
using ______Scripts______.Upgrade.All_Upgrade;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ______Scripts______.Upgrade
{
    public class UpManager_Chose_1 : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text MainSentence;
        [SerializeField] private Text ExtraSentence;

        [SerializeField] private GameObject ChoseMenu;

        private string _mainSentece;
        private string _extraSentecen;

        private UnityAction _action;

        [SerializeField] private GameObject Chose_1_Manager;
        private UpDmg _upDmg;

        private void Awake()
        {
            // _upDmg = Chose_1_Manager.GetComponent<UpDmg>();

            // UploadData(_upDmg);
            // WriteText();
            // ClickUptadeButton();
        }

        public void UploadData(Component ScriptName)
        {
            if (ScriptName != null)
            {
                IUpgrade_Chose_1 _upgradeScript_1 = ScriptName.GetComponent<IUpgrade_Chose_1>();

                if (_upgradeScript_1 != null)
                {
                    _mainSentece = _upgradeScript_1.Upgrade().Item1;
                    _extraSentecen = _upgradeScript_1.Upgrade().Item2;
                    _action = _upgradeScript_1.Upgrade().Item3;
                }
            }
            else
                print("Eror");
        }

        public void WriteText() // todo: açılma  sesi ekle
        {
            MainSentence.text = _mainSentece;
            ExtraSentence.text = _extraSentecen;
            ChoseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        public void ClickUptadeButton()
        {
            _button.onClick.AddListener(RunAction);
        }

        public void RunAction() // todo: kapanma sesi ekle
        {
            _action.Invoke();
            ChoseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
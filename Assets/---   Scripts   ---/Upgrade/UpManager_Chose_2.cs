using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ______Scripts______.Upgrade
{
    public class UpManager_Chose_2 : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text MainSentence;
        [SerializeField] private Text ExtraSentence;

        [SerializeField] private GameObject ChoseMenu;

        private string _mainSentece;
        private string _extraSentecen;

        private UnityAction _action;

        (string, string, UnityAction) Upgrade;

        public void AllTogether(Component _ScriptName)
        {
            UploadData(_ScriptName);
            WriteText();
            ClickUptadeButton();
        }

        void UploadData(Component ScriptName)
        {
            if (ScriptName != null)
            {
                IUpgrade_Chose_2[] Interface = ScriptName.GetComponents<IUpgrade_Chose_2>();

                if (Interface != null)
                {
                    foreach (var Script in Interface)
                    {
                        if (ScriptName == Script)
                        {
                            Upgrade = Script.Upgrade();

                            _mainSentece = Upgrade.Item1;
                            _extraSentecen = Upgrade.Item2;
                            _action = Upgrade.Item3;
                        }
                    }
                }
            }
            else
                print("Eror");
        }

        void WriteText() // todo: açılma  sesi ekle
        {
            MainSentence.text = _mainSentece;
            ExtraSentence.text = _extraSentecen;
            // ChoseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        void ClickUptadeButton()
        {
            _button.onClick.AddListener(RunAction);
        }

        void RunAction() // todo: kapanma sesi ekle
        {
            _action.Invoke();
            ChoseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
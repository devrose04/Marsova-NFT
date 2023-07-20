using System;
using System.Collections;
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

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClipOpen;
        [SerializeField] private AudioClip _audioClipClose;

        [SerializeField] private GameObject ButtonGameObject;

        private string _mainSentece;
        private string _extraSentecen;

        private UnityAction _action;

        (string, string, UnityAction) Upgrade;

        private int BugFixed_i = 0;

        public void AllTogether(Component _ScriptName)
        {
            StartCoroutine(DelayedCall());
            UploadData(_ScriptName);
            WriteText();
            ClickUptadeButton();
        }

        void UploadData(Component ScriptName)
        {
            if (ScriptName != null)
            {
                IUpgrade_Chose_1[] Interface = ScriptName.GetComponents<IUpgrade_Chose_1>();

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

        void WriteText()
        {
            _audioSource.PlayOneShot(_audioClipOpen);
            MainSentence.text = _mainSentece;
            ExtraSentence.text = _extraSentecen;
            ChoseMenu.SetActive(true);
            // Time.timeScale = 0;
            BugFixed_i = 0;
        }

        void ClickUptadeButton()
        {
            _button.onClick.AddListener(RunAction);
        }

        void RunAction()
        {
            if (BugFixed_i == 0)
            {
                BugFixed_i = 1;
                _audioSource.PlayOneShot(_audioClipClose);
                _action.Invoke();
                ButtonGameObject.SetActive(false);
                ChoseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }

        IEnumerator DelayedCall()
        {
            // 1 saniye beklet
            yield return new WaitForSecondsRealtime(1f);

            // İlgili işlemi gerçekleştir
            LateSetActiveButton();
        }

        void LateSetActiveButton()
        {
            ButtonGameObject.SetActive(true);
        }
    }
}
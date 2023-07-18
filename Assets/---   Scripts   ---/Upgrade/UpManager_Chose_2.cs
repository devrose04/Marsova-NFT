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

        private IUprade_Chose_2 _upradeChose2;

        private string _mainSentece;
        private string _extraSentecen;

        private UnityAction _action;

        private void Awake()
        {
            _upradeChose2 = this.gameObject.GetComponentInChildren<IUprade_Chose_2>();
        }

        public void UploadData()
        {
            _mainSentece = _upradeChose2.Upgrade().Item1;
            _extraSentecen = _upradeChose2.Upgrade().Item2;
            _action = _upradeChose2.Upgrade().Item3;
        }

        public void WriteText() // todo: açılma  sesi ekle
        {
            MainSentence.text = _mainSentece;
            ExtraSentence.text = _extraSentecen;
            ChoseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        public void RunAction() // todo: kapanma sesi ekle
        {
            _button.onClick.AddListener(_action);
            ChoseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
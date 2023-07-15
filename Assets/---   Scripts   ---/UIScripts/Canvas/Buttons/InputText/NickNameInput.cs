using System.Collections.Generic;
using ______Scripts______.DataBaseAssets.dreamlo.ScoreBoard;
using ______Using_Assets______.DataBaseAssets.dreamlo;
using ______Using_Assets______.DataBaseAssets.dreamlo.ScoreBoard;
using UnityEngine;
using UnityEngine.UI;

namespace ______Scripts______.UIScripts.Canvas.Buttons.InputText
{
    public class NickNameInput : MonoBehaviour
    {
        [SerializeField] private HighScores _highScores;
        [SerializeField] private GameObject _goButton;
        [SerializeField] private Text _text;

        [HideInInspector] public string NickName;

        private string ErorColor = "#FD5A45"; // Eror Red Color
        private string GreenColor = "#8DFD45";

        private int SpaceCount = 0;
        private int LenghtToWord = 0;

        private bool isSpaceRuleTrue = false;
        private bool isLenghtRuleTrue = false;
        private bool isNickNameNotUsing = false;

        private bool CanYouTakeThisName = false; // todo: bu True ise buton çalışsın.

        public void InputNickName(string InputNick)
        {
            NickName = InputNick;

            CheckSpace();
            CheckLenght();
            CheckName();
            YouCanTakeThisName();
        }

        void CheckSpace()
        {
            foreach (var Alfabe in NickName)
            {
                if (Alfabe == ' ')
                    SpaceCount++;

                if (SpaceCount <= 1)
                    isSpaceRuleTrue = true;
                else
                {
                    _text.text = "You have 1 space char and you used it";
                    ChangeTextErorColor();
                    // print("1 boşluk bırakma hakkınız var, onu aştınız");
                    isSpaceRuleTrue = false;
                }

                SpaceCount = 0; // bir sonraki input girdiginde, doğru sonucu versin diye bunu yazdım
            }
        }

        void CheckLenght()
        {
            LenghtToWord = 0; // bir sonraki input girdiginde, doğru sonucu versin diye bunu yazdım

            foreach (var Alfabe in NickName)
            {
                LenghtToWord++;
            }

            if (LenghtToWord < 4)
            {
                isLenghtRuleTrue = false;
                _text.text = "You need to enter a minimum of 4 characters.";
                // print("minimum 4 karakter girmen lazım.");
                ChangeTextErorColor();
            }
            else if (LenghtToWord > 14)
            {
                isLenghtRuleTrue = false;
                _text.text = "You can enter a maximum of 14 characters.";
                // print("max 14 karakter girebilirsin ");
                ChangeTextErorColor();
            }
            else
            {
                isLenghtRuleTrue = true;
            }
        }

        void CheckName()
        {
            PlayerScore[] _scoreList = _highScores.scoreList;

            isNickNameNotUsing = true;

            foreach (var list in _scoreList)
            {
                if (list.username == NickName)
                {
                    isNickNameNotUsing = false;
                    _text.text = "This username is already taken. Please enter a different NickName.";
                    // print("bu kullanıcı adı mevcut, başka bir isim giriniz.");
                    ChangeTextErorColor();
                }
            }
        }

        void YouCanTakeThisName()
        {
            if (isSpaceRuleTrue == true && isLenghtRuleTrue == true && isNickNameNotUsing == true)
                CanYouTakeThisName = true;
            else
                CanYouTakeThisName = false;

            if (CanYouTakeThisName == true)
            {
                _goButton.SetActive(true);
                ChangeTextGreenColor();
                _text.text = "Perfect";
            }
            else
                _goButton.SetActive(false);
        }

        void ChangeTextErorColor()
        {
            // Rengi string olarak tanımlanan hex değerinden al
            Color color;
            if (ColorUtility.TryParseHtmlString(ErorColor, out color))
            {
                // Text bileşeninin rengini ayarla
                _text.color = color;
            }
            else
                Debug.LogError("Geçersiz renk değeri: " + ErorColor);
        }

        void ChangeTextGreenColor()
        {
            // Rengi string olarak tanımlanan hex değerinden al
            Color color;
            if (ColorUtility.TryParseHtmlString(GreenColor, out color))
            {
                // Text bileşeninin rengini ayarla
                _text.color = color;
            }
            else
                Debug.LogError("Geçersiz renk değeri: " + ErorColor);
        }
    }
}
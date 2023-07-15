using ______Scripts______.UIScripts.Canvas.Buttons.InputText;
using UnityEngine;

namespace ______Scripts______.UIScripts.Canvas.Buttons.CloseButton
{
    public class CloseNickName_GoButton : MonoBehaviour
    {
        [SerializeField] private GameObject _nickNameMenu;
        [SerializeField] private NickNameInput _nickNameInput;

        public void CloseNickNameMenu()
        {
            PlayerPrefs.SetString("NickName", _nickNameInput.NickName);
            _nickNameMenu.SetActive(false);
        }
    }
}
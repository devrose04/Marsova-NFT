using System;
using ______Scripts______.GameManagerScript.SkillsScripts;
using UnityEngine;
using UnityEngine.UI;

namespace ______Scripts______.UIScripts.Canvas.Player
{
    public class JetPackBar : MonoBehaviour
    {
        private GameObject GameManager;
        [SerializeField] private Slider Bar;

        private SkillsScript _skillsScript;

        private void Awake()
        {
            GameManager = GameObject.Find("GameManager");
            _skillsScript = GameManager.GetComponent<SkillsScript>();
        }

        public void JetPackBarUpdate()
        {
            Bar.value = _skillsScript.JetPackFuel;
        }
    }
}
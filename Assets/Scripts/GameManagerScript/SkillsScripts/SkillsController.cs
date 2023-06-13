using System;
using PlayerScripts;
using PlayerScripts.SwordScripts;
using UIScripts;
using UnityEngine;

namespace GameManagerScript.SkillsScripts
{
    public class SkillsController : MonoBehaviour
    {
        private GameObject Player;
        private GameObject GameManager;

        private SkillsScript __SkillsScript;
        private ButtonScript __ButtonScript;
        private SwordController __SwordController;
        private SkillsDataScript __SkillsData;

        private void Awake()
        {
            GameManager = GameObject.Find("GameManager");
            Player = GameObject.Find("Player");
            __SwordController = Player.GetComponent<SwordController>();
            __ButtonScript = GameManager.GetComponent<ButtonScript>();
            __SkillsScript = GameManager.GetComponent<SkillsScript>();
            __SkillsData = GameManager.GetComponent<SkillsDataScript>();
        }

        // *** Burda, skillerin en son ne zaman basıldığı veya aktif olup kullanıldıgının verileri aktarılır.
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void HittingAll1_ctrl()
        {
            __SkillsData.HittingAllCanUse1  =
                __ButtonScript.ButtonCanUse_A(__SwordController.HittingAll1, __SkillsData.HittingAllCD1, __SkillsData.HittingAllCanUse1);
        }

        public void HittingAll2_ctrl()
        {
            (__SkillsData.HittingAll2,__SkillsData.HittingAllCanUse2) =
                __ButtonScript.ManyPressButton(null, __SwordController.HittingAll2, null, 1f, 2, __SkillsData.HittingAll2, __SkillsData.HittingAllCD2, __SkillsData.HittingAllCanUse2);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void SwordAttack1_ctrl()
        {
            __SkillsData.SwordCanUse1 =
                __ButtonScript.ButtonCanUse_A(__SwordController.SwordAttack1, __SkillsData.SwordCD1, __SkillsData.SwordCanUse1);
        }

        public void SwordAttack2_and_SwordAttack3_ctrl()
        {
            (__SkillsData.SwordCanUse2, __SkillsData.Sword2) =
                __ButtonScript.ManyPressButton(__SwordController.SwordAttack2, __SwordController.SwordAttack3, null, 1.5f, 3, __SkillsData.Sword2, __SkillsData.SwordCD2, __SkillsData.SwordCanUse2);
        }

        public void ArmorFrame_ctrl()
        {
            __SkillsData.ArmorFrameCanUse = __ButtonScript.ButtonCanUse_C(__SkillsScript.ArmorFrame, __SkillsData.ArmorFrameCD, __SkillsData.ArmorFrameCanUse);
        }

        public void DashAtack_ctrl()
        {
            (__SkillsData.DashAttack ,__SkillsData.DashAttackCanUse) =
                __ButtonScript.ManyPressButton(null, null, __SkillsScript.DashAtack, 0.4f, 3, __SkillsData.DashAttack, __SkillsData.DashAtackCD, __SkillsData.DashAttackCanUse);
        }

        public void DodgeSkils_q_ctrl()
        {
           __SkillsData.DodgeCanUse = __ButtonScript.ButtonCanUse_C(__SkillsScript.DodgeSkils_q, __SkillsData.DodgeCD, __SkillsData.DodgeCanUse);
        }

        public void DodgeSkils_e_ctrl()
        {
            __SkillsData.DodgeCanUse = __ButtonScript.ButtonCanUse_C(__SkillsScript.DodgeSkils_e, __SkillsData.DodgeCD, __SkillsData.DodgeCanUse);
        }
    }
}
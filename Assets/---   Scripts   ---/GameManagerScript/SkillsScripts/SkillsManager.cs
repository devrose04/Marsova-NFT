using System;
using UnityEngine;

namespace GameManagerScript.SkillsScripts
{
    public class SkillsManager : MonoBehaviour
    {
        GameObject GameManager;
        private SkillsController __SkillsController;
        private SkillsDataScript __SkillsData;
        private void Awake()
        {
            GameManager = this.gameObject;
            __SkillsController = GameManager.GetComponent<SkillsController>();
            __SkillsData = GameManager.GetComponent<SkillsDataScript>();
        }
        
        // *** Burda skillere en son basılma ve skilin aktıf olarak kullanılma verileri aktarılır.

        public void HittingAll1_manager()
        {
            __SkillsData.HittingAllCanUse1 = __SkillsController.HittingAll1_ctrl();
        }

        public void HittingAll2_manager()
        {
            (__SkillsData.HittingAll2, __SkillsData.HittingAllCanUse2) = __SkillsController.HittingAll2_ctrl();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void SwordAttack1_manager()
        {
            __SkillsData.SwordCanUse1 = __SkillsController.SwordAttack1_ctrl();
        }

        public void SwordAttack2_and_SwordAttack3_manager()
        {
            (__SkillsData.Sword2, __SkillsData.SwordCanUse2) = __SkillsController.SwordAttack2_and_SwordAttack3_ctrl();
        }

        public void ArmorFrame_manager()
        {
            __SkillsData.ArmorFrameCanUse = __SkillsController.ArmorFrame_ctrl();
        }

        public void DashAtack_manager()
        {
            (__SkillsData.DashAttack, __SkillsData.DashAttackCanUse) = __SkillsController.DashAtack_ctrl();
        }

        public void DodgeSkils_q_manager()
        {
            __SkillsData.DodgeCanUse = __SkillsController.DodgeSkils_q_ctrl();
        }

        public void DodgeSkils_e_manager()
        {
            __SkillsData.DodgeCanUse = __SkillsController.DodgeSkils_e_ctrl();
        }
    }
}

using System;
using ______Scripts______.Upgrade.All_Upgrade;
using ______Scripts______.Upgrade.All_Upgrade.UpgreadChech_1;
using ______Scripts______.Upgrade.All_Upgrade.UpgreadeCheck_2;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace ______Scripts______.Upgrade
{
    public class UpController : MonoBehaviour
    {
        // Chose_1
        [SerializeField] private UpDmg _upDmg;
        [SerializeField] private UpSpeed _upSpeed;
        [SerializeField] private UpBulletAmount _bulletAmount;
        [SerializeField] private UpHealth _upHealth;
        [SerializeField] private UpDroneDmg _droneDmg;
        [SerializeField] private UpLaserAttackTime _attackTime;

        // Chose_2
        [SerializeField] private UpArmor _upArmor;
        [SerializeField] private UpBulletDmg _bulletDmg;
        [SerializeField] private UpBulletReload _bulletReload;
        [SerializeField] private UpActiveDroneTime _activeDroneTime;
        [SerializeField] private UpDroneAttackRadius _attackRadius;
        [SerializeField] private UpDroneAttackTime_ attackTimeDrone;


        [SerializeField] private UpManager_Chose_1 _upManagerChose1;
        [SerializeField] private UpManager_Chose_2 _upmanagerChose2;

        private int RandomCount_1;
        private int RandomCount_2;

        private int oldCount_1 = 0;
        private int oldCount_2 = 0;

        public void ShowUpdateMenu()
        {
            SelectUpgrade_1();
            SelectUpgrade_2();
        }

        void SelectUpgrade_1()
        {
            SelectRandomCount_1();
            CheckSameCount_1();
            DataSave_1();

            switch (RandomCount_1)
            {
                case 1:
                    _upManagerChose1.AllTogether(_upDmg);
                    break;
                case 2:
                    _upManagerChose1.AllTogether(_upSpeed);
                    break;
                case 3:
                    _upManagerChose1.AllTogether(_bulletAmount);
                    break;
                case 4:
                    _upManagerChose1.AllTogether(_upHealth);
                    break;
                case 5:
                    _upManagerChose1.AllTogether(_droneDmg);
                    break;
                case 6:
                    _upManagerChose1.AllTogether(_attackTime);
                    break;
            }
        }

        void SelectUpgrade_2()
        {
            SelectRandomCount_2();
            CheckSameCount_2();
            DataSave_2();

            switch (RandomCount_2)
            {
                case 1:
                    _upmanagerChose2.AllTogether(_upArmor);
                    break;
                case 2:
                    _upmanagerChose2.AllTogether(_bulletDmg);
                    break;
                case 3:
                    _upmanagerChose2.AllTogether(_bulletReload);
                    break;
                case 4:
                    _upmanagerChose2.AllTogether(_activeDroneTime);
                    break;
                case 5:
                    _upmanagerChose2.AllTogether(_attackRadius);
                    break;
                case 6:
                    _upmanagerChose2.AllTogether(attackTimeDrone);
                    break;
            }
        }

        void SelectRandomCount_1()
        {
            RandomCount_1 = Random.Range(1, 7);
        }

        void SelectRandomCount_2()
        {
            RandomCount_2 = Random.Range(1, 7);
        }

        void DataSave_1()
        {
            oldCount_1 = RandomCount_1;
        }

        void DataSave_2()
        {
            oldCount_2 = RandomCount_2;
        }

        void CheckSameCount_1()
        {
            if (oldCount_1 == RandomCount_1)
            {
                SelectUpgrade_1();
                return;
            }
        }

        void CheckSameCount_2()
        {
            if (oldCount_2 == RandomCount_2)
            {
                SelectUpgrade_2();
                return;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using ______Scripts______.EnemyScripts.Enemy.Enemy;
using ______Scripts______.PlayerScripts.PlayerLaserAbout.Drone;
using EnemyScripts.Enemy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerScripts.PlayerLaserAbout.Drone
{
    public class EnemyDetector : MonoBehaviour
    {
        public float detectionRadius = 8; //Canvastan değiştir 
        private List<GameObject> detectedEnemies = new List<GameObject>();

        [SerializeField] private ParticleSystem BigElectric;
        [SerializeField] private ParticleSystem SmaleElectric;

        [SerializeField] private GameObject LookAtThis;

        private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClipEllectric;

        private Collider2D[] hitColliders;

        public bool DroneIsReadyToAttack = false;

        private float time;

        public int JustOneTimeWork; // Drone Player'ın yanında durmasına yarıcak değişken

        private DroneScript _droneScript;
        private GameObject Player;

        private void Awake()
        {
            _droneScript = this.gameObject.GetComponent<DroneScript>();
            Player = GameObject.Find("Player");
            _audioSource = this.gameObject.GetComponent<AudioSource>();
        }

        public void MYUpdate()
        {
            if (DroneIsReadyToAttack)
            {
                CalculationsWhichEnemiesAround();
                time += Time.deltaTime;
                if (time > 2)
                {
                    time = 0;
                    AttackingDrone();
                }
            }
        }

        void CalculationsWhichEnemiesAround()
        {
            // Enemy katmanındaki objeleri algılamak için bir küre yayılımı kullanın
            hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, LayerMask.GetMask("Enemy"));

            AddToList();

            AreThereEnemies();

            RemoveToList();
        }

        void AttackingDrone()
        {
            foreach (var enemy in detectedEnemies)
            {
                ParticleSystem _smaleElectric = Instantiate(SmaleElectric, enemy.transform.position, transform.rotation);
                Destroy(_smaleElectric.gameObject, 3f);

                enemy.GetComponent<EnemyScript>().TakeDamages(5, new Vector2(0, 0), false);
            }

            if (detectedEnemies != null)
            {
                _audioSource.PlayOneShot(_audioClipEllectric);
                ParticleSystem _bigElectric = Instantiate(BigElectric, this.transform.position, transform.rotation);
                Destroy(_bigElectric.gameObject, 3f);
            }
        }

        void OnDrawGizmosSelected()
        {
            // Gizmos üzerinde algılama yarıçapını göstermek için bir çember çizin
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }


        void AddToList()
        {
            // Algılanan her bir obje için işlem yapın
            foreach (Collider2D collider in hitColliders)
            {
                GameObject enemy = collider.gameObject;

                // Eğer daha önce algılanmamışsa listeye ekle
                if (!detectedEnemies.Contains(enemy))
                {
                    detectedEnemies.Add(enemy);
                }
            }
        }

        void AreThereEnemies()
        {
            if (detectedEnemies.Count == 0 && JustOneTimeWork == 0) // burda Etrafta hiç bir Enemy yok ise Drone Player'ın yanında dursun.
                _droneScript.EnemiesAreThere = false;
            else // burda 1 tane bile enemy görürse ise JustOneTimeWork'ü artırıyorum ki bir daha çalışmasın diye
            {
                _droneScript.EnemiesAreThere = true;
                JustOneTimeWork = 1;
            }
        }

        void RemoveToList()
        {
            // Algılanan objeleri kontrol edin ve listeden çıkarın
            for (int i = detectedEnemies.Count - 1; i >= 0; i--)
            {
                GameObject enemy = detectedEnemies[i];

                if (enemy == null) // Enemy ölmüş ise listeden çıkar    
                    detectedEnemies.RemoveAt(i);
                // Eğer obje hala algılanıyorsa ve 10 metrelik alandan çıkmışsa listeden çıkar
                else if (Vector2.Distance(transform.position, enemy.transform.position) > detectionRadius)
                    detectedEnemies.RemoveAt(i);
            }
        }

        public void ClearList()
        {
            detectedEnemies.Clear();
        }
    }
}
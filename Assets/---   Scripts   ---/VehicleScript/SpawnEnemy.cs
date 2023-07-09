using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ______Scripts______.VehicleScript
{
    public class SpawnEnemy : MonoBehaviour
    {
        private GameObject GameManager;
        private GameManager _gameManager;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClipSpawn;

        [SerializeField] private Transform SpawnPosition;

        [SerializeField] private GameObject Bee;
        [SerializeField] private GameObject White;
        [SerializeField] private GameObject Giant;
        [SerializeField] private GameObject Salyangoz;
        [SerializeField] private GameObject Ahtapot;
        [SerializeField] private GameObject Smale;

        private int SpanwLimitCount;

        private void Awake()
        {
            GameManager = GameObject.Find("GameManager");
            SpanwLimitCount = GameManager.GetComponent<GameManager>()._SpanwLimitCount;
            _gameManager = GameManager.GetComponent<GameManager>();
        }

        public IEnumerator SpawnEnemyForBigVehicle()
        {
            SpawnEneym();
            yield return new WaitForSeconds(1.5f);
            SpawnEneym();
            yield return new WaitForSeconds(1.5f);
            SpawnEneym();
            yield return new WaitForSeconds(1.5f);
            SpawnEneym();
            yield return new WaitForSeconds(1.5f);
            SpawnEneym();
            yield return new WaitForSeconds(1.5f);
            SpawnEneym();
        }

        public IEnumerator SpawnEnemyForMediumVehicle()
        {
            SpawnEneym();
            yield return new WaitForSeconds(1.5f);
            SpawnEneym();
            yield return new WaitForSeconds(1.5f);
            SpawnEneym();
            yield return new WaitForSeconds(1.5f);
            SpawnEneym();
        }

        public IEnumerator SpawnEnemyForSmallVehicle()
        {
            SpawnEneym();
            yield return new WaitForSeconds(1.5f);
            SpawnEneym();
            yield return new WaitForSeconds(1.5f);
            SpawnEneym();
        }

        void SpawnEneym()
        {
            if (_gameManager.EnemyCount <= SpanwLimitCount)
            {
                _audioSource.PlayOneShot(_audioClipSpawn);
                int spawCountPearly = Random.Range(1, 7);

                switch (spawCountPearly)
                {
                    case 1:
                        Instantiate(Bee, SpawnPosition.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(White, SpawnPosition.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(Giant, SpawnPosition.position, Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(Salyangoz, SpawnPosition.position, Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(Ahtapot, SpawnPosition.position, Quaternion.identity);
                        break;
                    case 6:
                        Instantiate(Smale, SpawnPosition.position, Quaternion.identity);
                        break;
                }
            }
        }
    }
}
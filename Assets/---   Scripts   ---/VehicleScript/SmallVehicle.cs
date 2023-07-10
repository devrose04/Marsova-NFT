using System.Collections;
using UnityEngine;

namespace ______Scripts______.VehicleScript
{
    public class SmallVehicle : MonoBehaviour
    {
        private GameObject thisVehicle;
        private Rigidbody2D RB2;

        private float Spawn_x;
        private float Spawn_y;

        private SpawnEnemy _spawnEnemy;

        private void Awake()
        {
            thisVehicle = this.gameObject;
            _spawnEnemy = thisVehicle.GetComponent<SpawnEnemy>();
            RB2 = thisVehicle.GetComponent<Rigidbody2D>();
            Spawn_x = Random.Range(-77f, 20f);
            Spawn_y = Random.Range(32f, 38f);
            thisVehicle.transform.position = new Vector3(Spawn_x, Spawn_y);
            StartCoroutine("BigVehicleFonks");
        }

        public IEnumerator BigVehicleFonks()
        {
            MoveVehicle();
            yield return new WaitForSeconds(12f); // bu 10'u dene

            MoveRight();
            StartCoroutine(_spawnEnemy.SpawnEnemyForSmallVehicle());
            yield return new WaitForSeconds(8f);
            MoveUp();

            yield return new WaitForSeconds(12f);
            Destroy(thisVehicle);
        }

        void MoveVehicle()
        {
            RB2.velocity = new Vector2(2, -2.6f);
        }

        void MoveRight()
        {
            RB2.velocity = new Vector2(1.7f, 0);
        }

        void MoveUp()
        {
            RB2.velocity = new Vector2(2f, 4f);
        }
    }
}
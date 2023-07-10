using System.Collections;
using UnityEngine;

namespace ______Scripts______.VehicleScript
{
    public class MediumVehicle : MonoBehaviour
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
            yield return new WaitForSeconds(13f); // bu 12 sn yi dene

            MoveRight();
            StartCoroutine(_spawnEnemy.SpawnEnemyForMediumVehicle());
            yield return new WaitForSeconds(10f);
            MoveUp();

            yield return new WaitForSeconds(13f);
            Destroy(thisVehicle);
        }

        void MoveVehicle()
        {
            RB2.velocity = new Vector2(2f, -2.3f);
        }

        void MoveRight()
        {
            RB2.velocity = new Vector2(1.5f, 0);
        }

        void MoveUp()
        {
            RB2.velocity = new Vector2(2f, 3.6f);
        }
    }
}
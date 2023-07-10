using System;
using System.Collections;
// using UnityEditor.Tilemaps;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ______Scripts______.VehicleScript
{
    public class BigVehicle : MonoBehaviour
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
            yield return new WaitForSeconds(15f);

            MoveRight();
            StartCoroutine(_spawnEnemy.SpawnEnemyForBigVehicle());
            yield return new WaitForSeconds(12f);
            MoveUp();

            yield return new WaitForSeconds(15f);
            Destroy(thisVehicle);
        }

        void MoveVehicle()
        {
            RB2.velocity = new Vector2(2, -2);
        }

        void MoveRight()
        {
            RB2.velocity = new Vector2(1.2f, 0);
        }

        void MoveUp()
        {
            RB2.velocity = new Vector2(2f, 3.3f);
        }
    }
}
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ______Scripts______.VehicleScript
{
    public class VehicleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject BigVehicle; // bunları prefab yap dene o zaman hat vermz
        [SerializeField] private GameObject MediumVehicle;
        [SerializeField] private GameObject SmalVehicle;

        private void Awake()
        {
            StartCoroutine(SpawnShip());
        }

        IEnumerator SpawnShip()
        {
            Spawn(BigVehicle);
            Spawn(MediumVehicle);
            yield return new WaitForSeconds(Random.Range(5f, 9f));

            Spawn(SmalVehicle);
            yield return new WaitForSeconds(Random.Range(5f, 9f));

            Spawn(MediumVehicle);
            Spawn(SmalVehicle);
            yield return new WaitForSeconds(Random.Range(5f, 9f));

            Spawn(BigVehicle);
            Spawn(SmalVehicle);
            Spawn(SmalVehicle);
            yield return new WaitForSeconds(Random.Range(5f, 9f));

            Spawn(MediumVehicle);
            Spawn(MediumVehicle);
            yield return new WaitForSeconds(Random.Range(5f, 9f));

            Spawn(BigVehicle);
            Spawn(MediumVehicle);
            Spawn(SmalVehicle);
            Spawn(SmalVehicle);
            yield return new WaitForSeconds(Random.Range(5f, 9f));

            Spawn(SmalVehicle);
            Spawn(SmalVehicle);
            StartCoroutine("SpawnShip");
        }

        void Spawn(GameObject vehicle)
        {
            Instantiate(vehicle, this.transform);
        }
    }
}
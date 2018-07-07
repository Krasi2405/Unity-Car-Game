using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarSpawner : MonoBehaviour {
    
    private bool ableToSpawnCars = false;

    [SerializeField]
    private HealthBar healthBarLeft;
    [SerializeField]
    private HealthBar healthBarRight;

    public void Awake()
    {
        GamePreparation();
    }

    private Dictionary<CarPhysics, GunController> carGunDict;

    /// <summary>
    /// Called by button before loading the game scene.
    /// </summary>
    public void GamePreparation()
    {
        CarDataTransfer[] playersInformation = FindObjectsOfType<CarDataTransfer>();
        List<SpawnLocation> spawnLocations = FindObjectsOfType<SpawnLocation>().ToList();

        if (playersInformation.Length == 0) // Probably running this from the editor. Simulate cars.
        {
            SimulateCars();
        }

        /* TODO
         * Create random spawnLocations if players are more than spawnLocations
        */
       

        for (int i = 0; i < playersInformation.Length; i++)
        {
            CarDataTransfer carData = playersInformation[i];
            if (!carData.hasData)
            {
                Debug.LogError(carData.name + " has no data in itself! Skipping instantiation");
                continue;
            }

            CarPhysics carInfo = carData.car;
            GunController gunInfo = carData.gun;
            Vector3 spawnLocation = GetSpawnLocation(spawnLocations);

            CarPhysics car = Instantiate(carInfo, spawnLocation, Quaternion.identity);
            GunController gun = Instantiate(gunInfo, car.transform.position + car.GetComponent<CarPhysics>().gunPosition, Quaternion.identity);
            gun.transform.parent = car.transform;
            // gun.transform.LookAt(car.transform.up);
            /*//* Set movement keys
            car.GetComponent<CarPhysics>().forward = forward;
            car.GetComponent<CarPhysics>().left = left;
            car.GetComponent<CarPhysics>().right = right;
            car.GetComponent<CarPhysics>().back = backwards;
            print("Car " + carName + " instantiated!"); */

            // Spawn the gun.
            /*
            print("Gun instantiated!");

            // Set firing key
            gun.GetComponent<GunController>().activationKey = activationKey;
            gun.transform.SetParent(carParent.transform);
            */
            Destroy(carData);
        }

        Destroy(gameObject);
    }

    private void SimulateCars()
    {
        throw new NotImplementedException();
    }

    private Vector3 GetSpawnLocation(List<SpawnLocation> spawnLocations)
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnLocations.Count);
        SpawnLocation spawn = spawnLocations[randomIndex];
        Vector3 spawnLocation = spawn.transform.position;
        spawnLocations.RemoveAt(randomIndex);
        Destroy(spawn);
        return spawnLocation;
    }

    
}

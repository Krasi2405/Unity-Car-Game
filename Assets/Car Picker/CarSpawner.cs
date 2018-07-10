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

    [SerializeField]
    private CarPhysics defaultPlayerOne;
    [SerializeField]
    private CarPhysics defaultPlayerTwo;
    private List<SpawnLocation> spawnLocations;
    private GameOverManager gameOverManager;

    public void Awake()
    {
        spawnLocations = FindObjectsOfType<SpawnLocation>().ToList();
        gameOverManager = FindObjectOfType<GameOverManager>();
        GamePreparation();
    }

    private Dictionary<CarPhysics, GunController> carGunDict;

    /// <summary>
    /// Called by button before loading the game scene.
    /// </summary>
    public void GamePreparation()
    {
        CarDataTransfer[] playersInformation = FindObjectsOfType<CarDataTransfer>();

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
                Debug.LogError(carData.name + " has no data in itself! Skipping instantiation. Shouldn't ever happen.");
                continue;
            }

            SpawnLocation spawnLocation = GetSpawnLocation();
            Vector3 spawnPosition = spawnLocation.transform.position;
            Quaternion spawnRotation = spawnLocation.transform.rotation;

            CarPhysics carInfo = carData.car;
            GunController gunInfo = carData.gun;

            CarPhysics car = Instantiate(carInfo, spawnPosition, spawnRotation);
            GunController gun = Instantiate(gunInfo, car.transform.position + car.GetComponent<CarPhysics>().gunPosition, Quaternion.identity);
            SetGunParent(gun, car);
            // TODO: Rework whole system to include more than 2 cars. Make it a lot more dynamic
            car.horizontalInputAxis = "Horizontal" + carData.GetIndex();
            car.verticalInputAxis = "Vertical" + carData.GetIndex();
            gun.activationKey = "Fire" + carData.GetIndex();

            if (carData.GetIndex() == 0)
            {
                healthBarLeft.car = car;
                gameOverManager.carOne = car;
            }
            else if (carData.GetIndex() == 1)
            {
                healthBarRight.car = car;
                gameOverManager.carTwo = car;
            }
            else
            {
                throw new NotImplementedException("More than 2 cars not supported!");
            }
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

    private static void SetGunParent(GunController gun, CarPhysics car)
    {
        Vector3 gunPosition = gun.transform.localPosition;
        Quaternion gunRotation = gun.transform.localRotation;
        Vector3 gunScale = gun.transform.localScale;
        gun.transform.parent = car.transform;
        gun.transform.localPosition = car.gunPosition;
        gun.transform.localScale = gunScale;
        gun.transform.localRotation = gunRotation;
    }

    // For debugging purposes only
    // TODO: Remove when deploying to play store
    private void SimulateCars()
    {

        // Note: BAD PRACTICE.
        // SPAGHETTI CODE INCOMING
        // <Brace yourselves>

        // Spawn car one
        SpawnLocation spawnLocationOne = GetSpawnLocation();
        Vector3 spawnPositionOne = spawnLocationOne.transform.position;
        Quaternion spawnRotationOne = spawnLocationOne.transform.rotation;
        CarPhysics carOne = Instantiate(defaultPlayerOne, spawnPositionOne, spawnRotationOne);


        // Spawn car two
        SpawnLocation spawnLocationTwo = GetSpawnLocation();
        Vector3 spawnPositionTwo = spawnLocationTwo.transform.position;
        Quaternion spawnRotationTwo = spawnLocationTwo.transform.rotation;
        CarPhysics carTwo = Instantiate(defaultPlayerTwo, spawnPositionTwo, spawnRotationTwo);

        healthBarLeft.car = carOne;
        healthBarRight.car = carTwo;
        gameOverManager.carOne = carOne;
        gameOverManager.carTwo = carTwo;
    }

    private SpawnLocation GetSpawnLocation()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnLocations.Count);
        SpawnLocation spawn = spawnLocations[randomIndex];
        spawnLocations.RemoveAt(randomIndex);
        return spawn;
    }
}

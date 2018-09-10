using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarSpawner : MonoBehaviour {

    [SerializeField]
    private CarPhysics[] defaultCarsList;

    [SerializeField]
    private GunController[] defaultGunsList;

    private List<SpawnLocation> spawnLocations;
    private GameOverManager gameOverManager;

    [SerializeField]
    private int carCount = 2;


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

        playersInformation = FindObjectsOfType<CarDataTransfer>();

        List<HealthBar> healthBars = FindObjectsOfType<HealthBar>().ToList();
        healthBars = healthBars.OrderBy(x => x.GetComponent<CarTag>().carTag).ToList();

        for (int i = 0; i < playersInformation.Length; i++)
        {
            CarDataTransfer carData = playersInformation[i];
            if (!carData.hasData)
            {
                Debug.LogError(carData.name + " has no data in itself! Skipping instantiation. [Shouldn't ever happen].");
                continue;
            }

            CarPhysics carInfo = carData.car;
            GunController gunInfo = carData.gun;

            SpawnLocation spawnLocation = GetSpawnLocation();
            Vector3 spawnPosition = spawnLocation.transform.position;
            Quaternion spawnRotation = spawnLocation.transform.rotation;            

            CarPhysics car = Instantiate(carInfo, spawnPosition, spawnRotation);
            GunController gun = Instantiate(gunInfo, car.transform.position + car.GetComponent<CarPhysics>().gunPosition, Quaternion.identity);
            SetGunParent(gun, car);
            
            car.horizontalInputAxis = "Horizontal" + carData.index;
            car.verticalInputAxis = "Vertical" + carData.index;
            gun.activationKey = "Fire" + carData.index;

            CarTag tag = car.gameObject.AddComponent<CarTag>();
            tag.carTag = carData.index;

            healthBars[i].SetTargetCar(car);
            gameOverManager.carList.Add(car);
        }
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
        
        if (defaultCarsList.Length == 0 || defaultGunsList.Length == 0)
        {
            Debug.LogError("Car spawner cannot simulate cars when no default cars or guns are set!");
            return;
        }
        Debug.LogWarning("Simulating cars!");
        for (int i = 0; i < carCount; i++)
        {
            GameObject carDataObj = Instantiate(new GameObject("CarData" + i), Vector3.zero, Quaternion.identity);
            CarDataTransfer carData = carDataObj.AddComponent<CarDataTransfer>() as CarDataTransfer;
            carData.car = defaultCarsList[Random.Range(0, defaultCarsList.Length)];
            carData.gun = defaultGunsList[Random.Range(0, defaultGunsList.Length)];
            carData.hasData = true;
            carData.index = i;
            Debug.LogWarning("Simulate cardata transfer!");
        }
    }

    private SpawnLocation GetSpawnLocation()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnLocations.Count);
        SpawnLocation spawn = spawnLocations[randomIndex];
        spawnLocations.RemoveAt(randomIndex);
        return spawn;
    }
}

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarSpawner : MonoBehaviour {

    [SerializeField]
    private CarList defaultCarsList;
    [SerializeField]
    private GunList defaultGunsList;

    private List<SpawnLocation> spawnLocations;
    private GameOverManager gameOverManager;

    [SerializeField]
    private int carCount = 2;


    public void Awake()
    {
        spawnLocations = FindObjectsOfType<SpawnLocation>().ToList();
        gameOverManager = FindObjectOfType<GameOverManager>();
    }

    private Dictionary<CarPhysics, GunBase> carGunDict;

    /*
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
        try
        {
            healthBars = healthBars.OrderBy(x => x.GetComponent<CarTag>().carTag).ToList();
        }
        catch(System.NullReferenceException)
        {
            Debug.LogWarning("Health bars have no CarTag so they are distributed randomly!");
        }

        for (int i = 0; i < playersInformation.Length; i++)
        {
            CarDataTransfer carData = playersInformation[i];
            if (!carData.hasData)
            {
                Debug.LogError(carData.name + " has no data in itself! Skipping instantiation. [Shouldn't ever happen].");
                continue;
            }

            Car carInfo = carData.car;
            GunBase gunInfo = carData.gun;

            SpawnLocation spawnLocation = GetSpawnLocation();
            Vector3 spawnPosition = spawnLocation.transform.position;
            Quaternion spawnRotation = spawnLocation.transform.rotation;            

            Car car = Instantiate(carInfo, spawnPosition, spawnRotation);
            GunBase gun = Instantiate(gunInfo, car.transform.position + car.GetComponent<CarPhysics>().gunPosition, Quaternion.identity);
            SetGunParent(gun, car);
            
            car.carPhysics.horizontalInputAxis = "Horizontal" + carData.index;
            car.carPhysics.verticalInputAxis = "Vertical" + carData.index;
            gun.SetActivationKey("Fire" + carData.index);

            CarTag tag = car.gameObject.AddComponent<CarTag>();
            tag.carTag = carData.index;

            car.health.SetHealthBar(healthBars[i]);
            gameOverManager.carList.Add(car);
        }
    }

    private static void SetGunParent(GunBase gun, Car car)
    {
        Vector3 gunPosition = gun.transform.localPosition;
        Quaternion gunRotation = gun.transform.localRotation;
        Vector3 gunScale = gun.transform.localScale;
        gun.transform.parent = car.transform;
        gun.transform.localPosition = car.carPhysics.gunPosition;
        gun.transform.localScale = gunScale;
        gun.transform.localRotation = gunRotation;
    }

    // For debugging purposes only
    // TODO: Remove when deploying to play store
    private void SimulateCars()
    {
        
        if (defaultCarsList.GetCarList().Length == 0 || defaultGunsList.GetGunList().Length == 0)
        {
            Debug.LogError("Car spawner cannot simulate cars when no default cars or guns are set!");
            return;
        }
        Debug.LogWarning("Simulating cars!");
        for (int i = 0; i < carCount; i++)
        {
            GameObject carDataObj = Instantiate(new GameObject("CarData" + i), Vector3.zero, Quaternion.identity);
            CarDataTransfer carData = carDataObj.AddComponent<CarDataTransfer>() as CarDataTransfer;
            carData.car = defaultCarsList.GetCarList()[Random.Range(0, defaultCarsList.GetCarList().Length)];
            carData.gun = defaultGunsList.GetGunList()[Random.Range(0, defaultGunsList.GetGunList().Length)];
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

    */
}

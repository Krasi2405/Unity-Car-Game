using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private int playerCount = 2;

    [SerializeField]
    private CarList defaultCarList = null;

    [SerializeField]
    private GunList defaultGunList = null;

    [SerializeField]
    private List<HealthBar> externalHealthbars = null;

    [SerializeField]
    private List<AmmoIndicatorUI> ammoIndicators = null;

    [SerializeField]
    private List<InventoryBoostUI> inventoryBoostUIList = null;

    private SpawnLocation[] spawnLocations;


    private void Awake()
    {
        spawnLocations = FindObjectsOfType<SpawnLocation>();
        if (playerCount > spawnLocations.Length)
        {
            Debug.LogError("More players than spawn locations!");
            return;
        }

        if(playerCount > externalHealthbars.Count)
        {
            Debug.LogError("More players than healthbars");
            return;
        }

        for(int i = 0; i < playerCount; i++)
        {
            SpawnLocation playerSpawnLocation = spawnLocations[i];
            CarSO carSO = defaultCarList.cars[PlayerPrefs.GetInt("SelectedCar" + i, 0)];
            GunSO gunSO = defaultGunList.guns[PlayerPrefs.GetInt("SelectedGun" + i, 0)];

            Car car = Instantiate(carSO.prefab, playerSpawnLocation.transform.position, playerSpawnLocation.transform.rotation);
            car.Setup(gunSO, i);
            externalHealthbars[i].Setup(car.GetComponent<HealthSystem>());
            ammoIndicators[i].Setup(car.GetGun());
            inventoryBoostUIList[i].Setup(car.GetComponent<CarBoostManager>());
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private int playerCount;

    [SerializeField]
    private CarList defaultCarList;

    [SerializeField]
    private GunList defaultGunList;


    private SpawnLocation[] spawnLocations;

    private void Awake()
    {
        spawnLocations = FindObjectsOfType<SpawnLocation>();
        for(int i = 0; i < playerCount; i++)
        {
            SpawnLocation playerSpawnLocation = spawnLocations[i];
            CarSO carSO = defaultCarList.cars[PlayerPrefs.GetInt("SelectedCar" + i, 0)];
            GunSO gunSO = defaultGunList.guns[PlayerPrefs.GetInt("SelectedGun" + i, 0)];

            Car car = Instantiate(carSO.prefab, playerSpawnLocation.transform.position, playerSpawnLocation.transform.rotation);
            car.SpawnGun(gunSO);
        }
    }
}

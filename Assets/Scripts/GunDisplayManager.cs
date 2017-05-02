using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDisplayManager : MonoBehaviour {

    public GameObject[] gunPrefabs;
    public DisplayGunInfo gunInfo;

    private int currentIndex = 0;
    private GameObject currentGun;


    void Start()
    {
        setCurrentGun();
    }


    public void IncreaseCurrentCar()
    {
        currentIndex++;
        if (currentIndex >= gunPrefabs.Length)
        {
            currentIndex = 0;
        }

        setCurrentGun();
    }


    public void DecreaseCurrentCar()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = gunPrefabs.Length - 1;
        }

        setCurrentGun();
    }

    private void setCurrentGun()
    {
        currentGun = gunPrefabs[currentIndex];
        gunInfo.gun = currentGun;
        gunInfo.UpdateInfo();
    }
}

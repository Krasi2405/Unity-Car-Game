using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDisplayManager : MonoBehaviour {

    public GameObject[] carPrefabs;
    public DisplayCarInfo carInfo;

    private int currentIndex = 0;
    private GameObject currentCar;
    
    
	void Start () {
        setCurrentCar();
	}
	

    public void IncreaseCurrentCar()
    {
        currentIndex++;
        if(currentIndex >= carPrefabs.Length)
        {
            currentIndex = 0;
        }

        setCurrentCar();
    }


    public void DecreaseCurrentCar()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = carPrefabs.Length - 1;
        }

        setCurrentCar();
    }

    private void setCurrentCar()
    {
        currentCar = carPrefabs[currentIndex];
        carInfo.car = currentCar;
        carInfo.UpdateInfo();
    }
}

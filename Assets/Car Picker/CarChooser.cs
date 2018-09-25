using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarChooser : MonoBehaviour {
    [SerializeField] private CarList carList;
    [SerializeField] private GunList gunList;
    [SerializeField] private bool flipImages = false;
    [SerializeField] private CarDataTransfer dataTransferAgent;

    public Car selectedCar { get; private set; }
    public GunBase selectedGun { get; private set; }


    private int carCounter = 0;
    private int gunCounter = 0;

    [SerializeField] private Image carImage;
    [SerializeField] private Text carText;

    [SerializeField] private Image gunImage;
    [SerializeField] private Text gunText;

    void Start()
    {
        SetCarInfo();
        SetGunInfo();

        if (flipImages)
        {
            gunImage.transform.localScale = new Vector3(
                -carImage.transform.localScale.x,
                -carImage.transform.localScale.y,
                carImage.transform.localScale.z);

            carImage.transform.localScale = new Vector3(
                -carImage.transform.localScale.x,
                -carImage.transform.localScale.y,
                carImage.transform.localScale.z);
        }
    }


    public T IncrementObject<T>(T[] objects, ref int counter)
    {
        if (counter < objects.Length - 1)
        {
            counter++;
        }
        return objects[counter];
    }

    public T DecrementObject<T>(T[] objects, ref int counter)
    {
        if (counter > 0)
        {
            counter--;
        }
        return objects[counter];
    }
    

    private void SetCarInfo()
    {
        selectedCar = carList.GetCarList()[carCounter];
        dataTransferAgent.car = selectedCar;    


        if (dataTransferAgent.gun != null) {
            dataTransferAgent.hasData = true;
            // Already set the car so need to check for gun.
        }

        carImage.sprite = selectedCar.GetComponent<SpriteRenderer>().sprite;
    }


    private void SetGunInfo()
    {
        selectedGun = gunList.GetGunList()[gunCounter];
        dataTransferAgent.gun = selectedGun;

        if (dataTransferAgent.car != null)
        {
            dataTransferAgent.hasData = true;
            // Already set the gun so need to check for car.
        }

        gunImage.sprite = selectedGun.GetComponent<SpriteRenderer>().sprite;
    }


    public void IncrementCarSelection()
    {
        selectedCar = IncrementObject(carList.GetCarList(), ref carCounter);
        SetCarInfo();
    }


    public void DecrementCarSelection()
    {
        selectedCar = DecrementObject(carList.GetCarList(), ref carCounter);
        SetCarInfo();
    }


    public void DecrementGunSelection()
    {
        selectedGun = DecrementObject(gunList.GetGunList(), ref gunCounter);
        SetGunInfo();
    }


    public void IncrementGunSelection()
    {
        selectedGun = IncrementObject(gunList.GetGunList(), ref gunCounter);
        SetGunInfo();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarChooser : MonoBehaviour {
    [SerializeField] private CarPhysics[] cars;
    [SerializeField] private GunController[] guns;
    [SerializeField] private bool flipImages = false;
    [SerializeField] private CarDataTransfer dataTransferAgent;

    public CarPhysics selectedCar { get; private set; }
    public GunController selectedGun { get; private set; }


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
        selectedCar = cars[carCounter];
        dataTransferAgent.car = selectedCar;


        if (dataTransferAgent.gun != null) {
            dataTransferAgent.hasData = true;
            // Already set the car so need to check for gun.
        }

        carImage.sprite = selectedCar.GetComponent<SpriteRenderer>().sprite;
        carText.text = selectedCar.GetComponent<TextInfoBase>().GetInformation();
    }


    private void SetGunInfo()
    {
        selectedGun = guns[gunCounter];
        dataTransferAgent.gun = selectedGun;

        if (dataTransferAgent.car != null)
        {
            dataTransferAgent.hasData = true;
            // Already set the gun so need to check for car.
        }

        gunImage.sprite = selectedGun.GetComponent<SpriteRenderer>().sprite;
        gunText.text = selectedGun.GetComponent<TextInfoBase>().GetInformation();
    }


    public void IncrementCarSelection()
    {
        selectedCar = IncrementObject(cars, ref carCounter);
        SetCarInfo();
    }


    public void DecrementCarSelection()
    {
        selectedCar = DecrementObject(cars, ref carCounter);
        SetCarInfo();
    }


    public void DecrementGunSelection()
    {
        selectedGun = DecrementObject(guns, ref gunCounter);
        SetGunInfo();
    }


    public void IncrementGunSelection()
    {
        selectedGun = IncrementObject(guns, ref gunCounter);
        SetGunInfo();
    }
}

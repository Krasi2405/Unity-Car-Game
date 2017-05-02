using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public static string playerOneCar = "Pickup Truck";
    public static string playerOneGun = "Laser Gun";
    public static string playerTwoCar = "Tractor";
    public static string playerTwoGun = "Machine Gun";

    public DisplayCarInfo carInfoOne;
    public DisplayCarInfo carInfoTwo;
    public DisplayGunInfo gunInfoOne;
    public DisplayGunInfo gunInfoTwo;

    public void SetInfo()
    {
        SetPlayerOneCar();
        SetPlayerTwoCar();
        SetPlayerOneGun();
        SetPlayerTwoGun();
    }


    public void SetPlayerOneCar()
    {
        playerOneCar = carInfoOne.car.name;
    }

    public void SetPlayerTwoCar()
    {
        playerTwoCar = carInfoTwo.car.name;
    }

    public void SetPlayerOneGun()
    {
        playerOneGun = gunInfoOne.gun.name;
    }

    public void SetPlayerTwoGun()
    {
        playerTwoGun = gunInfoTwo.gun.name;
    }
}

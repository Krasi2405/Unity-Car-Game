using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public static string playerOneCar = "Pickup Truck";
    public static string playerOneGun = "Laser Gun";
    public static string playerTwoCar = "Race Car";
    public static string playerTwoGun = "Shell Gun";

    public void SetPlayerOneCar(string carName)
    {
        playerOneCar = carName;
    }

    public void SetPlayerTwoCar(string carName)
    {
        playerTwoCar = carName;
    }

    public void SetPlayerOneGun(string gunName)
    {
        playerOneGun = gunName;
    }

    public void SetPlayerTwoGun(string gunName)
    {
        playerTwoGun = gunName;
    }
}

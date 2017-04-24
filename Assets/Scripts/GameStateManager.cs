using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public string playerOneCar;
    public string playerOneGun;
    public string playerTwoCar;
    public string playerTwoGun;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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

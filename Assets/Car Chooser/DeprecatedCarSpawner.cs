using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeprecatedCarSpawner : MonoBehaviour {

    /*
    public KeyCode playerOneForward = KeyCode.W;
    public KeyCode playerOneLeft = KeyCode.A;
    public KeyCode playerOneRight = KeyCode.D;
    public KeyCode playerOneBackwards = KeyCode.S;

    public KeyCode playerTwoForward = KeyCode.UpArrow;
    public KeyCode playerTwoLeft = KeyCode.LeftArrow;
    public KeyCode playerTwoRight = KeyCode.RightArrow;
    public KeyCode playerTwoBackwards = KeyCode.DownArrow;

    public KeyCode playerOneShoot = KeyCode.LeftShift;
    public KeyCode playerTwoShoot = KeyCode.RightControl;

    public DisplayHealthBar hitpointsBarOne;
    public DisplayHealthBar hitpointsBarTwo;

    public GameOver gameOverManager;

    void Awake () {
        // Instantiate Car 1
        GameObject carOne = SpawnCar(GameStateManager.playerOneCar, GameStateManager.playerOneGun, new Vector2(-5, -5), 
            playerOneForward, playerOneLeft, playerOneRight, playerOneBackwards, playerOneShoot, hitpointsBarOne);
        gameOverManager.car1 = carOne.GetComponent<CarPhysics>();

        // Instantiate Car 2
        GameObject carTwo = SpawnCar(GameStateManager.playerTwoCar, GameStateManager.playerTwoGun, new Vector2(5, 5), 
            playerTwoForward, playerTwoLeft, playerTwoRight, playerTwoBackwards, playerTwoShoot, hitpointsBarTwo);
        gameOverManager.car2 = carTwo.GetComponent<CarPhysics>();
    }

    GameObject SpawnCar(string carName, string gunName, Vector3 spawnLocation, KeyCode forward, KeyCode left, KeyCode right, KeyCode backwards, KeyCode gunActivation, DisplayHealthBar hpBar)
    {
        GameObject carPrefab;
        try
        {
            carPrefab = Resources.Load("Cars/" + carName) as GameObject;
        }
        catch (System.ArgumentException)
        {
            print(carName + " could not be found.");
            return null;
        }
        GameObject car = Instantiate(carPrefab, spawnLocation, Quaternion.identity);
        
        // Set movement keys
        car.GetComponent<CarPhysics>().forward = forward;
        car.GetComponent<CarPhysics>().left = left;
        car.GetComponent<CarPhysics>().right = right;
        car.GetComponent<CarPhysics>().back = backwards;
        print("Car " + carName + " instantiated!");

        // Spawn the gun.
        SpawnGun(gunName, car, gunActivation);
        hpBar.car = car;

        return car;
    }

    void SpawnGun(string prefabName, GameObject carParent, KeyCode activationKey)
    {
        GameObject gunPrefab;
        try
        {
            gunPrefab = Resources.Load("Guns/" + prefabName) as GameObject;

        }
        catch(System.ArgumentException)
        {
            print(prefabName + " could not be found.");
            return;
        }
        GameObject gun = Instantiate(gunPrefab, carParent.transform.position + carParent.GetComponent<CarPhysics>().gunPosition, Quaternion.identity);
        print("Gun instantiated!");

        // Set firing key
        gun.GetComponent<GunController>().activationKey = activationKey;
        gun.transform.SetParent(carParent.transform);

        // Set the position.z so the gun is seen above the car.
        Vector3 position = gun.transform.position;
        position.z = -5;
        gun.transform.position = position;

        // Rotate gun by 180 degrees
        Vector3 rotation = gun.transform.rotation.eulerAngles;
        rotation.z += 180;
        gun.transform.rotation = Quaternion.Euler(rotation);
    }
    */
}

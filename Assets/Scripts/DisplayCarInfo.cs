using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCarInfo : MonoBehaviour {

    public GameObject car;

    private Transform textCarInfo;
    private CarPhysics carScript;

	void Start()
    {
        textCarInfo = gameObject.transform.GetChild(0);
        carScript = car.GetComponent<CarPhysics>();

        // Set the car stats.
        string carInfo = "";
        carInfo += "Engine Power " + carScript.power + "\n";
        carInfo += "Linear Drag  " + carScript.linearDrag + "\n";
        carInfo += "Angular Drag " + carScript.angularDrag + "\n";
        carInfo += "Brake Force  " + carScript.brakeForce + "\n";
        carInfo += "Car Weight   " + carScript.weight + "\n";
        carInfo += "Ammo         " + carScript.maxAmmo + "\n";
        carInfo += "HP:          " + carScript.maxHealth + "\n";
        textCarInfo.gameObject.GetComponent<Text>().text = carInfo;

        // Set the image to the image of the car.
        gameObject.GetComponent<Image>().sprite = car.GetComponent<SpriteRenderer>().sprite;
    }
}

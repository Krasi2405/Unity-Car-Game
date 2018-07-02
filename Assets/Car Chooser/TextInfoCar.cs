using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextInfoCar : TextInfoBase
{

    public override string GetInformation()
    {
        CarPhysics carScript = GetComponent<CarPhysics>();
        string carInfo = "";
        carInfo += "Engine Power " + carScript.power + "\n";
        carInfo += "Linear Drag  " + carScript.linearDrag + "\n";
        carInfo += "Angular Drag " + carScript.angularDrag + "\n";
        carInfo += "Brake Force  " + carScript.brakeForce + "\n";
        carInfo += "Car Weight   " + carScript.weight + "\n";
        carInfo += "Max speed    " + carScript.maxSpeed + "\n";
        carInfo += "Ammo         " + carScript.maxAmmo + "\n";
        carInfo += "HP:          " + carScript.maxHealth + "\n";
        return carInfo;
    }
}

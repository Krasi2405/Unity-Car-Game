using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Vehicles/PhysicsSettings", order = 2)]
public class CarPhysicsSO : ScriptableObject
{
    public float power = 15f;
    public float angularDrag = 3f;
    public float linearDrag = 2f;
    public float weight = 2f;
    public float brakeForce = 2f;
    public float maxSpeed = 100f;
    public float turnPower = 1f;
}

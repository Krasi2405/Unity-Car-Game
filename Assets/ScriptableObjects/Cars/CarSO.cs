using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Vehicles/Vehicle", order = 1)]
public class CarSO : ScriptableObject
{
    public string carName;
    public Car prefab;
    public Sprite iconRepresentation;
    public float maxHealth;
    public CarPhysicsSO physics;
}

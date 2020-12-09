using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Vehicles/List", order = 0)]
public class CarList : ScriptableObject {
    public List<CarSO> cars;
}

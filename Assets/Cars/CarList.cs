using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="CarList")]
public class CarList : ScriptableObject {

    [SerializeField]
    private Car[] cars;

    public Car[] GetCarList()
    {
        return cars;
    }
}

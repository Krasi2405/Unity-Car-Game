using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Boosts used by car
/// </summary>
public class StashedBoosts : BoostBase {

    InventoryBoost boost;

    protected override void ApplyEffect(CarPhysics car)
    {
        InventoryBoost currentBoost = car.GetComponentInChildren<InventoryBoost>();
        if (currentBoost) 
        {
            currentBoost.DestroyBoost();
        }
        Instantiate(boost, car.transform);
    }
}

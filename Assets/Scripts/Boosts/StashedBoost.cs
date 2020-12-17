using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StashedBoost : BoostBase {

    [SerializeField]
    InventoryBoost boostPrefab = null;

    protected override void OnPickup(Car car)
    {
        InventoryBoost boost = Instantiate(boostPrefab, car.transform);
        CarBoostManager boostManager = car.GetBoostManager();
        boostManager.PickupBoost(boost);
    }
}

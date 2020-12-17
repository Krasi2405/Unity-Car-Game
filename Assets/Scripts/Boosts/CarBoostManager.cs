using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarBoostManager : MonoBehaviour
{
    public UnityEvent OnBoostChanged;
    public UnityEvent OnBoostUsed;

    private Car car;

    private InventoryBoost inventoryBoost;

    private void Awake()
    {
        car = GetComponent<Car>();
    }

    public void PickupBoost(InventoryBoost newInventoryBoost)
    {
        if(inventoryBoost)
        {
            Destroy(inventoryBoost);
        }

        inventoryBoost = newInventoryBoost;
        OnBoostChanged?.Invoke();
    }

    public void UseBoost()
    {
        if(inventoryBoost == null) { return; }

        inventoryBoost.Use(car);
        Destroy(inventoryBoost);
        inventoryBoost = null;
        OnBoostUsed?.Invoke();
    }

    public InventoryBoost GetBoost()
    {
        return inventoryBoost;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBoostUI : MonoBehaviour
{
    [SerializeField]
    private Image boostIcon = null;

    CarBoostManager carBoostManager;

    public void Setup(CarBoostManager boostManager)
    {
        carBoostManager = boostManager;
        Debug.Log(carBoostManager);
        carBoostManager.OnBoostChanged.AddListener(SetBoostIcon);
        carBoostManager.OnBoostUsed.AddListener(RemoveBoostIcon);
    }


    private void SetBoostIcon()
    {
        boostIcon.sprite = carBoostManager.GetBoost().GetIcon();
    }

    private void RemoveBoostIcon()
    {
        boostIcon.sprite = null;
    }
}

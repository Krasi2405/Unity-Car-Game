using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// Boost on child object of car.
/// </summary>
public abstract class InventoryBoost : MonoBehaviour {

    [SerializeField]
    [Tooltip("How long boost lasts without being used")]
    private float timeOfBoost; 
    
    private string activationKey;
    public CarPhysics attachedTo;

    private void Start()
    {
        // TODO Show icon on screen when picking up.
        attachedTo = GetComponentInParent<CarPhysics>();
        activationKey = attachedTo.specialInput;
        Invoke("DestroyBoost", timeOfBoost);
    }

    private void Update()
    {
        if(CrossPlatformInputManager.GetButton(activationKey))
        {
            ActivateBoost();
        }
    }

    public void ActivateBoost()
    {
        ApplyEffect(attachedTo);
    }


    public void DestroyBoost()
    {
        // TODO Remove icon of boost
        Destroy(gameObject);
    }

    protected abstract void ApplyEffect(CarPhysics car);
}

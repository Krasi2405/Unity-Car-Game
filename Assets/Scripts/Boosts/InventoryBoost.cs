using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class InventoryBoost : MonoBehaviour {

    [SerializeField]
    private Sprite icon = null;

    BoostEffect[] boostEffects;
    private void Awake()
    {
        boostEffects = GetComponents<BoostEffect>();
    }

    public void Use(Car car)
    {
        foreach (BoostEffect boostEffect in boostEffects)
        {
            boostEffect.ApplyEffect(car);
        }
        Destroy(gameObject);
    }

    public Sprite GetIcon()
    {
        return icon;
    }
}

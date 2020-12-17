using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantBoost : BoostBase
{
    BoostEffect[] boostEffects;
    protected void Awake()
    {
        base.Awake();

        boostEffects = GetComponents<BoostEffect>();
    }

    protected override void OnPickup(Car car)
    {
        foreach (BoostEffect boostEffect in boostEffects)
        {
            boostEffect.ApplyEffect(car);
        }
    }
}
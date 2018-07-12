using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Boosts applied on car at the moment
/// </summary>
public abstract class MomentaryBoosts : BoostBase
{
    protected override void ApplyEffect(CarPhysics car)
    {
        Effect(car);
    }

    protected abstract void Effect(CarPhysics car);


}

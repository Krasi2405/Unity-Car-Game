using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Boost not 
/// </summary>
public class EventBoost : BoostBase {

    EventEffect eventEffect;

    protected override void ApplyEffect(Car car)
    {
        Instantiate(eventEffect);
    }
}

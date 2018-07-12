using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MomentaryBoosts {

    public int staticHealthGain = 5;
    public int percentMissingHealthGain = 10;

    protected override void Effect(CarPhysics car)
    {
        car.currentHealth += 
            (Mathf.Abs(car.currentHealth - car.maxHealth) * percentMissingHealthGain / 100)
            + staticHealthGain;
        car.SmokeState();
    }
}

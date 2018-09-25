using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MomentaryBoosts {

    public int staticHealthGain = 5;
    public int percentMissingHealthGain = 10;

    protected override void Effect(Car car)
    {
        float healAmount = 
            (Mathf.Abs(car.health.GetCurrentHealth() - car.health.GetMaxHealth()) * percentMissingHealthGain / 100)
            + staticHealthGain;
        car.health.Heal(healAmount);
    }
}

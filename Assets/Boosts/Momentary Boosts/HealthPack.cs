using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MomentaryBoosts {

    public int staticHealthGain = 5;
    public int percentMissingHealthGain = 10;

    protected override void Effect(Car car)
    {
        Health carHealth = car.GetComponent<Health>();

        float healAmount = 
            (Mathf.Abs(carHealth.GetCurrentHealth() - carHealth.GetMaxHealth()) * percentMissingHealthGain / 100)
            + staticHealthGain;
        carHealth.Heal(healAmount);
    }
}

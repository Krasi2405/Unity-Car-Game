using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFlatIncreaseBoostEffect : BoostEffect {

    [SerializeField]
    private int health = 0;

    public override void ApplyEffect(Car car)
    {
        HealthSystem carHealth = car.GetComponent<HealthSystem>();
        carHealth.Heal(health);
    }
}

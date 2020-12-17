using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPercentIncreaseBoostEffect : BoostEffect
{
    [SerializeField][Range(0.0f, 1.0f)]
    private float percentHealthIncrease = 0;
    public override void ApplyEffect(Car car)
    {
        HealthSystem healthSystem = car.GetComponent<HealthSystem>();
        healthSystem.Heal(healthSystem.GetMaxHealth() * percentHealthIncrease);
    }
}

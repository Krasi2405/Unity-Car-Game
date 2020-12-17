using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : AreaOfEffectProjectile
{
    protected override void OnStayActivateEffect(CarCollider hitCollider)
    {
        hitCollider.TakeDamage(damage * Time.deltaTime);
    }

    protected override void OnStayActivateEffectOnObject(HealthSystem healthSystem)
    {
        healthSystem.TakeDamage(damage * Time.deltaTime);
    }

}

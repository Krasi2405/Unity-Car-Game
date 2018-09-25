using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : AreaOfEffectProjectile
{

    [SerializeField]
    private float damagePerSecond = 5;

    

    protected override void ActivateEffect(Car target)
    {
        if (target == owner) return;
        target.health.TakeDamage(damagePerSecond * Time.deltaTime);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProjectile : Projectile
{
    [SerializeField]
    private float damage = 5;

    protected override void ActivateEffect(Car target, CarCollider carCollider)
    {
        target.health.TakeDamage(damage);
    }
}

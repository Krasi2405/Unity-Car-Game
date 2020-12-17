using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Projectile : ProjectileBase {

    void OnTriggerEnter2D(Collider2D collision)
    {
        CarCollider carCollider = collision.GetComponent<CarCollider>();
        if (carCollider && carCollider.GetAttachedCar() != owner)
        {
            OnHitTarget(carCollider.GetAttachedCar(), carCollider);
            Destroy(gameObject);
        }
        else
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if(healthSystem)
            {
                healthSystem.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnHitTarget(Car target, CarCollider carCollider)
    {
        carCollider.TakeDamage(damage);
    }
}

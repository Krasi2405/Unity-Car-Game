using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class Projectile : ProjectileBase {

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            return;
        }
        else if (collision.gameObject.tag == "Boost")
        {
            Destroy(collision.gameObject, 0.1f);
            Destroy(gameObject);
            return;
        }

        CarCollider carCollider = collision.GetComponent<CarCollider>();
        if (carCollider && carCollider.GetAttachedCar() != owner)
        {
            ActivateEffect(carCollider.GetComponentInParent<Car>(), carCollider);
            Destroy(gameObject);
        }
    }

    protected abstract void ActivateEffect(Car target, CarCollider carCollider);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : Liquid {

    [SerializeField]
    private float damagePerSecond = 0.0f;

    void OnTriggerStay2D(Collider2D collision)
    {
        CarCollider carCollider = collision.GetComponent<CarCollider>();
        if(carCollider != null)
        {
            carCollider.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}

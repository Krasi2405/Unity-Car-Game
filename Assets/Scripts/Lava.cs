using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : Liquid {

    void OnTriggerStay2D(Collider2D collision)
    {
        CarCollider carCollider = collision.GetComponent<CarCollider>();
        if(carCollider)
        {
            carCollider.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : Liquid {

    void OnTriggerStay2D(Collider2D collision)
    {
        try
        {
            CarPhysics test = collision.gameObject.GetComponent<CarPhysics>();
            test.currentHealth -= damagePerSecond * Time.deltaTime;

        }
        catch (MissingComponentException)
        {

        }
    }
}

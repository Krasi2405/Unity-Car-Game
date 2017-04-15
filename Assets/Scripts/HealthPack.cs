using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

    public int health = 50;
    public int percent_health = 5;

    void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            CarPhysics car = collision.gameObject.GetComponent<CarPhysics>();
            car.curr_health += health;
            Destroy(gameObject);
        }
        catch (MissingComponentException)
        {
            print("Health pack collided with non-car object");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

    public int health = 50;
    public int percentHealth = 5;

    void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            CarPhysics car = collision.gameObject.GetComponent<CarPhysics>();
            car.currentHealth += health;
            car.SmokeState();
            Destroy(gameObject);
        }
        catch (MissingComponentException)
        {
            print("Health pack collided with non-car object");
        }
    }
}

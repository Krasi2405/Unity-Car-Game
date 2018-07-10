using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour {

    public int ammoInPack = 5;

    void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            CarPhysics car = collision.GetComponent<CarCollider>().car;
            car.currentAmmo += ammoInPack;
            Destroy(gameObject);
        }
        catch (MissingComponentException)
        {
            print("Ammo pack collided with non-car object");
        }
        catch (System.NullReferenceException)
        {
            print("Ammo pack collided with non-car object");
        }
    }
}

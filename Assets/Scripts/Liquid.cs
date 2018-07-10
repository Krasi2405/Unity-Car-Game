using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour {

    public float damagePerSecond = 3f;
    public float linearDragIncrease = 4f;
    public float angularDragIncrease = 2f;

    private float startDrag;

    void OnTriggerEnter2D(Collider2D collision)
    {
        CarPhysics car = collision.GetComponentInParent<CarPhysics>();
        // Test whether its a car
        if (car)
        {
            car.GetComponent<Rigidbody2D>().drag = car.linearDrag * linearDragIncrease;
            car.GetComponent<Rigidbody2D>().angularDrag = car.angularDrag * angularDragIncrease;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        CarPhysics car = collision.GetComponentInParent<CarPhysics>();
        // Test whether its a car
        if (car)
        {
            car.GetComponent<Rigidbody2D>().drag = car.linearDrag;
            car.GetComponent<Rigidbody2D>().angularDrag = car.angularDrag;
        }
    }   
}

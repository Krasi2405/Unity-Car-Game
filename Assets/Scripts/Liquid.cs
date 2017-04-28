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
        try
        {
            GameObject car = collision.gameObject;
            // Test whether its a car
            CarPhysics test = car.GetComponent<CarPhysics>();
            car.GetComponent<Rigidbody2D>().drag = test.linearDrag * linearDragIncrease;
            car.GetComponent<Rigidbody2D>().angularDrag = test.angularDrag * angularDragIncrease;
        }
        catch (MissingComponentException)
        {

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        try
        {
            GameObject car = collision.gameObject;
            // Test whether its a car
            CarPhysics test = car.GetComponent<CarPhysics>();
            car.GetComponent<Rigidbody2D>().drag = test.linearDrag;
            car.GetComponent<Rigidbody2D>().angularDrag = test.angularDrag;
        }
        catch (MissingComponentException)
        {

        }
    }   
}

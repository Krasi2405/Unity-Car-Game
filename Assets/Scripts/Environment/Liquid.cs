using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour {

    [SerializeField]
    private float linearDragIncrease = 4f;
    private float angularDragIncrease = 2f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Car car = collision.GetComponent<Car>();
        if (car != null)
        {
            CarPhysicsSO carPhysicsInfo = car.GetCarSO().physics;
            car.GetComponent<Rigidbody2D>().drag = carPhysicsInfo.linearDrag * linearDragIncrease;
            car.GetComponent<Rigidbody2D>().angularDrag = carPhysicsInfo.angularDrag * angularDragIncrease;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Car car = collision.GetComponent<Car>();
        if (car != null)
        {
            CarPhysicsSO carPhysicsInfo = car.GetCarSO().physics;
            car.GetComponent<Rigidbody2D>().drag = carPhysicsInfo.linearDrag;
            car.GetComponent<Rigidbody2D>().angularDrag = carPhysicsInfo.angularDrag;
        }
    }   
}

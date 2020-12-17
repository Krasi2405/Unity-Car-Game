using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(Rigidbody2D))]
public class CarPhysics : MonoBehaviour
{
    private CarPhysicsSO physicsInfo;

    private float currentVelocity;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    private Rigidbody2D rigidbody2D;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    private bool gas;

    // Use this for initialization
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        currentVelocity = rigidbody2D.velocity.sqrMagnitude;

        gas = false;
    }


    public void HandleMovement(float horizontalInput, float verticalInput)
    {
        float currentVelocity = rigidbody2D.velocity.sqrMagnitude;
        // Get the vector that is between the vector that is pointing upwards
        // from the car and the vector which points the direction the car is moving
        Vector2 velocityDirection = new Vector2(
                        (transform.up.x + rigidbody2D.velocity.normalized.x) / 2,
                        (transform.up.y + rigidbody2D.velocity.normalized.y) / 2).normalized;

        if (horizontalInput <= -0.05)
        {
            rigidbody2D.AddTorque(GetTorque() * -horizontalInput);
        }
        else if (horizontalInput >= 0.05)
        {
            rigidbody2D.AddTorque(-GetTorque() * horizontalInput);
        }


        if (verticalInput >= 0.05)
        {
            if (currentVelocity < physicsInfo.maxSpeed)
            {
                rigidbody2D.AddForce(transform.up * physicsInfo.power * verticalInput);
            }
            // When gas is true its harder to turn the car.
            gas = true;
        }
        if (verticalInput <= -0.05)
        {
            if (currentVelocity > 5) // Brakes
            {
                Vector2 brake_direction = velocityDirection;
                rigidbody2D.AddForce(-brake_direction * physicsInfo.brakeForce * -verticalInput);
            }
            else // Reverse movement
            {
                rigidbody2D.AddForce(transform.up * -physicsInfo.power * -verticalInput);
            }
        }
    }


    float GetTorque()
    {
        float speed = rigidbody2D.velocity.sqrMagnitude;
        float torque = 0f;

        for (int i = 0; i < speed; i++)
        {
            if (i < 7 && i >= 2)
            {
                torque += 0.2f * physicsInfo.turnPower;
            }
            else if (i >= 7 && i < physicsInfo.maxSpeed / 2)
            {
                torque += 0.03f * physicsInfo.turnPower;
            }
            else if (i >= (physicsInfo.maxSpeed / 2))
            {
                torque -= 0.04f * physicsInfo.turnPower;
            }
        }

        if (gas)
        {
            torque /= 1.6f;
        }
        return torque;
    }


    public void Setup(CarPhysicsSO carPhysicsInfo)
    {
        physicsInfo = carPhysicsInfo;
        rigidbody2D.drag = physicsInfo.linearDrag;
        rigidbody2D.angularDrag = physicsInfo.angularDrag;
        rigidbody2D.mass = carPhysicsInfo.weight;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{

    public float power = 15f;
    public float angularDrag = 3f;
    public float linearDrag = 2f;
    public float weight = 2f;
    public float brakeForce = 2f;
    public float maxHealth = 100;
    public float currentHealth;
    public float maxSpeed = 100f;
    public float turnPower = 1f;
    public int maxAmmo = 10;
    public int currentAmmo;
    public bool controlledByPlayer = false;
    public Vector3 gunPosition;

    public KeyCode forward;
    public KeyCode back;
    public KeyCode left;
    public KeyCode right;

    public float currentVelocity { get; private set; }

    private Rigidbody2D rb;
    private bool gas;
    private ParticleSystem smoke;

    private Collider2D colliderLeft;
    private Collider2D colliderRight;
    private Collider2D colliderFront;
    private Collider2D colliderBack;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = weight;
        rb.angularDrag = angularDrag;
        rb.drag = linearDrag;
        rb.gravityScale = 0f;

        smoke = GetComponentInChildren<ParticleSystem>();
        smoke.Stop();

        currentHealth = maxHealth;
        currentAmmo = maxAmmo;
    }

    void FixedUpdate()
    {
        currentVelocity = rb.velocity.sqrMagnitude;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if(currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }

        gas = false;
        BasicMovementControls();

    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SmokeState();
    }


    public void TakeCollisionDamage(float damage)
    {
        TakeDamage(damage / 10);
    }


    void BasicMovementControls()
    {
        float currentVelocity = rb.velocity.sqrMagnitude;
        // Get the vector that is between the vector that is pointing upwards
        // from the car and the vector which points the direction the car is moving
        Vector2 velocityDirection = new Vector2(
                        (transform.up.x + rb.velocity.normalized.x) / 2,
                        (transform.up.y + rb.velocity.normalized.y) / 2).normalized;

        if (controlledByPlayer)
        {
            if (Input.GetKey(left))
            {
                rb.AddTorque(GetTorque());
            }
            else if (Input.GetKey(right))
            {
                rb.AddTorque(-GetTorque());
            }
            if (Input.GetKey(forward))
            {
                if (currentVelocity < maxSpeed)
                {
                    rb.AddForce(transform.up * power);
                }
                // When gas is true its harder to turn the car.
                gas = true;
            }
            if (Input.GetKey(back))
            {
                // Brakes
                if (currentVelocity > 5)
                {
                    Vector2 brake_direction = velocityDirection;
                    rb.AddForce(brake_direction * -power * (brakeForce / 10));
                }
                // Reverse movement
                else
                {
                    rb.AddForce(transform.up * -power);
                }
            }
        }
    }

    public bool CanConsumeAmmo(int ammo)
    {
        if (ammo > currentAmmo)
        {
            return false;
        }
        else
        {
            currentAmmo -= ammo;
            return true;
        }
                
    }

    float GetTorque()
    {
        float speed = rb.velocity.sqrMagnitude;
        float torque = 0f;

        for (int i = 0; i < speed; i++)
        {
            if (i < 7 && i >= 2)
            {
                torque += 0.2f * turnPower;
            }
            else if (i >= 7 && i < maxSpeed / 2)
            {
                torque += 0.03f * turnPower;
            }
            else if (i >= (maxSpeed / 2))
            {
                torque -= 0.04f * turnPower;
            }
        }

        if (gas)
        {
            torque /= 1.6f;
        }
        return torque;
    }


    public void SmokeState()
    {
        
        if(currentHealth <= maxHealth / 4)
        {
            // TODO: Add burn effects.
            smoke.Play();
        }
        else if (currentHealth <= maxHealth / 2)
        {
            smoke.Play();
        }
        else
        {
            smoke.Stop();
        }
    }


    // Draw a circle around where the weapon is going to be placed.
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gunPosition + transform.position, 0.15f);
    }
}
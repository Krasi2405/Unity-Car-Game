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

    public KeyCode forward;
    public KeyCode back;
    public KeyCode left;
    public KeyCode right;

    public float currentVelocity { get; private set; }

    private Rigidbody2D rb;
    private bool gas;
    private ParticleSystem smoke;
    private Vector2 velocityDirection;


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

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        float force = 1f;
        float angle = Vector3.Angle(gameObject.transform.position - collidedWith.transform.position, gameObject.transform.up);
        float hitCoefficient = GetHitCoefficient(angle);

        print("Angle between " + gameObject.name + " and " + collidedWith.name + " is : " + angle);


        print("Hit coefficient is: " + hitCoefficient);

        try
        {
            force =
               (collidedWith.GetComponent<CarPhysics>().currentVelocity / 8 + currentVelocity / 4) *
                hitCoefficient;
        }
        catch (MissingComponentException)
        {
            force = currentVelocity / 20;
        }
        catch (System.NullReferenceException)
        {
            force = currentVelocity / 20;
        }

        if (force <= 0.2)
        {
            force = Random.Range(50, 2500) / 1000f;
        }

        currentHealth -= force / 3;
        print(gameObject.name + " Collided for " + force / 3 + " damage ");


        SmokeState();
    }

    void BasicMovementControls()
    {
        float currentVelocity = rb.velocity.sqrMagnitude;
        // Get the vector that is between the vector that is pointing upwards
        // from the car and the vector which points the direction the car is moving
        velocityDirection = new Vector2(
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


    private float GetHitCoefficient(float angle)
    {
        float hit_coefficient = 1f;
        for (int i = 0; i < angle; i++)
        {
            if (i <= 30 || i >= 150)
            {
                hit_coefficient += 0.05f;
            }
            else if (i >= 60 && i <= 90)
            {
                hit_coefficient += 0.08f;
            }
            else if (i >= 90 && i <= 120)
            {
                hit_coefficient -= 0.08f;
            }
            else
            {
                hit_coefficient -= 0.05f;
            }
        }
        return hit_coefficient;
    }

    public void SmokeState()
    {
        if (currentHealth <= maxHealth / 2)
        {
            smoke.Play();
        }
        else
        {
            smoke.Stop();
        }
    }
}
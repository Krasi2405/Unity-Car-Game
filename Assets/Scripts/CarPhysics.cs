using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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

    

    public float currentVelocity { get; private set; }
    
    public string horizontalInputAxis = "Horizontal0";
    public string verticalInputAxis = "Vertical0";
    public string specialInput = "Special";

    private Rigidbody2D rigidbody;
    private bool gas;
    private ParticleSystem smoke;

    private Collider2D colliderLeft;
    private Collider2D colliderRight;
    private Collider2D colliderFront;
    private Collider2D colliderBack;


    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.mass = weight;
        rigidbody.angularDrag = angularDrag;
        rigidbody.drag = linearDrag;
        rigidbody.gravityScale = 0f;

        smoke = GetComponentInChildren<ParticleSystem>();
        smoke.Stop();

        currentHealth = maxHealth;
        currentAmmo = maxAmmo;
    }

    void FixedUpdate()
    {
        currentVelocity = rigidbody.velocity.sqrMagnitude;
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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SmokeState();
    }


    void BasicMovementControls()
    {
        float currentVelocity = rigidbody.velocity.sqrMagnitude;
        // Get the vector that is between the vector that is pointing upwards
        // from the car and the vector which points the direction the car is moving
        Vector2 velocityDirection = new Vector2(
                        (transform.up.x + rigidbody.velocity.normalized.x) / 2,
                        (transform.up.y + rigidbody.velocity.normalized.y) / 2).normalized;
        
        if (controlledByPlayer)
        {
            float horizontalInput = CrossPlatformInputManager.GetAxis(horizontalInputAxis);
            float verticalInput = CrossPlatformInputManager.GetAxis(verticalInputAxis);
            // Debug.Log("Horizontal: " + horizontalInput + "\nVertical: " + verticalInput);
            if (horizontalInput <= -0.05)
            {
                rigidbody.AddTorque(GetTorque() * -horizontalInput);
            }
            else if (horizontalInput >= 0.05)
            {
                rigidbody.AddTorque(-GetTorque() * horizontalInput);
            }
            if (verticalInput >= 0.05)
            {
                if (currentVelocity < maxSpeed)
                {
                    rigidbody.AddForce(transform.up * power * verticalInput);
                }
                // When gas is true its harder to turn the car.
                gas = true;
            }
            if (verticalInput <= -0.05)
            {
                // Brakes
                if (currentVelocity > 5)
                {
                    Vector2 brake_direction = velocityDirection;
                    rigidbody.AddForce(-brake_direction * power * (brakeForce / 10) * -verticalInput);
                }
                // Reverse movement
                else
                {
                    rigidbody.AddForce(transform.up * -power * -verticalInput);
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
        float speed = rigidbody.velocity.sqrMagnitude;
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
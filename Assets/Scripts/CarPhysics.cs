using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour {

    public float acceleration = 15f;
    public float angularDrag = 3f;
    public float linearDrag = 2f;
    public float weight = 2f;
    public float brake_force = 2f;
    public float hit_points = 100;
    public float max_speed = 100f;
    public float torque_coefficient = 1f;
    public bool controlled_by_player = false;

    public KeyCode forward;
    public KeyCode back;
    public KeyCode left;
    public KeyCode right;

    public float curr_speed { get; private set;}

    private Rigidbody2D rb;
    private bool gas;
    private ParticleSystem smoke;
    

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = weight;
        rb.angularDrag = angularDrag;
        rb.drag = linearDrag;
        rb.gravityScale = 0f;
        smoke = GetComponentInChildren<ParticleSystem>();
        smoke.Stop();
	}

    void FixedUpdate()
    {
        BasicMovementControls();
        curr_speed = rb.velocity.sqrMagnitude;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        float force = 1f;
        float angle = Vector3.Angle(gameObject.transform.position - collidedWith.transform.position, gameObject.transform.up);
        float hit_coefficient = GetHitCoefficient(angle);

        print("Angle between " + gameObject.name + " and " + collidedWith.name + " is : " + angle);

        
        print("Hit coefficient is: " + hit_coefficient);
        
        try
        {
            force =
               (collidedWith.GetComponent<CarPhysics>().curr_speed / 8 + curr_speed / 4) *
                hit_coefficient / 3;
        }
        catch(MissingComponentException)
        {
            force = curr_speed / 50;
        }
        catch(System.NullReferenceException)
        {
            force = curr_speed / 50;
        }

        if (force <= 0.1)
        {
            force = Random.Range(1, 4);
        }

        hit_points -= force;
        print(gameObject.name + " Collided for " + force + " damage ");


        if(hit_points <= 40) {
            smoke.Play();
        }
    }

    void BasicMovementControls()
    {
        float curr_velocity = rb.velocity.sqrMagnitude;
        if (controlled_by_player)
        {
            if (Input.GetKey(left))
            {
                rb.AddTorque(GetTorque());
            }
            if (Input.GetKey(right))
            {
                rb.AddTorque(-GetTorque());
            }
            if (Input.GetKey(forward))
            {
                if (curr_velocity < max_speed)
                {
                    rb.AddForce(transform.up * acceleration);
                }
                else
                {
                    rb.AddForce(transform.up * acceleration / 4);
                }
                gas = true;
            }
            else
            {
                gas = false;
            }
            if (Input.GetKey(back))
            {
                rb.angularDrag *= 1.5f;
                if (curr_velocity > 5)
                {
                    Vector2 brake_direction = new Vector2(
                        (transform.up.x + rb.velocity.normalized.x) / 2,
                        (transform.up.y + rb.velocity.normalized.y) / 2);
                    rb.AddForce(brake_direction * -acceleration * brake_force / 5);
                }
                else
                {
                    rb.AddForce(transform.up * -acceleration);
                }

                rb.angularDrag = angularDrag;
                rb.drag = linearDrag;
            }
        }
    }

    float GetTorque()
    {
        float speed = rb.velocity.sqrMagnitude;
        float torque = 0f;

        for(int i = 0; i < speed; i++)
        {
            if(i < 8 && i >= 3)
            {
                torque += 0.2f * torque_coefficient;
            }
            else if(i >= 8 && i < max_speed / 2)
            {
                torque += 0.03f * torque_coefficient;
            }
            else if(i >= (max_speed / 2) )
            {
                torque -= 0.04f * torque_coefficient;
            }
        }

        if(gas)
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
}

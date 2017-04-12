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
    public float brake_difference = 2f;
    public float torque_coefficient = 1f;
    public bool controlled_by_player = false;

    public KeyCode forward;
    public KeyCode back;
    public KeyCode left;
    public KeyCode right;

    public float curr_speed { get; private set;}

    private Rigidbody2D rb;
    private bool gas;
    private bool brake_left = true;
    private float last_brake_tick;
    

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = weight;
        rb.angularDrag = angularDrag;
        rb.drag = linearDrag;
	}

    void FixedUpdate()
    {
        BasicMovementControls();
        curr_speed = rb.velocity.sqrMagnitude;


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        float force;
        try
        {
            force =
                Mathf.Clamp(collidedWith.GetComponent<Rigidbody2D>().mass / rb.mass, 0.5f, 1.5f)
                * (collidedWith.GetComponent<CarPhysics>().curr_speed / 4 + curr_speed / 4);
        }
        catch(MissingComponentException)
        {
            force = Mathf.Clamp(1 / rb.mass, 0.5f, 1.5f) * curr_speed / 2;
        }
        hit_points -= force;
        print(gameObject.name + " Collided for " + force + " damage ");
    }

    void BasicMovementControls()
    {

        last_brake_tick += Time.deltaTime;
        float curr_velocity = rb.velocity.sqrMagnitude;
        if (controlled_by_player)
        {
            if (Input.GetKey(left))
            {
                rb.AddTorque(GetTorqueDependingOnSpeed());
            }
            if (Input.GetKey(right))
            {
                rb.AddTorque(-GetTorqueDependingOnSpeed());
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
                if (curr_velocity > 10)
                {
                    Vector3 brake_rotate = Vector3.zero;
                    if (brake_left && last_brake_tick >= 0.2)
                    {
                        brake_rotate.z = brake_difference + Random.Range(-brake_difference / 5, brake_difference / 5);
                        brake_left = false;
                    }
                    else if (!brake_left && last_brake_tick >= 0.2)
                    {
                        brake_rotate.z = -(brake_difference + Random.Range(-brake_difference / 5, brake_difference / 5));
                        brake_left = true;
                    }
                    transform.Rotate(brake_rotate);
                    rb.AddForce(transform.up * -acceleration / brake_force);
                }
                else
                {
                    rb.AddForce(transform.up * -acceleration / 3);
                }


            }
        }
    }

    float GetTorqueDependingOnSpeed()
    {
        float speed = rb.velocity.sqrMagnitude;
        float torque = 0f;

        for(int i = 0; i < speed; i++)
        {
            if(i < 7 && i >= 1)
            {
                torque += 0.2f * torque_coefficient;
            }
            else if(i >= 7 && i < max_speed / 2)
            {
                torque += 0.02f * torque_coefficient;
            }
            else if(i >= max_speed / 2)
            {
                torque -= 0.03f * torque_coefficient;
            }
        }

        if(gas)
        {
            torque /= 1.5f;
        }
        return torque;
    }
}

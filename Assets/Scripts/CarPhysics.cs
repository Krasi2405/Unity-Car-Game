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
    public float brake_turbulence_coefficient = 1f;
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
    private ParticleSystem smoke;
    

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = weight;
        rb.angularDrag = angularDrag;
        rb.drag = linearDrag;
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
        float hit_coefficient = 1f;

        print("Angle between " + gameObject.name + " and " + collidedWith.name + " is : " + angle);

        for (int i = 0; i < angle; i++)
        {
            if(i <= 30 || i >= 150)
            {
                hit_coefficient += 0.05f;
            }
            else if(i >= 60 && i <= 90)
            {
                hit_coefficient += 0.08f;
            }
            else if(i >= 90 && i <= 120)
            {
                hit_coefficient -= 0.08f;
            }
            else
            {
                hit_coefficient -= 0.05f;
            }
        }
        print("Hit coefficient is: " + hit_coefficient);
        
        try
        {
            force =
               (collidedWith.GetComponent<CarPhysics>().curr_speed / 8 + curr_speed / 4) *
                hit_coefficient;
        }
        catch(MissingComponentException)
        {
            force = Mathf.Clamp(1 / rb.mass, 0.5f, 1.5f) * curr_speed / 4;
        }

        if (force <= 0)
        {
            force = Random.Range(1, 5);
        }

        hit_points -= force;
        print(gameObject.name + " Collided for " + force + " damage ");


        if(hit_points <= 40) {
            smoke.Play();
        }
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
                if (curr_velocity > max_speed / 3)
                {
                    Vector3 brake_rotate = Vector3.zero;
                    if (brake_left && last_brake_tick >= 0.2)
                    {
                        brake_rotate.z = brake_difference + Random.Range(-brake_difference * brake_turbulence_coefficient, brake_difference * brake_turbulence_coefficient);
                        brake_left = false;
                    }
                    else if (!brake_left && last_brake_tick >= 0.2)
                    {
                        brake_rotate.z = -(brake_difference + Random.Range(-brake_difference * brake_turbulence_coefficient, brake_difference * brake_turbulence_coefficient));
                        brake_left = true;
                    }
                    transform.Rotate(brake_rotate);
                    rb.AddForce((transform.up * -acceleration) * brake_force);
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

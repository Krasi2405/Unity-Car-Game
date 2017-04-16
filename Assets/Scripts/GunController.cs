using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public int ammoPerBullet = 1;
    public float destroyTime = 5f;
    public float fireDelay = 1f;
    public float projectileSpeed = 5f;
    public GameObject projectilePrefab;
    public KeyCode activationKey = KeyCode.Space;

    private GameObject car;
    private float currentDelay;


	void Start () {
        // The gun should always be a child of a car
        car = gameObject.transform.parent.gameObject;
	}
	
	void Update () {
        currentDelay += Time.deltaTime;

		if(Input.GetKey(activationKey) && currentDelay >= fireDelay)
        {
            currentDelay = 0;
            foreach(Transform child in transform)
            {
                Vector3 position = child.position;
                position.z = -5;
                if(car.GetComponent<CarPhysics>().CanConsumeAmmo(ammoPerBullet))
                {
                    Fire(position);
                }
            }
        }
	}

    void Fire(Vector3 position)
    {
        
        GameObject shot = Instantiate(projectilePrefab, position, car.transform.rotation) as GameObject;
        float speedCoefficient = Mathf.Clamp(car.GetComponent<CarPhysics>().currentVelocity / car.GetComponent<CarPhysics>().maxSpeed, 0.25f, 1);
        shot.GetComponent<Rigidbody2D>().velocity = car.transform.up * projectileSpeed * speedCoefficient;
        shot.GetComponent<Projectile>().parent = car;
        Destroy(shot, destroyTime);
        print("Shot fired from " + car.name + " with " + car.transform.up * projectileSpeed + " velocity");
    }
}

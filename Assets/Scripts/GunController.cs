using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public int ammoPerBullet = 1;
    public float destroyTime = 5f;
    public float fireDelay = 1f;
    public float projectileSpeed = 5f;
    public float shotDeviation = 1f;
    public float recoil = 5f;
    public GameObject projectilePrefab;
    public KeyCode activationKey = KeyCode.Space;

    protected GameObject car;
    protected float currentDelay = 0;


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
                if(car.GetComponent<CarPhysics>().CanConsumeAmmo(ammoPerBullet))
                {
                    Fire(position);
                }
            }
        }
	}

    void Fire(Vector3 position)
    {
        Vector3 deviatedShotRotation = car.transform.rotation.eulerAngles;
        deviatedShotRotation.z += Random.Range(-shotDeviation, shotDeviation);

        GameObject shot = Instantiate(projectilePrefab, position, Quaternion.Euler(deviatedShotRotation)) as GameObject;
        float speedCoefficient = Mathf.Clamp(car.GetComponent<CarPhysics>().currentVelocity / car.GetComponent<CarPhysics>().maxSpeed, 0.75f, 1);

        // Shot deviation
        Transform shotTransform = car.transform;
        shotTransform.rotation = Quaternion.Euler(deviatedShotRotation);
        shot.GetComponent<Rigidbody2D>().velocity = shotTransform.up * projectileSpeed * speedCoefficient;

        // Set the parent of the shot so it doesnt target the car that it shot from.
        shot.GetComponent<Projectile>().parent = car;
        
        // Recoil
        car.GetComponent<Rigidbody2D>().AddForce(-car.transform.up * recoil);

        Destroy(shot, destroyTime);
        print("Shot fired from " + car.name + " with " + car.transform.up * projectileSpeed + " velocity");
    }
}

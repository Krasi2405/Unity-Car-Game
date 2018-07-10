using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunController : GunController {

    public float damageIncreasePerSecond = 1f;
    public float maxSeconds = 5f;
    public float sizeIncreasePerSecond = 0.5f;

    private float currentSeconds = 0f;
    private GameObject shot;

	void Update () {
        if (currentDelay >= fireDelay) {
            if (Input.GetKeyDown(activationKey))
            {
                // Create the ball
                Transform child = transform.GetChild(0);
                shot = Instantiate(projectilePrefab, child.position, Quaternion.identity) as GameObject;
                shot.GetComponent<Projectile>().carParent = car;
            }
            else if (Input.GetKey(activationKey) && shot != null)
            {
                // Grow the ball
                Vector3 scale = shot.transform.localScale;
                scale.x += sizeIncreasePerSecond * Time.deltaTime;
                scale.y += sizeIncreasePerSecond * Time.deltaTime;
                shot.transform.localScale = scale;
                shot.transform.position = transform.GetChild(0).position;

                shot.GetComponent<Projectile>().damage += damageIncreasePerSecond * Time.deltaTime;
                currentSeconds += Time.deltaTime;
                if (currentSeconds >= maxSeconds)
                {
                    Fire();
                }
            }
            else if (Input.GetKeyUp(activationKey) && shot != null)
            {
                // Shoot the ball
                Fire();
            }
        }
        else
        {
            currentDelay += Time.deltaTime;
        }
    }

    void Fire()
    {
        float speedCoefficient = Mathf.Clamp(car.GetComponent<CarPhysics>().currentVelocity / car.GetComponent<CarPhysics>().maxSpeed, 0.75f, 1);
        shot.GetComponent<Rigidbody2D>().velocity = car.transform.up * projectileSpeed * speedCoefficient;
        Destroy(shot, destroyTime);
        shot = null;
        currentSeconds = 0;
        currentDelay = 0;
    }
}

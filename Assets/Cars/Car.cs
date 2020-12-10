using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CarPhysics))]
public class Car : MonoBehaviour {


    [SerializeField]
    private AudioClip destructionSound;

    private Health health;
    private CarPhysics carPhysics;

    private GunBase gun;


	void Awake () {
        health = GetComponent<Health>();
        carPhysics = GetComponent<CarPhysics>();
	}
	

    public void SpawnGun(GunSO gunSO)
    {
        gun = Instantiate(gunSO.prefab, transform.position, Quaternion.identity);
    }

    public void ActivateDeathSequence()
    {
        Destroy(carPhysics);

        Invoke("MakeCarImmovable", 1.5f);
    }


    private void MakeCarImmovable()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.mass *= 100;
        rigidbody.drag *= 10;
        rigidbody.angularDrag *= 10;
    }
}

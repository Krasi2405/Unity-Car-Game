using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CarPhysics))]
public class Car : MonoBehaviour {
    
    public Health health { get; private set; }
    public CarPhysics carPhysics { get; private set; }
    public GunBase gun { get; private set; }

    [SerializeField]
    private AudioClip destructionSound;

    [SerializeField]
    private 

	void Awake () {
        health = GetComponent<Health>();
        carPhysics = GetComponent<CarPhysics>();
        gun = GetComponentInChildren<GunBase>();
	}
	

    public void ActivateDeathSequence()
    {
        Destroy(carPhysics);

        Invoke("MakeCarImmovable", 1.5f);
        Debug.LogWarning("Car " + gameObject.name + " is dead!");
    }


    private void MakeCarImmovable()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.mass *= 100;
        rigidbody.drag *= 10;
        rigidbody.angularDrag *= 10;
    }
}

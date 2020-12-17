using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthSystem))]
public abstract class BoostBase : MonoBehaviour {

    [SerializeField]
    ParticleSystem pickupParticleSystem = null;

    [SerializeField]
    ParticleSystem destructionParticleSystem = null;

    [SerializeField]
    AudioClip audioClipOnPickup = null;

    [SerializeField]
    AudioClip audioClipOnDestroy = null;

    protected HealthSystem healthSystem;

    protected void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += Boost_OnDeath;
    }

    private void Boost_OnDeath(object sender, System.EventArgs e)
    {
        Instantiate(destructionParticleSystem, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(audioClipOnDestroy, transform.position);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CarCollider carCollider = collision.GetComponent<CarCollider>();
        if(carCollider)
        {
            OnPickup(carCollider.GetAttachedCar());

            AudioSource.PlayClipAtPoint(audioClipOnPickup, transform.position);
            Instantiate(pickupParticleSystem, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    protected abstract void OnPickup(Car car);
}

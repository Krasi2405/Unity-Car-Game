using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoostBase : MonoBehaviour {

    [SerializeField]
    ParticleSystem pickupParticleSystem;

    [SerializeField]
    AudioClip audioClipOnPickup;

    [SerializeField]
    AudioClip audioClipOnDestroy;

    private bool isBeingDestroyed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBeingDestroyed) return;
        CarCollider carCollider = collision.GetComponent<CarCollider>();
        if(carCollider)
        {
            if (audioClipOnPickup)
                AudioSource.PlayClipAtPoint(audioClipOnPickup, transform.position);
            if (pickupParticleSystem)
                Instantiate(pickupParticleSystem, transform.position, Quaternion.identity);

            ApplyEffect(carCollider.gameObject.GetComponent<Car>());
            isBeingDestroyed = true;
            Destroy(gameObject, 0.1f);
        }
    }

    protected abstract void ApplyEffect(Car car);
}

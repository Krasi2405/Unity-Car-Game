using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour {
    
    private enum ColliderNormal{ Forward, Left, Back, Right}

    [SerializeField]
    ColliderNormal colliderNormal = ColliderNormal.Forward;

    [SerializeField]
    [Range(0.25f, 3f)]
    private float damageCoefficientOnPart = 1f;

    [SerializeField] [Range(0.01f, 2f)]
    private float carCrashDamageCoefficient = 0.1f;

    [SerializeField] [Range(0.01f, 1f)]
    private float objectCrashDamageCoefficient = 0.02f;

    private Car car;

    private HealthSystem carHealthSystem;

    private void Start()
    {
        car = GetComponentInParent<Car>();
        if(car == false)
        {
            Debug.LogError($"{name} is not attached to a car!");
            return;
        }
        carHealthSystem = car.GetComponent<HealthSystem>();
    }
    
    public void TakeDamage(float damage)
    {
        carHealthSystem.TakeDamage(damage * damageCoefficientOnPart);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProjectileBase projectile = collision.gameObject.GetComponent<ProjectileBase>();
        if(projectile) { return; } // Ignore projectile collisions since they handle dealing damage to the car based on their damage.

        float hitCoefficient = Mathf.Abs(Vector2.Dot(collision.relativeVelocity.normalized, GetColliderNormal()));
        float hitSpeed = collision.relativeVelocity.sqrMagnitude;
        float damage = hitSpeed * hitCoefficient;


        CarPhysics enemyCar = collision.gameObject.GetComponent<CarPhysics>();
        if (enemyCar)
        {
            Debug.Log($"{car.name} hit {collision.rigidbody.gameObject.name} " +
                $"with speed {hitSpeed}, coefficient {hitCoefficient} and dealt {damage} pure damage");
            TakeDamage(damage * carCrashDamageCoefficient);
            // Don't deal damage to other car collider as it's own script takes care of it.
        }
        else
        {
            TakeDamage(damage * objectCrashDamageCoefficient);
            
            HealthSystem objectHealthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if(objectHealthSystem)
            {
                objectHealthSystem.TakeDamage(damage);
            }
        }
    }


    private Vector2 GetColliderNormal()
    {
        if(Application.isEditor)
        {
            car = GetComponentInParent<Car>();
        }

        switch(colliderNormal)
        {
            case ColliderNormal.Forward:
                return car.transform.up;
            case ColliderNormal.Back:
                return car.transform.up * -1;
            case ColliderNormal.Right:
                return car.transform.right;
            case ColliderNormal.Left:
                return car.transform.right * -1;
            default:
                return Vector3.zero;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, (Vector2) transform.position + GetColliderNormal() * 0.2f);
    }


    public Car GetAttachedCar()
    {
        return car;
    }
}

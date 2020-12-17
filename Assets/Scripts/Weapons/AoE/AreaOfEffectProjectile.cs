using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AreaOfEffectProjectile : ProjectileBase {

    [SerializeField]
    private float destructionTime = 1;

    [SerializeField]
    private UnityEvent OnStartDestruction = null;

    // Separate list is needed so OnStayActivateEffect(Car) is not activated for every collider.
    private List<Car> affectedCars = new List<Car>();
    

    private void Update()
    {
        foreach(Car affectedCar in affectedCars)
        {
            OnStayActivateEffect(affectedCar);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CarCollider carCollider = collision.GetComponent<CarCollider>();
        if(carCollider && carCollider.GetAttachedCar() != owner)
        {
            Car car = carCollider.GetAttachedCar();
            if(affectedCars.Contains(car) == false)
            {
                OnEnterActivateEffect(car);
                affectedCars.Add(car);
            }
        }
    }

    protected virtual void OnEnterActivateEffect(Car car) { }

    private void OnTriggerStay2D(Collider2D other)
    {
        CarCollider carCollider = other.GetComponent<CarCollider>();
        if (carCollider && carCollider.GetAttachedCar() != owner)
        {
            OnStayActivateEffect(carCollider);
        }
        else
        {
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            if (healthSystem)
            {
                OnStayActivateEffectOnObject(healthSystem);
            }
        }
    }

    protected virtual void OnStayActivateEffect(CarCollider hitCollider) { }
    protected virtual void OnStayActivateEffect(Car car) { }

    protected virtual void OnStayActivateEffectOnObject(HealthSystem healthSystem) { }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CarCollider carCollider = collision.GetComponent<CarCollider>();
        if (carCollider && carCollider.GetAttachedCar() != owner)
        {
            Car car = carCollider.GetAttachedCar();
            if (affectedCars.Contains(car))
            {
                OnExitActivateEffect(car);
                affectedCars.Remove(car);
            }
        }
    }

    protected virtual void OnExitActivateEffect(Car car) { }


    public virtual void DestroyEffect()
    {
        OnStartDestruction?.Invoke();
        Destroy(gameObject, destructionTime);
        GetComponent<Collider2D>().enabled = false;
    }

    public void DestroyImmediate()
    {
        Destroy(gameObject);
    }
}

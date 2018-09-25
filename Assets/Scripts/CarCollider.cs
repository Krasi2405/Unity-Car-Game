using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour {
    
    enum ProjectTowards{ Forward, Left, Back, Right}
    [SerializeField]
    ProjectTowards option;
    [SerializeField]
    Vector3 projectTowards;

    [SerializeField]
    [Range(0.25f, 2f)]
    private float damageCoefficientOnPart = 1f;

    [SerializeField]
    public Car car { get; private set; }

    private void Start()
    {
        car = transform.parent.GetComponent<Car>();
    }
    
    public void TakeDamage(float damage)
    {
        car.health.TakeDamage(damage * damageCoefficientOnPart);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CarPhysics enemyCar = collision.gameObject.GetComponent<CarPhysics>();
        if (!enemyCar) return;

        UpdateCarProjection();
        Vector2 enemyVelocity = enemyCar.GetComponent<Rigidbody2D>().velocity;
        Vector3 enemyVelocity3D = new Vector3(enemyVelocity.x, enemyVelocity.y, 0);

        // TODO: use dot product, since i now actually understand how it works, instead of this piece of shit.

        float damage = Vector3.Project(enemyVelocity3D, projectTowards).sqrMagnitude;
        Debug.Log("Damage coefficient of hit on " + name + " by " + enemyCar.name + ": " + damage + "(" + Vector3.Project(enemyVelocity3D, projectTowards) + ")");
        Debug.Log("Enemy velocity: " + enemyVelocity3D);

        TakeDamage(damage / 10);
    }


    private void UpdateCarProjection()
    {
        Transform carTransform = car.transform;
        if (option == ProjectTowards.Forward)
        {
            projectTowards = carTransform.up;
        }
        else if (option == ProjectTowards.Back)
        {
            projectTowards = -carTransform.up;
        }
        else if (option == ProjectTowards.Left)
        {
            projectTowards = -carTransform.right;
        }
        else if (option == ProjectTowards.Right)
        {
            projectTowards = carTransform.right;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + projectTowards / 10);
    }
}

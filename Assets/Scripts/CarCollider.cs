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
    public CarPhysics car { get; private set; }

    private void Start()
    {
        car = transform.parent.GetComponent<CarPhysics>();
    }

    void Update () {
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
        else if(option == ProjectTowards.Right)
        {
            projectTowards = carTransform.right;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(name + " of " + transform.parent.name + " hit by " + collision.gameObject.name)
        CarPhysics enemyCar = collision.gameObject.GetComponent<CarPhysics>();
        if (!enemyCar) return;
        Vector2 enemyVelocity = enemyCar.GetComponent<Rigidbody2D>().velocity;
        Vector3 enemyVelocity3D = new Vector3(enemyVelocity.x, enemyVelocity.y, 0);
        
        float damage = Vector3.Project(enemyVelocity3D, projectTowards).sqrMagnitude;
        Debug.Log("Damage coefficient of hit on " + name + " by " + enemyCar.name + ": " + damage + "(" + Vector3.Project(enemyVelocity3D, projectTowards) + ")");
        Debug.Log("Enemy velocity: " + enemyVelocity3D);

        TakeCollisionDamage(damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + projectTowards / 10);
    }

    private void TakeDamage(float damage)
    {
        car.TakeDamage(damage * damageCoefficientOnPart);
    }

    public void TakeCollisionDamage(float damage)
    {
        TakeDamage(damage / 10);
    }

    public void TakeFireDamage(float damage)
    {
        TakeDamage(damage / 5);
    }

    public void TakeExplosiveDamage(float damage)
    {
        TakeDamage(damage / 4);
    }

    public void TakePenetrationDamage(float damage)
    {
        TakeDamage(damage / 5);
    }
}

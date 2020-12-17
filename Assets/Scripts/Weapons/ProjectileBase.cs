using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour {

    [SerializeField]
    protected float damage;

    protected Car owner;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    private Rigidbody2D rigidbody2D;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetOwner(Car owner)
    {
        this.owner = owner;
    }

    public void AddVelocity(Vector2 velocity)
    {
        rigidbody2D.velocity += velocity;
    }

    public void SetVelocity(Vector2 velocity)
    {
        rigidbody2D.velocity = velocity;
    }
}

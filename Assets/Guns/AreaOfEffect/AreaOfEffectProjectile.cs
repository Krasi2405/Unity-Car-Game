using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaOfEffectProjectile : ProjectileBase {
    

    [SerializeField]
    private float destructionTime = 1;
    [SerializeField]
    private bool parentToCar = true;

    private Animator animator;
    

    protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        ActivateEffect(other.GetComponentInParent<Car>());
    }

    protected abstract void ActivateEffect(Car target);
    public virtual void DestroyEffect()
    {
        if (animator) animator.SetTrigger("Destroy");
        Destroy(gameObject, destructionTime);
    }

    public void DestroyImmediate()
    {
        Destroy(gameObject);
    }
}

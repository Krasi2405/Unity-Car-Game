using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : GunBase
{
    [SerializeField]
    Projectile projectilePrefab;

    [SerializeField]
    private float launchVelocity = 5f;

    [SerializeField]
    float shotDelay = 0.5f;
    
    [SerializeField]
    private float currentDelay;


    protected override void ActivateGun()
    {
        currentDelay = 0;
        PlaySound();
    }

    protected override void GunActive()
    {
        currentDelay -= Time.deltaTime;
        if (currentDelay <= 0)
        {
            currentDelay = shotDelay;
            List<ProjectileBase> projectiles = InstantiateProjectiles(projectilePrefab);
            foreach(Projectile projectile in projectiles)
            {
                DecreaseAmmo();
                projectile.SetOwner(owner);
                AddVelocity(projectile);
            }
        }
    }

    protected override void DeactivateGun()
    {
        StopSound();
        currentDelay = 0;
    }

    private void AddVelocity(Projectile projectile)
    {
        Rigidbody2D rigidbody = projectile.GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.up * launchVelocity;
    }
}

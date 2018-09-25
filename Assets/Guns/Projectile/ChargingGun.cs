using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingGun : GunBase
{
    [SerializeField]
    Projectile projectilePrefab;

    [SerializeField]
    float maxSeconds = 4f;

    [SerializeField]
    private float launchVelocity = 10;
    private List<ProjectileBase> currentProjectiles = new List<ProjectileBase>();

    private bool gunIsActive = false;


    protected override void ActivateGun()
    {
        currentProjectiles = InstantiateProjectiles(projectilePrefab);
        PlaySound();
        gunIsActive = true;

        Invoke("DeactivateGun", maxSeconds);
        foreach(ChargingProjectile projectile in currentProjectiles)
        {
            projectile.transform.parent = transform.parent;
            projectile.gameObject.GetComponent<Collider2D>().enabled = false;
            projectile.SetOwner(owner);
        }
    }


    protected override void GunActive()
    {
        foreach(ChargingProjectile projectile in currentProjectiles)
        {
            projectile.OnCharge();
        }
    }

    protected override void DeactivateGun()
    {
        if (!gunIsActive) return;
        Debug.Log("Shoot projectile laser!");
        foreach(Projectile projectile in currentProjectiles)
        {
            AddVelocity(projectile);
            projectile.transform.parent = null;
            projectile.gameObject.GetComponent<Collider2D>().enabled = true;
            DecreaseAmmo();
        }

        gunIsActive = false;
        currentProjectiles.Clear();
        StopSound();
    }


    private void AddVelocity(Projectile projectile)
    {
        // Velocity added!
        Rigidbody2D rigidbody = projectile.GetComponent<Rigidbody2D>();
        Vector3 velocity = transform.up * launchVelocity;
        Debug.Log("add velocity: " + velocity);
        rigidbody.velocity = velocity;
    }
}

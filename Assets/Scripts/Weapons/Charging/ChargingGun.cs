using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingGun : GunBase
{
    private List<ProjectileBase> currentProjectiles = null;

    private bool gunIsCharging = false;


    protected override void ActivateGun()
    {
        base.ActivateGun();

        gunIsCharging = true;
        currentProjectiles = InstantiateProjectiles(gunInfo.projectilePrefab);

        Invoke("DeactivateGun", gunInfo.fireCooldown);
        foreach(ChargingProjectile projectile in currentProjectiles)
        {
            projectile.transform.SetParent(owner.transform, true);
            projectile.GetComponent<Collider2D>().enabled = false;
            // Setting to kinematic because otherwise projectile will not follow gun.
            projectile.GetComponent<Rigidbody2D>().isKinematic = true; 
        }
    }


    protected override void HandleGunActivity()
    {
        base.HandleGunActivity();

        foreach(ChargingProjectile projectile in currentProjectiles)
        {
            projectile.OnCharge();
            DecreaseAmmo(Time.deltaTime);
        }
    }

    protected override void DeactivateGun()
    {
        base.DeactivateGun();

        if (gunIsCharging == false) return;
        gunIsCharging = false;

        foreach (Projectile projectile in currentProjectiles)
        {
            projectile.transform.SetParent(null);
            projectile.GetComponent<Collider2D>().enabled = true;
            projectile.GetComponent<Rigidbody2D>().isKinematic = false;
            projectile.AddVelocity(transform.up * gunInfo.projectileSpeed);
        }
        currentProjectiles.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : GunBase
{
    private float currentFireCooldown = 0.0f;

    protected override void ActivateGun()
    {
        base.ActivateGun();

        currentFireCooldown = 0.0f;
    }

    protected override void HandleGunActivity()
    {
        base.HandleGunActivity();

        if (currentFireCooldown <= 0.0f)
        {
            List<ProjectileBase> projectiles = InstantiateProjectiles(gunInfo.projectilePrefab);
            foreach(ProjectileBase projectile in projectiles)
            {
                projectile.AddVelocity(transform.up * gunInfo.projectileSpeed);
            }
            currentFireCooldown = gunInfo.fireCooldown;
        }
        currentFireCooldown -= Time.deltaTime;
    }
}

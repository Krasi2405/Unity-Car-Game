using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AreaOfEffectGun : GunBase
{
    private List<AreaOfEffectProjectile> instantiatedProjectiles = new List<AreaOfEffectProjectile>();


    protected override void ActivateGun()
    {
        base.ActivateGun();

        instantiatedProjectiles = InstantiateProjectiles(gunInfo.projectilePrefab).Cast<AreaOfEffectProjectile>().ToList();

        foreach (AreaOfEffectProjectile aoeProjectile in instantiatedProjectiles)
        {
            aoeProjectile.transform.SetParent(gameObject.transform, true);
        }
    }
    
    protected override void HandleGunActivity()
    {
        base.HandleGunActivity();

        DecreaseAmmo(Time.deltaTime);
    }

    protected override void DeactivateGun()
    {
        base.DeactivateGun();

        for(int i = 0; i < instantiatedProjectiles.Count; i++)
        {
            instantiatedProjectiles[i].DestroyEffect();
        }
        instantiatedProjectiles.Clear();
    }

    protected override float GetAmmoCostForFire()
    {
        return base.GetAmmoCostForFire() * Time.deltaTime;
    }


}

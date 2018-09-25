using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffectGun : GunBase
{
    [SerializeField]
    private AreaOfEffectProjectile areaOfEffectPrefab;

    private List<AreaOfEffectProjectile> instantiatedProjectiles;
    private void Start()
    {
        instantiatedProjectiles = new List<AreaOfEffectProjectile>();
    }

    protected override void ActivateGun()
    {
        foreach (Position position in shotPositions)
        {
            Vector3 ownerRotation = Vector3.zero;
            if(owner)
            {
                ownerRotation = owner.transform.rotation.eulerAngles;
            }

            Vector3 rotation = ownerRotation + areaOfEffectPrefab.transform.rotation.eulerAngles;
            AreaOfEffectProjectile AOE = Instantiate(areaOfEffectPrefab, position.transform.position, 
                Quaternion.Euler(rotation));
            instantiatedProjectiles.Add(AOE);
            AOE.SetOwner(owner);
            AOE.transform.parent = gameObject.transform;
        }

        PlaySound();
    }
    
    protected override void GunActive()
    {
        DecreaseAmmo();
    }

    protected override void DeactivateGun()
    {
        for(int i = 0; i < instantiatedProjectiles.Count; i++)
        {
            instantiatedProjectiles[i].DestroyEffect();
            instantiatedProjectiles.RemoveAt(i);
        }

        StopSound();
    }

    
}

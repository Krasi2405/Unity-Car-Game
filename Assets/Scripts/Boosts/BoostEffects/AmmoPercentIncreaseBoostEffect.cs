using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPercentIncreaseBoostEffect : BoostEffect
{
    [SerializeField][Range(0.0f, 1.0f)]
    private float percentAmmoIncrease = 0.0f;
    public override void ApplyEffect(Car car)
    {
        GunBase gun = car.GetGun();
        GunSO gunInfo = gun.GetGunInfo();

        gun.AddAmmo(gunInfo.maxAmmo * percentAmmoIncrease);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoFlatIncreaseBoostEffect : BoostEffect {

    [SerializeField]
    private int ammoInPack = 0;

    public override void ApplyEffect(Car car)
    {
        car.GetGun().AddAmmo(ammoInPack);
    }
}

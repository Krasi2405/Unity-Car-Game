using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MomentaryBoosts {

    public int ammoInPack = 5;

    protected override void Effect(Car car)
    {
        car.gun.AddAmmo(ammoInPack);
    }
}

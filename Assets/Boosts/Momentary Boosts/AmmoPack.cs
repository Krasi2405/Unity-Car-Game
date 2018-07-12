using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MomentaryBoosts {

    public int ammoInPack = 5;

    protected override void Effect(CarPhysics car)
    {
        car.currentAmmo += ammoInPack;
        if(car.currentAmmo > car.maxAmmo)
        {
            car.currentAmmo = car.maxAmmo;
        }
    }
}

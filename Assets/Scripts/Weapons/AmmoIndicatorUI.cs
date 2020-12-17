using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoIndicatorUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform indicatorMask = null;

    private GunBase gun;

    public void Setup(GunBase newGun)
    {
        gun = newGun;
        gun.OnAmmoChange.AddListener(UpdateAmmo);
        UpdateAmmo();
    }

    public void UpdateAmmo()
    {
        float missingAmmoPercentage = gun.GetAmmo() / gun.GetGunInfo().maxAmmo;
        indicatorMask.localScale = new Vector3(1 - missingAmmoPercentage, 1);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInfoGun : TextInfoBase
{
    public override string GetInformation()
    {
        GunController gunScript = GetComponent<GunController>();
        
        string gunInfo = "";
        gunInfo += "Ammo Per Shot  " + gunScript.ammoPerBullet + "\n";
        gunInfo += "Delay          " + gunScript.fireDelay + "\n";
        gunInfo += "Recoil         " + gunScript.recoil + "\n";
        gunInfo += "Shot Deviation " + gunScript.shotDeviation + "\n";
        gunInfo += "Shot Speed     " + gunScript.projectileSpeed + "\n";

        return gunInfo;
    }
}

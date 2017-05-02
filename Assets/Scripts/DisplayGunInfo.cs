using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGunInfo : MonoBehaviour
{

    public GameObject gun;

    private Transform textCarInfo;
    private GunController gunScript;

    void Start()
    {
        textCarInfo = gameObject.transform.GetChild(0);

        UpdateInfo();
    }

    public void UpdateInfo()
    {
        gunScript = gun.GetComponent<GunController>();

        // Set the gun stats.
        string gunInfo = "";
        gunInfo += "Ammo Per Shot  " + gunScript.ammoPerBullet + "\n";
        gunInfo += "Delay          " + gunScript.fireDelay + "\n";
        gunInfo += "Recoil         " + gunScript.recoil + "\n";
        gunInfo += "Shot Deviation " + gunScript.shotDeviation + "\n";
        gunInfo += "Shot Speed     " + gunScript.projectileSpeed + "\n";
        textCarInfo.gameObject.GetComponent<Text>().text = gunInfo;

        // Set the image to the image of the gun.
        gameObject.GetComponent<Image>().sprite = gun.GetComponent<SpriteRenderer>().sprite;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingLaserBallProjectile : ChargingProjectile
{

    [SerializeField]
    private float damageIncreasePerSecond = 5f;

    [SerializeField]
    private float sizeIncreasePerSecond = 0.5f;

    private Vector3 startSize;
    private float startDamage;

    void Start()
    {
        startSize = transform.localScale;
        startDamage = damage;
    }

    public override void OnCharge()
    {
        transform.localScale += startSize * sizeIncreasePerSecond * Time.deltaTime;
        damage += startDamage * damageIncreasePerSecond * Time.deltaTime;
    }
}

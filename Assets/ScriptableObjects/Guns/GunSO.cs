using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Gun", order = 0)]
public class GunSO : ScriptableObject
{
    public string gunName;
    public GunBase prefab;
    public Sprite iconRepresentation;
    public int maxAmmo;
    public float ammoCostPerAction;
    public float fireCooldown;
    public float velocity;
    public Projectile projectilePrefab;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Weapons/List", order = 0)]
public class GunList : ScriptableObject {
    public List<GunSO> guns;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="GunList")]
public class GunList : ScriptableObject {

    [SerializeField]
    private GunBase[] guns;

    public GunBase[] GetGunList()
    {
        return guns;
    }
}

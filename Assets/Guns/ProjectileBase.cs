using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour {

    protected Car owner;


    public void SetOwner(Car owner)
    {
        this.owner = owner;
    }

}

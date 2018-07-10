using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDataTransfer : MonoBehaviour {

    [SerializeField]
    private int index;

    public CarPhysics car;
    public GunController gun;

    public bool hasData = false;


	void Awake () {
        DontDestroyOnLoad(this);
	}

    public int GetIndex()
    {
        return index;
    }
}

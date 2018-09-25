using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDataTransfer : MonoBehaviour {

    [SerializeField]
    public int index;

    public Car car;
    public GunBase gun;

    public bool hasData = false;


	void Awake () {
        DontDestroyOnLoad(this);
	}
}

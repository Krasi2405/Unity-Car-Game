using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDataTransfer : MonoBehaviour {

    public enum PlayerOwner {PlayerOne, PlayerTwo};

    [SerializeField]
    public PlayerOwner owner { get; private set; }

    public CarPhysics car;
    public GunController gun;


    public bool hasData = false;

	void Awake () {
        DontDestroyOnLoad(this);
	}
}

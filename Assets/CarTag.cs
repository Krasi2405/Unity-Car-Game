using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTag : MonoBehaviour {
    [SerializeField]
    private int carTag = 0;

    public int GetCarTag()
    {
        return carTag;
    }
}

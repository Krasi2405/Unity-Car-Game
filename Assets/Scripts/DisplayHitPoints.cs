using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHitPoints : MonoBehaviour {
    
    void Update () {
        gameObject.GetComponent<Text>().text = GetComponentInParent<CarPhysics>().hit_points.ToString();	
	}
}

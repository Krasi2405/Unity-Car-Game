using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public GameObject obj;
	
	// Update is called once per frame
	void Update () {
        transform.position = obj.transform.position + new Vector3(0, 0, -10f);
	}
}

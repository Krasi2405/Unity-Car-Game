using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProject : MonoBehaviour {
    [SerializeField]
    Vector3 vector1, vector2 = Vector3.forward;

	void Update () {
        Debug.Log(Vector3.Project(vector1, vector2).ToString());
	}
}

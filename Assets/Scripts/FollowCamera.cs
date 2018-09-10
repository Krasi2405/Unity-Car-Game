using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour {

    public GameObject followObject;

    [SerializeField]
    [Range(0, 1)]
    private float lerpMovementSpeed = 0.5f;

    // Update is called once per frame
    private void Start()
    {
        transform.position = followObject.transform.position + new Vector3(0, 0, -10f);
    }

    void Update () {
        Vector3 targetDestination = followObject.transform.position + new Vector3(0, 0, -10f);
        transform.position = Vector3.Lerp(transform.position, targetDestination, lerpMovementSpeed);
	}
}

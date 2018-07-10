using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerSystem : MonoBehaviour {

    Animator animator;

	void Start () {
        animator = GetComponent<Animator>();
	}
	

	public void ForwardProcess()
    {
        animator.SetTrigger("ForwardTrigger");
    }


    public void BackwardProcess()
    {
        animator.SetTrigger("BackwardTrigger");
    }
}

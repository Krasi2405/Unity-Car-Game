using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add event
public abstract class EventEffect : MonoBehaviour {
    [SerializeField]
    private int duration;

    private void Start()
    {
        Destroy(gameObject, duration);
        StartCoroutine(startEvent());
    }

    protected abstract IEnumerator startEvent();
}

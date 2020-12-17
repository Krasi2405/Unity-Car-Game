using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour {

    [SerializeField]
    Color mapColor = Color.black;
    private void OnDrawGizmos()
    {
        Gizmos.color = mapColor;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
        Gizmos.DrawRay(transform.position, transform.up);
    }
}

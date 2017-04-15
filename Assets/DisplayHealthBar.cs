using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHealthBar : MonoBehaviour {

   
    public GameObject car;
    
    private float max_hp;
    private float current_hp;
    private float max_height;

    void Start()
    {
        max_height = gameObject.transform.lossyScale.y;
        max_hp = car.GetComponent<CarPhysics>().max_health;
    }

    void Update () {
        current_hp = car.GetComponent<CarPhysics>().curr_health;
        float health_bar_height = max_height / max_hp * current_hp;
        Vector3 scale = new Vector3(50, health_bar_height, 0);
        gameObject.transform.localScale = scale;
	}
}

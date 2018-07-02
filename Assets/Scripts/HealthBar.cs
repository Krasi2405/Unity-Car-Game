using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    
    public GameObject car;
    
    private float maxHitPoints;
    private float currentHP;
    private float maxBarHeight;

    void Start()
    {
        maxBarHeight = gameObject.transform.lossyScale.y;
        maxHitPoints = car.GetComponent<CarPhysics>().maxHealth;
    }

    void Update () {
        currentHP = car.GetComponent<CarPhysics>().currentHealth;
        float healthBarHeight = maxBarHeight / maxHitPoints * currentHP;

        if (healthBarHeight < 0)
            healthBarHeight = 0;

        else if(healthBarHeight <= maxBarHeight / 4)
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;

        else if(healthBarHeight <= maxBarHeight / 2)
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

        else
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        Vector3 scale = new Vector3(gameObject.transform.localScale.x, healthBarHeight, 0);
        gameObject.transform.localScale = scale;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    private CarPhysics car;
    private float maxHitPoints;
    private float currentHP;
    private float maxBarFilled;

    [SerializeField]
    private bool isHorizontal = true;

    void Start()
    {
        if (isHorizontal)
            maxBarFilled = gameObject.transform.localScale.x;
        else
            maxBarFilled = gameObject.transform.localScale.y;

        
    }

    void Update () {
        if (!car) return;
        currentHP = car.currentHealth;
        float healthBarFilledPercent = maxBarFilled / maxHitPoints * currentHP;

        if (healthBarFilledPercent < 0)
            healthBarFilledPercent = 0;

        else if(healthBarFilledPercent <= maxBarFilled / 4)
            gameObject.GetComponent<Image>().color = Color.red;

        else if(healthBarFilledPercent <= maxBarFilled / 2)
            gameObject.GetComponent<Image>().color = Color.yellow;

        else
            gameObject.GetComponent<Image>().color = Color.green;

        Vector3 scale;
        if (isHorizontal)
            scale = new Vector3(healthBarFilledPercent, gameObject.transform.localScale.y, 0);
        else
            scale = new Vector3(gameObject.transform.localScale.x, healthBarFilledPercent, 0);
        gameObject.transform.localScale = scale;
	}

    public void SetTargetCar(CarPhysics car)
    {
        this.car = car;
        maxHitPoints = car.maxHealth;
    }
}

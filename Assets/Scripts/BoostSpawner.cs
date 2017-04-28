using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpawner : MonoBehaviour {

    public GameObject[] boosts;
    public float boostDuration = 20f;
    public int maxBoosts = 6;
    public float timerBetweenBoosts = 15f;
    public float mapWidth;
    public float mapHeight;


    private int currentBoosts = 0;
    private float currentTimer = 0;
	
	void Update () {
        currentTimer += Time.deltaTime;
        GameObject boost = boosts[Random.Range(0, boosts.Length)];
        if (currentTimer >= timerBetweenBoosts && currentBoosts <= maxBoosts)
        {
            GameObject createdBoost = Instantiate(boost, 
                new Vector2(Random.Range(-mapWidth, mapWidth), Random.Range(-mapHeight, mapHeight)),
                Quaternion.identity);
            Destroy(createdBoost, boostDuration);
            currentTimer = 0;
            currentBoosts++;
        }
    }
}

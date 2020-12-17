using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpawner : MonoBehaviour {

    private enum BoostSelectionMethod
    {
        Random,
        RoundRobin
    }

    [SerializeField]
    private List<BoostBase> boostPrefabs;

    [SerializeField]
    private float boostDuration = 20f;

    [SerializeField]
    private int maxSpawnedBoosts = 6;
    
    [SerializeField]
    private float timerBetweenBoosts = 15f;
    
    [SerializeField]
    private float mapWidth;
    
    [SerializeField]
    private float mapHeight;

    [SerializeField]
    private BoostSelectionMethod boostSelectionMethod;


    private int currentBoosts = 0;
    private float currentTimer = 0;
    private int roundRobinIndex;
	
	void Update () {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0 && currentBoosts <= maxSpawnedBoosts)
        {
            SpawnBoost();
            currentTimer = timerBetweenBoosts;
        }
    }

    private void SpawnBoost()
    {
        BoostBase boostPrefab = GetBoostPrefab();

        Vector3 spawnPosition = transform.position;
        spawnPosition.x += Random.Range(-mapWidth, mapWidth);
        spawnPosition.y += Random.Range(-mapHeight, mapHeight);

        BoostBase boost = Instantiate<BoostBase>(boostPrefab, spawnPosition, Quaternion.identity);
        Destroy(boost, boostDuration);
        currentBoosts++;
    }

    private BoostBase GetBoostPrefab()
    {
        switch(boostSelectionMethod)
        {
            case BoostSelectionMethod.Random:
                return boostPrefabs[Random.Range(0, boostPrefabs.Count)];
            case BoostSelectionMethod.RoundRobin:
                BoostBase boostPrefab = boostPrefabs[roundRobinIndex];
                roundRobinIndex++;
                roundRobinIndex %= boostPrefabs.Count;
                return boostPrefab;
            default:
                return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(mapWidth * 2, mapHeight * 2));
    }
}

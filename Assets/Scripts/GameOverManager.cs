using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    public static int num = 0;
    public float gameOverCooldown = 2f;

    public LevelManager levelManager;
    public CarPhysics carOne;
    public CarPhysics carTwo;

    public bool carOneDead { get; private set; }
    public bool carTwoDead { get; private set; }

    void Awake()
    {
        if (num == 1)
        {
            Destroy(gameObject);
        }
        num++;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        carOneDead = false;
        carTwoDead = false;
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update () {
		if(carOne.currentHealth <= 0)
        {
            carOneDead = true;
            Invoke("EndGame", gameOverCooldown);
        }
        else if(carTwo.currentHealth <= 0)
        {
            carTwoDead = true;
            Invoke("EndGame", gameOverCooldown);
        }
	}

    void EndGame()
    {
        levelManager.LoadLevel("Game Over");
    }
}

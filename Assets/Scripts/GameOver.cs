using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public static int num = 0;

    public LevelManager levelManager;
    public CarPhysics car1;
    public CarPhysics car2;

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
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void Update () {
		if(car1.currentHealth <= 0)
        {
            carOneDead = true;
            Invoke("EndGame", 3f);
        }
        else if(car2.currentHealth <= 0)
        {
            carTwoDead = true;
            Invoke("EndGame", 3f);
        }
	}

    void EndGame()
    {
        levelManager.LoadLevel("Game Over");
    }
}

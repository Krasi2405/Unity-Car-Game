using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {
    
    public float gameOverCooldown = 2f;

    public List<CarPhysics> carList;

    public int winningPlayerIndex { get; private set; }

    private LevelManager levelManager;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update () {
		if(carList.Count == 1)
        {
            winningPlayerIndex = carList[0].GetComponent<CarTag>().carTag;
            EndGame();
        }
        else
        {
            foreach(CarPhysics car in carList)
            {
                if(car.currentHealth <= 0)
                {
                    carList.Remove(car);
                    car.ActivateDeathSequence();
                }
            }
        }
	}

    void EndGame()
    {
        levelManager.LoadLevel("Game Over");
    }
}

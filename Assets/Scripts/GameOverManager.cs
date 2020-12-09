using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {
    
    public float gameOverCooldown = 2f;

    public List<Car> carList;

    public int winningPlayerIndex { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update () {
		if(carList.Count == 1)
        {
            winningPlayerIndex = carList[0].GetComponent<CarTag>().carTag;
            Invoke("EndGame", gameOverCooldown);
        }
        else
        {
            foreach(Car car in carList)
            {
                if(car.health.GetCurrentHealth() <= 0)
                {
                    carList.Remove(car);
                    car.ActivateDeathSequence();
                }
            }
        }
	}
}

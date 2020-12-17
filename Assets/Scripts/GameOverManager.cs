using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    public event System.EventHandler OnGameOver;

    List<Car> aliveCars;

    private void Start()
    {
        aliveCars = FindObjectsOfType<Car>().ToList();
        foreach(Car car in aliveCars)
        {
            car.GetComponent<HealthSystem>().OnDeath += Car_OnDeath;
        }
    }

    private void Car_OnDeath(object sender, System.EventArgs e)
    {
        HealthSystem healthSystem = (HealthSystem)(sender);
        Car deadCar = healthSystem.GetComponent<Car>();
        aliveCars.Remove(deadCar);
        if (aliveCars.Count == 1)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        GameOverUI gameOverUI = GameOverUI.Instance;
        gameOverUI.Show();
        gameOverUI.SetWinner(aliveCars[0]);
        OnGameOver?.Invoke(this, System.EventArgs.Empty);
    }
}

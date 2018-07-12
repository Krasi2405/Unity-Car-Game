using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEndGameStats : MonoBehaviour {
    
    
    private GameOverManager gameOverManager;
    private Text text;

	void Start () {
        text = gameObject.GetComponent<Text>();
        gameOverManager = FindObjectOfType<GameOverManager>();

        if (gameOverManager.carOneDead && gameOverManager.carTwoDead)
        {
            text.text = "Draw!\nBoth players have died.";
        }
        else if (gameOverManager.carOneDead)
        {
            text.text = "Player 2 has won!";
        }
        else if (gameOverManager.carTwoDead)
        {
            text.text = "Player 1 has won!";
        }
        Destroy(gameOverManager);
    }
}

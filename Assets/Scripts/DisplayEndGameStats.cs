using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEndGameStats : MonoBehaviour {
    


    private GameOver gameOver;
    private Text text;

	void Start () {
        text = gameObject.GetComponent<Text>();
        gameOver = GameObject.Find("GameOverManager").GetComponent<GameOver>();

        if (gameOver.carOneDead && gameOver.carTwoDead)
        {
            text.text = "Draw!\nBoth players have died.";
        }
        else if (gameOver.carOneDead)
        {
            text.text = "Player 2 has won!";
        }
        else if (gameOver.carTwoDead)
        {
            text.text = "Player 1 has won!";
        }
    }
}

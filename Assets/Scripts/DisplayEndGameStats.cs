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
        text.text = "Player " + gameOverManager.winningPlayerIndex + " wins!";
        Destroy(gameOverManager);
    }
}

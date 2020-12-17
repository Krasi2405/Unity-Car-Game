using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    [SerializeField]
    private Image winnerImage = null;

    [SerializeField]
    private Button replayButton = null;

    [SerializeField]
    private Button selectorButton = null;

    [SerializeField]
    private Button mainMenuButton = null;

    private Car winner;


    private void Awake()
    {
        Hide();
        replayButton.onClick.AddListener(() =>
        {
            LevelManager.LoadScene(LevelManager.Scene.GameLocal);
        });

        selectorButton.onClick.AddListener(() =>
        {
            LevelManager.LoadScene(LevelManager.Scene.PickerLocal);
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            LevelManager.LoadScene(LevelManager.Scene.MainMenu);
        });

        if(Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetWinner(Car winnerCar)
    {
        winner = winnerCar;
        winnerImage.sprite = winner.GetCarSO().iconRepresentation;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

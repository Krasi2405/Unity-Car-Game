using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager {

    public enum Scene
    {
        MainMenu,
        PickerLocal,
        GameLocal,
        PickerMultiplayer,
        GameMultiplayer
    }

	public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}

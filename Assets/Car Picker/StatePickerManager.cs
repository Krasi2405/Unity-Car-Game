using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePickerManager : MonoBehaviour {

    PlayerChooseStatus[] playerStatuses;

    private void Start()
    {
        playerStatuses = FindObjectsOfType<PlayerChooseStatus>();
    }


    void Update () {
        // TODO: Optimize. Really unoptimized to be called every frame.
        bool loadNextLevel = true;
		for(int i = 0; i < playerStatuses.Length; i++)
        {
            if(playerStatuses[i].status != PlayerChooseStatus.Status.Ready)
            {
                loadNextLevel = false;
                break;
            }
        }

        if(loadNextLevel)
            FindObjectOfType<LevelManager>().LoadLevel("Game");
    }
}

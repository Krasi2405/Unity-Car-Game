using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChooseStatus : MonoBehaviour {

    public enum Status { Ready, NotReady, Abandoned}


    public Status status { get; private set; }

    private void Awake()
    {
        status = Status.NotReady;
    }

    public void SetReady()
    {
        status = Status.Ready;
    }

    public void ResetReady()
    {
        status = Status.NotReady;
    }

    public void Disconnect()
    {
        status = Status.Abandoned;
        FindObjectOfType<LevelManager>().LoadLevel("Main Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickerController : MonoBehaviour
{
    [SerializeField]
    List<PickerSystem> playerPickerSystems;

    private void Awake()
    {
        foreach(PickerSystem playerPickerSystem in playerPickerSystems)
        {
            playerPickerSystem.OnReady += Player_OnReady;
        }
    }

    private void Player_OnReady(object sender, System.EventArgs e)
    {
        foreach(PickerSystem playerPickerSystem in playerPickerSystems)
        {
            if(playerPickerSystem.IsReady() == false)
            {
                return;
            }
        }
        LevelManager.LoadScene(LevelManager.Scene.GameLocal);
    }
}

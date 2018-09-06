using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour {

    [SerializeField]
    private RenderTexture playerOneRenderTexture;

    [SerializeField]
    private RenderTexture playerTwoRenderTexture;

	void Start () {
        AdjustRenderTextureToScreen(playerOneRenderTexture);
        AdjustRenderTextureToScreen(playerTwoRenderTexture);
	}


    private void AdjustRenderTextureToScreen(RenderTexture renderTexture)
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        renderTexture.width = screenWidth;
        renderTexture.height = screenHeight / 2;
    }
}

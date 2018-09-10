using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraSetup : MonoBehaviour {

    [SerializeField]
    FollowCamera cameraPrefab;

    [SerializeField]
    private List<RenderTexture> renderTextures;

    [SerializeField]
    private int screenDivisionCountWidth = 1;
    [SerializeField]
    private int screenDivisionCountHeight = 2;

    void Start () {
        List<CarPhysics> cars = FindObjectsOfType<CarPhysics>().ToList();
        try
        {
            cars = cars.OrderBy(x => x.GetComponent<CarTag>().carTag).ToList();
        }
        catch(System.NullReferenceException)
        {
            Debug.LogError("Couldn't order by car tag. ");
        }

        for(int i = 0; i < cars.Count; i++)
        {
            if(i >= renderTextures.Count)
            {
                Debug.LogError("More cars than available render textures!");
                break;
            }
            RenderTexture renderTexture = renderTextures[i];
            AdjustRenderTextureToScreen(
                renderTexture, 
                Screen.width / screenDivisionCountWidth, 
                Screen.height / screenDivisionCountHeight
            );

            FollowCamera camera = Instantiate(cameraPrefab);
            camera.followObject = cars[i].gameObject;
            camera.GetComponent<Camera>().targetTexture = renderTexture;
        }
	}


    private void AdjustRenderTextureToScreen(RenderTexture renderTexture, int width, int height)
    {
        renderTexture.width = width;
        renderTexture.height = height;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{
    Renderer rend;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(mainCamera.orthographic)
        {
            if(mainCamera.orthographicSize > 6)
                rend.material.mainTextureScale = new Vector2(80, 80);
            else if (mainCamera.orthographicSize > 2)
                rend.material.mainTextureScale = new Vector2(200, 200);
            else if (mainCamera.orthographicSize >= 1)
                rend.material.mainTextureScale = new Vector2(400, 400);
            else
                rend.material.mainTextureScale = new Vector2(2000, 2000);

        }
        else
        {
            if (mainCamera.transform.position.y > 10)
                rend.material.mainTextureScale = new Vector2(80, 80);
            else if (mainCamera.transform.position.y > 4)
                rend.material.mainTextureScale = new Vector2(200, 200);
            else if (mainCamera.transform.position.y > 1)
                rend.material.mainTextureScale = new Vector2(400, 400);
            else
                rend.material.mainTextureScale = new Vector2(2000, 2000);
        }



    }
}

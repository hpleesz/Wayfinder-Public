using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float minFov = 10f;
    float maxFov = 120f;
    float sensitivity = 20f;
 
    void Update()
    {
        float fov = Camera.main.fieldOfView;
        fov += (-1)*Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}

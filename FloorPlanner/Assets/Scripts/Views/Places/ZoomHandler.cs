using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SliderZoom()
    {
        var sliderValue = gameObject.GetComponent<Slider>().value;
        if(Camera.main.orthographic)
        {
            Camera.main.orthographicSize = sliderValue/2;
        }
        else
        {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, sliderValue * 2, Camera.main.transform.position.z);
        }
    }

    public void ChangeCamera ()
    {
        if(Camera.main.orthographic)
        {
        Camera.main.orthographic = false;
        }
        else
        {
            Camera.main.orthographic = true;

        }
        SliderZoom();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARFoundation.Samples;

public class ARTrackTest2 : MonoBehaviour
{
    public GameObject cam;
    public GameObject aRSessionOrigin;
    public static bool first;
    public static float targetHeightY;
    public GameObject floorSwitchPanel;

    // Start is called before the first frame update
    void Start()
    {
        first = true;
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.forward = cam.transform.forward;
        gameObject.transform.up = cam.transform.up;
        gameObject.transform.right = cam.transform.right;

        gameObject.transform.localPosition = new Vector3(cam.transform.localPosition.x, 0, cam.transform.localPosition.z) - new Vector3(QRScript.CAMLOCALPOS.x, 0, QRScript.CAMLOCALPOS.z);
        GameObject.Find("HeightTest").transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, cam.transform.localPosition.z) - new Vector3(QRScript.CAMLOCALPOS.x, QRScript.CAMLOCALPOS.y, QRScript.CAMLOCALPOS.z);
        
        if(Math.Abs(GameObject.Find("HeightTest").transform.position.y-targetHeightY) > 2)
        {

            floorSwitchPanel.SetActive(true);
        }
        else
        {
            floorSwitchPanel.SetActive(false);
        }

        gameObject.transform.localEulerAngles = new Vector3(0, cam.transform.eulerAngles.y, 0);


        if (first)
        {
            Vector3 diff = GameObject.Find("TrackTest").transform.eulerAngles - GameObject.Find("TrackTestPos").transform.eulerAngles;

            GameObject.Find("TrackParentPos").transform.eulerAngles = new Vector3(GameObject.Find("TrackParentPos").transform.eulerAngles.x, GameObject.Find("TrackParentPos").transform.eulerAngles.y + diff.y, GameObject.Find("TrackParentPos").transform.eulerAngles.x);
            first = false;
        }


    }
}

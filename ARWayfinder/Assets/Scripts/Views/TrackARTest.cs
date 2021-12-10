using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARFoundation.Samples;

public class TrackARTest : MonoBehaviour
{
    public GameObject cam;
    public GameObject aRSessionOrigin;

    public static Quaternion ROTDIFF = Quaternion.identity;
    public static Vector3 ROTDIFFEULER = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        gameObject.transform.forward = cam.transform.forward - QRScript.CAMFORWARD;
        gameObject.transform.up = cam.transform.up - QRScript.CAMUP;
        gameObject.transform.right = cam.transform.right - QRScript.CAMRIGHT;

        gameObject.transform.localEulerAngles = new Vector3(cam.transform.eulerAngles.x - QRScript.CAMROT.eulerAngles.x, cam.transform.eulerAngles.y - QRScript.CAMROT.eulerAngles.y, cam.transform.eulerAngles.z - QRScript.CAMROT.eulerAngles.z);


        ROTDIFF = gameObject.transform.rotation * Quaternion.Inverse(GameObject.Find("TrackTestPos").transform.rotation);
        ROTDIFFEULER = gameObject.transform.rotation.eulerAngles - GameObject.Find("TrackTestPos").transform.rotation.eulerAngles;

        if(QRScript.target != null && QRScript.ready)
        {
            GameObject.Find("TrackTestPos").GetComponent<ARTrackTest2>().enabled = true;

        }



    }
}

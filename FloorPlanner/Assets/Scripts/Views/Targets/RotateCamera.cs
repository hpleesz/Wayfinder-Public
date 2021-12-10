using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    public Transform target;
 

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Quaternion canTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 5.0f, Vector3.up);
            cameraOffset = canTurnAngle * cameraOffset;



            Vector3 newPos = target.position + cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, 0.2f);

            Debug.Log("a"+transform.localPosition);
        transform.LookAt(target);}
        Debug.Log(transform.localPosition);
    }


}

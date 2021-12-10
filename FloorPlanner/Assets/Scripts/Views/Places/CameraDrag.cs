using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraDrag : MonoBehaviour
{


    
    public float dragSpeed = 1;
    private Vector3 dragOrigin;


    private bool bDragging;
    private Vector3 oldPos;
    private Vector3 panOrigin;
    private float panSpeed = 1;

    Vector3 mousePos;
    void Update()
    {
        
         if (!EventSystem.current.IsPointerOverGameObject() && EditorOptions.Instance.MoveFloor)
         {
             if (Input.GetMouseButtonDown(0))
             {
                mousePos = Input.mousePosition;
                mousePos = new Vector3(mousePos.x, mousePos.y, 10);
                 dragOrigin = Camera.main.ScreenToWorldPoint(mousePos);
                 return;
             }

             if (Input.GetMouseButton(0))
             {
                mousePos = Input.mousePosition;
                mousePos = new Vector3(mousePos.x, mousePos.y, 10);
                Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(mousePos);
                difference = new Vector3(difference.x, 0, difference.z);
                 Camera.main.transform.position = Camera.main.transform.position + difference;
             }
         }

    }


}
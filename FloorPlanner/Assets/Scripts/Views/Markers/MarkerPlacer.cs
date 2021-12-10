using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MarkerPlacer : MonoBehaviour, IMarkerEditView
{
    public GameObject prefabObject;
    Vector3 newPosition;
    public GameObject marker;
    private GameObject placed;

    public InputField angleInput;

    public event IMarkerEditView.AddMarkerToFloorCall onAddMarkerToFloorCall;
    public event IMarkerEditView.EditMarkerCall onEditMarkerCall;


    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject() && EditorOptions.Instance.SelectWall)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit) /*&& hit.transform.gameObject.tag == "Model"*/)
                {
                    Debug.Log("hit2");
                    newPosition = hit.point;
                    if (placed == null)
                    {
                        placed = Instantiate(prefabObject);
                    }
                    placed.transform.position = newPosition;
                    placed.transform.SetParent(marker.transform);

                }
            }
        }

    }

    public void SaveMarker()
    {
        Marker markerNew = new Marker();
        markerNew.xCoordinate = placed.transform.localPosition.x;
        markerNew.yCoordinate = placed.transform.localPosition.y;
        markerNew.zCoordinate = placed.transform.localPosition.z;
        markerNew.xRotation = placed.transform.localEulerAngles.x;
        markerNew.yRotation = placed.transform.localEulerAngles.y;
        markerNew.zRotation = placed.transform.localEulerAngles.z;

        onEditMarkerCall(PlayerPrefs.GetInt("MarkerId"), markerNew);
    }


    public void SetMarkerRotation()
    {
        float angle = float.Parse(angleInput.text, CultureInfo.InvariantCulture);
        placed.transform.localEulerAngles = new Vector3(placed.transform.localEulerAngles.x, angle, placed.transform.localEulerAngles.z);

    }


    public void NewMarkerCreated(int markerId)
    {
        throw new System.NotImplementedException();
    }

    public void MarkerUpdated(int markerId)
    {
        SceneManager.LoadScene("Markers");

    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LineDrawer1 : MonoBehaviour
{
    public GameObject targetStart;
    public GameObject targetEnd;
    public GameObject pointStart;
    public GameObject pointEnd;
    Vector3 newPosition;
    public customGrid grid;
    public Text lengthText;

    void Start()
    {
        newPosition = transform.position;
        //grid = GetComponent<customGrid>();
    }

    void LateUpdate()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && EditorOptions.Instance.DrawWall)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    newPosition = hit.point;
                    newPosition.y = 0.05f; //Z
                    pointStart.SetActive(true);
                    targetStart.transform.position = newPosition;
                    //Debug.Log(newPosition.x + " " + newPosition.y + " " + newPosition.z);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    newPosition = hit.point;
                    newPosition.y = 0.05f; //Z
                    targetEnd.transform.position = newPosition;
                    
                }
                coroutine = WaitAndPrint();
                StartCoroutine(coroutine);
            }
            else if (Input.GetMouseButton(0))
            {

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    newPosition = hit.point;
                    newPosition.y = 0.05f; //Z
                    pointEnd.SetActive(true);

                    targetEnd.transform.position = newPosition;
                    Vector3 distance = targetStart.transform.position - newPosition;
                    //Debug.Log(distance.magnitude);
                    var dist = Mathf.Floor(distance.magnitude * 10) + 1;
                    lengthText.text = dist.ToString();
                }
            }
            else
            {
                
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    newPosition = hit.point;
                    newPosition.y = 0.05f; //Z
                    targetEnd.transform.position = newPosition;
                    pointEnd.SetActive(true);

                }

            }
        }
    }
    private IEnumerator coroutine;

    // every 2 seconds perform the print()
    private IEnumerator WaitAndPrint()
    {
        //while (true)
        //{
            Debug.Log("wall");
            yield return null;
            grid.DrawWall();
            pointStart.SetActive(false);
            pointEnd.SetActive(false);
            //isDown = false;
        //}
    }
}
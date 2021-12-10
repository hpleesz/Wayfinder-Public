using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface ITargetDistancesListView
{

    public delegate void AddFloorSwitcherPointDistancesToTargetCall(TargetDistanceList targetDistanceList, int targetId);
    public delegate void AddTargetDistancesToFloorSwitcherPointCall(TargetDistanceList targetDistanceList, int floorSwitcherPointId);

    public event AddFloorSwitcherPointDistancesToTargetCall onAddFloorSwitcherPointDistancesToTargetCall;
    public event AddTargetDistancesToFloorSwitcherPointCall onAddTargetDistancesToFloorSwitcherPointCall;
    public void FloorSwitcherPointsAdded();


}

public class TargetPlacer : MonoBehaviour, ITargetEditView, IFloorSwitcherPointsListView, ITargetDistancesListView
{
    public GameObject prefabObject;
    Vector3 newPosition;
    public GameObject target;
    [SerializeField]
    public GameObject placed;
    public InputField angleInput;
    public NavMeshAgent agent;
    private List<TargetDistance> targetDistances;
    private FloorSwitcherPointList floorSwitcherPointList;

    public event ITargetEditView.AddTargetToFloorCall onAddTargetToFloorCall;
    public event ITargetEditView.EditTargetCall onEditTargetCall;

    public event IFloorSwitcherPointsListView.GetAllFloorSwitcherPointsByPlaceCall onGetAllFloorSwitcherPointsByPlaceCall;
    public event IFloorSwitcherPointsListView.GetFreeFloorSwitcherPointsByPlaceCall onGetFreeFloorSwitcherPointsByPlaceCall;
    public event IFloorSwitcherPointsListView.SearchFloorSwitcherPointsByPlaceCall onSearchFloorSwitcherPointsByPlaceCall;
    public event IFloorSwitcherPointsListView.SearchFloorSwitcherPointsByFloorCall onSearchFloorSwitcherPointsByFloorCall;
    public event IFloorSwitcherPointsListView.GetAllFloorSwitcherPointsByFloorSwitcherCall onGetAllFloorSwitcherPointsByFloorSwitcherCall;
    public event IFloorSwitcherPointsListView.ClickFloorSwitcherPointCall onClickFloorSwitcherPointCall;

    public event ITargetDistancesListView.AddFloorSwitcherPointDistancesToTargetCall onAddFloorSwitcherPointDistancesToTargetCall;
    public event ITargetDistancesListView.AddTargetDistancesToFloorSwitcherPointCall onAddTargetDistancesToFloorSwitcherPointCall;

    // Start is called before the first frame update
    void Start()
    {
        targetDistances = new List<TargetDistance>();

    }

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

                    newPosition = hit.point;
                    if (placed == null)
                    {
                        placed = Instantiate(prefabObject);
                    }
                    placed.transform.position = newPosition;
                    placed.transform.SetParent(target.transform);

                    GameObject.Find("NavMeshAgent").transform.position = new Vector3(placed.transform.position.x, GameObject.Find("NavMeshAgent").transform.position.y, placed.transform.position.z);

                }
            }
        }

    }

    public void SaveTarget()
    {
        Target targetNew = new Target();
        targetNew.xCoordinate = placed.transform.localPosition.x;
        targetNew.yCoordinate = placed.transform.localPosition.y;
        targetNew.zCoordinate = placed.transform.localPosition.z;
        targetNew.xRotation = placed.transform.localEulerAngles.x;
        targetNew.yRotation = placed.transform.localEulerAngles.y;
        targetNew.zRotation = placed.transform.localEulerAngles.z;

        onEditTargetCall(PlayerPrefs.GetInt("TargetId"), targetNew);
    }

    public void SetTargetRotation()
    {
        float angle = float.Parse(angleInput.text, CultureInfo.InvariantCulture);
        placed.transform.localEulerAngles = new Vector3(placed.transform.localEulerAngles.x, angle, placed.transform.localEulerAngles.z);

    }

    public void NewTargetCreated(int targetId)
    {
        throw new System.NotImplementedException();
    }

    public void TargetUpdated(int targetId)
    {
        //SceneManager.LoadScene("Targets");
        agent.GetComponent<NavMeshAgent>().enabled = true;
        
        onSearchFloorSwitcherPointsByFloorCall(TargetDrawer.target.Floor.id, "");
        //httpRequest.GetFloorSwitcherPointsByFloor(TargetDrawer.target.floor.id);
    }

    public void CreateFloorSwitcherPointsList(IFloorSwitcherPointList floorSwitcherPointList)
    {

        for (int i = 0; i < floorSwitcherPointList.FloorSwitcherPoints.Count; i++)
        {
            GameObject prefabInstance = Instantiate(prefabObject);

            int id = floorSwitcherPointList.FloorSwitcherPoints[i].Id;
            //prefabInstance.transform.position = new Vector3(floorSwitcherPointList.floorswitcherpoints[i].xCoordinate, floorSwitcherPointList.floorswitcherpoints[i].yCoordinate, floorSwitcherPointList.floorswitcherpoints[i].zCoordinate);
            prefabInstance.transform.SetParent(GameObject.Find("Floor Model").transform);
            prefabInstance.transform.localPosition = new Vector3(floorSwitcherPointList.FloorSwitcherPoints[i].XCoordinate, floorSwitcherPointList.FloorSwitcherPoints[i].YCoordinate, floorSwitcherPointList.FloorSwitcherPoints[i].ZCoordinate);
            prefabInstance.name = "FloorSwitcherPoint_" + id;



            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(agent.gameObject.transform.position, prefabInstance.transform.position, NavMesh.AllAreas, path);
            Vector3[] corners = path.corners;
            foreach (var cor in corners)
            {
                Debug.LogError(cor);
            }

            if (corners.Length >= 2)
            {
                float lengthSoFar = 0.0F;

                Vector3 previousCorner = corners[0];
                //float lengthSoFar = 0.0F;
                int j = 1;
                while (j < corners.Length)
                {
                    Vector3 currentCorner = path.corners[j];

                    //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Vector3 between = previousCorner - currentCorner;

                    Debug.LogError(previousCorner + " " + currentCorner);


                    float distance = between.magnitude;
                    cube.transform.localScale = new Vector3(0.01f, 0.01f, distance);
                    cube.transform.position = currentCorner + new Vector3(between.x / 2.0f, between.y / 2.0f, between.z / 2.0f);
                    cube.transform.LookAt(previousCorner);
                    cube.layer = 0;
                    cube.GetComponent<MeshRenderer>().material.color = Color.red;
                    cube.name = "Path";

                    lengthSoFar += Vector3.Distance(previousCorner, currentCorner);


                    previousCorner = currentCorner;
                    j++;
                }

                TargetDistance newTargetDistance = new TargetDistance();
                newTargetDistance.target = new Target();
                newTargetDistance.target.id = PlayerPrefs.GetInt("TargetId");
                newTargetDistance.floorSwitcherPoint = new FloorSwitcherPoint();
                newTargetDistance.floorSwitcherPoint.id = id;
                newTargetDistance.distance = lengthSoFar;
                targetDistances.Add(newTargetDistance);
            }

        }

        TargetDistanceList targetDistanceList = new TargetDistanceList();
        targetDistanceList.targetdistances = targetDistances;
        string json = targetDistanceList.TargetDistanceListToJson();

        //
        //httpRequest.AddFloorSwitcherPointDistancesToTarget(PlayerPrefs.GetInt("TargetId"), json);
        onAddFloorSwitcherPointDistancesToTargetCall(targetDistanceList, PlayerPrefs.GetInt("TargetId"));
        
    }

    public void CreateFreeFloorSwitcherPointsList(IFloorSwitcherPointList floorSwitcherPointsList)
    {
        throw new System.NotImplementedException();
    }

    public void ClickFloorSwitcherPoint(IFloorSwitcherPoint selectedFloorSwitcherPoint)
    {
        throw new System.NotImplementedException();
    }

    public void FloorSwitcherPointsAdded()
    {
        SceneManager.LoadScene("Targets");
    }
}

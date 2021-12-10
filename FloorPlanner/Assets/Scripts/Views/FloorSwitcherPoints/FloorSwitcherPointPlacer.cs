using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface IFloorSwitcherPointDistancesListView
{

    public delegate void AddFloorSwitcherPointDistancesToFloorSwitcherPointCall(FloorSwitcherPointDistanceList floorSwitcherPointDistanceList, int floorSwitcherPointId);

    public event AddFloorSwitcherPointDistancesToFloorSwitcherPointCall onAddFloorSwitcherPointDistancesToFloorSwitcherPointCall;
    void FloorSwitcherPointsAdded();


}

public class FloorSwitcherPointPlacer : MonoBehaviour, IFloorSwitcherPointEditView, IFloorSwitcherPointsListView, IFloorSwitcherPointDistancesListView, ITargetsListView, ITargetDistancesListView
{
    public GameObject prefabObject;
    Vector3 newPosition;
    public GameObject floorSwitcherPoint;
    private GameObject placed;
    private FloorSwitcherPointList floorSwitcherPointList;
    private TargetList targetList;
    public NavMeshAgent agent;
    private List<TargetDistance> targetDistances;
    private List<FloorSwitcherPointDistance> floorSwitcherPointDistances;

    public event IFloorSwitcherPointEditView.AddFloorSwitcherPointToFloorCall onAddFloorSwitcherPointToFloorCall;
    public event IFloorSwitcherPointEditView.EditFloorSwitcherPointCall onEditFloorSwitcherPointCall;

    public event IFloorSwitcherPointsListView.GetAllFloorSwitcherPointsByPlaceCall onGetAllFloorSwitcherPointsByPlaceCall;
    public event IFloorSwitcherPointsListView.GetFreeFloorSwitcherPointsByPlaceCall onGetFreeFloorSwitcherPointsByPlaceCall;
    public event IFloorSwitcherPointsListView.SearchFloorSwitcherPointsByPlaceCall onSearchFloorSwitcherPointsByPlaceCall;
    public event IFloorSwitcherPointsListView.SearchFloorSwitcherPointsByFloorCall onSearchFloorSwitcherPointsByFloorCall;
    public event IFloorSwitcherPointsListView.GetAllFloorSwitcherPointsByFloorSwitcherCall onGetAllFloorSwitcherPointsByFloorSwitcherCall;
    public event IFloorSwitcherPointsListView.ClickFloorSwitcherPointCall onClickFloorSwitcherPointCall;

    public event IFloorSwitcherPointDistancesListView.AddFloorSwitcherPointDistancesToFloorSwitcherPointCall onAddFloorSwitcherPointDistancesToFloorSwitcherPointCall;
    
    public event ITargetsListView.GetAllTargetsByPlaceCall onGetAllTargetsByPlaceCall;
    public event ITargetsListView.GetAllTargetsByFloorCall onGetAllTargetsByFloorCall;
    public event ITargetsListView.SearchTargetsByPlaceCall onSearchTargetsByPlaceCall;
    public event ITargetsListView.SearchTargetsByFloorAndCategoryCall onSearchTargetsByFloorAndCategoryCall;
    public event ITargetsListView.ClickTargetCall onClickTargetCall;

    public event ITargetDistancesListView.AddFloorSwitcherPointDistancesToTargetCall onAddFloorSwitcherPointDistancesToTargetCall;
    public event ITargetDistancesListView.AddTargetDistancesToFloorSwitcherPointCall onAddTargetDistancesToFloorSwitcherPointCall;

    // Start is called before the first frame update
    void Start()
    {
        targetDistances = new List<TargetDistance>();
        floorSwitcherPointDistances = new List<FloorSwitcherPointDistance>();

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
                    placed.transform.SetParent(floorSwitcherPoint.transform);

                    //GameObject.Find("NavMeshAgent").transform.SetParent(floorSwitcherPoint.transform);
                    GameObject.Find("NavMeshAgent").transform.position = new Vector3(placed.transform.position.x, GameObject.Find("NavMeshAgent").transform.position.y, placed.transform.position.z);
                    //GameObject.Find("NavMeshAgent").transform.position = placed.transform.position;
                }
            }
        }

    }

    public void SaveFloorSwitcherPoint()
    {
        FloorSwitcherPoint floorSwitcherPointNew = new FloorSwitcherPoint();
        floorSwitcherPointNew.xCoordinate = placed.transform.localPosition.x;
        floorSwitcherPointNew.yCoordinate = placed.transform.localPosition.y;
        floorSwitcherPointNew.zCoordinate = placed.transform.localPosition.z;

        onEditFloorSwitcherPointCall(PlayerPrefs.GetInt("FloorSwitcherPointId"), floorSwitcherPointNew);
    }


    public void Back()
    {
        SceneManager.LoadScene("Floor Switcher Points");
    }

    public void NewFloorSwitcherPointCreated(int floorSwitcherPointId)
    {
        throw new System.NotImplementedException();
    }

    public void FloorSwitcherPointUpdated(int floorSwitcherPointId)
    {
        agent.GetComponent<NavMeshAgent>().enabled = true;

        //SceneManager.LoadScene("Floor Switcher Points");

        onSearchFloorSwitcherPointsByFloorCall(FloorSwitcherPointDrawer.floorSwitcherPoint.Floor.id, "");
        onGetAllTargetsByFloorCall(FloorSwitcherPointDrawer.floorSwitcherPoint.Floor.id);

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

                FloorSwitcherPointDistance newFloorSwitcherPointDistance = new FloorSwitcherPointDistance();
                newFloorSwitcherPointDistance.floorSwitcherPoint2 = new FloorSwitcherPoint();
                newFloorSwitcherPointDistance.floorSwitcherPoint2.id = id;
                newFloorSwitcherPointDistance.floorSwitcherPoint1 = new FloorSwitcherPoint();
                newFloorSwitcherPointDistance.floorSwitcherPoint1.id = PlayerPrefs.GetInt("FloorSwitcherPointId");
                newFloorSwitcherPointDistance.distance = lengthSoFar;
                floorSwitcherPointDistances.Add(newFloorSwitcherPointDistance);
            }

        }

        FloorSwitcherPointDistanceList floorSwitcherPointDistanceList = new FloorSwitcherPointDistanceList();
        floorSwitcherPointDistanceList.floorswitcherpointdistances = floorSwitcherPointDistances;
        //string json = floorSwitcherPointDistanceList.FloorSwitcherPointDistanceListToJson();
        //httpRequest.AddFloorSwitcherPointDistancesToFloorSwitcherPoint(PlayerPrefs.GetInt("FloorSwitcherPointId"), json);

        onAddFloorSwitcherPointDistancesToFloorSwitcherPointCall(floorSwitcherPointDistanceList, PlayerPrefs.GetInt("FloorSwitcherPointId"));
    }

    public void CreateFreeFloorSwitcherPointsList(IFloorSwitcherPointList floorSwitcherPointsList)
    {
        throw new System.NotImplementedException();
    }

    public void ClickFloorSwitcherPoint(IFloorSwitcherPoint selectedFloorSwitcherPoint)
    {
        throw new System.NotImplementedException();
    }


    public void CreateTargetsList(ITargetList targetList)
    {

        for (int i = 0; i < targetList.Targets.Count; i++)
        {
            GameObject prefabInstance = Instantiate(prefabObject);

            int id = targetList.Targets[i].id;
            //prefabInstance.transform.position = new Vector3(floorSwitcherPointList.floorswitcherpoints[i].xCoordinate, floorSwitcherPointList.floorswitcherpoints[i].yCoordinate, floorSwitcherPointList.floorswitcherpoints[i].zCoordinate);
            prefabInstance.transform.SetParent(GameObject.Find("Floor Model").transform);
            prefabInstance.transform.localPosition = new Vector3(targetList.Targets[i].xCoordinate, targetList.Targets[i].yCoordinate, targetList.Targets[i].zCoordinate);
            prefabInstance.name = "Target_" + id;




            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(agent.gameObject.transform.position, new Vector3(targetList.Targets[i].xCoordinate, targetList.Targets[i].yCoordinate, targetList.Targets[i].zCoordinate), NavMesh.AllAreas, path);
            Vector3[] corners = path.corners;


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
                    float distance = between.magnitude;
                    cube.transform.localScale = new Vector3(0.01f, 0.01f, distance);
                    cube.transform.position = currentCorner + new Vector3(between.x / 2.0f, between.y / 2.0f, between.z / 2.0f);
                    cube.transform.LookAt(previousCorner);
                    cube.layer = 0;
                    cube.GetComponent<MeshRenderer>().material.color = Color.green;
                    cube.name = "Path";

                    lengthSoFar += Vector3.Distance(previousCorner, currentCorner);

                    previousCorner = currentCorner;
                    j++;
                }


                TargetDistance newTargetDistance = new TargetDistance();
                newTargetDistance.target = new Target();
                newTargetDistance.target.id = id;
                newTargetDistance.floorSwitcherPoint = new FloorSwitcherPoint();
                newTargetDistance.floorSwitcherPoint.id = PlayerPrefs.GetInt("FloorSwitcherPointId");
                newTargetDistance.distance = lengthSoFar;
                targetDistances.Add(newTargetDistance);

            }
        }

        TargetDistanceList targetDistanceList = new TargetDistanceList();
        targetDistanceList.targetdistances = targetDistances;
        //string json = targetDistanceList.TargetDistanceListToJson();
        //httpRequest.AddTargetDistancesToFloorSwitcherPoint(PlayerPrefs.GetInt("FloorSwitcherPointId"), json);
        onAddTargetDistancesToFloorSwitcherPointCall(targetDistanceList, PlayerPrefs.GetInt("FloorSwitcherPointId"));
    }

    public void ClickTarget(ITarget selectedTarget)
    {
        throw new System.NotImplementedException();
    }

    /*
    public void IFloorSwitcherPointDistancesListView.FloorSwitcherPointsAdded()
    {
        throw new System.NotImplementedException();
    }*/

    //target distances addded
    public void TargetDistancesAdded()
    {
        //SceneManager.LoadScene("Floor Switcher Points");
    }
    public void FloorSwitcherPointsAdded()
    {
        //SceneManager.LoadScene("Floor Switcher Points");
    }
}

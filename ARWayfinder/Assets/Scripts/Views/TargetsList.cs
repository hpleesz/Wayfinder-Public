using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARFoundation.Samples;

public interface ICategoriesListView
{
    public delegate void GetCategoriesByPlaceCall(int placeId);

    public event GetCategoriesByPlaceCall onGetCategoriesByPlaceCall;
    void CreateCategoriesList(ICategoryList categoryList);
}

public interface ITargetPathStepsListView
{

    public delegate void GetAllTargetDistancesByPlaceCall(int placeId, PathStepList pathStepList);
    public delegate void SearchTargetDistancesByPlaceAndCategoryCall(int placeId, string term, string category, PathStepList pathStepList);

    //erre iratkozik fel majd valaki
    public event GetAllTargetDistancesByPlaceCall onGetAllTargetDistancesByPlaceCall;
    public event SearchTargetDistancesByPlaceAndCategoryCall onSearchTargetDistancesByPlaceAndCategoryCall;

    void CreateTargetDistancesList(ITargetPathStepsList targetPathStepsList);
}


public class TargetsList : MonoBehaviour, IFloorSwitcherPointsListView, ITargetPathStepsInfoView, ICategoriesListView, ITargetPathStepsListView
{
    public event IFloorSwitcherPointsListView.GetAllFloorSwitcherPointsByFloorCall onGetAllFloorSwitcherPointsByFloorCall;
    public event ITargetPathStepsInfoView.GetTargetPathStepsCall onGetTargetPathStepsCall;
    public event ICategoriesListView.GetCategoriesByPlaceCall onGetCategoriesByPlaceCall;
    public event ITargetPathStepsListView.GetAllTargetDistancesByPlaceCall onGetAllTargetDistancesByPlaceCall;
    public event ITargetPathStepsListView.SearchTargetDistancesByPlaceAndCategoryCall onSearchTargetDistancesByPlaceAndCategoryCall;

    private ITargetPathStepsList targetPathStepsList;
    public Image listElement;
    public GameObject listParent;
    public GameObject detailsPanel;
    public GameObject listPanel;
    public InputField searchTerm;
    public GameObject searchPanel;
    public PlayerController playerController;
    public Dropdown categoryDropdown;
    public GameObject floorSwitcherPrefab;



    private PathStepList pathStepList;
    private Dictionary<int, FloorSwitcherPoint> floorSwitchers;

    // Start is called before the first frame update
    void Start()
    {

        onGetCategoriesByPlaceCall(PlayerPrefs.GetInt("PlaceId"));


        onGetAllFloorSwitcherPointsByFloorCall(PlayerPrefs.GetInt("FloorId"));
    }



    private void clickPlace(int id)
    {
        foreach (Transform child in GameObject.Find("UserDummy").transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in GameObject.Find("TrackedImageDummy").transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        PlayerPrefs.SetInt("TargetId", id);
        string json = pathStepList.PathStepListToJson();
        onGetTargetPathStepsCall(id, pathStepList);

    }

    public void clearTarget()
    {
        foreach (Transform child in GameObject.Find("UserDummy").transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in GameObject.Find("TrackedImageDummy").transform)
        {
            GameObject.Destroy(child.gameObject);

        }
        PlayerPrefs.SetInt("TargetId", 0);
    }


    public void searchTargets()
    {
        string text = searchTerm.transform.Find("Text").GetComponent<Text>().text.ToString();
        int categoryNumber = categoryDropdown.value;
        onSearchTargetDistancesByPlaceAndCategoryCall(PlayerPrefs.GetInt("PlaceId"), text, categories[categoryNumber].id.ToString(), pathStepList);
    }

    public void backButton()
    {
        listPanel.SetActive(true);
        detailsPanel.SetActive(false);
    }

    public void openSearch()
    {
        searchPanel.SetActive(true);
        detailsPanel.SetActive(false);
    }

    public void ViewPlace()
    {
        SceneManager.LoadScene("SplitScreen");
    }

    private List<Category> categories;





    public void CreateFloorSwitcherPointsList(IFloorSwitcherPointList floorSwitcherPointList)
    {


        floorSwitchers = new Dictionary<int, FloorSwitcherPoint>();


        pathStepList = new PathStepList();
        List<PathStep> pathSteps = new List<PathStep>();

        for (int i = 0; i < floorSwitcherPointList.FloorSwitcherPoints.Count; i++)
        {
            floorSwitchers.Add(floorSwitcherPointList.FloorSwitcherPoints[i].id, floorSwitcherPointList.FloorSwitcherPoints[i]);

            float pathLength = playerController.CalculatePathLength(new Vector3(floorSwitcherPointList.FloorSwitcherPoints[i].xCoordinate, floorSwitcherPointList.FloorSwitcherPoints[i].yCoordinate, floorSwitcherPointList.FloorSwitcherPoints[i].zCoordinate));
            PathStep pathStep = new PathStep();
            pathStep.stepId = floorSwitcherPointList.FloorSwitcherPoints[i].id;
            pathStep.distance = double.Parse(pathLength.ToString());
            pathSteps.Add(pathStep);
        }

        pathStepList.pathSteps = pathSteps;

        string json = pathStepList.PathStepListToJson();
        onGetAllTargetDistancesByPlaceCall(PlayerPrefs.GetInt("PlaceId"), pathStepList);
    }

    public void TargetPathResult(ITargetPathSteps targetPathSteps)
    {
        //IF target is on same floor
        if (targetPathSteps.Target.floor.id == PlayerPrefs.GetInt("FloorId"))
        {
            Target target = targetPathSteps.Target;
            playerController.setPath(new Vector3(target.xCoordinate, target.yCoordinate, target.zCoordinate));
        }
        //IF target not on same floor
        else
        {
            //start point of floor switcher
            FloorSwitcherPoint point = targetPathSteps.FloorSwitcherPointSteps[1].floorSwitcherPoint;
            int floorSwitcherId = targetPathSteps.FloorSwitcherPointSteps[1].floorSwitcherPoint.floorSwitcher.id;


            playerController.setPath(new Vector3(point.xCoordinate, point.yCoordinate, point.zCoordinate));

            //point.floor.number == current floor number
            int goToFloor = point.floor.number;
            int i = 0;
            foreach (FloorSwitcherPointStep floorSwitcherPointStep in targetPathSteps.FloorSwitcherPointSteps)
            {
                if (i > 0)
                {

                    if (floorSwitcherPointStep.floorSwitcherPoint.floorSwitcher.id == floorSwitcherId)
                    {
                        goToFloor = floorSwitcherPointStep.floorSwitcherPoint.floor.number;
                    }
                    else
                    {
                        break;
                    }
                }
                i++;
            }


            //MAP FLOOR SWITCHER INFO
            GameObject dummyUser = GameObject.Find("UserDummy");

            GameObject go = Instantiate(floorSwitcherPrefab);
            go.transform.position = new Vector3(point.xCoordinate, 1f, point.zCoordinate);
            go.transform.SetParent(dummyUser.transform);
            go.layer = 6;

            GameObject[] mapPaths = GameObject.FindGameObjectsWithTag("PathMap");
            //go.transform.LookAt(mapPaths[mapPaths.Length - 1].transform.position);
            Vector3 targetPostitionMap = new Vector3(mapPaths[mapPaths.Length - 1].transform.position.x,
                            go.transform.position.y,
                             mapPaths[mapPaths.Length - 1].transform.position.z);
            go.transform.LookAt(targetPostitionMap);
            go.tag = "Linecast2";
            go.name = "FloorSwitcherPointM" + point.id;

            //VIRTUAL FOOR SWITCHER INFO
            GameObject trackedImageDummy = GameObject.Find("TrackedImageDummy");
            GameObject go2 = Instantiate(floorSwitcherPrefab);

            go2.transform.SetParent(trackedImageDummy.transform);

            go2.transform.localPosition = go.transform.localPosition;
            go2.transform.localScale = new Vector3(go.transform.localScale.x / trackedImageDummy.transform.localScale.x, go.transform.localScale.y / trackedImageDummy.transform.localScale.y, go.transform.localScale.z / trackedImageDummy.transform.localScale.z);
            go2.layer = 0;

            go2.transform.Find("Canvas").Find("Text").GetComponent<Text>().text = "Go to floor: " + goToFloor;

            GameObject[] virtualPaths = GameObject.FindGameObjectsWithTag("PathVirtual");

            Vector3 targetPostitionVirtual = new Vector3(virtualPaths[virtualPaths.Length - 1].transform.position.x,
                                        go2.transform.position.y,
                                         virtualPaths[virtualPaths.Length - 1].transform.position.z);
            go2.transform.LookAt(targetPostitionVirtual);
            go2.name = "FloorSwitcherPointV" + point.id;


        }

        searchPanel.SetActive(false);
    }

    public void CreateCategoriesList(ICategoryList categoryList)
    {

        List<string> m_DropOptions = new List<string>();

        categories = new List<Category>();
        Category categoryNone = new Category();
        categoryNone.id = 0;
        categoryNone.name = "All categories";

        m_DropOptions.Add(categoryNone.name);
        categories.Add(categoryNone);

        foreach (var category in categoryList.Categories)
        {
            categories.Add(category);
            m_DropOptions.Add(category.name);
        }

        categoryDropdown.ClearOptions();
        //Add the options created in the List above
        categoryDropdown.AddOptions(m_DropOptions);
    }

    public void CreateTargetDistancesList(ITargetPathStepsList targetPathStepsList)
    {
        Debug.Log("Target:" + targetPathStepsList.TargetPathSteps.Count);

        foreach (Transform child in listParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        for (int i = 0; i < targetPathStepsList.TargetPathSteps.Count; i++)
        {
            Image prefabInstance = Instantiate(listElement) as Image;

            int id = targetPathStepsList.TargetPathSteps[i].target.id;
            prefabInstance.GetComponent<Button>().onClick.AddListener(delegate { clickPlace(id); });
            Text textComponent = prefabInstance.transform.Find("Target Name Text").GetComponent<Text>();
            Text textComponentFloor = prefabInstance.transform.Find("Target Floor Text").GetComponent<Text>();
            Text textComponentCategory = prefabInstance.transform.Find("Target Category Text").GetComponent<Text>();

            textComponent.text = targetPathStepsList.TargetPathSteps[i].target.name;
            textComponentFloor.text = targetPathStepsList.TargetPathSteps[i].target.floor.number + ". " + targetPathStepsList.TargetPathSteps[i].target.floor.name;
            textComponentCategory.text = targetPathStepsList.TargetPathSteps[i].target.category.name;

            prefabInstance.rectTransform.SetParent(listParent.transform);
            prefabInstance.rectTransform.localScale = new Vector3(1f, 1f, 1f);


            int steplength = targetPathStepsList.TargetPathSteps[i].floorSwitcherPointSteps.Count;

            //IF path found
            if (steplength > 0)
            {
                double pathLength = targetPathStepsList.TargetPathSteps[i].floorSwitcherPointSteps[steplength - 1].distance;
                Text textComponentDistance = prefabInstance.transform.Find("Target Distance Text").GetComponent<Text>();
                textComponentDistance.text = pathLength.ToString("0.0") + "m";

                //IF target and source are on the same floor
                if (PlayerPrefs.GetInt("FloorId") == targetPathStepsList.TargetPathSteps[i].target.floor.id)
                {
                    //Path on same floor
                    float pathLengthLocal = playerController.CalculatePathLength(new Vector3(targetPathStepsList.TargetPathSteps[i].target.xCoordinate, targetPathStepsList.TargetPathSteps[i].target.yCoordinate, targetPathStepsList.TargetPathSteps[i].target.zCoordinate));

                    //IF path on same floor shorter than path including floor switcher
                    if (pathLengthLocal < pathLength)
                    {
                        textComponentDistance.text = pathLengthLocal.ToString("0.0") + "m";
                    }
                }
            }
            //No path found
            else
            {
                //IF target and source are on the same floor
                if (PlayerPrefs.GetInt("FloorId") == targetPathStepsList.TargetPathSteps[i].target.floor.id)
                {

                    float pathLength = playerController.CalculatePathLength(new Vector3(targetPathStepsList.TargetPathSteps[i].target.xCoordinate, targetPathStepsList.TargetPathSteps[i].target.yCoordinate, targetPathStepsList.TargetPathSteps[i].target.zCoordinate));
                    Text textComponentDistance = prefabInstance.transform.Find("Target Distance Text").GetComponent<Text>();
                    textComponentDistance.text = pathLength.ToString("0.0") + "m";
                }
                //Not on the same floor -> no path
                else
                {
                    Text textComponentDistance = prefabInstance.transform.Find("Target Distance Text").GetComponent<Text>();
                    textComponentDistance.text = "---m";
                }

            }


        }
    }
   }

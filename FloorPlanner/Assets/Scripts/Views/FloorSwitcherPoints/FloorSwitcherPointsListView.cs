using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public interface IFloorSwitcherPointInfoView
{
    public delegate void GetFloorSwitcherPointCall(int floorSwitcherPointId);

    public event GetFloorSwitcherPointCall onGetFloorSwitcherPointCall;
    void ShowFloorSwitcherPoint(IFloorSwitcherPoint floorSwitcherPoint);

}

public interface IFloorSwitcherPointEditView
{
    public delegate void AddFloorSwitcherPointToFloorCall(int placeId, IFloorSwitcherPoint floorSwitcherPoint);
    public delegate void EditFloorSwitcherPointCall(int floorSwitcherPointId, IFloorSwitcherPoint floorSwitcherPoint);

    public event AddFloorSwitcherPointToFloorCall onAddFloorSwitcherPointToFloorCall;
    public event EditFloorSwitcherPointCall onEditFloorSwitcherPointCall;

    void NewFloorSwitcherPointCreated(int floorSwitcherPointId);
    void FloorSwitcherPointUpdated(int floorSwitcherPointId);
}
public interface IFloorSwitcherPointsListView
{

    public delegate void GetAllFloorSwitcherPointsByPlaceCall(int placeId);
    public delegate void GetFreeFloorSwitcherPointsByPlaceCall(int placeId);

    public delegate void SearchFloorSwitcherPointsByPlaceCall(int placeId, string term);
    public delegate void SearchFloorSwitcherPointsByFloorCall(int floorId, string term);
    public delegate void GetAllFloorSwitcherPointsByFloorSwitcherCall(int floorSwitcherId);

    public delegate void ClickFloorSwitcherPointCall(int id);


    public event GetAllFloorSwitcherPointsByPlaceCall onGetAllFloorSwitcherPointsByPlaceCall;
    public event GetFreeFloorSwitcherPointsByPlaceCall onGetFreeFloorSwitcherPointsByPlaceCall;

    public event SearchFloorSwitcherPointsByPlaceCall onSearchFloorSwitcherPointsByPlaceCall;
    public event SearchFloorSwitcherPointsByFloorCall onSearchFloorSwitcherPointsByFloorCall;
    public event GetAllFloorSwitcherPointsByFloorSwitcherCall onGetAllFloorSwitcherPointsByFloorSwitcherCall;

    public event ClickFloorSwitcherPointCall onClickFloorSwitcherPointCall;

    void CreateFloorSwitcherPointsList(IFloorSwitcherPointList floorSwitcherPointsList);
    void CreateFreeFloorSwitcherPointsList(IFloorSwitcherPointList floorSwitcherPointsList);

    void ClickFloorSwitcherPoint(IFloorSwitcherPoint selectedFloorSwitcherPoint);

}

public class FloorSwitcherPointsListView : MonoBehaviour, IFloorSwitcherPointEditView, IFloorSwitcherPointsListView, IFloorsListView
{
    // Start is called before the first frame update
    //private FloorSwitcherPointList floorSwitcherPointsList;
    //public HttpRequest httpRequest;
    public Image listElement;
    public GameObject listParent;
    public GameObject detailsPanel;
    public GameObject newPanel;
    public InputField searchTerm;

    public Dropdown floorDropdown;
    public Dropdown newFloorDropdown;
    private List<int> floors = new List<int>();

    public event IFloorSwitcherPointEditView.AddFloorSwitcherPointToFloorCall onAddFloorSwitcherPointToFloorCall;
    public event IFloorSwitcherPointEditView.EditFloorSwitcherPointCall onEditFloorSwitcherPointCall;

    public event IFloorSwitcherPointsListView.GetAllFloorSwitcherPointsByPlaceCall onGetAllFloorSwitcherPointsByPlaceCall;
    public event IFloorSwitcherPointsListView.SearchFloorSwitcherPointsByPlaceCall onSearchFloorSwitcherPointsByPlaceCall;
    public event IFloorSwitcherPointsListView.SearchFloorSwitcherPointsByFloorCall onSearchFloorSwitcherPointsByFloorCall;

    public event IFloorSwitcherPointsListView.ClickFloorSwitcherPointCall onClickFloorSwitcherPointCall;

    public event IFloorsListView.GetAllFloorsByPlaceCall onGetAllFloorsByPlaceCall;
    public event IFloorsListView.SearchFloorsByPlaceCall onSearchFloorsByPlaceCall;
    public event IFloorsListView.ClickFloorCall onClickFloorCall;
    public event IFloorSwitcherPointsListView.GetAllFloorSwitcherPointsByFloorSwitcherCall onGetAllFloorSwitcherPointsByFloorSwitcherCall;
    public event IFloorSwitcherPointsListView.GetFreeFloorSwitcherPointsByPlaceCall onGetFreeFloorSwitcherPointsByPlaceCall;

    // Start is called before the first frame update
    void Start()
    {

        onGetAllFloorSwitcherPointsByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
        onGetAllFloorsByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void searchFloorSwitcherPoints()
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        string text = searchTerm.transform.Find("Text").GetComponent<Text>().text.ToString();

        if (floorDropdown.value > 0)
        {
            onSearchFloorSwitcherPointsByFloorCall(floors[floorDropdown.value], text);

        }
        else
        {
            onSearchFloorSwitcherPointsByPlaceCall(PlayerPrefs.GetInt("PlaceId"), text);
        }
    }

    public void NewFloorSwitcherPointCreated(int floorSwitcherPointId)
    {

        PlayerPrefs.SetInt("FloorSwitcherPointId", floorSwitcherPointId);
        onGetAllFloorSwitcherPointsByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
        //httpRequest.GetFloorSwitcherPointsByPlace(PlayerPrefs.GetInt("PlaceId"));
    }

    public void FloorSwitcherPointUpdated(int floorSwitcherPointId)
    {
        throw new System.NotImplementedException();
    }

    public void CreateFloorSwitcherPointsList(IFloorSwitcherPointList floorSwitcherPointsList)
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        foreach (Transform child in listParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        for (int i = 0; i < floorSwitcherPointsList.FloorSwitcherPoints.Count; i++)
        {
            Image prefabInstance = Instantiate(listElement) as Image;
            Debug.Log(floorSwitcherPointsList.FloorSwitcherPoints[i].Id);
            int id = floorSwitcherPointsList.FloorSwitcherPoints[i].Id;
            prefabInstance.GetComponent<Button>().onClick.AddListener(delegate { onClickFloorSwitcherPointCall(id); });

            prefabInstance.transform.Find("Floor Switcher Point Name Text").GetComponent<Text>().text = floorSwitcherPointsList.FloorSwitcherPoints[i].Name;
            prefabInstance.transform.Find("Floor Switcher Point Floor Value Text").GetComponent<Text>().text = floorSwitcherPointsList.FloorSwitcherPoints[i].Floor.Name;
            prefabInstance.transform.Find("Floor Switcher Value Text").GetComponent<Text>().text = floorSwitcherPointsList.FloorSwitcherPoints[i].FloorSwitcher.name;

            prefabInstance.rectTransform.SetParent(listParent.transform);
            prefabInstance.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        }
        if (floorSwitcherPointsList.FloorSwitcherPoints.Count > 0)
        {
            onClickFloorSwitcherPointCall(floorSwitcherPointsList.FloorSwitcherPoints[0].Id);
        }

        //Debug.Log(placeList.places.Count);    
    }

    public void ClickFloorSwitcherPoint(IFloorSwitcherPoint selectedFloorSwitcherPoint)
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        PlayerPrefs.SetInt("FloorSwitcherPointId", selectedFloorSwitcherPoint.Id);


        //TODO

        detailsPanel.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>().text = selectedFloorSwitcherPoint.Name;
        //detailsPanel.transform.Find("Title Panel").Find("Number Text").GetComponent<Text>().text = selectedFloor.number.ToString();
        detailsPanel.transform.Find("Details Panel").Find("Floor Switcher Value Text").GetComponent<Text>().text = selectedFloorSwitcherPoint.FloorSwitcher.name;
        detailsPanel.transform.Find("Details Panel").Find("Floor Value Text").GetComponent<Text>().text = selectedFloorSwitcherPoint.Floor.Name;
        detailsPanel.transform.Find("Details Panel").Find("X Value Text").GetComponent<Text>().text = selectedFloorSwitcherPoint.XCoordinate.ToString();
        detailsPanel.transform.Find("Details Panel").Find("Y Value Text").GetComponent<Text>().text = selectedFloorSwitcherPoint.YCoordinate.ToString();
        detailsPanel.transform.Find("Details Panel").Find("Z Value Text").GetComponent<Text>().text = selectedFloorSwitcherPoint.ZCoordinate.ToString();
    }

    public void CreateFloorsList(IFloorList floorsList)
    {

        floorDropdown.options.Add(new Dropdown.OptionData() { text = "All floors" });
        floors.Add(0);




        for (int i = 0; i < floorsList.Floors.Count; i++)
        {

            floorDropdown.options.Add(new Dropdown.OptionData() { text = floorsList.Floors[i].Number + ". " + floorsList.Floors[i].Name });
            floors.Add(floorsList.Floors[i].Id);

        }
        if (floorsList.Floors.Count > 0)
        {
            floorDropdown.value = 1;

            floorDropdown.value = 0;
        }
        //Debug.Log(placeList.places.Count);
        //httpRequest.onResult -= CreateFloorDropdown;  






        

        for (int i = 0; i < floorsList.Floors.Count; i++)
        {

            newFloorDropdown.options.Add(new Dropdown.OptionData() { text = floorsList.Floors[i].Number + ". " + floorsList.Floors[i].Name });
            //floors.Add(floorsList.Floors[i].Id);

        }
        if (floorsList.Floors.Count > 0)
        {
            newFloorDropdown.value = 0;
        }
        //Debug.Log(placeList.places.Count);
        //httpRequest.onResult -= CreateFloorDropdown;
    }

    public void ClickFloor(IFloor selectedFloor)
    {
        throw new System.NotImplementedException();
    }

    public void CreateNewFloorSwitcherPoint()
    {
        string name = gameObject.transform.Find("New Panel").Find("Title Panel Edit").Find("Title Input").Find("Text").GetComponent<Text>().text.ToString();

        FloorSwitcherPoint floorSwitcherPoint = new FloorSwitcherPoint();
        floorSwitcherPoint.name = name;


        //httpRequest.AddFloorSwitcherPoint(floors[floorDropdown.value], json);
        onAddFloorSwitcherPointToFloorCall(floors[newFloorDropdown.value+1], floorSwitcherPoint);
    }

    public void CreateFreeFloorSwitcherPointsList(IFloorSwitcherPointList floorSwitcherPointsList)
    {
        throw new System.NotImplementedException();
    }
}

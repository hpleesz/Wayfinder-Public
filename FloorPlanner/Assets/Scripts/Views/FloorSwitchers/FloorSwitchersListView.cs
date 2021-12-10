using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface IFloorSwitcherInfoView
{
    public delegate void GetFloorSwitcherCall(int floorSwitcherId);

    public event GetFloorSwitcherCall onGetFloorSwitcherCall;
    void ShowFloorSwitcher(IFloorSwitcher floorSwitcher);

}

public interface IFloorSwitcherEditView
{
    public delegate void AddFloorSwitcherToPlaceCall(int placeId, IFloorSwitcher floorSwitcher);
    public delegate void EditFloorSwitcherCall(int floorSwitcherId, IFloorSwitcher floorSwitcher);
    public delegate void AddFloorSwitcherPointsToFloorSwitcherCall(int floorSwitcherId, IFloorSwitcherPointList floorSwitcherPointList);

    public event AddFloorSwitcherToPlaceCall onAddFloorSwitcherToPlaceCall;
    public event EditFloorSwitcherCall onEditFloorSwitcherCall;
    public event AddFloorSwitcherPointsToFloorSwitcherCall onAddFloorSwitcherPointsToFloorSwitcherCall;

    void NewFloorSwitcherCreated(int floorSwitcherId);
    void FloorSwitcherUpdated(int floorSwitcherId);
    void FloorSwitcherPointsAddedToFloorSwitcher(int floorSwitcherId);


}
public interface IFloorSwitchersListView
{

    public delegate void GetAllFloorSwitchersByPlaceCall(int placeId);
    public delegate void SearchFloorSwitchersByPlaceCall(int placeId, string term);
    public delegate void ClickFloorSwitcherCall(int id);


    public event GetAllFloorSwitchersByPlaceCall onGetAllFloorSwitchersByPlaceCall;

    public event SearchFloorSwitchersByPlaceCall onSearchFloorSwitchersByPlaceCall;

    public event ClickFloorSwitcherCall onClickFloorSwitcherCall;

    void CreateFloorSwitchersList(IFloorSwitcherList floorSwitchersList);
    void ClickFloorSwitcher(IFloorSwitcher selectedFloorSwitcher);

}

public class FloorSwitchersListView : MonoBehaviour, IFloorSwitcherEditView, IFloorSwitchersListView, IFloorSwitcherPointsListView
{

    public Image listElement;
    public GameObject listParent;
    public GameObject detailsPanel;
    public GameObject newPanel;
    public InputField searchTerm;

    public GameObject newListParent;
    public GameObject editListParent;

    public Toggle Up;
    public Toggle Down;

    public Image floorSwitcherPointListElement;
    public Image newFloorSwitcherPointListElement;



    public event IFloorSwitcherEditView.AddFloorSwitcherToPlaceCall onAddFloorSwitcherToPlaceCall;
    public event IFloorSwitcherEditView.EditFloorSwitcherCall onEditFloorSwitcherCall;

    public event IFloorSwitchersListView.GetAllFloorSwitchersByPlaceCall onGetAllFloorSwitchersByPlaceCall;
    public event IFloorSwitchersListView.SearchFloorSwitchersByPlaceCall onSearchFloorSwitchersByPlaceCall;
    public event IFloorSwitchersListView.ClickFloorSwitcherCall onClickFloorSwitcherCall;

    public event IFloorSwitcherPointsListView.GetAllFloorSwitcherPointsByPlaceCall onGetAllFloorSwitcherPointsByPlaceCall;
    public event IFloorSwitcherPointsListView.SearchFloorSwitcherPointsByPlaceCall onSearchFloorSwitcherPointsByPlaceCall;
    public event IFloorSwitcherPointsListView.SearchFloorSwitcherPointsByFloorCall onSearchFloorSwitcherPointsByFloorCall;
    public event IFloorSwitcherPointsListView.GetAllFloorSwitcherPointsByFloorSwitcherCall onGetAllFloorSwitcherPointsByFloorSwitcherCall;
    public event IFloorSwitcherPointsListView.ClickFloorSwitcherPointCall onClickFloorSwitcherPointCall;
    public event IFloorSwitcherPointsListView.GetFreeFloorSwitcherPointsByPlaceCall onGetFreeFloorSwitcherPointsByPlaceCall;
    public event IFloorSwitcherEditView.AddFloorSwitcherPointsToFloorSwitcherCall onAddFloorSwitcherPointsToFloorSwitcherCall;

    // Start is called before the first frame update
    void Start()
    {
        onGetAllFloorSwitchersByPlaceCall(PlayerPrefs.GetInt("PlaceId"));

    }

    public void searchFloorSwitchers()
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        string text = searchTerm.transform.Find("Text").GetComponent<Text>().text.ToString();

        onSearchFloorSwitchersByPlaceCall(PlayerPrefs.GetInt("PlaceId"), text);

    }


    public void CreateNewFloorSwitcher()
    {
        string name = newPanel.transform.Find("Title Panel Edit").Find("Title Input").Find("Text").GetComponent<Text>().text.ToString();
        bool up = Up.GetComponent<Toggle>().isOn;
        bool down = Down.GetComponent<Toggle>().isOn;

        FloorSwitcher floorSwitcher = new FloorSwitcher();
        floorSwitcher.name = name;
        floorSwitcher.up = up;
        floorSwitcher.down = down;

        //string json = floorSwitcher.FloorSwitcherToJson();
        //httpRequest.AddFloorSwitcher(PlayerPrefs.GetInt("PlaceId"), json);
        onAddFloorSwitcherToPlaceCall(PlayerPrefs.GetInt("PlaceId"), floorSwitcher);
    }


    public void NewFloorSwitcherCreated(int floorSwitcherId)
    {
        PlayerPrefs.SetInt("FloorSwitcherId", floorSwitcherId);

        AddFloorSwitcherPointsToFloorSwitcher(floorSwitcherId);
    }

    public void AddFloorSwitcherPointsToFloorSwitcher(int floorSwitcherId)
    {
        List<FloorSwitcherPoint> floorSwitcherPoints = new List<FloorSwitcherPoint>();

        //get checked items
        foreach (Transform listItem in newListParent.transform)
        {
            Debug.Log(listItem.name);


            bool isChecked = listItem.Find("Toggle").GetComponent<Toggle>().isOn;
            Debug.Log(isChecked);
            if (isChecked)
            {
                FloorSwitcherPoint floorSwitcherPoint = new FloorSwitcherPoint();
                floorSwitcherPoint.id = Int32.Parse(listItem.name);
                floorSwitcherPoints.Add(floorSwitcherPoint);
            }

        }

        FloorSwitcherPointList pointList = new FloorSwitcherPointList();
        pointList.floorswitcherpoints = floorSwitcherPoints;
        //string json = pointList.FloorSwitcherPointListToJson();
        //Debug.Log(json);

        //httpRequest.AddFloorSwitcherPointsToFloorSwitcher(floorSwitcherId, json);
        onAddFloorSwitcherPointsToFloorSwitcherCall(floorSwitcherId, pointList);

    }



    public void FloorSwitcherUpdated(int floorSwitcherId)
    {
        throw new System.NotImplementedException();
    }

    public void ShowFloorSwitcher(IFloorSwitcher floorSwitcher)
    {
        throw new System.NotImplementedException();
    }

    public void CreateFloorSwitchersList(IFloorSwitcherList floorSwitchersList)
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        foreach (Transform child in listParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < floorSwitchersList.FloorSwitchers.Count; i++)
        {
            Image prefabInstance = Instantiate(listElement) as Image;
            Debug.Log(floorSwitchersList.FloorSwitchers[i].id);
            int id = floorSwitchersList.FloorSwitchers[i].id;
            prefabInstance.GetComponent<Button>().onClick.AddListener(delegate { onClickFloorSwitcherCall(id); });

            prefabInstance.transform.Find("Floor Switcher Name Text").GetComponent<Text>().text = floorSwitchersList.FloorSwitchers[i].name;
            //prefabInstance.transform.Find("Floor Switcher Up Value Text").GetComponent<Text>().text = floorSwitchersList.floorswitchers[i].up.ToString();
            prefabInstance.transform.Find("Floor Switcher Points Value Text").GetComponent<Text>().text = floorSwitchersList.FloorSwitchers[i].floorSwitcherPoints.Count.ToString();

            prefabInstance.rectTransform.SetParent(listParent.transform);
            prefabInstance.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        }
        if (floorSwitchersList.FloorSwitchers.Count > 0)
        {
            onClickFloorSwitcherCall(floorSwitchersList.FloorSwitchers[0].id);
        }
    }

    public void ClickFloorSwitcher(IFloorSwitcher selectedFloorSwitcher)
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        PlayerPrefs.SetInt("FloorSwitcherId", selectedFloorSwitcher.Id);

        detailsPanel.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>().text = selectedFloorSwitcher.Name;
        detailsPanel.transform.Find("Details Panel").Find("Toggle Up").GetComponent<Toggle>().isOn = selectedFloorSwitcher.Up;
        detailsPanel.transform.Find("Details Panel").Find("Toggle Down").GetComponent<Toggle>().isOn = selectedFloorSwitcher.Down;

        //!!!!!!!!!!!!!!!!!!!
        //httpRequest.GetFloorSwitcherPointsByFloorSwitcher(PlayerPrefs.GetInt("FloorSwitcherId"));
        onGetAllFloorSwitcherPointsByFloorSwitcherCall(PlayerPrefs.GetInt("FloorSwitcherId"));

    }

    public void CreateFloorSwitcherPointsList(IFloorSwitcherPointList floorSwitcherPointsList)
    {
        foreach (Transform child in editListParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        for (int i = 0; i < floorSwitcherPointsList.FloorSwitcherPoints.Count; i++)
        {
            Image prefabInstance = Instantiate(floorSwitcherPointListElement) as Image;
            prefabInstance.name = floorSwitcherPointsList.FloorSwitcherPoints[i].Id.ToString();
            Debug.Log(floorSwitcherPointsList.FloorSwitcherPoints[i].Id);
            int id = floorSwitcherPointsList.FloorSwitcherPoints[i].Id;

            prefabInstance.transform.Find("Floor Switcher Point Name Text").GetComponent<Text>().text = floorSwitcherPointsList.FloorSwitcherPoints[i].Name;
            prefabInstance.transform.Find("Floor Value Text").GetComponent<Text>().text = floorSwitcherPointsList.FloorSwitcherPoints[i].Floor.Name;

            prefabInstance.rectTransform.SetParent(editListParent.transform);
            prefabInstance.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        }
    }

    public void ClickFloorSwitcherPoint(IFloorSwitcherPoint selectedFloorSwitcherPoint)
    {
        throw new System.NotImplementedException();
    }

    public void CreateFreeFloorSwitcherPointsList(IFloorSwitcherPointList floorSwitcherPointsList)
    {
        foreach (Transform child in newListParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < floorSwitcherPointsList.FloorSwitcherPoints.Count; i++)
        {
            Image prefabInstance = Instantiate(newFloorSwitcherPointListElement) as Image;
            prefabInstance.name = floorSwitcherPointsList.FloorSwitcherPoints[i].Id.ToString();

            Debug.Log(floorSwitcherPointsList.FloorSwitcherPoints[i].Id);
            int id = floorSwitcherPointsList.FloorSwitcherPoints[i].Id;

            prefabInstance.transform.Find("Floor Switcher Point Name Text").GetComponent<Text>().text = floorSwitcherPointsList.FloorSwitcherPoints[i].Name;
            prefabInstance.transform.Find("Floor Value Text").GetComponent<Text>().text = floorSwitcherPointsList.FloorSwitcherPoints[i].Floor.Name;

            prefabInstance.rectTransform.SetParent(newListParent.transform);
            prefabInstance.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        }
    }


    public void NewFloorSwitcher()
    {
        //PlayerPrefs.SetInt("FloorId", 0);
        detailsPanel.SetActive(false);
        newPanel.SetActive(true);

        //httpRequest.GetFreeFloorSwitcherPointsByPlace(PlayerPrefs.GetInt("PlaceId"));
        onGetFreeFloorSwitcherPointsByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
        //SceneManager.LoadScene("Place Edit");
    }

    public void EditFloorSwitcher()
    {
        SceneManager.LoadScene("Floor Switcher Edit");
    }


    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void FloorSwitcherPointsAddedToFloorSwitcher(int floorSwitcherId)
    {
        onGetAllFloorSwitchersByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
    }
}

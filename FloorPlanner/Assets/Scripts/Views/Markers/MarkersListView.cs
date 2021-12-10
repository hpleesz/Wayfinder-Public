using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface IMarkerInfoView
{
    public delegate void GetMarkerCall(int markerId);

    public event GetMarkerCall onGetMarkerCall;
    void ShowMarker(IMarker marker);

}

public interface IMarkerEditView
{
    public delegate void AddMarkerToFloorCall(int floorId, IMarker marker);
    public delegate void EditMarkerCall(int markerId, IMarker marker);

    public event AddMarkerToFloorCall onAddMarkerToFloorCall;
    public event EditMarkerCall onEditMarkerCall;

    void NewMarkerCreated(int markerId);
    void MarkerUpdated(int markerId);
}
public interface IMarkersListView
{

    public delegate void GetAllMarkersByPlaceCall(int placeId);
    public delegate void GetAllMarkersByFloorCall(int floorId);

    public delegate void SearchMarkersByPlaceCall(int placeId, string term);
    public delegate void SearchMarkersByFloorCall(int floorId, string term);

    public delegate void ClickMarkerCall(int id);


    public event GetAllMarkersByPlaceCall onGetAllMarkersByPlaceCall;
    public event GetAllMarkersByFloorCall onGetAllMarkersByFloorCall;

    public event SearchMarkersByPlaceCall onSearchMarkersByPlaceCall;
    public event SearchMarkersByFloorCall onSearchMarkersByFloorCall;

    public event ClickMarkerCall onClickMarkerCall;

    void CreateMarkersList(IMarkerList markersList);

    void ClickMarker(IMarker selectedMarker);


}
public class MarkersListView : MonoBehaviour, IMarkersListView, IFloorsListView, IMarkerEditView
{

    public Image listElement;
    public GameObject listParent;
    public GameObject detailsPanel;
    public GameObject newPanel;
    public InputField searchTerm;

    public Dropdown floorDropdown;

    private List<int> floors = new List<int>();

    public event IMarkersListView.GetAllMarkersByPlaceCall onGetAllMarkersByPlaceCall;
    public event IMarkersListView.GetAllMarkersByFloorCall onGetAllMarkersByFloorCall;
    public event IMarkersListView.SearchMarkersByPlaceCall onSearchMarkersByPlaceCall;
    public event IMarkersListView.SearchMarkersByFloorCall onSearchMarkersByFloorCall;
    public event IMarkersListView.ClickMarkerCall onClickMarkerCall;

    public event IFloorsListView.GetAllFloorsByPlaceCall onGetAllFloorsByPlaceCall;
    public event IFloorsListView.SearchFloorsByPlaceCall onSearchFloorsByPlaceCall;
    public event IFloorsListView.ClickFloorCall onClickFloorCall;

    public event IMarkerEditView.AddMarkerToFloorCall onAddMarkerToFloorCall;
    public event IMarkerEditView.EditMarkerCall onEditMarkerCall;

    // Start is called before the first frame update
    void Start()
    {
        onGetAllMarkersByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
        onGetAllFloorsByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
    }


    public void searchMarkers()
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        string text = searchTerm.transform.Find("Text").GetComponent<Text>().text.ToString();
        onSearchMarkersByPlaceCall(PlayerPrefs.GetInt("PlaceId"), text);
    }

    public void CreateMarkersList(IMarkerList markersList)
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        foreach (Transform child in listParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < markersList.Markers.Count; i++)
        {
            Image prefabInstance = Instantiate(listElement) as Image;
            Debug.Log(markersList.Markers[i].id);
            int id = markersList.Markers[i].id;
            prefabInstance.GetComponent<Button>().onClick.AddListener(delegate { onClickMarkerCall(id); });

            prefabInstance.transform.Find("Marker Name Text").GetComponent<Text>().text = markersList.Markers[i].name;
            prefabInstance.transform.Find("Marker Floor Value Text").GetComponent<Text>().text = markersList.Markers[i].floor.name;

            prefabInstance.rectTransform.SetParent(listParent.transform);
            prefabInstance.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        }
        if (markersList.Markers.Count > 0)
        {
            onClickMarkerCall(markersList.Markers[0].id);
        }

        //Debug.Log(placeList.places.Count);    
    }

    public void ClickMarker(IMarker selectedMarker)
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        Debug.Log(selectedMarker.Id);
        PlayerPrefs.SetInt("MarkerId", selectedMarker.Id);

        detailsPanel.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>().text = selectedMarker.Name;
        detailsPanel.transform.Find("Details Panel").Find("Floor Value Text").GetComponent<Text>().text = selectedMarker.Floor.name;
        detailsPanel.transform.Find("Details Panel").Find("X Value Text").GetComponent<Text>().text = selectedMarker.XCoordinate.ToString();
        detailsPanel.transform.Find("Details Panel").Find("Y Value Text").GetComponent<Text>().text = selectedMarker.YCoordinate.ToString();
        detailsPanel.transform.Find("Details Panel").Find("Z Value Text").GetComponent<Text>().text = selectedMarker.ZCoordinate.ToString();

    }

    public void CreateFloorsList(IFloorList floorsList)
    {
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
    }

    public void CreateNewMarker()
    {
        string name = gameObject.transform.Find("New Panel").Find("Title Panel Edit").Find("Title Input").Find("Text").GetComponent<Text>().text.ToString();

        Marker marker = new Marker();
        marker.name = name;

        onAddMarkerToFloorCall(floors[floorDropdown.value], marker);
    }

    public void ClickFloor(IFloor selectedFloor)
    {
        throw new System.NotImplementedException();
    }

    public void NewMarkerCreated(int markerId)
    {
        PlayerPrefs.SetInt("MarkerId", markerId);
        onGetAllMarkersByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
    }

    public void MarkerUpdated(int markerId)
    {
        throw new System.NotImplementedException();
    }
}

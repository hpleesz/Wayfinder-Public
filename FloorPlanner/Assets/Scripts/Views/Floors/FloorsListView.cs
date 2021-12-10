using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static IFloorEditView;
using static IFloorsListView;


public interface IFloorEditView
{
    public delegate void AddFloorToPlaceCall(int placeId, IFloor floor);
    public delegate void EditFloorCall(int floorId, IFloor floor);

    public event AddFloorToPlaceCall onAddFloorToPlaceCall;
    public event EditFloorCall onEditFloorCall;

    void NewFloorCreated(int floorId);
    void FloorUpdated(int floorId);


}
public interface IFloorsListView
{

    public delegate void GetAllFloorsByPlaceCall(int placeId);
    public delegate void SearchFloorsByPlaceCall(int placeId, string term);
    public delegate void ClickFloorCall(int id);


    public event GetAllFloorsByPlaceCall onGetAllFloorsByPlaceCall;
    public event SearchFloorsByPlaceCall onSearchFloorsByPlaceCall;
    public event ClickFloorCall onClickFloorCall;

    void CreateFloorsList(IFloorList floorsList);
    void ClickFloor(IFloor selectedFloor);

}

public class FloorsListView : MonoBehaviour, IFloorsListView, IFloorEditView
{
    //private FloorList floorList;
    //public HttpRequest httpRequest;
    public Image listElement;
    public GameObject listParent;
    public GameObject detailsPanel;
    public GameObject newPanel;
    public InputField searchTerm;

    public event GetAllFloorsByPlaceCall onGetAllFloorsByPlaceCall;
    public event SearchFloorsByPlaceCall onSearchFloorsByPlaceCall;
    public event ClickFloorCall onClickFloorCall;
    public event AddFloorToPlaceCall onAddFloorToPlaceCall;
    public event EditFloorCall onEditFloorCall;


    // Start is called before the first frame update
    void Start()
    {
        onGetAllFloorsByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
    }


    public void searchFloors()
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        string text = searchTerm.transform.Find("Text").GetComponent<Text>().text.ToString();
        //httpRequest.SearchFloors(PlayerPrefs.GetInt("PlaceId"), text);
        onSearchFloorsByPlaceCall(PlayerPrefs.GetInt("PlaceId"), text);
    }

    public void CreateFloorsList(IFloorList floorList)
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        foreach (Transform child in listParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < floorList.Floors.Count; i++)
        {
            Image prefabInstance = Instantiate(listElement) as Image;
            Debug.Log(floorList.Floors[i].Id);
            int id = floorList.Floors[i].Id;
            prefabInstance.GetComponent<Button>().onClick.AddListener(delegate { onClickFloorCall(id); });

            prefabInstance.transform.Find("Floor Name Text").GetComponent<Text>().text = floorList.Floors[i].Name;
            prefabInstance.transform.Find("Floor Number Text").GetComponent<Text>().text = floorList.Floors[i].Number.ToString();
            prefabInstance.transform.Find("Floor Targets Number Text").GetComponent<Text>().text = floorList.Floors[i].Targets.Count.ToString();

            prefabInstance.rectTransform.SetParent(listParent.transform);
            prefabInstance.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        }
        if(floorList.Floors.Count > 0)
        {
            onClickFloorCall(floorList.Floors[0].Id);
        }
    }

    public void ClickFloor(IFloor selectedFloor)
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        PlayerPrefs.SetInt("FloorId", selectedFloor.Id);


        detailsPanel.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>().text = selectedFloor.Name;
        detailsPanel.transform.Find("Title Panel").Find("Number Text").GetComponent<Text>().text = selectedFloor.Number.ToString();
        detailsPanel.transform.Find("Details Panel").Find("Targets Number Text").GetComponent<Text>().text = selectedFloor.Targets.Count.ToString();

        if (selectedFloor.FloorPlan2D.fileLocation != null && selectedFloor.FloorPlan2D.fileLocation != "")
        {
            string someUrl = HttpRequestSingleton.SERVER_URL + "/" + selectedFloor.FloorPlan2D.fileLocation;
            Debug.Log(someUrl);
            using (var webClient = new WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(someUrl);

                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(imageBytes);
                Sprite sprite2 = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));

                detailsPanel.transform.Find("Image Panel").Find("Image").GetComponent<Image>().sprite = sprite2; //Image is a defined reference to an image component
                detailsPanel.transform.Find("Image Panel").Find("Image").GetComponent<Image>().preserveAspect = true;
                detailsPanel.transform.Find("Image Panel").Find("Image").GetComponent<Image>().rectTransform.sizeDelta = new Vector2(10, 10000);
            }
        }
    }




    public void CreateNewFloor()
    {
        string floorNumber = newPanel.transform.Find("Data Panel").Find("Floor Number Input").Find("Text").GetComponent<Text>().text.ToString();
        string floorName = newPanel.transform.Find("Data Panel").Find("Floor Name Input").Find("Text").GetComponent<Text>().text.ToString();

        Floor floor = new Floor();
        floor.number = Int32.Parse(floorNumber);
        floor.name = floorName;

        onAddFloorToPlaceCall(PlayerPrefs.GetInt("PlaceId"), floor);
        //string json = floor.FloorToJson();
        //httpRequest.AddFloor(PlayerPrefs.GetInt("PlaceId"), json);
    }

    public void NewFloorCreated(int floorId)
    {
        PlayerPrefs.SetInt("FloorId", floorId);
        onGetAllFloorsByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
        //httpRequest.GetFloorsByPlace(PlayerPrefs.GetInt("PlaceId"));
        //AddPlaceImage(placeId);
        //SceneManager.LoadScene("Places");
    }

    public void FloorUpdated(int floorId)
    {
        throw new NotImplementedException();
    }
}

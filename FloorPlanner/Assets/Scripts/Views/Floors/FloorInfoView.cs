using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface IFloorInfoView
{
    public delegate void GetFloorCall(int floorId);

    public event GetFloorCall onGetFloorCall;
    void ShowFloor(IFloor floor);

}

public class FloorInfoView : MonoBehaviour, IFloorInfoView, IFloorEditView
{
    //public HttpRequest httpRequest;
    public GameObject titlePanel;
    public GameObject editPanel;

    public Image targetPrefab;
    public GameObject listParent2;


    public event IFloorInfoView.GetFloorCall onGetFloorCall;

    public event IFloorEditView.AddFloorToPlaceCall onAddFloorToPlaceCall;
    public event IFloorEditView.EditFloorCall onEditFloorCall;

    // Start is called before the first frame update
    void Start()
    {
        //httpRequest.onGetFloor += GetFloor;
        //httpRequest.onEditFloor += FloorUpdated;

        //httpRequest.GetFloor(PlayerPrefs.GetInt("FloorId"));
        onGetFloorCall(PlayerPrefs.GetInt("FloorId"));
    }

    public void BackToFloors()
    {
        SceneManager.LoadScene("Floors");
    }

    public void EditFloor()
    {
        titlePanel.SetActive(false);
        editPanel.SetActive(true);

        gameObject.transform.Find("Title Panel Edit").Find("Title Input").GetComponent<InputField>().text = gameObject.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>().text;
        gameObject.transform.Find("Title Panel Edit").Find("Number Input").GetComponent<InputField>().text = gameObject.transform.Find("Title Panel").Find("Number Text").GetComponent<Text>().text;
    }

    public void SaveFloor()
    {
        string floorNumber = gameObject.transform.Find("Title Panel Edit").Find("Number Input").Find("Text").GetComponent<Text>().text.ToString();
        string floorName = gameObject.transform.Find("Title Panel Edit").Find("Title Input").Find("Text").GetComponent<Text>().text.ToString();

        IFloor floor = new Floor();
        floor.Number = Int32.Parse(floorNumber);
        floor.Name = floorName;
        //string json = floor.FloorToJson();

        //httpRequest.EditFloor(PlayerPrefs.GetInt("FloorId"), json);
        onEditFloorCall(PlayerPrefs.GetInt("FloorId"), floor);

    }

    public void ModelFloor()
    {
        SceneManager.LoadScene("Floor Drawer");
    }

    public void NewFloorCreated(int floorId)
    {
        throw new NotImplementedException();
    }

    public void ShowFloor(IFloor floor)
    {

        gameObject.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>().text = floor.Name;
        gameObject.transform.Find("Title Panel").Find("Number Text").GetComponent<Text>().text = floor.Number.ToString();

        if (floor.FloorPlan2D.fileLocation != null && floor.FloorPlan2D.fileLocation != "")
        {
            string someUrl = HttpRequestSingleton.SERVER_URL + "/" + floor.FloorPlan2D.fileLocation;
            Debug.Log(someUrl);
            using (var webClient = new WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(someUrl);

                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(imageBytes);
                Sprite sprite2 = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));

                transform.Find("Image Panel").Find("Image").GetComponent<Image>().sprite = sprite2; //Image is a defined reference to an image component
                transform.Find("Image Panel").Find("Image").GetComponent<Image>().preserveAspect = true;
                transform.Find("Image Panel").Find("Image").GetComponent<Image>().rectTransform.sizeDelta = new Vector2(10, 10000);
            }
        }

        if (floor.FloorPlan2D.templateLocation != null && floor.FloorPlan2D.templateLocation != "")
        {
            string someUrl = HttpRequestSingleton.SERVER_URL + "/" + floor.FloorPlan2D.templateLocation;
            Debug.Log(someUrl);
            using (var webClient = new WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(someUrl);

                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(imageBytes);
                Sprite sprite2 = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));

                transform.Find("Image Template Panel").Find("Image").GetComponent<Image>().sprite = sprite2; //Image is a defined reference to an image component
                transform.Find("Image Template Panel").Find("Image").GetComponent<Image>().preserveAspect = true;
                transform.Find("Image Template Panel").Find("Image").GetComponent<Image>().rectTransform.sizeDelta = new Vector2(10, 10000);
            }
        }


        if (floor.Targets.Count > 0)
        {
            foreach (var target in floor.Targets)
            {
                Image prefabInstance = Instantiate(targetPrefab) as Image;

                prefabInstance.transform.Find("Target Name Text").GetComponent<Text>().text = target.name;
                prefabInstance.transform.Find("Target Floor Value Text").GetComponent<Text>().text = floor.Name;

                prefabInstance.rectTransform.SetParent(listParent2.transform);
                prefabInstance.rectTransform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    public void FloorUpdated(int floorId)
    {
        gameObject.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>().text = gameObject.transform.Find("Title Panel Edit").Find("Title Input").Find("Text").GetComponent<Text>().text;
        gameObject.transform.Find("Title Panel").Find("Number Text").GetComponent<Text>().text = gameObject.transform.Find("Title Panel Edit").Find("Number Input").Find("Text").GetComponent<Text>().text;

        titlePanel.SetActive(true);
        editPanel.SetActive(false);
    }
}

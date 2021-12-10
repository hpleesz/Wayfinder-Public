using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface IPlacesListView
{

    public delegate void GetAllPlacesCall();
    public delegate void SearchPlacesCall(string term);
    public delegate void ClickPlaceCall(int id);

    public event GetAllPlacesCall onGetAllPlacesCall;
    public event SearchPlacesCall onSearchPlacesCall;
    public event ClickPlaceCall onClickPlaceCall;

    void CreatePlacesList(IPlaceList placesList);
    void ClickPlace(IPlace selectedPlace);
}
public class PlacesList : MonoBehaviour, IPlacesListView
{
    private PlaceList placeList;
    //public HttpRequest httpRequest;
    public Image listElement;
    public GameObject listParent;
    public GameObject detailsPanel;
    public GameObject listPanel;
    public InputField searchTerm;

    public event IPlacesListView.GetAllPlacesCall onGetAllPlacesCall;
    public event IPlacesListView.SearchPlacesCall onSearchPlacesCall;
    public event IPlacesListView.ClickPlaceCall onClickPlaceCall;

    // Start is called before the first frame update
    void Start()
    {

        onGetAllPlacesCall();

        PlayerPrefs.SetInt("FloorId", 0);
        PlayerPrefs.SetInt("TargetId", 0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void searchPlaces()
    {
        string text = searchTerm.transform.Find("Text").GetComponent<Text>().text.ToString();
        onSearchPlacesCall(text);
    }

    public void backButton()
    {
        listPanel.SetActive(true);
        detailsPanel.SetActive(false);
    }

    public void ViewPlace()
    {
        SceneManager.LoadScene("SplitScreen");
    }

    public void CreatePlacesList(IPlaceList placeList)
    {
        foreach (Transform child in listParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < placeList.Places.Count; i++)
        {
            Image prefabInstance = Instantiate(listElement) as Image;
            int id = placeList.Places[i].id;
            prefabInstance.GetComponent<Button>().onClick.AddListener(delegate { onClickPlaceCall(id); });
            Text textComponent = prefabInstance.transform.Find("Place Name Text").GetComponent<Text>();
            Text textComponentAddress = prefabInstance.transform.Find("Place Address Text").GetComponent<Text>();

            textComponent.text = placeList.Places[i].name;
            textComponentAddress.text = placeList.Places[i].country + " " + placeList.Places[i].zip + ", " + placeList.Places[i].city + " " + placeList.Places[i].address;

            prefabInstance.rectTransform.SetParent(listParent.transform);
            prefabInstance.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        }
    }

    public void ClickPlace(IPlace selectedPlace)
    {
        listPanel.SetActive(false);
        detailsPanel.SetActive(true);
        PlayerPrefs.SetInt("PlaceId", selectedPlace.Id);


        Text titleText = detailsPanel.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>();
        titleText.text = selectedPlace.Name;

        Text addressText1 = detailsPanel.transform.Find("Address Panel").Find("Address1 Text").GetComponent<Text>();
        addressText1.text = selectedPlace.Country + " " + selectedPlace.Zip + ",";

        Text addressText2 = detailsPanel.transform.Find("Address Panel").Find("Address2 Text").GetComponent<Text>();
        addressText2.text = selectedPlace.City + " " + selectedPlace.Address;

        Text descriptionText = detailsPanel.transform.Find("Data Panel").Find("Description Text").GetComponent<Text>();
        descriptionText.text = selectedPlace.Description;

        Texture2D textureQR = QRcode.generateQR(selectedPlace.Id.ToString());
        Image QrImage = detailsPanel.transform.Find("Title Panel").Find("QR Image").GetComponent<Image>();

        Sprite sprite = Sprite.Create(textureQR, new Rect(0, 0, textureQR.width, textureQR.height), new Vector2(.5f, .5f));

        QrImage.sprite = sprite; //Image is a defined reference to an image component
        QrImage.preserveAspect = true;


        if (selectedPlace.Image != null && selectedPlace.Image != "")
        {
            string someUrl = HttpRequestSingleton.SERVER_URL + "/" + selectedPlace.Image;
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
}

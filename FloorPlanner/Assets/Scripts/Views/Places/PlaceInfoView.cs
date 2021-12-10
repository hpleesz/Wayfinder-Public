using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;


public interface IPlaceInfoView
{
    public delegate void GetPlaceCall(int placeId);

    public event GetPlaceCall onGetPlaceCall;

    public void ShowPlace(IPlace place);

}

public class PlaceInfoView : MonoBehaviour, IPlaceInfoView
{
    public Image floorPrefab;
    public GameObject listParent;
    public Sprite imagePlaceholder;

    public event IPlaceInfoView.GetPlaceCall onGetPlaceCall;

    // Start is called before the first frame update
    void Start()
    {
        
        onGetPlaceCall(PlayerPrefs.GetInt("PlaceId"));

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowPlace(IPlace place)
    {

        gameObject.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>().text = place.Name;
        gameObject.transform.Find("Address Panel").Find("Address1 Text").GetComponent<Text>().text = place.Country + " " + place.Zip;
        gameObject.transform.Find("Address Panel").Find("Address2 Text").GetComponent<Text>().text = place.City + " " + place.Address;
        gameObject.transform.Find("Data Panel").Find("Description Text").GetComponent<Text>().text = place.Description;

        if (place.Floors.Count > 0)
        {
            foreach (var floor in place.Floors)
            {
                Image prefabInstance = Instantiate(floorPrefab) as Image;

                prefabInstance.transform.Find("Floor Number Text").GetComponent<Text>().text = floor.number.ToString();
                prefabInstance.transform.Find("Floor Name Text").GetComponent<Text>().text = floor.name;


                prefabInstance.transform.Find("Floor Targets Number Text").GetComponent<Text>().text = floor.targets.Count.ToString();
                //prefabInstance.transform.Find("Floor Name Text").GetComponent<Text>().text = floor.name;

                prefabInstance.rectTransform.SetParent(listParent.transform);
                prefabInstance.rectTransform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        Texture2D textureQR = QRcode.generateQR(place.Id.ToString());
        Image QrImage = gameObject.transform.Find("QR Panel").Find("QR Image").GetComponent<Image>();

        Sprite sprite = Sprite.Create(textureQR, new Rect(0, 0, textureQR.width, textureQR.height), new Vector2(.5f, .5f));

        QrImage.sprite = sprite; //Image is a defined reference to an image component
        QrImage.preserveAspect = true;


        if (place.Image != null && place.Image != "")
        {
            string someUrl = HttpRequestSingleton.SERVER_URL + "/" + place.Image;
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
        else
        {
            transform.Find("Image Panel").Find("Image").GetComponent<Image>().sprite = imagePlaceholder;
        }
    }

}

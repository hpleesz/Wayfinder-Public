using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static IPlaceEditView;

public interface IPlaceEditView
{
    public delegate void EditPlaceCall(int placeId, IPlace place);
    public delegate void AddPlaceCall(IPlace place);
    public delegate void AddPlaceImage(int placeId, byte[] form);

    public event EditPlaceCall onEditPlaceCall;
    public event AddPlaceCall onAddPlaceCall;
    public event AddPlaceImage onAddPlaceImageCall;


    public void NewPlaceCreated(int placeId);
    public void PlaceUpdated(int placeId);
    public void PlaceImageAdded();

}
public class PlaceEditView : MonoBehaviour, IPlaceEditView, IPlaceInfoView
{
    public event IPlaceInfoView.GetPlaceCall onGetPlaceCall;

    public event EditPlaceCall onEditPlaceCall;
    public event AddPlaceCall onAddPlaceCall;
    public event AddPlaceImage onAddPlaceImageCall;


    // Start is called before the first frame update
    void Start()
    {

        //new
        if (PlayerPrefs.GetInt("PlaceId") == 0)
        {
            gameObject.transform.Find("Title Panel").gameObject.SetActive(false);
            gameObject.transform.Find("Title Panel Edit").gameObject.SetActive(true);
            gameObject.transform.Find("Nav Panel").gameObject.SetActive(true);
            gameObject.transform.Find("Nav Panel Edit").gameObject.SetActive(false);
        }
        //edit
        else
        {
            gameObject.transform.Find("Nav Panel").gameObject.SetActive(false);
            gameObject.transform.Find("Nav Panel Edit").gameObject.SetActive(true);


            onGetPlaceCall(PlayerPrefs.GetInt("PlaceId"));


        }
    }

    private void AddPlaceImage(int placeId)
    {
        Sprite sprite = gameObject.transform.Find("Image Panel").Find("Image").GetComponent<Image>().sprite;
        if (sprite.name != "image_placeholder")
        {

            Texture2D texture = gameObject.transform.Find("Image Panel").Find("Image").GetComponent<Image>().sprite.texture;
            byte[] itemBytes = texture.EncodeToPNG();

            //WWWForm form = new WWWForm();
            //form.AddBinaryData("file", itemBytes);


            //httpRequest.AddPlaceImage(placeId, form);
            onAddPlaceImageCall(placeId, itemBytes);
        }
        else
        {
            SceneManager.LoadScene("Place");
        }
    }


    public void CreateNewPlace()
    {
        string title = gameObject.transform.Find("Title Panel Edit").Find("Title Input").Find("Text").GetComponent<Text>().text.ToString();
        string country = gameObject.transform.Find("Address Panel").Find("Country Input").Find("Text").GetComponent<Text>().text.ToString();
        string zip = gameObject.transform.Find("Address Panel").Find("Postal Code Input").Find("Text").GetComponent<Text>().text.ToString();
        string city = gameObject.transform.Find("Address Panel").Find("City Input").Find("Text").GetComponent<Text>().text.ToString();
        string address = gameObject.transform.Find("Address Panel").Find("Address Input").Find("Text").GetComponent<Text>().text.ToString();
        string description = gameObject.transform.Find("Details Panel").Find("Description Input").Find("Text").GetComponent<Text>().text.ToString();

        Place place = new Place();
        place.name = title;
        place.country = country;
        place.zip = zip;
        place.city = city;
        place.address = address;
        place.description = description;

        onAddPlaceCall(place);
    }


    public void EditPlace()
    {
        string title = gameObject.transform.Find("Title Panel Edit").Find("Title Input").Find("Text").GetComponent<Text>().text.ToString();
        string country = gameObject.transform.Find("Address Panel").Find("Country Input").Find("Text").GetComponent<Text>().text.ToString();
        string zip = gameObject.transform.Find("Address Panel").Find("Postal Code Input").Find("Text").GetComponent<Text>().text.ToString();
        string city = gameObject.transform.Find("Address Panel").Find("City Input").Find("Text").GetComponent<Text>().text.ToString();
        string address = gameObject.transform.Find("Address Panel").Find("Address Input").Find("Text").GetComponent<Text>().text.ToString();
        string description = gameObject.transform.Find("Details Panel").Find("Description Input").Find("Text").GetComponent<Text>().text.ToString();

        Place place = new Place();
        place.name = title;
        place.country = country;
        place.zip = zip;
        place.city = city;
        place.address = address;
        place.description = description;

        //string json = place.PlaceToJson();
        onEditPlaceCall(PlayerPrefs.GetInt("PlaceId"), place);
        //httpRequest.EditPlace(PlayerPrefs.GetInt("PlaceId"), json);
    }

    public void Back()
    {
        if (PlayerPrefs.GetInt("PlaceId") == 0)
        {
            SceneManager.LoadScene("Places");
        }
        else
        {
            SceneManager.LoadScene("Place");
        }
    }

    public void NewPlaceCreated(int placeId)
    {
        PlayerPrefs.SetInt("PlaceId", placeId);

        AddPlaceImage(placeId);

    }

    public void PlaceUpdated(int placeId)
    {
        AddPlaceImage(placeId);
    }

    public void ShowPlace(IPlace place)
    {

        Debug.Log(place.Floors.Count);

        Debug.Log(place.Name);

        gameObject.transform.Find("Title Panel Edit").gameObject.SetActive(true);
        gameObject.transform.Find("Title Panel").gameObject.SetActive(false);

        gameObject.transform.Find("Title Panel Edit").Find("Title Input").GetComponent<InputField>().text = place.Name;
        gameObject.transform.Find("Address Panel").Find("Country Input").GetComponent<InputField>().text = place.Country;
        gameObject.transform.Find("Address Panel").Find("Postal Code Input").GetComponent<InputField>().text = place.Zip;
        gameObject.transform.Find("Address Panel").Find("City Input").GetComponent<InputField>().text = place.City;
        gameObject.transform.Find("Address Panel").Find("Address Input").GetComponent<InputField>().text = place.Address;
        gameObject.transform.Find("Details Panel").Find("Description Input").GetComponent<InputField>().text = place.Description;

        if (place.Image != null && place.Image != "")
        {
            string someUrl = HttpRequestSingleton.SERVER_URL + "/" + place.Image;
            Debug.Log(someUrl);
            using (var webClient = new WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(someUrl);

                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(imageBytes);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));

                gameObject.transform.Find("Image Panel").Find("Image").GetComponent<Image>().sprite = sprite; //Image is a defined reference to an image component
                gameObject.transform.Find("Image Panel").Find("Image").GetComponent<Image>().preserveAspect = true;
                gameObject.transform.Find("Image Panel").Find("Image").GetComponent<Image>().rectTransform.sizeDelta = new Vector2(10, 10000);
            }
        }
    }

    public void PlaceImageAdded()
    {
        SceneManager.LoadScene("Place");
    }
}

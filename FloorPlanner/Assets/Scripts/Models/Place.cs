using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HttpRequestSingleton;

public interface IPlace
{
    public delegate void PlaceResult(IPlace place);
    public delegate void IdResult(int id);
    public delegate void Result();

    event PlaceResult onGetPlaceResult;
    event IdResult onEditPlaceResult;
    event IdResult onAddPlaceResult;
    event Result onAddPlaceImageResult;

    public string Name { get; set; }
    public string Country { get; set; }
    public string Zip { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int Id { get; set; }
    public List<Floor> Floors { get; set; }

    public IPlace PlaceFromJson(string json);
    public string PlaceToJson();

    public void EditPlace(int placeId, IPlace place);
    public void AddPlace(IPlace place);
    public void AddPlaceImage(int placeId, byte[] form);
    public void GetPlace(int placeId);

}
[System.Serializable]
public class Place: IPlace
{
    private event Result onGetPlace;
    private event Result onAddPlace;
    private event Result onAddPlaceImage;
    private event Result onEditPlace;

    public event IPlace.PlaceResult onGetPlaceResult;
    public event IPlace.IdResult onEditPlaceResult;
    public event IPlace.IdResult onAddPlaceResult;
    public event IPlace.Result onAddPlaceImageResult;

    public string name;
    public string country;
    public string zip;
    public string city;
    public string address;
    public string description;
    public string image;
    public int id;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string Country
    {
        get { return country; }
        set { country = value; }
    }
    public string Zip
    {
        get { return zip; }
        set { zip = value; }
    }
    public string City
    {
        get { return city; }
        set { zip = value; }
    }
    public string Address
    {
        get { return address; }
        set { address = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public string Image
    {
        get { return image; }
        set { image = value; }
    }
    public int Id
    {
        get { return id; }
        set { id = value; }
    }


    public List<Floor> floors;
    public List<Floor> Floors
    {
        get { return floors; }
        set { floors = value; }
    }
    public IPlace PlaceFromJson(string json)
    {
        return JsonUtility.FromJson<Place>(json);
    }

    public string PlaceToJson()
    {
        return JsonUtility.ToJson(this);
    }

    //EDIT PLACE
    public void EditPlace(int placeId, IPlace place)
    {
        onEditPlace += onResultEditPlace;
        string json = place.PlaceToJson();
        HttpRequestSingleton.Instance.CallPut("/places/" + placeId, json, onEditPlace);
    }

    public void onResultEditPlace(byte[] result)
    {
        int placeId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onEditPlaceResult(placeId);
    }

    //ADD PLACE
    public void AddPlace(IPlace place)
    {
        onAddPlace += onResultAddPlace;
        string json = place.PlaceToJson();
        HttpRequestSingleton.Instance.CallPost("/places", json, onAddPlace);
    }
    public void onResultAddPlace(byte[] result)
    {
        int placeId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onAddPlaceResult(placeId);
    }

    //ADD PLACE IMAGE
    public void AddPlaceImage(int placeId, byte[] itemBytes)
    {
        onAddPlaceImage += onResultAddPlaceImage;
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", itemBytes);
        HttpRequestSingleton.Instance.CallPostForm("/places/" + placeId + "/image", form, onAddPlaceImage);
    }

    public void onResultAddPlaceImage(byte[] result)
    {
        onAddPlaceImageResult();
    }

    //GET PLACE
    public void GetPlace(int placeId)
    {
        onGetPlace += onResultGetPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId, onGetPlace);
    }

    private void onResultGetPlace(byte[] result)
    {
        string placeJson = System.Text.Encoding.UTF8.GetString(result);
        IPlace place = new Place();
        place = place.PlaceFromJson(placeJson);
        onGetPlaceResult(place);
    }
}


public interface IPlaceList
{
    public delegate void PlacesResult(IPlaceList placeModelList);
    public delegate void PlaceResult(IPlace place);

    event PlacesResult onGetAllPlacesResult;
    event PlacesResult onSearchPlacesResult;


    public List<Place> Places { get; set; }

    public void GetAllPlaces();
    public void SearchPlaces(string term);

    //
    public Place GetPlaceById(int placeId);



}
public class PlaceList : IPlaceList
{
    private event Result onGetAllPlaces;
    private event Result onSearchPlaces;


    public event IPlaceList.PlacesResult onGetAllPlacesResult;
    public event IPlaceList.PlacesResult onSearchPlacesResult;


    public List<Place> places;
    public List<Place> Places
    {
        get { return places; }
        set { places = value; }
    }

    public static PlaceList PlaceListFromJson(string json)
    {
        return JsonUtility.FromJson<PlaceList>(json);
    }
    
    //GET ALL PLACES
    public void GetAllPlaces()
    {
        onGetAllPlaces += onResultGetAllPlaces;
        HttpRequestSingleton.Instance.CallGet("/places", onGetAllPlaces);
    }

    private void onResultGetAllPlaces(byte[] result)
    {
        string placesJson = System.Text.Encoding.UTF8.GetString(result);
        places = PlaceListFromJson(placesJson).places;
        onGetAllPlacesResult(this);
    }


    //SEARCH PLACES
    public void SearchPlaces(string term)
    {
        onSearchPlaces += onResultSearchPlaces;
        HttpRequestSingleton.Instance.CallGet("/places/search?term=" + term, onSearchPlaces);
    }
    private void onResultSearchPlaces(byte[] result)
    {
        string placesJson = System.Text.Encoding.UTF8.GetString(result);
        places = PlaceListFromJson(placesJson).places;
        onGetAllPlacesResult(this);
    }

    


    // NEM DB
    public Place GetPlaceById(int placeId)
    {
        Place selectedPlace = places.Where(i => i.id == placeId).FirstOrDefault();
        return selectedPlace;
    }
}

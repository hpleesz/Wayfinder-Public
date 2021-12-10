using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HttpRequestSingleton;

public interface IPlace
{

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


}
[System.Serializable]
public class Place : IPlace
{

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
    //ez a http request resultra hívódik
    private event Result onGetAllPlaces;
    private event Result onSearchPlaces;


    //erre iratkozik fel majd valaki
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HttpRequestSingleton;

public interface IMarker
{
    public delegate void IdResult(int id);
    public delegate void MarkerResult(IMarker marker);

    event IdResult onAddMarkerResult;
    event MarkerResult onGetMarkerResult;
    event IdResult onEditMarkerResult;


    public int Id { get; set; }
    public string Name { get; set; }
    public string QrCode { get; set; }
    public float XCoordinate { get; set; }
    public float YCoordinate { get; set; }
    public float ZCoordinate { get; set; }
    public float XRotation { get; set; }
    public float YRotation { get; set; }
    public float ZRotation { get; set; }
    public Floor Floor { get; set; }


    public IMarker MarkerFromJson(string json);
    public string MarkerToJson();


    public void AddMarkerToFloor(int floorId, IMarker marker);

    public void EditMarker(int markerId, IMarker marker);
    public void GetMarker(int markerId);
}


[System.Serializable]
public class Marker : IMarker
{
    private event Result onAddMarker;
    private event Result onGetMarker;
    private event Result onEditMarker;

    public event IMarker.IdResult onAddMarkerResult;
    public event IMarker.MarkerResult onGetMarkerResult;
    public event IMarker.IdResult onEditMarkerResult;

    public int id;
    public string name;
    public float xCoordinate;
    public float yCoordinate;
    public float zCoordinate;
    public float xRotation;
    public float yRotation;
    public float zRotation;

    public Floor floor;
    public string qrCode;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string QrCode
    {
        get { return qrCode; }
        set { qrCode = value; }
    }
    public float XCoordinate
    {
        get { return xCoordinate; }
        set { xCoordinate = value; }
    }
    public float YCoordinate
    {
        get { return yCoordinate; }
        set { yCoordinate = value; }
    }
    public float ZCoordinate
    {
        get { return zCoordinate; }
        set { zCoordinate = value; }
    }
    public float XRotation
    {
        get { return xRotation; }
        set { xRotation = value; }
    }
    public float YRotation
    {
        get { return yRotation; }
        set { yRotation = value; }
    }
    public float ZRotation
    {
        get { return zRotation; }
        set { zRotation = value; }
    }
    public Floor Floor
    {
        get { return floor; }
        set { floor = value; }
    }



    //ADD TARGET
    public void AddMarkerToFloor(int floorId, IMarker marker)
    {
        onAddMarker += onResultAddMarker;
        string json = marker.MarkerToJson();
        HttpRequestSingleton.Instance.CallPost("/floors/" + floorId + "/markers", json, onAddMarker);
    }
    public void onResultAddMarker(byte[] result)
    {
        int markerId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onAddMarkerResult(markerId);
    }

    //EDIT TARGET
    public void EditMarker(int markerId, IMarker marker)
    {
        onEditMarker += onResultEditMarker;
        string json = marker.MarkerToJson();
        HttpRequestSingleton.Instance.CallPut("/markers/" + markerId, json, onEditMarker);
    }

    public void onResultEditMarker(byte[] result)
    {
        int markerId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onEditMarkerResult(markerId);
    }

    //GET TARGET
    public void GetMarker(int markerId)
    {
        onGetMarker += onResultGetMarker;
        HttpRequestSingleton.Instance.CallGet("/markers/" + markerId, onGetMarker);
    }

    private void onResultGetMarker(byte[] result)
    {
        string markerJson = System.Text.Encoding.UTF8.GetString(result);
        IMarker marker = new Marker();
        marker = marker.MarkerFromJson(markerJson);
        onGetMarkerResult(marker);
    }



    public IMarker MarkerFromJson(string json)
    {
        return JsonUtility.FromJson<Marker>(json);
    }

    public string MarkerToJson()
    {
        return JsonUtility.ToJson(this);
    }
}



public interface IMarkerList
{
    public delegate void MarkersResult(IMarkerList markerModelList);
    public delegate void IdResult(int id);
    public delegate void Result();

    event MarkersResult onGetAllMarkersByPlaceResult;
    event MarkersResult onGetAllMarkersByFloorResult;

    event MarkersResult onSearchMarkersByPlaceResult;
    event MarkersResult onSearchMarkersByFloorResult;


    public List<Marker> Markers { get; set; }

    public IMarkerList MarkerListFromJson(string json);


    public void GetAllMarkersByPlace(int placeId);
    public void GetAllMarkersByFloor(int floorId);
    public void SearchMarkersByPlace(int placeId, string term);
    public void SearchMarkersByFloor(int floorId, string term);


    //
    public IMarker GetMarkerById(int markerId);



}

public class MarkerList : IMarkerList
{
    private event Result onGetAllMarkersByFloor;
    private event Result onGetAllMarkersByPlace;
    private event Result onSearchMarkersByPlace;
    private event Result onSearchMarkersByFloor;

    public event IMarkerList.MarkersResult onGetAllMarkersByPlaceResult;
    public event IMarkerList.MarkersResult onGetAllMarkersByFloorResult;
    public event IMarkerList.MarkersResult onSearchMarkersByPlaceResult;
    public event IMarkerList.MarkersResult onSearchMarkersByFloorResult;

    public List<Marker> markers;

    public List<Marker> Markers
    {
        get { return markers; }
        set { markers = value; }
    }

    public IMarkerList MarkerListFromJson(string json)
    {
        return JsonUtility.FromJson<MarkerList>(json);
    }



    //GET TARGETS BY FLOOR
    public void GetAllMarkersByFloor(int floorId)
    {
        onGetAllMarkersByFloor += onResultGetAllMarkersByFloor;
        HttpRequestSingleton.Instance.CallGet("/floors/" + floorId + "/markers", onGetAllMarkersByFloor);
    }

    private void onResultGetAllMarkersByFloor(byte[] result)
    {
        string markersJson = System.Text.Encoding.UTF8.GetString(result);
        markers = MarkerListFromJson(markersJson).Markers;
        onGetAllMarkersByFloorResult(this);
    }

    //GET TARGETS BY PLACE
    public void GetAllMarkersByPlace(int placeId)
    {
        onGetAllMarkersByPlace += onResultGetAllMarkersByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/markers", onGetAllMarkersByPlace);
    }

    private void onResultGetAllMarkersByPlace(byte[] result)
    {
        string markersJson = System.Text.Encoding.UTF8.GetString(result);
        markers = MarkerListFromJson(markersJson).Markers;
        onGetAllMarkersByPlaceResult(this);
    }

    //SEARCH 
    public void SearchMarkersByFloor(int floorId, string term)
    {
        onSearchMarkersByFloor += onResultSearchMarkersByFloor;
        HttpRequestSingleton.Instance.CallGet("/floors/" + floorId + "/floors/search?term=" + term, onSearchMarkersByFloor);
    }
    private void onResultSearchMarkersByFloor(byte[] result)
    {
        string markersJson = System.Text.Encoding.UTF8.GetString(result);
        markers = MarkerListFromJson(markersJson).Markers;
        onSearchMarkersByFloorResult(this);
    }

    public void SearchMarkersByPlace(int placeId, string term)
    {
        onSearchMarkersByPlace += onResultSearchMarkersByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/markers/search?term=" + term, onSearchMarkersByPlace);
    }
    private void onResultSearchMarkersByPlace(byte[] result)
    {
        string markersJson = System.Text.Encoding.UTF8.GetString(result);
        markers = MarkerListFromJson(markersJson).Markers;
        onSearchMarkersByPlaceResult(this);
    }


    //
    public IMarker GetMarkerById(int markerId)
    {
        IMarker selectedMarker = markers.Where(i => i.Id == markerId).FirstOrDefault();
        return selectedMarker;
    }
}

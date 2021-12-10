using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HttpRequestSingleton;

public interface IFloorSwitcherPoint
{
    public delegate void FloorSwitcherPointResult(IFloorSwitcherPoint floorSwitcherPoint);
    public delegate void IdResult(int id);
    public delegate void Result();

    event FloorSwitcherPointResult onGetFloorSwitcherPointResult;
    event IdResult onEditFloorSwitcherPointResult;
    event IdResult onAddFloorSwitcherPointToFloorResult;

    public int Id { get; set; }
    public string Name { get; set; }
    public float XCoordinate { get; set; }
    public float YCoordinate { get; set; }
    public float ZCoordinate { get; set; }
    public Floor Floor { get; set; }
    public FloorSwitcher FloorSwitcher { get; set; }

    public IFloorSwitcherPoint FloorSwitcherPointFromJson(string json);
    public string FloorSwitcherPointToJson();


    public void EditFloorSwitcherPoint(int floorSwitcherPointId, IFloorSwitcherPoint floorSwitcherPoint);
    public void AddFloorSwitcherPointToFloor(int floorId, IFloorSwitcherPoint floorSwitcherPoint);
    public void GetFloorSwitcherPoint(int floorSwitcherPointId);
}


[System.Serializable]
public class FloorSwitcherPoint : IFloorSwitcherPoint
{
    private event Result onAddFloorSwitcherPointToFloor;
    private event Result onGetFloorSwitcherPoint;
    private event Result onEditFloorSwitcherPoint;

    public event IFloorSwitcherPoint.FloorSwitcherPointResult onGetFloorSwitcherPointResult;
    public event IFloorSwitcherPoint.IdResult onEditFloorSwitcherPointResult;
    public event IFloorSwitcherPoint.IdResult onAddFloorSwitcherPointToFloorResult;

    public int id;
    public string name;
    public float xCoordinate;
    public float yCoordinate;
    public float zCoordinate;

    public Floor floor;
    public FloorSwitcher floorSwitcher;

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

    public Floor Floor
    {
        get { return floor; }
        set { floor = value; }
    }

    public FloorSwitcher FloorSwitcher
    {
        get { return floorSwitcher; }
        set { floorSwitcher = value; }
    }

    //ADD FLOOR SWITCHER POINT
    public void AddFloorSwitcherPointToFloor(int floorId, IFloorSwitcherPoint floorSwitcherPoint)
    {
        onAddFloorSwitcherPointToFloor += onResultAddFloorSwitcherPointToFloor;
        string json = floorSwitcherPoint.FloorSwitcherPointToJson();
        HttpRequestSingleton.Instance.CallPost("/floors/" + floorId + "/floor-switcher-points", json, onAddFloorSwitcherPointToFloor);
    }
    public void onResultAddFloorSwitcherPointToFloor(byte[] result)
    {
        int floorSwitcherPointId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onAddFloorSwitcherPointToFloorResult(floorSwitcherPointId);
    }

    //EDIT FLOOR SWITCHER POINT
    public void EditFloorSwitcherPoint(int floorSwitcherPointId, IFloorSwitcherPoint floorSwitcherPoint)
    {
        onEditFloorSwitcherPoint += onResultEditFloorSwitcherPoint;
        string json = floorSwitcherPoint.FloorSwitcherPointToJson();
        HttpRequestSingleton.Instance.CallPut("/floorswitcherpoints/" + floorSwitcherPointId, json, onEditFloorSwitcherPoint);
    }

    public void onResultEditFloorSwitcherPoint(byte[] result)
    {
        int floorId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onEditFloorSwitcherPointResult(floorId);
    }

    //GET FLOOR SWITCHER POINT
    public void GetFloorSwitcherPoint(int floorSwitcherPointId)
    {
        onGetFloorSwitcherPoint += onResultGetFloorSwitcherPoint;
        HttpRequestSingleton.Instance.CallGet("/floorswitcherpoints/" + floorSwitcherPointId, onGetFloorSwitcherPoint);
    }
    private void onResultGetFloorSwitcherPoint(byte[] result)
    {
        string floorSwitcherPointJson = System.Text.Encoding.UTF8.GetString(result);
        IFloorSwitcherPoint floorSwitcherPoint = new FloorSwitcherPoint();
        floorSwitcherPoint = floorSwitcherPoint.FloorSwitcherPointFromJson(floorSwitcherPointJson);
        onGetFloorSwitcherPointResult(floorSwitcherPoint);
    }



    public IFloorSwitcherPoint FloorSwitcherPointFromJson(string json)
    {
        return JsonUtility.FromJson<FloorSwitcherPoint>(json);
    }

    public string FloorSwitcherPointToJson()
    {
        return JsonUtility.ToJson(this);
    }

}


public interface IFloorSwitcherPointList
{
    public delegate void FloorSwitcherPointsResult(IFloorSwitcherPointList floorSwitcherPointsModelList);
    public delegate void IdResult(int id);
    public delegate void Result();

    event FloorSwitcherPointsResult onGetAllFloorSwitcherPointsByPlaceResult;
    event FloorSwitcherPointsResult onSearchFloorSwitcherPointsByPlaceResult;
    event FloorSwitcherPointsResult onSearchFloorSwitcherPointsByFloorResult;

    event FloorSwitcherPointsResult onGetAllFloorSwitcherPointsByFloorSwitcherResult;
    event FloorSwitcherPointsResult onGetFreeFloorSwitcherPointsByPlaceResult;



    public List<FloorSwitcherPoint> FloorSwitcherPoints { get; set; }

    public IFloorSwitcherPointList FloorSwitcherPointListFromJson(string json);

    public string FloorSwitcherPointListToJson();


    public void GetAllFloorSwitcherPointsByPlace(int placeId);
    public void GetFreeFloorSwitcherPointsByPlace(int placeId);
    public void SearchFloorSwitcherPointsByPlace(int placeId, string term);
    public void SearchFloorSwitcherPointsByFloor(int floorId, string term);
    public void GetAllFloorSwitcherPointsByFloorSwitcher(int floorSwitcherId);


    //
    public IFloorSwitcherPoint GetFloorSwitcherPointById(int floorSwitcherPointId);



}


public class FloorSwitcherPointList :IFloorSwitcherPointList
{
    private event Result onGetAllFloorSwitcherPointsByPlace;
    private event Result onSearchFloorSwitcherPointsByPlace;
    private event Result onSearchFloorSwitcherPointsByFloor;

    private event Result onGetAllFloorSwitcherPointsByFloorSwitcher;
    private event Result onGetFreeFloorSwitcherPointsByPlace;



    public event IFloorSwitcherPointList.FloorSwitcherPointsResult onGetAllFloorSwitcherPointsByPlaceResult;
    public event IFloorSwitcherPointList.FloorSwitcherPointsResult onSearchFloorSwitcherPointsByPlaceResult;
    public event IFloorSwitcherPointList.FloorSwitcherPointsResult onSearchFloorSwitcherPointsByFloorResult;
    public event IFloorSwitcherPointList.FloorSwitcherPointsResult onGetAllFloorSwitcherPointsByFloorSwitcherResult;

    public event IFloorSwitcherPointList.FloorSwitcherPointsResult onGetFreeFloorSwitcherPointsByPlaceResult;

    public List<FloorSwitcherPoint> floorswitcherpoints;

    public List<FloorSwitcherPoint> FloorSwitcherPoints
    {
        get { return floorswitcherpoints; }
        set { floorswitcherpoints = value; }
    }


    public IFloorSwitcherPointList FloorSwitcherPointListFromJson(string json)
    {
        return JsonUtility.FromJson<FloorSwitcherPointList>(json);
    }

    public string FloorSwitcherPointListToJson()
    {
        return JsonUtility.ToJson(this);
    }

    //GET ALL FLOOR SWITCHER POINTS
    public void GetAllFloorSwitcherPointsByPlace(int placeId)
    {

        onGetAllFloorSwitcherPointsByPlace += onResultGetAllFloorSwitcherPointsByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/floor-switcher-points", onGetAllFloorSwitcherPointsByPlace);
    }
    private void onResultGetAllFloorSwitcherPointsByPlace(byte[] result)
    {
        string floorSwitcherPointsJson = System.Text.Encoding.UTF8.GetString(result);
        if(FloorSwitcherPointListFromJson(floorSwitcherPointsJson).FloorSwitcherPoints == null)
        {
        }
        floorswitcherpoints = FloorSwitcherPointListFromJson(floorSwitcherPointsJson).FloorSwitcherPoints;
        onGetAllFloorSwitcherPointsByPlaceResult(this);

    }

    //SEARCH FLOOR SWITCHER POINTS
    public void SearchFloorSwitcherPointsByPlace(int placeId, string term)
    {
        onSearchFloorSwitcherPointsByPlace += onResultSearchFloorSwitcherPointsByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/floor-switcher-points/search?term=" + term, onSearchFloorSwitcherPointsByPlace);
    }
    private void onResultSearchFloorSwitcherPointsByPlace(byte[] result)
    {
        string floorSwitcherPointsJson = System.Text.Encoding.UTF8.GetString(result);
        floorswitcherpoints = FloorSwitcherPointListFromJson(floorSwitcherPointsJson).FloorSwitcherPoints;
        onSearchFloorSwitcherPointsByPlaceResult(this);
    }

    public void SearchFloorSwitcherPointsByFloor(int floorId, string term)
    {
        onSearchFloorSwitcherPointsByFloor += onResultSearchFloorSwitcherPointsByFloor;
        HttpRequestSingleton.Instance.CallGet("/floors/" + floorId + "/floor-switcher-points/search?term=" + term, onSearchFloorSwitcherPointsByFloor);
    }
    private void onResultSearchFloorSwitcherPointsByFloor(byte[] result)
    {
        string floorSwitcherPointsJson = System.Text.Encoding.UTF8.GetString(result);
        floorswitcherpoints = FloorSwitcherPointListFromJson(floorSwitcherPointsJson).FloorSwitcherPoints;
        onSearchFloorSwitcherPointsByFloorResult(this);
    }

    //GET ALL FLOOR SWITCHER POINTS BY FLOOR SWITCHER
    public void GetAllFloorSwitcherPointsByFloorSwitcher(int floorSwitcherId)
    {
        onGetAllFloorSwitcherPointsByFloorSwitcher += onResultGetAllFloorSwitcherPointsByFloorSwitcher;
        HttpRequestSingleton.Instance.CallGet("/floorswitchers/" + floorSwitcherId + "/floor-switcher-points", onGetAllFloorSwitcherPointsByFloorSwitcher);
    }
    private void onResultGetAllFloorSwitcherPointsByFloorSwitcher(byte[] result)
    {
        string floorSwitcherPointsJson = System.Text.Encoding.UTF8.GetString(result);
        floorswitcherpoints = FloorSwitcherPointListFromJson(floorSwitcherPointsJson).FloorSwitcherPoints;
        onGetAllFloorSwitcherPointsByFloorSwitcherResult(this);

    }


    //GET FREE FLOOR SWITCHER POINTS
    public void GetFreeFloorSwitcherPointsByPlace(int placeId)
    {
        onGetFreeFloorSwitcherPointsByPlace += onResultGetFreeFloorSwitcherPointsByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/free-floor-switcher-points", onGetFreeFloorSwitcherPointsByPlace);
    }
    private void onResultGetFreeFloorSwitcherPointsByPlace(byte[] result)
    {
        string floorSwitcherPointsJson = System.Text.Encoding.UTF8.GetString(result);
        floorswitcherpoints = FloorSwitcherPointListFromJson(floorSwitcherPointsJson).FloorSwitcherPoints;
        onGetFreeFloorSwitcherPointsByPlaceResult(this);

    }


    public IFloorSwitcherPoint GetFloorSwitcherPointById(int floorSwitcherPointId)
    {
        IFloorSwitcherPoint selectedFloorSwitcherPoint = floorswitcherpoints.Where(i => i.Id == floorSwitcherPointId).FirstOrDefault();
        return selectedFloorSwitcherPoint;
    }

}

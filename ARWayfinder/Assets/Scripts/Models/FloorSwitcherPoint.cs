using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HttpRequestSingleton;

public interface IFloorSwitcherPoint
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float XCoordinate { get; set; }
    public float YCoordinate { get; set; }
    public float ZCoordinate { get; set; }
    public Floor Floor { get; set; }
    public FloorSwitcher FloorSwitcher { get; set; }

    public IFloorSwitcherPoint FloorSwitcherPointFromJson(string json);
    public string FloorSwitcherPointToJson();

}


[System.Serializable]
public class FloorSwitcherPoint : IFloorSwitcherPoint
{


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

    event FloorSwitcherPointsResult onGetAllFloorSwitcherPointsByFloorResult;
    event FloorSwitcherPointsResult onSearchFloorSwitcherPointsByFloorResult;




    public List<FloorSwitcherPoint> FloorSwitcherPoints { get; set; }

    public IFloorSwitcherPointList FloorSwitcherPointListFromJson(string json);

    public string FloorSwitcherPointListToJson();


    public void GetAllFloorSwitcherPointsByFloor(int floorId);

    public void SearchFloorSwitcherPointsByFloor(int floorId, string term);



    


}


public class FloorSwitcherPointList : IFloorSwitcherPointList
{
    //ez a http request resultra hívódik
    private event Result onGetAllFloorSwitcherPointsByFloor;
    private event Result onSearchFloorSwitcherPointsByFloor;





    public event IFloorSwitcherPointList.FloorSwitcherPointsResult onGetAllFloorSwitcherPointsByFloorResult;
    public event IFloorSwitcherPointList.FloorSwitcherPointsResult onSearchFloorSwitcherPointsByFloorResult;

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
    public void GetAllFloorSwitcherPointsByFloor(int floorId)
    {

        onGetAllFloorSwitcherPointsByFloor += onResultGetAllFloorSwitcherPointsByFloor;
        HttpRequestSingleton.Instance.CallGet("/floors/" + floorId + "/floor-switcher-points", onGetAllFloorSwitcherPointsByFloor);
    }
    private void onResultGetAllFloorSwitcherPointsByFloor(byte[] result)
    {
        string floorSwitcherPointsJson = System.Text.Encoding.UTF8.GetString(result);

        floorswitcherpoints = FloorSwitcherPointListFromJson(floorSwitcherPointsJson).FloorSwitcherPoints;
        onGetAllFloorSwitcherPointsByFloorResult(this);

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


}

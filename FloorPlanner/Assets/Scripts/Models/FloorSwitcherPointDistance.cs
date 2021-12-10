using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HttpRequestSingleton;

public interface IFloorSwitcherPointDistance
{
    public int Id { get; set; }
    public FloorSwitcherPoint FloorSwitcherPoint1 { get; set; }
    public FloorSwitcherPoint FloorSwitcherPoint2 { get; set; }
    public float Distance { get; set; }


    public IFloorSwitcherPointDistance FloorSwitcherPointDistanceFromJson(string json);
    public string FloorSwitcherPointDistanceToJson();

}

[System.Serializable]
public class FloorSwitcherPointDistance : IFloorSwitcherPointDistance
{
    public int id;
    public FloorSwitcherPoint floorSwitcherPoint1;
    public FloorSwitcherPoint floorSwitcherPoint2;
    public float distance;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public FloorSwitcherPoint FloorSwitcherPoint1
    {
        get { return floorSwitcherPoint1; }
        set { floorSwitcherPoint1 = value; }
    }
    public FloorSwitcherPoint FloorSwitcherPoint2
    {
        get { return floorSwitcherPoint2; }
        set { floorSwitcherPoint2 = value; }
    }
    public float Distance
    {
        get { return distance; }
        set { distance = value; }
    }
    public IFloorSwitcherPointDistance FloorSwitcherPointDistanceFromJson(string json)
    {
        return JsonUtility.FromJson<FloorSwitcherPointDistance>(json);
    }

    public string FloorSwitcherPointDistanceToJson()
    {
        return JsonUtility.ToJson(this);
    }

}


public interface IFloorSwitcherPointDistanceList
{
    public delegate void Result();

    event Result onAddFloorSwitcherPointDistancesToFloorSwitcherPointResult;

    public List<FloorSwitcherPointDistance> FloorSwitcherPointDistances { get; set; }

    public IFloorSwitcherPointDistanceList FloorSwitcherPointDistanceListFromJson(string json);
    public string FloorSwitcherPointDistanceListToJson();

    public void AddFloorSwitcherPointDistancesToFloorSwitcherPoint(FloorSwitcherPointDistanceList floorSwitcherPointList, int floorSwitcherPointId);



}

public class FloorSwitcherPointDistanceList : IFloorSwitcherPointDistanceList
{
    private event Result onAddFloorSwitcherPointDistancesToFloorSwitcherPoint;

    public event IFloorSwitcherPointDistanceList.Result onAddFloorSwitcherPointDistancesToFloorSwitcherPointResult;

    public List<FloorSwitcherPointDistance> floorswitcherpointdistances;

    public List<FloorSwitcherPointDistance> FloorSwitcherPointDistances
    {
        get { return floorswitcherpointdistances; }
        set { floorswitcherpointdistances = value; }
    }


    public IFloorSwitcherPointDistanceList FloorSwitcherPointDistanceListFromJson(string json)
    {
        return JsonUtility.FromJson<FloorSwitcherPointDistanceList>(json);
    }

    public string FloorSwitcherPointDistanceListToJson()
    {
        return JsonUtility.ToJson(this);

    }
    public void AddFloorSwitcherPointDistancesToFloorSwitcherPoint(FloorSwitcherPointDistanceList floorSwitcherPointList, int floorSwitcherPointId)
    {
        onAddFloorSwitcherPointDistancesToFloorSwitcherPoint += onResultAddFloorSwitcherPointDistancesToFloorSwitcherPoint;
        string json = floorSwitcherPointList.FloorSwitcherPointDistanceListToJson();
        HttpRequestSingleton.Instance.CallPut("/floorswitcherpoints/" + floorSwitcherPointId + "/floor-switcher-point-distances", json, onAddFloorSwitcherPointDistancesToFloorSwitcherPoint);
    }
    public void onResultAddFloorSwitcherPointDistancesToFloorSwitcherPoint(byte[] result)
    {
        int floorSwitcherPointId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onAddFloorSwitcherPointDistancesToFloorSwitcherPointResult();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HttpRequestSingleton;

public interface ITargetDistance
{
    public int Id { get; set; }
    public FloorSwitcherPoint FloorSwitcherPoint { get; set; }
    public Target Target { get; set; }
    public float Distance { get; set; }


    public ITargetDistance TargetDistanceFromJson(string json);
    public string TargetDistanceToJson();

}

[System.Serializable]
public class TargetDistance : ITargetDistance
{
    public int id;
    public FloorSwitcherPoint floorSwitcherPoint;
    public Target target;
    public float distance;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public FloorSwitcherPoint FloorSwitcherPoint
    {
        get { return floorSwitcherPoint; }
        set { floorSwitcherPoint = value; }
    }
    public Target Target
    {
        get { return target; }
        set { target = value; }
    }
    public float Distance
    {
        get { return distance; }
        set { distance = value; }
    }

    public ITargetDistance TargetDistanceFromJson(string json)
    {
        return JsonUtility.FromJson<TargetDistance>(json);
    }

    public string TargetDistanceToJson()
    {
        return JsonUtility.ToJson(this);
    }
}

public interface ITargetDistanceList
{
    public delegate void Result();

    event Result onAddFloorSwitcherPointDistancesToTargetResult;
    event Result onAddTargetDistancesToFloorSwitcherPointResult;

    public List<TargetDistance> TargetDistances { get; set; }

    public ITargetDistanceList TargetDistanceListFromJson(string json);
    public string TargetDistanceListToJson();


    public void AddFloorSwitcherPointDistancesToTarget(TargetDistanceList targetDistanceList, int targetId);
    public void AddTargetDistancesToFloorSwitcherPoint(TargetDistanceList targetDistanceList, int floorswitcherPointId);



}

public class TargetDistanceList : ITargetDistanceList
{
    private event Result onAddFloorSwitcherPointDistancesToTarget;
    private event Result onAddTargetDistancesToFloorSwitcherPoint;

    public List<TargetDistance> targetdistances;

    public List<TargetDistance> TargetDistances
    {
        get { return targetdistances; }
        set { targetdistances = value; }
    }

    public event ITargetDistanceList.Result onAddFloorSwitcherPointDistancesToTargetResult;
    public event ITargetDistanceList.Result onAddTargetDistancesToFloorSwitcherPointResult;

    public ITargetDistanceList TargetDistanceListFromJson(string json)
    {
        return JsonUtility.FromJson<TargetDistanceList>(json);
    }

    public string TargetDistanceListToJson()
    {
        return JsonUtility.ToJson(this);
    }


    public void AddFloorSwitcherPointDistancesToTarget(TargetDistanceList targetDistanceList, int targetId)
    {
        onAddFloorSwitcherPointDistancesToTarget += onResultAddFloorSwitcherPointDistancesToTarget;
        string json = targetDistanceList.TargetDistanceListToJson();
        HttpRequestSingleton.Instance.CallPut("/targets/" + targetId + "/floor-switcher-point-distances", json, onAddFloorSwitcherPointDistancesToTarget);
    }
    public void onResultAddFloorSwitcherPointDistancesToTarget(byte[] result)
    {
        int targetId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onAddFloorSwitcherPointDistancesToTargetResult();
    }

    public void AddTargetDistancesToFloorSwitcherPoint(TargetDistanceList targetDistanceList, int floorswitcherPointId)
    {
        onAddTargetDistancesToFloorSwitcherPoint += onResultAddTargetDistancesToFloorSwitcherPoint;
        string json = targetDistanceList.TargetDistanceListToJson();
        HttpRequestSingleton.Instance.CallPut("/floorswitcherpoints/" + floorswitcherPointId + "/target-distances", json, onAddTargetDistancesToFloorSwitcherPoint);
    }
    public void onResultAddTargetDistancesToFloorSwitcherPoint(byte[] result)
    {
        int targetId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onAddTargetDistancesToFloorSwitcherPointResult();
    }
}

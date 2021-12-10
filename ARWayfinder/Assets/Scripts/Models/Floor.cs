using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Floor
{
    public int id;
    public string name;
    public int number;
    public FloorPlan3D floorPlan3D;
    public FloorPlan2D floorPlan2D;
    public List<Target> targets;
    public static Floor FloorFromJson(string json)
    {
        return JsonUtility.FromJson<Floor>(json);
    }

    public string FloorToJson()
    {
        return JsonUtility.ToJson(this);
    }
}


public class FloorList
{

    public List<Floor> floors;
    public static FloorList FloorListFromJson(string json)
    {
        return JsonUtility.FromJson<FloorList>(json);
    }
}

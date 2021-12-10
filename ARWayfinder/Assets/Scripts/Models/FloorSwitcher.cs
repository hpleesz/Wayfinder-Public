using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorSwitcher
{
    public int id;
    public string name;
    public bool up;
    public bool down;

    public Place place;
    public List<FloorSwitcherPoint> floorSwitcherPoints;
    public static FloorSwitcher FloorSwitcherFromJson(string json)
    {
        return JsonUtility.FromJson<FloorSwitcher>(json);
    }

    public string FloorSwitcherToJson()
    {
        return JsonUtility.ToJson(this);
    }
}


public class FloorSwitcherList
{

    public List<FloorSwitcher> floorswitchers;
    public static FloorSwitcherList FloorSwitcherListFromJson(string json)
    {
        return JsonUtility.FromJson<FloorSwitcherList>(json);
    }
}

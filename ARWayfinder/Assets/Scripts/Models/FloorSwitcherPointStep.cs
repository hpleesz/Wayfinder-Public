using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorSwitcherPointStep
{
    public FloorSwitcherPoint floorSwitcherPoint;
    public double distance;

    public static FloorSwitcherPointStep FloorSwitcherPointStepFromJson(string json)
    {
        return JsonUtility.FromJson<FloorSwitcherPointStep>(json);
    }

    public string FloorSwitcherPointStepToJson()
    {
        return JsonUtility.ToJson(this);
    }
}


public class FloorSwitcherPointStepList
{

    public List<FloorSwitcherPointStep> floorSwitcherPointSteps;
    public static FloorSwitcherPointStepList FloorSwitcherPointStepListFromJson(string json)
    {
        return JsonUtility.FromJson<FloorSwitcherPointStepList>(json);
    }
    public string FloorSwitcherPointStepListToJson()
    {
        return JsonUtility.ToJson(this);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathStep
{
    public int stepId;
    public double distance;

    public static PathStep PathStepFromJson(string json)
    {
        return JsonUtility.FromJson<PathStep>(json);
    }

    public string PathStepToJson()
    {
        return JsonUtility.ToJson(this);
    }
}


public class PathStepList
{

    public List<PathStep> pathSteps;
    public static PathStepList PathStepListFromJson(string json)
    {
        return JsonUtility.FromJson<PathStepList>(json);
    }
    public string PathStepListToJson()
    {
        return JsonUtility.ToJson(this);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorPlan2D
{
    public int id;
    public string fileLocation;
    public string templateLocation;


    public static FloorPlan2D FloorPlan2DFromJson(string json)
    {
        return JsonUtility.FromJson<FloorPlan2D>(json);
    }

    public string FloorPlan2DToJson()
    {
        return JsonUtility.ToJson(this);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class FloorPlan3D
{
    public int id;
    public string fileLocation;

    public static FloorPlan3D FloorPlan3DFromJson(string json)
    {
        return JsonUtility.FromJson<FloorPlan3D>(json);
    }

    public string FloorPlan3DToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
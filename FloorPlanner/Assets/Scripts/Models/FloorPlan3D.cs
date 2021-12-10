using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HttpRequestSingleton;

public interface IFloorPlan3D
{
    public delegate void Result();

    event Result onAddFloorPlan3DToFloorResult;

    public int Id { get; set; }
    public string FileLocation { get; set; }

    public IFloorPlan3D FloorPlan3DFromJson(string json);
    public string FloorPlan3DToJson();

    public void AddFloorPlan3DToFloor(int floorId, string fileContent);

}


[System.Serializable]

public class FloorPlan3D : IFloorPlan3D
{
    private event Result onAddFloorPlan3DToFloor;

    public event IFloorPlan3D.Result onAddFloorPlan3DToFloorResult;

    public int id;
    public string fileLocation;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public string FileLocation
    {
        get { return fileLocation; }
        set { fileLocation = value; }
    }


    public IFloorPlan3D FloorPlan3DFromJson(string json)
    {
        return JsonUtility.FromJson<FloorPlan3D>(json);
    }

    public string FloorPlan3DToJson()
    {
        return JsonUtility.ToJson(this);
    }


    //ADD FLOOR PLAN 3D
    public void AddFloorPlan3DToFloor(int floorId, string fileContent)
    {
        onAddFloorPlan3DToFloor += onResultAddFloorPlan3DToFloor;
        HttpRequestSingleton.Instance.CallPost("/floors/" + floorId + "/3d-model", fileContent, onAddFloorPlan3DToFloor);
    }

    public void onResultAddFloorPlan3DToFloor(byte[] result)
    {
        onAddFloorPlan3DToFloorResult();
    }
}
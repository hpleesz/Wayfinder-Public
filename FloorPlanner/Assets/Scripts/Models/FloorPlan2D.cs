using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HttpRequestSingleton;

public interface IFloorPlan2D
{
    public delegate void Result();

    event Result onAddFloorPlan2DToFloorResult;
    event Result onAddTemplateToFloorResult;

    public int Id { get; set; }
    public string FileLocation { get; set; }
    public string TemplateLocation { get; set; }

    public IFloorPlan2D FloorPlan2DFromJson(string json);
    public string FloorPlan2DToJson();


    public void AddTemplateToFloor(int floorId, byte[] form);
    public void AddFloorPlan2DToFloor(int floorId, byte[] form);

}

[System.Serializable]
public class FloorPlan2D : IFloorPlan2D
{
    private event Result onAddTemplateToFloor;
    private event Result onAddFloorPlan2DToFloor;

    public event IFloorPlan2D.Result onAddFloorPlan2DToFloorResult;
    public event IFloorPlan2D.Result onAddTemplateToFloorResult;



    public int id;
    public string fileLocation;
    public string templateLocation;

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
    public string TemplateLocation
    {
        get { return templateLocation; }
        set { templateLocation = value; }
    }


    public IFloorPlan2D FloorPlan2DFromJson(string json)
    {
        return JsonUtility.FromJson<FloorPlan2D>(json);
    }

    public string FloorPlan2DToJson()
    {
        return JsonUtility.ToJson(this);
    }




    //ADD TEMPLATE IMAGE
    public void AddTemplateToFloor(int floorId, byte[] itemBytes)
    {
        onAddTemplateToFloor += onResultAddTemplateToFloor;
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", itemBytes);
        HttpRequestSingleton.Instance.CallPostForm("/floors/" + floorId + "/template", form, onAddTemplateToFloor);
    }

    public void onResultAddTemplateToFloor(byte[] result)
    {
        onAddTemplateToFloorResult();
    }


    //ADD FLOOR PLAN 2D
    public void AddFloorPlan2DToFloor(int floorId, byte[] itemBytes)
    {
        onAddFloorPlan2DToFloor += onResultAddFloorPlan2DToFloor;
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", itemBytes);
        HttpRequestSingleton.Instance.CallPostForm("/floors/" + floorId + "/2d-map", form, onAddFloorPlan2DToFloor);
    }

    public void onResultAddFloorPlan2DToFloor(byte[] result)
    {
        onAddFloorPlan2DToFloorResult();
    }


}


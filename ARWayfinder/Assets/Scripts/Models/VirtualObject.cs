using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HttpRequestSingleton;

public interface IVirtualObject
{
    public delegate void Result();
    public delegate void IdResult(int id);
    public delegate void VirtualObjectResult(IVirtualObject virtualObject);

    event IdResult onAddVirtualObjectResult;
    event IdResult onEditVirtualObjectResult;
    event Result onAddImageToVirtualObjectResult;
    event Result onAddVideoToVirtualObjectResult;
    event IdResult onAddObjToVirtualObjectResult;
    event Result onAddTextureToVirtualObjectResult;


    public int Id { get; set; }
    public string FileLocation { get; set; }
    public string TextureLocation { get; set; }
    public float XCoordinate { get; set; }
    public float YCoordinate { get; set; }
    public float ZCoordinate { get; set; }
    public float XRotation { get; set; }
    public float YRotation { get; set; }
    public float ZRotation { get; set; }
    public float XScale { get; set; }
    public float YScale { get; set; }
    public float ZScale { get; set; }
    public Target Target { get; set; }
    public VirtualObjectType Type { get; set; }


    public IVirtualObject VirtualObjectFromJson(string json);
    public string VirtualObjectToJson();


}
[System.Serializable]
public class VirtualObject : IVirtualObject
{
    private event Result onAddVirtualObjectToTarget;
    private event Result onEditVirtualObject;
    private event Result onAddImageToVirtualObject;
    private event Result onAddVideoToVirtualObject;
    private event Result onAddObjToVirtualObject;
    private event Result onAddTextureToVirtualObject;


    public event IVirtualObject.IdResult onAddVirtualObjectResult;
    public event IVirtualObject.IdResult onEditVirtualObjectResult;
    public event IVirtualObject.Result onAddImageToVirtualObjectResult;
    public event IVirtualObject.Result onAddVideoToVirtualObjectResult;
    public event IVirtualObject.IdResult onAddObjToVirtualObjectResult;
    public event IVirtualObject.Result onAddTextureToVirtualObjectResult;

    public int id;
    public string fileLocation;
    public string textureLocation;
    public float xCoordinate;
    public float yCoordinate;
    public float zCoordinate;
    public float xRotation;
    public float yRotation;
    public float zRotation;
    public float xScale;
    public float yScale;
    public float zScale;

    public Target target;
    public VirtualObjectType type;

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
    public string TextureLocation
    {
        get { return textureLocation; }
        set { textureLocation = value; }
    }
    public float XCoordinate
    {
        get { return xCoordinate; }
        set { xCoordinate = value; }
    }
    public float YCoordinate
    {
        get { return yCoordinate; }
        set { yCoordinate = value; }
    }
    public float ZCoordinate
    {
        get { return zCoordinate; }
        set { zCoordinate = value; }
    }
    public float XRotation
    {
        get { return xRotation; }
        set { xRotation = value; }
    }
    public float YRotation
    {
        get { return yRotation; }
        set { yRotation = value; }
    }
    public float ZRotation
    {
        get { return zRotation; }
        set { zRotation = value; }
    }
    public float XScale
    {
        get { return xScale; }
        set { xScale = value; }
    }
    public float YScale
    {
        get { return yScale; }
        set { yScale = value; }
    }
    public float ZScale
    {
        get { return zScale; }
        set { zScale = value; }
    }
    public Target Target
    {
        get { return target; }
        set { target = value; }
    }
    public VirtualObjectType Type
    {
        get { return type; }
        set { type = value; }
    }

    public IVirtualObject VirtualObjectFromJson(string json)
    {
        return JsonUtility.FromJson<VirtualObject>(json);
    }
    public string VirtualObjectToJson()
    {
        return JsonUtility.ToJson(this);
    }


}


public interface IVirtualObjectList
{
    public delegate void VirtualObjectsResult(IVirtualObjectList virtualObjectList);

    event VirtualObjectsResult onGetAllVirtualObjectsByTargetResult;

    public List<VirtualObject> VirtualObjects { get; set; }

    public IVirtualObjectList VirtualObjectListFromJson(string json);


    public void GetAllVirtualObjectsByTarget(int targetId);
}


public class VirtualObjectList : IVirtualObjectList
{
    private event Result onGetAllVirtualObjectsByTarget;

    public event IVirtualObjectList.VirtualObjectsResult onGetAllVirtualObjectsByTargetResult;


    public List<VirtualObject> virtualObjects;

    public List<VirtualObject> VirtualObjects
    {
        get { return virtualObjects; }
        set { virtualObjects = value; }
    }


    public IVirtualObjectList VirtualObjectListFromJson(string json)
    {
        return JsonUtility.FromJson<VirtualObjectList>(json);
    }

    public void GetAllVirtualObjectsByTarget(int targetId)
    {
        onGetAllVirtualObjectsByTarget += onResultGetAllVirtualObjectsByTarget;
        HttpRequestSingleton.Instance.CallGet("/targets/" + targetId + "/virtual-objects", onGetAllVirtualObjectsByTarget);
    }
    private void onResultGetAllVirtualObjectsByTarget(byte[] result)
    {
        string virtualObjectsJson = System.Text.Encoding.UTF8.GetString(result);
        virtualObjects = VirtualObjectListFromJson(virtualObjectsJson).VirtualObjects;
        onGetAllVirtualObjectsByTargetResult(this);
    }

}

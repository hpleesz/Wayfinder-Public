using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HttpRequestSingleton;

public interface ITarget
{
    public delegate void IdResult(int id);
    public delegate void TargetResult(ITarget target);

    event TargetResult onGetTargetResult;
    event TargetResult onGetQrCodeResult;


    public int Id { get; set; }
    public string Name { get; set; }
    public string QrCode { get; set; }
    public float XCoordinate { get; set; }
    public float YCoordinate { get; set; }
    public float ZCoordinate { get; set; }
    public float XRotation { get; set; }
    public float YRotation { get; set; }
    public float ZRotation { get; set; }
    public Floor Floor { get; set; }
    public Category Category { get; set; }


    public ITarget TargetFromJson(string json);
    public string TargetToJson();

    public void GetTarget(int targetId);
    public void GetQrCode(int placeId, string term);

}


[System.Serializable]
public class Target : ITarget
{
    private event Result onGetTarget;
    private event Result onGetQrCode;

    public event ITarget.TargetResult onGetTargetResult;
    public event ITarget.TargetResult onGetQrCodeResult;


    public int id;
    public string name;
    public float xCoordinate;
    public float yCoordinate;
    public float zCoordinate;
    public float xRotation;
    public float yRotation;
    public float zRotation;

    public Floor floor;
    public string qrCode;

    public Category category;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string QrCode
    {
        get { return qrCode; }
        set { qrCode = value; }
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
    public Floor Floor
    {
        get { return floor; }
        set { floor = value; }
    }
    public Category Category
    {
        get { return category; }
        set { category = value; }
    }


    //GET TARGET
    public void GetTarget(int targetId)
    {
        onGetTarget += onResultGetTarget;
        HttpRequestSingleton.Instance.CallGet("/targets/" + targetId, onGetTarget);
    }

    private void onResultGetTarget(byte[] result)
    {
        string targetJson = System.Text.Encoding.UTF8.GetString(result);
        ITarget target = new Target();
        target = target.TargetFromJson(targetJson);
        onGetTargetResult(target);
    }

    //GET QR CODE
    public void GetQrCode(int placeId, string term)
    {
        onGetQrCode += onResultGetQrCode;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/qr-code?term=" + term, onGetQrCode);
    }

    private void onResultGetQrCode(byte[] result)
    {
        string targetJson = System.Text.Encoding.UTF8.GetString(result);
        ITarget target = new Target();
        target = target.TargetFromJson(targetJson);
        onGetQrCodeResult(target);
    }

    public string TargetToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public ITarget TargetFromJson(string json)
    {
        return JsonUtility.FromJson<Target>(json);
    }
}


public interface ITargetList
{
    public delegate void TargetsResult(ITargetList targetModelList);
    public delegate void IdResult(int id);
    public delegate void Result();

    event TargetsResult onGetAllTargetsByPlaceResult;
    event TargetsResult onGetAllTargetsByFloorResult;

    event TargetsResult onSearchTargetsByPlaceResult;
    event TargetsResult onSearchTargetsByFloorAndCategoryResult;

    event TargetsResult onGetQRCodesByPlaceResult;

    public List<Target> Targets { get; set; }

    public ITargetList TargetListFromJson(string json);


    public void GetAllTargetsByPlace(int placeId);
    public void GetAllTargetsByFloor(int floorId);
    public void SearchTargetsByPlace(int placeId, string term);
    public void SearchTargetsByFloorAndCategory(int floorId, string term, int category);

    public void GetQRCodesByPlace(int placeId);

    //
    public ITarget GetTargetById(int targetId);



}

public class TargetList : ITargetList
{
    private event Result onGetAllTargetsByFloor;
    private event Result onGetAllTargetsByPlace;
    private event Result onSearchTargetsByPlace;
    private event Result onSearchTargetsByFloorAndCategory;
    private event Result onGetQRCodesByPlace;

    public event ITargetList.TargetsResult onGetAllTargetsByPlaceResult;
    public event ITargetList.TargetsResult onGetAllTargetsByFloorResult;
    public event ITargetList.TargetsResult onSearchTargetsByPlaceResult;
    public event ITargetList.TargetsResult onSearchTargetsByFloorAndCategoryResult;

    public event ITargetList.TargetsResult onGetQRCodesByPlaceResult;


    public List<Target> targets;
    public List<Target> Targets
    {
        get { return targets; }
        set { targets = value; }
    }


    public ITargetList TargetListFromJson(string json)
    {
        return JsonUtility.FromJson<TargetList>(json);
    }


    //GET TARGETS BY FLOOR
    public void GetAllTargetsByFloor(int floorId)
    {
        onGetAllTargetsByFloor += onResultGetAllTargetsByFloor;
        HttpRequestSingleton.Instance.CallGet("/floors/" + floorId + "/targets", onGetAllTargetsByFloor);
    }

    private void onResultGetAllTargetsByFloor(byte[] result)
    {
        string targetsJson = System.Text.Encoding.UTF8.GetString(result);
        targets = TargetListFromJson(targetsJson).Targets;
        onGetAllTargetsByFloorResult(this);
    }

    //GET TARGETS BY PLACE
    public void GetAllTargetsByPlace(int placeId)
    {
        onGetAllTargetsByPlace += onResultGetAllTargetsByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/targets", onGetAllTargetsByPlace);
    }

    private void onResultGetAllTargetsByPlace(byte[] result)
    {
        string targetsJson = System.Text.Encoding.UTF8.GetString(result);
        targets = TargetListFromJson(targetsJson).Targets;
        onGetAllTargetsByPlaceResult(this);
    }

    //SEARCH 
    public void SearchTargetsByFloorAndCategory(int floorId, string term, int category)
    {
        onSearchTargetsByFloorAndCategory += onResultSearchTargetsByFloorAndCategory;
        HttpRequestSingleton.Instance.CallGet("/floors/" + floorId + "/floors/search?term=" + term + "&category=" + category, onSearchTargetsByFloorAndCategory);
    }
    private void onResultSearchTargetsByFloorAndCategory(byte[] result)
    {
        string targetsJson = System.Text.Encoding.UTF8.GetString(result);
        targets = TargetListFromJson(targetsJson).Targets;
        onSearchTargetsByFloorAndCategoryResult(this);
    }

    public void SearchTargetsByPlace(int placeId, string term)
    {
        onSearchTargetsByPlace += onResultSearchTargetsByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/targets/search?term=" + term, onSearchTargetsByPlace);
    }
    private void onResultSearchTargetsByPlace(byte[] result)
    {
        string targetsJson = System.Text.Encoding.UTF8.GetString(result);
        targets = TargetListFromJson(targetsJson).Targets;
        onSearchTargetsByPlaceResult(this);
    }


    //
    public ITarget GetTargetById(int targetId)
    {
        ITarget selectedTarget = targets.Where(i => i.Id == targetId).FirstOrDefault();
        return selectedTarget;
    }

    public void GetQRCodesByPlace(int placeId)
    {
        onGetQRCodesByPlace += onResultGetQRCodesByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/qr-codes", onGetQRCodesByPlace);
    }

    private void onResultGetQRCodesByPlace(byte[] result)
    {
        string targetsJson = System.Text.Encoding.UTF8.GetString(result);
        targets = TargetListFromJson(targetsJson).Targets;
        onGetQRCodesByPlaceResult(this);
    }
}

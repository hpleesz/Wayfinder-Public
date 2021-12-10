using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HttpRequestSingleton;

public interface IFloor
{
    public delegate void IdResult(int id);
    public delegate void FloorResult(IFloor floor);

    event IdResult onAddFloorResult;
    event FloorResult onGetFloorResult;
    event IdResult onEditFloorResult;


    public int Id { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public FloorPlan3D FloorPlan3D { get; set; }
    public FloorPlan2D FloorPlan2D { get; set; }
    public List<Target> Targets { get; set; }

    public IFloor FloorFromJson(string json);
    public string FloorToJson();


    public void AddFloor(int placeId, IFloor floor);

    public void EditFloor(int floorId, IFloor floor);
    public void GetFloor(int floorId);
}

[System.Serializable]
public class Floor : IFloor
{
    private event Result onAddFloor;
    private event Result onGetFloor;
    private event Result onEditFloor;


    public event IFloor.IdResult onAddFloorResult;
    public event IFloor.FloorResult onGetFloorResult;
    public event IFloor.IdResult onEditFloorResult;

    public int id;
    public string name;
    public int number;
    public FloorPlan3D floorPlan3D;
    public FloorPlan2D floorPlan2D;
    public List<Target> targets;
    public int Id
    {
        get { return id;  }
        set { id = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Number
    {
        get { return number; }
        set { number = value; }
    }
    public FloorPlan3D FloorPlan3D
    {
        get { return floorPlan3D; }
        set { floorPlan3D = value; }
    }
    public FloorPlan2D FloorPlan2D
    {
        get { return floorPlan2D; }
        set { floorPlan2D = value; }
    }
    public List<Target> Targets
    {
        get { return targets; }
        set { targets = value; }
    }

    public IFloor FloorFromJson(string json)
    {
        return JsonUtility.FromJson<Floor>(json);
    }

    public string FloorToJson()
    {
        return JsonUtility.ToJson(this);
    }


    //ADD FLOOR
    public void AddFloor(int placeId, IFloor floor)
    {
        onAddFloor += onResultAddFloor;
        string json = floor.FloorToJson();
        HttpRequestSingleton.Instance.CallPost("/places/" + placeId + "/floors", json, onAddFloor);
    }
    public void onResultAddFloor(byte[] result)
    {
        int floorId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onAddFloorResult(floorId);
    }


    //GET FLOOR
    public void GetFloor(int floorId)
    {
        onGetFloor += onResultGetFloor;
        HttpRequestSingleton.Instance.CallGet("/floors/" + floorId, onGetFloor);
    }

    private void onResultGetFloor(byte[] result)
    {
        string floorJson = System.Text.Encoding.UTF8.GetString(result);
        IFloor floor = new Floor();
        floor = floor.FloorFromJson(floorJson);
        onGetFloorResult(floor);
    }

    //EDIT FLOOR
    public void EditFloor(int floorId, IFloor floor)
    {
        onEditFloor += onResultEditFloor;
        string json = floor.FloorToJson();
        HttpRequestSingleton.Instance.CallPut("/floors/" + floorId, json, onEditFloor);
    }

    public void onResultEditFloor(byte[] result)
    {
        int floorId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onEditFloorResult(floorId);
    }

}


public interface IFloorList
{
    public delegate void FloorsResult(IFloorList floorModelList);
    public delegate void IdResult(int id);
    public delegate void Result();

    event FloorsResult onGetAllFloorsByPlaceResult;
    event FloorsResult onSearchFloorsByPlaceResult;


    public List<Floor> Floors { get; set; }

    public IFloorList FloorListFromJson(string json);

    public void GetAllFloorsByPlace(int placeId);
    public void SearchFloorsByPlace(int placeId, string term);


    //
    public IFloor GetFloorById(int floorId);



}

public class FloorList : IFloorList
{
    private event Result onGetAllFloorsByPlace;
    private event Result onSearchFloorsByPlace;



    public event IFloorList.FloorsResult onGetAllFloorsByPlaceResult;
    public event IFloorList.FloorsResult onSearchFloorsByPlaceResult;


    public List<Floor> floors;

    public List<Floor> Floors
    {
        get { return floors; }
        set { floors = value; }
    }

    public IFloorList FloorListFromJson(string json)
    {
        return JsonUtility.FromJson<FloorList>(json);
    }






    //GET ALL FLOORS
    public void GetAllFloorsByPlace(int placeId)
    {
        onGetAllFloorsByPlace += onResultGetAllFloorsByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/floors", onGetAllFloorsByPlace);
    }

    private void onResultGetAllFloorsByPlace(byte[] result)
    {
        string floorsJson = System.Text.Encoding.UTF8.GetString(result);
        floors = FloorListFromJson(floorsJson).Floors;
        if(floors == null)
        {
        }
        onGetAllFloorsByPlaceResult(this);
    }


    //SEARCH FLOORS
    public void SearchFloorsByPlace(int placeId, string term)
    {
        onSearchFloorsByPlace += onResultSearchFloorsByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/floors/search?term=" + term, onSearchFloorsByPlace);
    }
    private void onResultSearchFloorsByPlace(byte[] result)
    {
        string floorsJson = System.Text.Encoding.UTF8.GetString(result);
        floors = FloorListFromJson(floorsJson).Floors;
        onSearchFloorsByPlaceResult(this);
    }



    // NEM DB
    public IFloor GetFloorById(int floorId)
    {
        IFloor selectedFloor = floors.Where(i => i.Id == floorId).FirstOrDefault();
        return selectedFloor;
    }
}

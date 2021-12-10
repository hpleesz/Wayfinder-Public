using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HttpRequestSingleton;

public interface IFloorSwitcher
{
    public delegate void FloorSwitcherResult(IFloorSwitcher floorSwitcher);
    public delegate void IdResult(int id);
    public delegate void Result();

    event FloorSwitcherResult onGetFloorSwitcherResult;
    event IdResult onEditFloorSwitcherResult;
    event IdResult onAddFloorSwitcherToPlaceResult;
    event IdResult onAddFloorSwitcherPointsToFloorSwitcherResult;

    public int Id { get; set; }
    public string Name { get; set; }
    public bool Up { get; set; }
    public bool Down { get; set; }
    public Place Place { get; set; }
    public List<FloorSwitcherPoint> FloorSwitcherPoints { get; set; }

    public IFloorSwitcher FloorSwitcherFromJson(string json);
    public string FloorSwitcherToJson();


    public void EditFloorSwitcher(int floorSwitcherId, IFloorSwitcher floorSwitcher);
    public void AddFloorSwitcherToPlace(int placeId, IFloorSwitcher floorSwitcher);
    public void GetFloorSwitcher(int floorSwitcherId);
    public void AddFloorSwitcherPointsToFloorSwitcher(int floorSwitcherId, IFloorSwitcherPointList floorSwitcherPointList);
}
[System.Serializable]
public class FloorSwitcher : IFloorSwitcher
{
    private event Result onAddFloorSwitcherToPlace;
    private event Result onGetFloorSwitcher;
    private event Result onEditFloorSwitcher;
    private event Result onAddFloorSwitcherPointsToFloorSwitcher;

    public event IFloorSwitcher.FloorSwitcherResult onGetFloorSwitcherResult;
    public event IFloorSwitcher.IdResult onEditFloorSwitcherResult;
    public event IFloorSwitcher.IdResult onAddFloorSwitcherToPlaceResult;
    public event IFloorSwitcher.IdResult onAddFloorSwitcherPointsToFloorSwitcherResult;

    public int id;
    public string name;
    public bool up;
    public bool down;

    public Place place;
    public List<FloorSwitcherPoint> floorSwitcherPoints;


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
    public bool Up
    {
        get { return up; }
        set { up = value; }
    }
    public bool Down
    {
        get { return down; }
        set { down = value; }
    }
    public Place Place
    {
        get { return place; }
        set { place = value; }
    }
    public List<FloorSwitcherPoint> FloorSwitcherPoints
    {
        get { return floorSwitcherPoints; }
        set { floorSwitcherPoints = value; }
    }
    
    //ADD FLOOR SWITCHER
    public void AddFloorSwitcherToPlace(int placeId, IFloorSwitcher floorSwitcher)
    {
        onAddFloorSwitcherToPlace += onResultAddFloorSwitcherToPlace;
        string json = floorSwitcher.FloorSwitcherToJson();
        HttpRequestSingleton.Instance.CallPost("/places/" + placeId + "/floor-switchers", json, onAddFloorSwitcherToPlace);
    }
    public void onResultAddFloorSwitcherToPlace(byte[] result)
    {
        int floorSwitcherId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onAddFloorSwitcherToPlaceResult(floorSwitcherId);
    }

    //EDIT FLOOR SWITCHER
    public void EditFloorSwitcher(int floorSwitcherId, IFloorSwitcher floorSwitcher)
    {
        onEditFloorSwitcher += onResultEditFloorSwitcher;
        string json = floorSwitcher.FloorSwitcherToJson();
        HttpRequestSingleton.Instance.CallPut("/floorswitchers/" + floorSwitcherId, json, onEditFloorSwitcher);
    }

    public void onResultEditFloorSwitcher(byte[] result)
    {
        int floorSwitcherId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onEditFloorSwitcherResult(floorSwitcherId);
    }

    //GET FLOOR SWITCHER
    public void GetFloorSwitcher(int floorSwitcherId)
    {
        onGetFloorSwitcher += onResultGetFloorSwitcher;
        HttpRequestSingleton.Instance.CallGet("/floorswitchers/" + floorSwitcherId, onGetFloorSwitcher);
    }
    private void onResultGetFloorSwitcher(byte[] result)
    {
        string floorSwitcherJson = System.Text.Encoding.UTF8.GetString(result);
        IFloorSwitcher floorSwitcher = new FloorSwitcher();
        floorSwitcher = floorSwitcher.FloorSwitcherFromJson(floorSwitcherJson);
        onGetFloorSwitcherResult(floorSwitcher);
    }

    public void AddFloorSwitcherPointsToFloorSwitcher(int floorSwitcherId, IFloorSwitcherPointList floorSwitcherPointList)
    {
        onAddFloorSwitcherPointsToFloorSwitcher += onResultAddFloorSwitcherPointsToFloorSwitcher;
        string json = floorSwitcherPointList.FloorSwitcherPointListToJson();
        HttpRequestSingleton.Instance.CallPut("/floorswitchers/" + floorSwitcherId + "/floor-switcher-points", json, onAddFloorSwitcherPointsToFloorSwitcher);
    }
    public void onResultAddFloorSwitcherPointsToFloorSwitcher(byte[] result)
    {
        int floorSwitcherId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onAddFloorSwitcherPointsToFloorSwitcherResult(floorSwitcherId);
    }


    public IFloorSwitcher FloorSwitcherFromJson(string json)
    {
        return JsonUtility.FromJson<FloorSwitcher>(json);
    }

    public string FloorSwitcherToJson()
    {
        return JsonUtility.ToJson(this);
    }


}

public interface IFloorSwitcherList
{
    public delegate void FloorSwitchersResult(IFloorSwitcherList floorSwitchersModelList);
    public delegate void IdResult(int id);
    public delegate void Result();

    event FloorSwitchersResult onGetAllFloorSwitchersByPlaceResult;
    event FloorSwitchersResult onSearchFloorSwitchersByPlaceResult;

    public List<FloorSwitcher> FloorSwitchers { get; set; }

    public IFloorSwitcherList FloorSwitcherListFromJson(string json);

    public string FloorSwitcherListToJson();


    public void GetAllFloorSwitchersByPlace(int placeId);
    public void SearchFloorSwitchersByPlace(int placeId, string term);

    //
    public IFloorSwitcher GetFloorSwitcherById(int floorSwitcherId);

}

public class FloorSwitcherList : IFloorSwitcherList
{
    private event Result onGetAllFloorSwitchersByPlace;
    private event Result onSearchFloorSwitchersByPlace;

    public event IFloorSwitcherList.FloorSwitchersResult onGetAllFloorSwitchersByPlaceResult;
    public event IFloorSwitcherList.FloorSwitchersResult onSearchFloorSwitchersByPlaceResult;

    public List<FloorSwitcher> floorswitchers;

    public List<FloorSwitcher> FloorSwitchers
    {
        get { return floorswitchers; }
        set { floorswitchers = value; }
    }


    public IFloorSwitcherList FloorSwitcherListFromJson(string json)
    {
        return JsonUtility.FromJson<FloorSwitcherList>(json);
    }

    public string FloorSwitcherListToJson()
    {
        return JsonUtility.ToJson(this);
    }


    //GET ALL FLOOR SWITCHERS
    public void GetAllFloorSwitchersByPlace(int placeId)
    {
        onGetAllFloorSwitchersByPlace += onResultGetAllFloorSwitchersByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/floor-switchers", onGetAllFloorSwitchersByPlace);
    }
    private void onResultGetAllFloorSwitchersByPlace(byte[] result)
    {
        string floorSwitchersJson = System.Text.Encoding.UTF8.GetString(result);
        floorswitchers = FloorSwitcherListFromJson(floorSwitchersJson).FloorSwitchers;
        onGetAllFloorSwitchersByPlaceResult(this);

    }

    //SEARCH FLOOR SWITCHERS
    public void SearchFloorSwitchersByPlace(int placeId, string term)
    {
        onSearchFloorSwitchersByPlace += onResultSearchFloorSwitchersByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/floor-switchers/search?term=" + term, onSearchFloorSwitchersByPlace);
    }
    private void onResultSearchFloorSwitchersByPlace(byte[] result)
    {
        string floorSwitchersJson = System.Text.Encoding.UTF8.GetString(result);
        floorswitchers = FloorSwitcherListFromJson(floorSwitchersJson).FloorSwitchers;
        onSearchFloorSwitchersByPlaceResult(this);
    }


    //
    public IFloorSwitcher GetFloorSwitcherById(int floorSwitcherId)
    {
        IFloorSwitcher selectedFloorSwitcher = floorswitchers.Where(i => i.Id == floorSwitcherId).FirstOrDefault();
        return selectedFloorSwitcher;
    }




}

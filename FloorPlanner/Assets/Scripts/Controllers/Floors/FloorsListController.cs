using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorsListController
{ }

public class FloorsListController : IFloorsListController
{
    private readonly IFloorList floorModelList;
    private readonly IFloorsListView floorsListView;

    public FloorsListController(IFloorList floorModelList, IFloorsListView floorsListView)
    {
        this.floorModelList = floorModelList;
        this.floorsListView = floorsListView;

        this.floorModelList.onGetAllFloorsByPlaceResult += HandleGetAllFloorsByPlaceResult;
        this.floorModelList.onSearchFloorsByPlaceResult += HandleSearchFloorsByPlaceResult;

        this.floorsListView.onGetAllFloorsByPlaceCall += HandleGetAllFloorsByPlaceCall;
        this.floorsListView.onSearchFloorsByPlaceCall += HandleSearchFloorsByPlaceCall;
        this.floorsListView.onClickFloorCall += HandleClickFloorCall;

    }

    private void HandleGetAllFloorsByPlaceCall(int placeId)
    {
        floorModelList.GetAllFloorsByPlace(placeId);
    }
    private void HandleGetAllFloorsByPlaceResult(IFloorList floorModelList)
    {
        floorsListView.CreateFloorsList(floorModelList);
    }

    private void HandleSearchFloorsByPlaceCall(int placeId, string term)
    {
        floorModelList.SearchFloorsByPlace(placeId, term);
    }

    private void HandleSearchFloorsByPlaceResult(IFloorList floorModelList)
    {
        floorsListView.CreateFloorsList(floorModelList);
    }

    private void HandleClickFloorCall(int floorId)
    {
        IFloor floorModel = floorModelList.GetFloorById(floorId);
        floorsListView.ClickFloor(floorModel);
    }

}

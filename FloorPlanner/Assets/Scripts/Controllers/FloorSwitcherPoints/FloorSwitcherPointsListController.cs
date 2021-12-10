using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorSwitcherPointsListController
{ }

public class FloorSwitcherPointsListController : IFloorSwitcherPointsListController
{
    private readonly IFloorSwitcherPointList floorSwitcherPointModelList;
    private readonly IFloorSwitcherPointsListView floorSwitcherPointsListView;

    public FloorSwitcherPointsListController(IFloorSwitcherPointList floorSwitcherPointModelList, IFloorSwitcherPointsListView floorSwitcherPointsListView)
    {
        this.floorSwitcherPointModelList = floorSwitcherPointModelList;
        this.floorSwitcherPointsListView = floorSwitcherPointsListView;

        this.floorSwitcherPointModelList.onGetAllFloorSwitcherPointsByPlaceResult += HandleGetAllFloorSwitcherPointsByPlaceResult;
        this.floorSwitcherPointModelList.onSearchFloorSwitcherPointsByPlaceResult += HandleSearchFloorSwitcherPointsByPlaceResult;
        this.floorSwitcherPointModelList.onGetFreeFloorSwitcherPointsByPlaceResult += HandleGetFreeFloorSwitcherPointsByPlaceResult;

        this.floorSwitcherPointModelList.onSearchFloorSwitcherPointsByFloorResult += HandleSearchFloorSwitcherPointsByFloorResult;
        this.floorSwitcherPointModelList.onGetAllFloorSwitcherPointsByFloorSwitcherResult += HandleGetAllFloorSwitcherPointsByFloorSwitcherResult;


        this.floorSwitcherPointsListView.onGetAllFloorSwitcherPointsByPlaceCall += HandleGetAllFloorSwitcherPointsByPlaceCall;
        this.floorSwitcherPointsListView.onSearchFloorSwitcherPointsByPlaceCall += HandleSearchFloorSwitcherPointsByPlaceCall;
        this.floorSwitcherPointsListView.onGetFreeFloorSwitcherPointsByPlaceCall += HandleGetFreeFloorSwitcherPointsByPlaceCall;

        this.floorSwitcherPointsListView.onClickFloorSwitcherPointCall += HandleClickFloorSwitcherPointCall;
        this.floorSwitcherPointsListView.onSearchFloorSwitcherPointsByFloorCall += HandleSearchFloorSwitcherPointsByFloorCall;
        this.floorSwitcherPointsListView.onGetAllFloorSwitcherPointsByFloorSwitcherCall += HandleGetAllFloorSwitcherPointsByFloorSwitcherCall;

    }

    private void HandleGetAllFloorSwitcherPointsByPlaceCall(int placeId)
    {
        floorSwitcherPointModelList.GetAllFloorSwitcherPointsByPlace(placeId);
    }
    private void HandleGetAllFloorSwitcherPointsByPlaceResult(IFloorSwitcherPointList floorSwitcherPointModelList)
    {

        floorSwitcherPointsListView.CreateFloorSwitcherPointsList(floorSwitcherPointModelList);
    }

    private void HandleSearchFloorSwitcherPointsByPlaceCall(int placeId, string term)
    {
        floorSwitcherPointModelList.SearchFloorSwitcherPointsByPlace(placeId, term);
    }

    private void HandleSearchFloorSwitcherPointsByFloorCall(int floorId, string term)
    {
        floorSwitcherPointModelList.SearchFloorSwitcherPointsByFloor(floorId, term);
    }

    private void HandleSearchFloorSwitcherPointsByPlaceResult(IFloorSwitcherPointList floorSwitcherPointModelList)
    {
        floorSwitcherPointsListView.CreateFloorSwitcherPointsList(floorSwitcherPointModelList);
    }

    private void HandleSearchFloorSwitcherPointsByFloorResult(IFloorSwitcherPointList floorSwitcherPointModelList)
    {
        floorSwitcherPointsListView.CreateFloorSwitcherPointsList(floorSwitcherPointModelList);

    }




    private void HandleGetFreeFloorSwitcherPointsByPlaceCall(int placeId)
    {
        floorSwitcherPointModelList.GetFreeFloorSwitcherPointsByPlace(placeId);
    }
    private void HandleGetFreeFloorSwitcherPointsByPlaceResult(IFloorSwitcherPointList floorSwitcherPointModelList)
    {
        floorSwitcherPointsListView.CreateFreeFloorSwitcherPointsList(floorSwitcherPointModelList);
    }


    private void HandleClickFloorSwitcherPointCall(int floorSwitcherPointId)
    {
        IFloorSwitcherPoint floorSwitcherPointModel = floorSwitcherPointModelList.GetFloorSwitcherPointById(floorSwitcherPointId);
        floorSwitcherPointsListView.ClickFloorSwitcherPoint(floorSwitcherPointModel);
    }


    private void HandleGetAllFloorSwitcherPointsByFloorSwitcherCall(int floorSwitcherId)
    {
        floorSwitcherPointModelList.GetAllFloorSwitcherPointsByFloorSwitcher(floorSwitcherId);
    }
    private void HandleGetAllFloorSwitcherPointsByFloorSwitcherResult(IFloorSwitcherPointList floorSwitcherPointModelList)
    {
        floorSwitcherPointsListView.CreateFloorSwitcherPointsList(floorSwitcherPointModelList);
    }

}

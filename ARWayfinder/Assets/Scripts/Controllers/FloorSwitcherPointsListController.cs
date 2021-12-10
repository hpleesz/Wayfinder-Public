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

        this.floorSwitcherPointModelList.onGetAllFloorSwitcherPointsByFloorResult += HandleGetAllFloorSwitcherPointsByFloorResult;

        this.floorSwitcherPointsListView.onGetAllFloorSwitcherPointsByFloorCall += HandleGetAllFloorSwitcherPointsByFloorCall;

    }

    private void HandleGetAllFloorSwitcherPointsByFloorCall(int floorId)
    {
        floorSwitcherPointModelList.GetAllFloorSwitcherPointsByFloor(floorId);
    }
    private void HandleGetAllFloorSwitcherPointsByFloorResult(IFloorSwitcherPointList floorSwitcherPointModelList)
    {
        floorSwitcherPointsListView.CreateFloorSwitcherPointsList(floorSwitcherPointModelList);
    }


}

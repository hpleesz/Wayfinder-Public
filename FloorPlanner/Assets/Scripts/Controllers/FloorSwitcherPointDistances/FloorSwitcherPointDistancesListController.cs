using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorSwitcherPointDistancesListController
{ }

public class FloorSwitcherPointDistancesListController : IFloorSwitcherPointDistancesListController
{
    private readonly IFloorSwitcherPointDistanceList floorSwitcherPointDistanceModelList;
    private readonly IFloorSwitcherPointDistancesListView floorSwitcherPointDistancesListView;

    public FloorSwitcherPointDistancesListController(IFloorSwitcherPointDistanceList floorSwitcherPointDistanceModelList, IFloorSwitcherPointDistancesListView floorSwitcherPointDistancesListView)
    {
        this.floorSwitcherPointDistanceModelList = floorSwitcherPointDistanceModelList;
        this.floorSwitcherPointDistancesListView = floorSwitcherPointDistancesListView;

        this.floorSwitcherPointDistanceModelList.onAddFloorSwitcherPointDistancesToFloorSwitcherPointResult += HandleAddFloorSwitcherPointDistancesToFloorSwitcherPointResult;


        this.floorSwitcherPointDistancesListView.onAddFloorSwitcherPointDistancesToFloorSwitcherPointCall += HandleAddFloorSwitcherPointDistancesToFloorSwitcherPointCall;

    }

    //ADD FLOOR SWITCHER POINTS TO TARGET
    private void HandleAddFloorSwitcherPointDistancesToFloorSwitcherPointCall(FloorSwitcherPointDistanceList floorSwitcherPointDistanceList, int floorSwitcherPointId)
    {
        floorSwitcherPointDistanceModelList.AddFloorSwitcherPointDistancesToFloorSwitcherPoint(floorSwitcherPointDistanceList, floorSwitcherPointId);
    }
    private void HandleAddFloorSwitcherPointDistancesToFloorSwitcherPointResult()
    {
        floorSwitcherPointDistancesListView.FloorSwitcherPointsAdded();
    }

}

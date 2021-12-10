using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetDistancesListController
{ }

public class TargetDistancesListController : ITargetDistancesListController
{
    private readonly ITargetDistanceList targetDistanceModelList;
    private readonly ITargetDistancesListView targetDistancesListView;

    public TargetDistancesListController(ITargetDistanceList targetDistanceModelList, ITargetDistancesListView targetDistancesListView)
    {
        this.targetDistanceModelList = targetDistanceModelList;
        this.targetDistancesListView = targetDistancesListView;

        this.targetDistanceModelList.onAddFloorSwitcherPointDistancesToTargetResult += HandleAddFloorSwitcherPointDistancesToTargetResult;
        this.targetDistanceModelList.onAddTargetDistancesToFloorSwitcherPointResult += HandleAddTargetDistancesToFloorSwitcherPointResult;


        this.targetDistancesListView.onAddFloorSwitcherPointDistancesToTargetCall += HandleAddFloorSwitcherPointDistancesToTargetCall;
        this.targetDistancesListView.onAddTargetDistancesToFloorSwitcherPointCall += HandleAddTargetDistancesToFloorSwitcherPointCall;

    }

    //ADD FLOOR SWITCHER POINTS TO TARGET
    private void HandleAddFloorSwitcherPointDistancesToTargetCall(TargetDistanceList targetDistanceList, int targetId)
    {
        targetDistanceModelList.AddFloorSwitcherPointDistancesToTarget(targetDistanceList, targetId);
    }
    private void HandleAddFloorSwitcherPointDistancesToTargetResult()
    {
        targetDistancesListView.FloorSwitcherPointsAdded();
    }

    //ADD FLOOR SWITCHER POINTS TO TARGET
    private void HandleAddTargetDistancesToFloorSwitcherPointCall(TargetDistanceList targetDistanceList, int floorSwitcherPointId)
    {
        targetDistanceModelList.AddTargetDistancesToFloorSwitcherPoint(targetDistanceList, floorSwitcherPointId);
    }
    private void HandleAddTargetDistancesToFloorSwitcherPointResult()
    {
        targetDistancesListView.FloorSwitcherPointsAdded();
    }
}

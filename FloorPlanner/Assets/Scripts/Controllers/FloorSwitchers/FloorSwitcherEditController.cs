using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IFloorSwitcherEditController
{ }


public class FloorSwitcherEditController : IFloorSwitcherEditController
{
    private readonly IFloorSwitcher floorSwitcherModel;
    private readonly IFloorSwitcherEditView floorSwitcherView;

    public FloorSwitcherEditController(IFloorSwitcher floorSwitcherModel, IFloorSwitcherEditView floorSwitcherView)
    {
        this.floorSwitcherModel = floorSwitcherModel;
        this.floorSwitcherView = floorSwitcherView;

        this.floorSwitcherModel.onAddFloorSwitcherToPlaceResult += HandleAddFloorSwitcherToPlaceResult;
        this.floorSwitcherModel.onEditFloorSwitcherResult += HandleEditFloorSwitcherResult;
        this.floorSwitcherModel.onAddFloorSwitcherPointsToFloorSwitcherResult += HandleAddFloorSwitcherPointsToFloorSWitcherResult;

        this.floorSwitcherView.onAddFloorSwitcherToPlaceCall += HandleAddFloorSwitcherToPlaceCall;
        this.floorSwitcherView.onEditFloorSwitcherCall += HandleEditFloorSwitcherCall;
        this.floorSwitcherView.onAddFloorSwitcherPointsToFloorSwitcherCall += HandleAddFloorSwitcherPointsToFloorSwitcherCall;

    }


    private void HandleAddFloorSwitcherToPlaceCall(int placeId, IFloorSwitcher floorSwitcher)
    {
        floorSwitcherModel.AddFloorSwitcherToPlace(placeId, floorSwitcher);
    }

    private void HandleAddFloorSwitcherToPlaceResult(int floorSwitcherId)
    {
        floorSwitcherView.NewFloorSwitcherCreated(floorSwitcherId);
    }


    private void HandleEditFloorSwitcherCall(int floorSwitcherId, IFloorSwitcher floorSwitcher)
    {
        floorSwitcherModel.EditFloorSwitcher(floorSwitcherId, floorSwitcher);
    }

    private void HandleEditFloorSwitcherResult(int floorSwitcherId)
    {
        floorSwitcherView.FloorSwitcherUpdated(floorSwitcherId);
    }


    private void HandleAddFloorSwitcherPointsToFloorSwitcherCall(int floorSwitcherId, IFloorSwitcherPointList floorSwitcherPoints)
    {
        floorSwitcherModel.AddFloorSwitcherPointsToFloorSwitcher(floorSwitcherId, floorSwitcherPoints);
    }

    private void HandleAddFloorSwitcherPointsToFloorSWitcherResult(int floorSwitcherId)
    {
        floorSwitcherView.FloorSwitcherPointsAddedToFloorSwitcher(floorSwitcherId);
    }
}

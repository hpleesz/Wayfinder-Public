using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IFloorSwitcherPointEditController
{ }


public class FloorSwitcherPointEditController : IFloorSwitcherPointEditController
{
    private readonly IFloorSwitcherPoint floorSwitcherPointModel;
    private readonly IFloorSwitcherPointEditView floorSwitcherPointView;

    public FloorSwitcherPointEditController(IFloorSwitcherPoint floorSwitcherPointModel, IFloorSwitcherPointEditView floorSwitcherPointView)
    {
        this.floorSwitcherPointModel = floorSwitcherPointModel;
        this.floorSwitcherPointView = floorSwitcherPointView;

        this.floorSwitcherPointModel.onAddFloorSwitcherPointToFloorResult += HandleAddFloorSwitcherPointToFloorResult;
        this.floorSwitcherPointModel.onEditFloorSwitcherPointResult += HandleEditFloorSwitcherPointResult;

        this.floorSwitcherPointView.onAddFloorSwitcherPointToFloorCall += HandleAddFloorSwitcherPointToFloorCall;
        this.floorSwitcherPointView.onEditFloorSwitcherPointCall += HandleEditFloorSwitcherPointCall;

    }


    private void HandleAddFloorSwitcherPointToFloorCall(int floorId, IFloorSwitcherPoint floorSwitcherPoint)
    {
        floorSwitcherPointModel.AddFloorSwitcherPointToFloor(floorId, floorSwitcherPoint);
    }

    private void HandleAddFloorSwitcherPointToFloorResult(int floorSwitcherPointId)
    {
        floorSwitcherPointView.NewFloorSwitcherPointCreated(floorSwitcherPointId);
    }


    private void HandleEditFloorSwitcherPointCall(int floorSwitcherPointId, IFloorSwitcherPoint floorSwitcherPoint)
    {
        floorSwitcherPointModel.EditFloorSwitcherPoint(floorSwitcherPointId, floorSwitcherPoint);
    }

    private void HandleEditFloorSwitcherPointResult(int floorSwitcherPointId)
    {
        floorSwitcherPointView.FloorSwitcherPointUpdated(floorSwitcherPointId);
    }
}

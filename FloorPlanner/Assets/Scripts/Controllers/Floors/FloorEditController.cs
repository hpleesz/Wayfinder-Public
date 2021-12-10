using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IFloorEditController
{ }


public class FloorEditController : IFloorEditController
{
    private readonly IFloor floorModel;
    private readonly IFloorEditView floorView;

    public FloorEditController(IFloor floorModel, IFloorEditView floorView)
    {
        this.floorModel = floorModel;
        this.floorView = floorView;

        this.floorModel.onAddFloorResult += HandleAddFloorToPlaceResult;
        this.floorModel.onEditFloorResult += HandleEditFloorResult;

        this.floorView.onAddFloorToPlaceCall += HandleAddFloorToPlaceCall;
        this.floorView.onEditFloorCall += HandleEditFloorCall;

    }


    private void HandleAddFloorToPlaceCall(int placeId, IFloor floor)
    {
        floorModel.AddFloor(placeId, floor);
    }

    private void HandleAddFloorToPlaceResult(int floorId)
    {
        floorView.NewFloorCreated(floorId);
    }


    private void HandleEditFloorCall(int floorId, IFloor floor)
    {
        floorModel.EditFloor(floorId, floor);
    }

    private void HandleEditFloorResult(int floorId)
    {
        floorView.FloorUpdated(floorId);
    }
}

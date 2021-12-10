using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorInfoController
{ }

public class FloorInfoController : IFloorInfoController
{
    private readonly IFloor floorModel;
    private readonly IFloorInfoView floorInfoView;

    public FloorInfoController(IFloor floorModel, IFloorInfoView floorInfoView)
    {
        this.floorModel = floorModel;
        this.floorInfoView = floorInfoView;

        this.floorModel.onGetFloorResult += HandleGetFloorResult;

        this.floorInfoView.onGetFloorCall += HandleGetFloorCall;

    }


    //GET PLACE
    private void HandleGetFloorCall(int floorId)
    {
        floorModel.GetFloor(floorId);
    }
    private void HandleGetFloorResult(IFloor floor)
    {
        floorInfoView.ShowFloor(floor);
    }

}

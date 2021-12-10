using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorSwitcherPointInfoController
{ }

public class FloorSwitcherPointInfoController : IFloorSwitcherPointInfoController
{
    private readonly IFloorSwitcherPoint floorSwitcherPointModel;
    private readonly IFloorSwitcherPointInfoView floorSwitcherPointInfoView;

    public FloorSwitcherPointInfoController(IFloorSwitcherPoint floorSwitcherPointModel, IFloorSwitcherPointInfoView floorSwitcherPointInfoView)
    {
        this.floorSwitcherPointModel = floorSwitcherPointModel;
        this.floorSwitcherPointInfoView = floorSwitcherPointInfoView;

        this.floorSwitcherPointModel.onGetFloorSwitcherPointResult += HandleGetFloorSwitcherPointResult;

        this.floorSwitcherPointInfoView.onGetFloorSwitcherPointCall += HandleGetFloorSwitcherPointCall;

    }


    //GET PLACE
    private void HandleGetFloorSwitcherPointCall(int floorSwitcherPointId)
    {
        Debug.Log(1);
        floorSwitcherPointModel.GetFloorSwitcherPoint(floorSwitcherPointId);
    }
    private void HandleGetFloorSwitcherPointResult(IFloorSwitcherPoint floorSwitcherPoint)
    {
        floorSwitcherPointInfoView.ShowFloorSwitcherPoint(floorSwitcherPoint);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorSwitcherInfoController
{ }

public class FloorSwitcherInfoController : IFloorSwitcherInfoController
{
    private readonly IFloorSwitcher floorSwitcherModel;
    private readonly IFloorSwitcherInfoView floorSwitcherInfoView;

    public FloorSwitcherInfoController(IFloorSwitcher floorSwitcherModel, IFloorSwitcherInfoView floorSwitcherInfoView)
    {
        this.floorSwitcherModel = floorSwitcherModel;
        this.floorSwitcherInfoView = floorSwitcherInfoView;

        this.floorSwitcherModel.onGetFloorSwitcherResult += HandleGetFloorSwitcherResult;

        this.floorSwitcherInfoView.onGetFloorSwitcherCall += HandleGetFloorSwitcherCall;

    }


    //GET PLACE
    private void HandleGetFloorSwitcherCall(int floorSwitcherId)
    {
        floorSwitcherModel.GetFloorSwitcher(floorSwitcherId);
    }
    private void HandleGetFloorSwitcherResult(IFloorSwitcher floorSwitcher)
    {
        floorSwitcherInfoView.ShowFloorSwitcher(floorSwitcher);
    }

}

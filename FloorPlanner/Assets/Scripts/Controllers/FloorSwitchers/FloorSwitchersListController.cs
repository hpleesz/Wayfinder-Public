using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorSwitchersListController
{ }

public class FloorSwitchersListController : IFloorSwitchersListController
{
    private readonly IFloorSwitcherList floorSwitcherModelList;
    private readonly IFloorSwitchersListView floorSwitchersListView;

    public FloorSwitchersListController(IFloorSwitcherList floorSwitcherModelList, IFloorSwitchersListView floorSwitchersListView)
    {
        this.floorSwitcherModelList = floorSwitcherModelList;
        this.floorSwitchersListView = floorSwitchersListView;

        this.floorSwitcherModelList.onGetAllFloorSwitchersByPlaceResult += HandleGetAllFloorSwitchersByPlaceResult;
        this.floorSwitcherModelList.onSearchFloorSwitchersByPlaceResult += HandleSearchFloorSwitchersByPlaceResult;

        this.floorSwitchersListView.onGetAllFloorSwitchersByPlaceCall += HandleGetAllFloorSwitchersByPlaceCall;
        this.floorSwitchersListView.onSearchFloorSwitchersByPlaceCall += HandleSearchFloorSwitchersByPlaceCall;

        this.floorSwitchersListView.onClickFloorSwitcherCall += HandleClickFloorSwitcherCall;
    }

    private void HandleGetAllFloorSwitchersByPlaceCall(int placeId)
    {
        floorSwitcherModelList.GetAllFloorSwitchersByPlace(placeId);
    }
    private void HandleGetAllFloorSwitchersByPlaceResult(IFloorSwitcherList floorSwitcherModelList)
    {
        floorSwitchersListView.CreateFloorSwitchersList(floorSwitcherModelList);
    }

    private void HandleSearchFloorSwitchersByPlaceCall(int placeId, string term)
    {
        floorSwitcherModelList.SearchFloorSwitchersByPlace(placeId, term);
    }

    private void HandleSearchFloorSwitchersByPlaceResult(IFloorSwitcherList floorSwitcherModelList)
    {
        floorSwitchersListView.CreateFloorSwitchersList(floorSwitcherModelList);
    }

    private void HandleClickFloorSwitcherCall(int floorSwitcherId)
    {
        IFloorSwitcher floorSwitcherModel = floorSwitcherModelList.GetFloorSwitcherById(floorSwitcherId);
        floorSwitchersListView.ClickFloorSwitcher(floorSwitcherModel);
    }


}

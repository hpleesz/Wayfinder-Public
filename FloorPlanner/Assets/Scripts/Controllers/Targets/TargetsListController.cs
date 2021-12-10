using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetsListController
{ }

public class TargetsListController : ITargetsListController
{
    private readonly ITargetList targetModelList;
    private readonly ITargetsListView targetsListView;

    public TargetsListController(ITargetList targetModelList, ITargetsListView targetsListView)
    {
        this.targetModelList = targetModelList;
        this.targetsListView = targetsListView;

        this.targetModelList.onGetAllTargetsByPlaceResult += HandleGetAllTargetsByPlaceResult;
        this.targetModelList.onSearchTargetsByPlaceResult += HandleSearchTargetsByPlaceResult;
        this.targetModelList.onGetAllTargetsByFloorResult += HandleGetAllTargetsByFloorResult;
        this.targetModelList.onSearchTargetsByFloorAndCategoryResult += HandleSearchTargetsByFloorAndCategoryResult;

        this.targetsListView.onGetAllTargetsByPlaceCall += HandleGetAllTargetsByPlaceCall;
        this.targetsListView.onSearchTargetsByPlaceCall += HandleSearchTargetsByPlaceCall;

        this.targetsListView.onGetAllTargetsByFloorCall += HandleGetAllTargetsByFloorCall;
        this.targetsListView.onSearchTargetsByFloorAndCategoryCall += HandleSearchTargetsByFloorAndCategoryCall;

        this.targetsListView.onClickTargetCall += HandleClickTargetCall;

    }

    //GET ALL TARGETS BY PLACE
    private void HandleGetAllTargetsByPlaceCall(int placeId)
    {
        targetModelList.GetAllTargetsByPlace(placeId);
    }
    private void HandleGetAllTargetsByPlaceResult(ITargetList targetModelList)
    {
        targetsListView.CreateTargetsList(targetModelList);
    }
    //GET ALL TARGETS BY FLOOR
    private void HandleGetAllTargetsByFloorCall(int floorId)
    {
        targetModelList.GetAllTargetsByFloor(floorId);
    }
    private void HandleGetAllTargetsByFloorResult(ITargetList targetModelList)
    {
        targetsListView.CreateTargetsList(targetModelList);

    }
    //SEARCH TARGETS BY PLACE
    private void HandleSearchTargetsByPlaceCall(int placeId, string term)
    {
        targetModelList.SearchTargetsByPlace(placeId, term);
    }

    private void HandleSearchTargetsByPlaceResult(ITargetList targetModelList)
    {
        targetsListView.CreateTargetsList(targetModelList);
    }
    //SEARCH TARGETS BY FLOOR
    private void HandleSearchTargetsByFloorAndCategoryCall(int floorId, string term, int category)
    {
        targetModelList.SearchTargetsByFloorAndCategory(floorId, term, category);
    }

    private void HandleSearchTargetsByFloorAndCategoryResult(ITargetList targetModelList)
    {
        targetsListView.CreateTargetsList(targetModelList);
    }

    private void HandleClickTargetCall(int targetId)
    {
        ITarget targetModel = targetModelList.GetTargetById(targetId);
        targetsListView.ClickTarget(targetModel);
    }

}

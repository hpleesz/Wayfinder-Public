using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetPathStepsListController
{ }

public class TargetPathStepsListController : ITargetPathStepsListController
{
    private readonly ITargetPathStepsList targetPathStepsListModel;
    private readonly ITargetPathStepsListView targetPathStepsListView;

    public TargetPathStepsListController(ITargetPathStepsList targetPathStepsListModel, ITargetPathStepsListView targetPathStepsListView)
    {
        this.targetPathStepsListModel = targetPathStepsListModel;
        this.targetPathStepsListView = targetPathStepsListView;

        this.targetPathStepsListModel.onGetAllTargetDistancesByPlaceResult += HandleGetAllTargetDistancesByPlaceResult;
        this.targetPathStepsListModel.onSearchTargetDistancesByPlaceAndCategoryResult += HandleSearchTargetDistancesByPlaceAndCategoryResult;

        this.targetPathStepsListView.onGetAllTargetDistancesByPlaceCall += HandleGetAllTargetDistancesByPlaceCall;
        this.targetPathStepsListView.onSearchTargetDistancesByPlaceAndCategoryCall += HandleSearchTargetDistancesByPlaceAndCategoryCall;

    }

    private void HandleGetAllTargetDistancesByPlaceCall(int placeId, PathStepList pathStepList)
    {
        targetPathStepsListModel.GetAllTargetDistancesByPlace(placeId, pathStepList);
    }
    private void HandleGetAllTargetDistancesByPlaceResult(ITargetPathStepsList targetPathStepsList)
    {
        targetPathStepsListView.CreateTargetDistancesList(targetPathStepsList);
    }

    private void HandleSearchTargetDistancesByPlaceAndCategoryCall(int placeId, string term, string category, PathStepList pathStepList)
    {
        targetPathStepsListModel.SearchTargetDistancesByPlaceAndCategory(placeId, term, category, pathStepList);
    }

    private void HandleSearchTargetDistancesByPlaceAndCategoryResult(ITargetPathStepsList targetPathStepsList)
    {
        targetPathStepsListView.CreateTargetDistancesList(targetPathStepsList);
    }


}

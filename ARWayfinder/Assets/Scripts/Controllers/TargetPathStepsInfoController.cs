using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetPathStepsInfoController
{ }

public class TargetPathStepsInfoController : ITargetPathStepsInfoController
{
    private readonly ITargetPathSteps targetPathStepsModel;
    private readonly ITargetPathStepsInfoView targetPathStepsInfoView;

    public TargetPathStepsInfoController(ITargetPathSteps targetPathStepsModel, ITargetPathStepsInfoView targetPathStepsInfoView)
    {
        this.targetPathStepsModel = targetPathStepsModel;
        this.targetPathStepsInfoView = targetPathStepsInfoView;

        this.targetPathStepsModel.onGetTargetPathResult += HandleGetTargetPathResult;

        this.targetPathStepsInfoView.onGetTargetPathStepsCall += HandleGetTargetPathStepsCall;
    }


    //GET PLACE
    private void HandleGetTargetPathStepsCall(int targetId, PathStepList pathStepList)
    {
        targetPathStepsModel.GetTargetPath(targetId, pathStepList);
    }
    private void HandleGetTargetPathResult(ITargetPathSteps targetPathSteps)
    {
        targetPathStepsInfoView.TargetPathResult(targetPathSteps);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetInfoController
{ }

public class TargetInfoController : ITargetInfoController
{
    private readonly ITarget targetModel;
    private readonly ITargetInfoView targetInfoView;

    public TargetInfoController(ITarget targetModel, ITargetInfoView targetInfoView)
    {
        this.targetModel = targetModel;
        this.targetInfoView = targetInfoView;

        this.targetModel.onGetTargetResult += HandleGetTargetResult;

        this.targetInfoView.onGetTargetCall += HandleGetTargetCall;

    }


    //GET PLACE
    private void HandleGetTargetCall(int targetId)
    {
        targetModel.GetTarget(targetId);
    }
    private void HandleGetTargetResult(ITarget target)
    {
        Debug.Log(target);
        targetInfoView.ShowTarget(target);
    }

}

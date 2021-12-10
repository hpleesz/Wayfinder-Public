using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ITargetEditController
{ }


public class TargetEditController : ITargetEditController
{
    private readonly ITarget targetModel;
    private readonly ITargetEditView targetView;

    public TargetEditController(ITarget targetModel, ITargetEditView targetView)
    {
        this.targetModel = targetModel;
        this.targetView = targetView;

        this.targetModel.onAddTargetResult += HandleAddTargetToFloorResult;
        this.targetModel.onEditTargetResult += HandleEditTargetResult;

        this.targetView.onAddTargetToFloorCall += HandleAddTargetToFloorCall;
        this.targetView.onEditTargetCall += HandleEditTargetCall;

    }


    private void HandleAddTargetToFloorCall(int floorId, ITarget target)
    {
        targetModel.AddTargetToFloor(floorId, target);
    }

    private void HandleAddTargetToFloorResult(int targetId)
    {
        targetView.NewTargetCreated(targetId);
    }


    private void HandleEditTargetCall(int targetId, ITarget target)
    {
        targetModel.EditTarget(targetId, target);
    }

    private void HandleEditTargetResult(int targetId)
    {
        targetView.TargetUpdated(targetId);
    }
}

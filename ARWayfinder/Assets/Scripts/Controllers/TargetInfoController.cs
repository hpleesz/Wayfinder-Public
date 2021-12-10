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

        this.targetModel.onGetQrCodeResult += HandleGetQrCodeResult;


        this.targetInfoView.onGetQrCodeCall += HandleGetQrCodeCall;
    }


    //GET PLACE
    private void HandleGetQrCodeCall(int placeId, string term)
    {
        targetModel.GetQrCode(placeId, term);
    }
    private void HandleGetQrCodeResult(ITarget target)
    {
        Debug.Log(target);
        targetInfoView.QrCodeResult(target);
    }

}

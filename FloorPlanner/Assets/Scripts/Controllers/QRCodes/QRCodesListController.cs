using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQRCodesListController
{ }

public class QRCodesListController : IQRCodesListController
{
    private readonly ITargetList targetModelList;
    private readonly IQRCodesListView qrCodesListView;

    public QRCodesListController(ITargetList targetModelList, IQRCodesListView qrCodesListView)
    {
        this.targetModelList = targetModelList;
        this.qrCodesListView = qrCodesListView;

        this.targetModelList.onGetQRCodesByPlaceResult += HandleGetQRCodesByPlaceResult;

        this.qrCodesListView.onGetQRCodesByPlaceCall += HandleGetQRCodesByPlaceCall;


    }

    //GET ALL TARGETS BY PLACE
    private void HandleGetQRCodesByPlaceCall(int placeId)
    {
        targetModelList.GetQRCodesByPlace(placeId);
    }
    private void HandleGetQRCodesByPlaceResult(ITargetList targetModelList)
    {
        qrCodesListView.CreateQRCodesList(targetModelList);
    }
    

}

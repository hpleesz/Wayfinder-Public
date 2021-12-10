using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceInfoController
{ }

public class PlaceInfoController : IPlaceInfoController
{
    private readonly IPlace placeModel;
    private readonly IPlaceInfoView placeInfoView;

    public PlaceInfoController(IPlace placeModel, IPlaceInfoView placeInfoView)
    {
        this.placeModel = placeModel;
        this.placeInfoView = placeInfoView;

        this.placeModel.onGetPlaceResult += HandleGetPlaceResult;

        this.placeInfoView.onGetPlaceCall += HandleGetPlaceCall;

    }


    //GET PLACE
    private void HandleGetPlaceCall(int placeId)
    {
        placeModel.GetPlace(placeId);
    }
    private void HandleGetPlaceResult(IPlace place)
    {
        placeInfoView.ShowPlace(place);
    }

}

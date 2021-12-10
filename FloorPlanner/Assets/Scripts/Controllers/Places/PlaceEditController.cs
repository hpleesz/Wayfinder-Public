using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceEditController
{ }

public class PlaceEditController: IPlaceEditController
{
    private readonly IPlace placeModel;
    private readonly IPlaceEditView placeEditView;

    public PlaceEditController(IPlace placeModel, IPlaceEditView placeEditView)
    {
        this.placeModel = placeModel;
        this.placeEditView = placeEditView;

        this.placeModel.onEditPlaceResult += HandleEditPlaceResult;
        this.placeModel.onAddPlaceResult += HandleAddPlaceResult;
        this.placeModel.onAddPlaceImageResult += HandleAddPlaceImageResult;

        this.placeEditView.onEditPlaceCall += HandleEditPlaceCall;
        this.placeEditView.onAddPlaceCall += HandleAddPlaceCall;
        this.placeEditView.onAddPlaceImageCall += HandleAddPlaceImageCall;

    }


    //EDIT PLACE
    private void HandleEditPlaceCall(int placeId, IPlace place)
    {
        placeModel.EditPlace(placeId, place);
    }
    private void HandleEditPlaceResult(int placeId)
    {
        placeEditView.PlaceUpdated(placeId);
    }


    //ADD PLACE
    private void HandleAddPlaceCall(IPlace place)
    {
        placeModel.AddPlace(place);
    }

    private void HandleAddPlaceResult(int placeId)
    {
        placeEditView.NewPlaceCreated(placeId);
    }


    //ADD PLACE IMAGE
    private void HandleAddPlaceImageCall(int placeId, byte[] form)
    {
        placeModel.AddPlaceImage(placeId, form);
    }

    private void HandleAddPlaceImageResult()
    {
        placeEditView.PlaceImageAdded();
    }
}

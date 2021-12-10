using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlacesListController
{ }

public class PlacesListController: IPlacesListController
{
    private readonly IPlaceList placeModelList;
    private readonly IPlacesListView placesListView;

    public PlacesListController(IPlaceList placeModelList, IPlacesListView placesListView)
    {
        this.placeModelList = placeModelList;
        this.placesListView = placesListView;

        this.placeModelList.onGetAllPlacesResult += HandleGetAllPlacesResult;
        this.placeModelList.onGetAllPlacesResult += HandleSearchPlacesResult;

        this.placesListView.onGetAllPlacesCall += HandleGetAllPlacesCall;
        this.placesListView.onSearchPlacesCall += HandleSearchPlacesCall;
        this.placesListView.onClickPlaceCall += HandleClickPlaceCall;

    }

    private void HandleGetAllPlacesCall()
    {
        placeModelList.GetAllPlaces();
    }
    private void HandleGetAllPlacesResult(IPlaceList placeModelList)
    {
        placesListView.CreatePlacesList(placeModelList);
    }

    private void HandleSearchPlacesCall(string term)
    {
        placeModelList.SearchPlaces(term);
    }

    private void HandleSearchPlacesResult(IPlaceList placeModelList)
    {
        placesListView.CreatePlacesList(placeModelList);
    }

    private void HandleClickPlaceCall(int placeId)
    {
        IPlace placeModel = placeModelList.GetPlaceById(placeId);
        placesListView.ClickPlace(placeModel);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarkersListController
{ }

public class MarkersListController : IMarkersListController
{
    private readonly IMarkerList markerModelList;
    private readonly IMarkersListView markersListView;

    public MarkersListController(IMarkerList markerModelList, IMarkersListView markersListView)
    {
        this.markerModelList = markerModelList;
        this.markersListView = markersListView;

        this.markerModelList.onGetAllMarkersByPlaceResult += HandleGetAllMarkersByPlaceResult;
        this.markerModelList.onSearchMarkersByPlaceResult += HandleSearchMarkersByPlaceResult;
        this.markerModelList.onGetAllMarkersByFloorResult += HandleGetAllMarkersByFloorResult;
        this.markerModelList.onSearchMarkersByFloorResult += HandleSearchMarkersByFloorResult;

        this.markersListView.onGetAllMarkersByPlaceCall += HandleGetAllMarkersByPlaceCall;
        this.markersListView.onSearchMarkersByPlaceCall += HandleSearchMarkersByPlaceCall;
        this.markersListView.onGetAllMarkersByFloorCall += HandleGetAllMarkersByFloorCall;
        this.markersListView.onSearchMarkersByFloorCall += HandleSearchMarkersByFloorCall;

        this.markersListView.onClickMarkerCall += HandleClickMarkerCall;

    }

    //GET ALL TARGETS BY PLACE
    private void HandleGetAllMarkersByPlaceCall(int placeId)
    {
        markerModelList.GetAllMarkersByPlace(placeId);
    }
    private void HandleGetAllMarkersByPlaceResult(IMarkerList markerModelList)
    {
        markersListView.CreateMarkersList(markerModelList);
    }
    //GET ALL TARGETS BY FLOOR
    private void HandleGetAllMarkersByFloorCall(int floorId)
    {
        markerModelList.GetAllMarkersByFloor(floorId);
    }
    private void HandleGetAllMarkersByFloorResult(IMarkerList targetModelList)
    {
        markersListView.CreateMarkersList(markerModelList);

    }
    //SEARCH TARGETS BY PLACE
    private void HandleSearchMarkersByPlaceCall(int placeId, string term)
    {
        markerModelList.SearchMarkersByPlace(placeId, term);
    }

    private void HandleSearchMarkersByPlaceResult(IMarkerList markerModelList)
    {
        markersListView.CreateMarkersList(markerModelList);
    }
    //SEARCH TARGETS BY FLOOR
    private void HandleSearchMarkersByFloorCall(int floorId, string term)
    {
        markerModelList.SearchMarkersByFloor(floorId, term);
    }

    private void HandleSearchMarkersByFloorResult(IMarkerList markerModelList)
    {
        markersListView.CreateMarkersList(markerModelList);
    }

    private void HandleClickMarkerCall(int markerId)
    {
        IMarker markerModel = markerModelList.GetMarkerById(markerId);
        markersListView.ClickMarker(markerModel);
    }

}

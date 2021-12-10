using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarkerInfoController
{ }

public class MarkerInfoController : IMarkerInfoController
{
    private readonly IMarker markerModel;
    private readonly IMarkerInfoView markerInfoView;

    public MarkerInfoController(IMarker markerModel, IMarkerInfoView markerInfoView)
    {
        this.markerModel = markerModel;
        this.markerInfoView = markerInfoView;

        this.markerModel.onGetMarkerResult += HandleGetMarkerResult;

        this.markerInfoView.onGetMarkerCall += HandleGetMarkerCall;

    }


    //GET PLACE
    private void HandleGetMarkerCall(int markerId)
    {
        Debug.Log(1);
        markerModel.GetMarker(markerId);
    }
    private void HandleGetMarkerResult(IMarker marker)
    {
        markerInfoView.ShowMarker(marker);
    }

}

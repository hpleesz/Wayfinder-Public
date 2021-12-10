using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IMarkerEditController
{ }


public class MarkerEditController : IMarkerEditController
{
    private readonly IMarker markerModel;
    private readonly IMarkerEditView markerView;

    public MarkerEditController(IMarker markerModel, IMarkerEditView markerView)
    {
        this.markerModel = markerModel;
        this.markerView = markerView;

        this.markerModel.onAddMarkerResult += HandleAddMarkerToFloorResult;
        this.markerModel.onEditMarkerResult += HandleEditMarkerResult;

        this.markerView.onAddMarkerToFloorCall += HandleAddMarkerToFloorCall;
        this.markerView.onEditMarkerCall += HandleEditMarkerCall;

    }


    private void HandleAddMarkerToFloorCall(int floorId, IMarker marker)
    {
        markerModel.AddMarkerToFloor(floorId, marker);
    }

    private void HandleAddMarkerToFloorResult(int markerId)
    {
        markerView.NewMarkerCreated(markerId);
    }


    private void HandleEditMarkerCall(int markerId, IMarker marker)
    {
        markerModel.EditMarker(markerId, marker);
    }

    private void HandleEditMarkerResult(int markerId)
    {
        markerView.MarkerUpdated(markerId);
    }
}

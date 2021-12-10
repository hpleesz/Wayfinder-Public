using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorPlan3DEditController { }
public class FloorPlan3DEditController : IFloorPlan3DEditController
{
    private readonly IFloorPlan3D floorPlan3D;
    private readonly IFloorPlan3DEditView floorPlan3DEditView;

    public FloorPlan3DEditController(IFloorPlan3D floorPlan3D, IFloorPlan3DEditView floorPlan3DEditView)
    {
        this.floorPlan3D = floorPlan3D;
        this.floorPlan3DEditView = floorPlan3DEditView;

        this.floorPlan3D.onAddFloorPlan3DToFloorResult += HandleAddFloorPlan3DToFloorResult;

        this.floorPlan3DEditView.onAddFloorPlan3DToFloorCall += HandleAddFloorPlan3DToFloorCall;

    }


    //ADD FLOOR TEMPLATE
    private void HandleAddFloorPlan3DToFloorCall(int floorId, string fileContent)
    {
        floorPlan3D.AddFloorPlan3DToFloor(floorId, fileContent);
    }
    private void HandleAddFloorPlan3DToFloorResult()
    {
        floorPlan3DEditView.FloorPlan3DAdded();
    }
}

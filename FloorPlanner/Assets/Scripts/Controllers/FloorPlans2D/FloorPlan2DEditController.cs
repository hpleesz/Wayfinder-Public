using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorPlan2DEditController { }
public class FloorPlan2DEditController : IFloorPlan2DEditController
{
    private readonly IFloorPlan2D floorPlan2D;
    private readonly IFloorPlan2DEditView floorPlan2DEditView;

    public FloorPlan2DEditController(IFloorPlan2D floorPlan2D, IFloorPlan2DEditView floorPlan2DEditView)
    {
        this.floorPlan2D = floorPlan2D;
        this.floorPlan2DEditView = floorPlan2DEditView;

        this.floorPlan2D.onAddTemplateToFloorResult += HandleAddTemplateToFloorResult;
        this.floorPlan2D.onAddFloorPlan2DToFloorResult += HandleAddFloorPlan2DToFloorResult;

        this.floorPlan2DEditView.onAddFloorTemplate += HandleAddFloorTemplateCall;
        this.floorPlan2DEditView.onAdd2DImageToFloorCall += HandleAdd2DImageToFloorCall;

    }


    //ADD FLOOR TEMPLATE
    private void HandleAddFloorTemplateCall(int floorId, byte[] form)
    {
        floorPlan2D.AddTemplateToFloor(floorId, form);
    }
    private void HandleAddTemplateToFloorResult()
    {
        floorPlan2DEditView.FloorTemplateAdded();
    }

    //ADD FLOOR IMAGE
    private void HandleAdd2DImageToFloorCall(int floorId, byte[] form)
    {
        floorPlan2D.AddFloorPlan2DToFloor(floorId, form);
    }

    private void HandleAddFloorPlan2DToFloorResult()
    {
        floorPlan2DEditView.FloorPlan2DAdded();
    }
}

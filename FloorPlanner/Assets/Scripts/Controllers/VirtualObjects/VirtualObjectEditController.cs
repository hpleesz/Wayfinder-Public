using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVirtualObjectEditController { }

public class VirtualObjectEditController : IVirtualObjectEditController
{
    private readonly IVirtualObject virtualObjectModel;
    private readonly IVirtualObjectEditView virtualObjectEditView;

    public VirtualObjectEditController(IVirtualObject virtualObject, IVirtualObjectEditView virtualObjectEditView)
    {
        this.virtualObjectModel = virtualObject;
        this.virtualObjectEditView = virtualObjectEditView;

        this.virtualObjectModel.onAddVirtualObjectResult += HandleAddVirtualObjectResult;
        this.virtualObjectModel.onEditVirtualObjectResult += HandleEditVirtualObjectResult;
        this.virtualObjectModel.onAddImageToVirtualObjectResult += HandleAddImageToVirtualObjectResult;
        this.virtualObjectModel.onAddVideoToVirtualObjectResult += HandleAddVideoToVirtualObjectResult;
        this.virtualObjectModel.onAddObjToVirtualObjectResult += HandleAddObjToVirtualObjectResult;
        this.virtualObjectModel.onAddTextureToVirtualObjectResult += HandleAddTextureToVirtualObjectResult;


        this.virtualObjectEditView.onAddVirtualObjectToTargetCall += HandleAddVirtualObjectToTargetCall;
        this.virtualObjectEditView.onEditVirtualObjectCall += HandleEditVirtualObjectCall;
        this.virtualObjectEditView.onAddImageToVirtualObjectCall += HandleAddImageToVirtualObjectCall;
        this.virtualObjectEditView.onAddVideoToVirtualObjectCall += HandleAddVideoToVirtualObjectCall;
        this.virtualObjectEditView.onAddObjToVirtualObjectCall += HandleAddObjToVirtualObjectCall;
        this.virtualObjectEditView.onAddTextureToVirtualObjectCall += HandleAddTextureToVirtualObjectCall;

    }


    //ADD FLOOR TEMPLATE
    private void HandleAddVirtualObjectToTargetCall(int targetId, IVirtualObject virtualObject)
    {
        virtualObjectModel.AddVirtualObjectToTarget(targetId, virtualObject);
    }
    private void HandleAddVirtualObjectResult(int virtualObjectId)
    {
        virtualObjectEditView.VirtualObjectAdded(virtualObjectId);
    }

    //ADD FLOOR TEMPLATE
    private void HandleEditVirtualObjectCall(int virtualObjectId, IVirtualObject virtualObject)
    {
        virtualObjectModel.EditVirtualObject(virtualObjectId, virtualObject);
    }
    private void HandleEditVirtualObjectResult(int virtualObjectId)
    {
        virtualObjectEditView.VirtualObjectUpdated(virtualObjectId);
    }

    //ADD FLOOR TEMPLATE
    private void HandleAddImageToVirtualObjectCall(int virtualObjectId, byte[] image)
    {
        virtualObjectModel.AddImageToVirtualObject(virtualObjectId, image);
    }
    private void HandleAddImageToVirtualObjectResult()
    {
        virtualObjectEditView.ImageAddedToVirtualObject();
    }

    //ADD FLOOR TEMPLATE
    private void HandleAddVideoToVirtualObjectCall(int virtualObjectId, byte[] video)
    {
        virtualObjectModel.AddVideoToVirtualObject(virtualObjectId, video);
    }
    private void HandleAddVideoToVirtualObjectResult()
    {
        virtualObjectEditView.VideoAddedToVirtualObject();
    }

    //ADD FLOOR TEMPLATE
    private void HandleAddObjToVirtualObjectCall(int virtualObjectId, byte[] obj)
    {
        virtualObjectModel.AddObjToVirtualObject(virtualObjectId, obj);
    }
    private void HandleAddObjToVirtualObjectResult(int virtualObjectId)
    {
        virtualObjectEditView.ObjAddedToVirtualObject(virtualObjectId);
    }

    //ADD FLOOR TEMPLATE
    private void HandleAddTextureToVirtualObjectCall(int virtualObjectId, byte[] texture)
    {
        virtualObjectModel.AddTextureToVirtualObject(virtualObjectId, texture);
    }
    private void HandleAddTextureToVirtualObjectResult()
    {
        virtualObjectEditView.TextureAddedToVirtualObject();
    }
}

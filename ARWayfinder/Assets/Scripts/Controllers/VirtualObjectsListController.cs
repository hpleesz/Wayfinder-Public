using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVirtualObjectsListController
{ }

public class VirtualObjectsListController : IVirtualObjectsListController
{
    private readonly IVirtualObjectList virtualObjectListModel;
    private readonly IVirtualObjectsListView virtualObjectListView;

    public VirtualObjectsListController(IVirtualObjectList virtualObjectListModel, IVirtualObjectsListView virtualObjectListView)
    {
        this.virtualObjectListModel = virtualObjectListModel;
        this.virtualObjectListView = virtualObjectListView;

        this.virtualObjectListModel.onGetAllVirtualObjectsByTargetResult += HandleGetAllVirtualObjectsByTargetResult;

        this.virtualObjectListView.onGetAllVirtualObjectsByTargetCall += HandleGetAllVirtualObjectsByTargetCall;

    }

    private void HandleGetAllVirtualObjectsByTargetCall(int targetId)
    {
        virtualObjectListModel.GetAllVirtualObjectsByTarget(targetId);
    }
    private void HandleGetAllVirtualObjectsByTargetResult(IVirtualObjectList virtualObjectList)
    {
        virtualObjectListView.CreateVirtualObjectsList(virtualObjectList);
    }


}

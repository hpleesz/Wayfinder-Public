using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVirtualObjectTypesListController { }
public class VirtualObjectTypesListController : IVirtualObjectTypesListController
{
    private readonly IVirtualObjectTypeList virtualObjectTypeModelList;
    private readonly IVirtualObjectTypesListView virtualObjectTypesListView;


    public VirtualObjectTypesListController(IVirtualObjectTypeList virtualObjectTypeModelList, IVirtualObjectTypesListView virtualObjectTypesListView)
    {
        this.virtualObjectTypeModelList = virtualObjectTypeModelList;
        this.virtualObjectTypesListView = virtualObjectTypesListView;

        this.virtualObjectTypeModelList.onGetAllVirtualObjectTypesResult += HandleGetAllVirtualObjectTypesResult;

        this.virtualObjectTypesListView.onGetAllVirtualObjectTypesCall += HandleGetAllVirtualObjectTypesCall;


    }

    //GET ALL TARGETS BY PLACE
    private void HandleGetAllVirtualObjectTypesCall()
    {
        virtualObjectTypeModelList.GetAllVirtualObjectTypes();
    }
    private void HandleGetAllVirtualObjectTypesResult(IVirtualObjectTypeList virtualObjectTypeModelList)
    {
        virtualObjectTypesListView.CreateVirtualObjectTypesList(virtualObjectTypeModelList);
    }
}

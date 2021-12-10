using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVirtualObjectDrawerModelFactory
{
    IVirtualObject Model { get; }
    IVirtualObjectTypeList Model2 { get; }

}
public class VirtualObjectDrawerModelFactory : IVirtualObjectDrawerModelFactory
{
    public IVirtualObject Model { get; private set; }
    public IVirtualObjectTypeList Model2 { get; private set; }

    public VirtualObjectDrawerModelFactory()
    {
        Model = new VirtualObject();
        Model2 = new VirtualObjectTypeList();
    }
}

//IVirtualObjectsListView, IFloorsListView, ICategoriesListView, IVirtualObjectEditView
public interface IVirtualObjectDrawerViewFactory
{
    IVirtualObjectEditView View { get; }
    IVirtualObjectTypesListView View2 { get; }
}
public class VirtualObjectDrawerViewFactory : IVirtualObjectDrawerViewFactory
{
    public IVirtualObjectEditView View { get; private set; }
    public IVirtualObjectTypesListView View2 { get; private set; }

    public VirtualObjectDrawerViewFactory()
    {
        var instance = GameObject.Find("VirtualObjectDrawer");

        View = instance.transform.Find("Grid").GetComponent<IVirtualObjectEditView>();
        View2 = instance.transform.Find("Grid").GetComponent<IVirtualObjectTypesListView>();
    }
}


public interface IVirtualObjectDrawerControllerFactory
{
    IVirtualObjectEditController Controller { get; }
    IVirtualObjectTypesListController Controller2 { get; }


}
public class VirtualObjectDrawerControllerFactory : IVirtualObjectDrawerControllerFactory
{
    public IVirtualObjectEditController Controller { get; private set; }
    public IVirtualObjectTypesListController Controller2 { get; private set; }

    public VirtualObjectDrawerControllerFactory(IVirtualObject model, IVirtualObjectTypeList model2, IVirtualObjectEditView view, IVirtualObjectTypesListView view2)
    {

        Controller = new VirtualObjectEditController(model, view);
        Controller2 = new VirtualObjectTypesListController(model2, view2);

    }

    public VirtualObjectDrawerControllerFactory() : this(new VirtualObject(), new VirtualObjectTypeList(), new VirtualObjectEditor(), new VirtualObjectEditor()) { }
}



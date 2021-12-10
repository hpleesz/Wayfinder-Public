using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorDrawerModelFactory
{
    IFloor Model { get; }
    IFloorPlan2D Model2 { get; }
    IFloorPlan3D Model3 { get; }

}
public class FloorDrawerModelFactory : IFloorDrawerModelFactory
{
    public IFloor Model { get; private set; }
    public IFloorPlan2D Model2 { get; private set; }
    public IFloorPlan3D Model3 { get; private set; }

    public FloorDrawerModelFactory()
    {
        Model = new Floor();
        Model2 = new FloorPlan2D();
        Model3 = new FloorPlan3D();

    }
}

//IFloorsListView, IFloorsListView, ICategoriesListView, IFloorEditView
public interface IFloorDrawerViewFactory
{
    IFloorInfoView View { get; }
    IFloorPlan3DEditView View2 { get; }
    IFloorPlan2DEditView View3 { get; }

}
public class FloorDrawerViewFactory : IFloorDrawerViewFactory
{
    public IFloorInfoView View { get; private set; }
    public IFloorPlan3DEditView View2 { get; private set; }
    public IFloorPlan2DEditView View3 { get; private set; }
    public FloorDrawerViewFactory()
    {
        var instance = GameObject.Find("FloorDrawerCanvas");
        View = instance.transform.Find("Main Panel").GetComponent<IFloorInfoView>();
        View2 = instance.transform.Find("Main Panel").GetComponent<IFloorPlan3DEditView>();
        View3 = instance.transform.Find("Main Panel").GetComponent<IFloorPlan2DEditView>();
    }
}


public interface IFloorDrawerControllerFactory
{
    IFloorInfoController Controller { get; }
    IFloorPlan2DEditController Controller2 { get; }
    IFloorPlan3DEditController Controller3 { get; }


}
public class FloorDrawerControllerFactory : IFloorDrawerControllerFactory
{
    public IFloorInfoController Controller { get; private set; }
    public IFloorPlan2DEditController Controller2 { get; private set; }

    public IFloorPlan3DEditController Controller3 { get; private set; }

    public FloorDrawerControllerFactory(IFloor model, IFloorPlan2D model2, IFloorPlan3D model3, IFloorInfoView view, IFloorPlan2DEditView view2, IFloorPlan3DEditView view3)
    {

        Controller = new FloorInfoController(model, view);
        Controller2 = new FloorPlan2DEditController(model2, view2);
        Controller3 = new FloorPlan3DEditController(model3, view3);
    }

    public FloorDrawerControllerFactory() : this(new Floor(), new FloorPlan2D(), new FloorPlan3D(), new FloorDrawer(), new FloorDrawer(), new FloorDrawer()) { }
}



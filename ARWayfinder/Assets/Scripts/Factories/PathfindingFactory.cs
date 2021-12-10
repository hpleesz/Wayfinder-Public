using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFindingModelFactory
{
    public ITarget Model { get; }
    public IVirtualObjectList Model2 { get; }
    public ITargetPathSteps Model3 { get; }
    public IFloorSwitcherPointList Model4 { get; }
    public ICategoryList Model5 { get; }
    public ITargetPathStepsList Model6 { get; }
}
public class PathFindingModelFactory : IPathFindingModelFactory
{
    public ITarget Model { get; private set; }
    public IVirtualObjectList Model2 { get; private set; }
    public ITargetPathSteps Model3 { get; private set; }
    public IFloorSwitcherPointList Model4 { get; private set; }
    public ICategoryList Model5 { get; private set; }
    public ITargetPathStepsList Model6 { get; private set; }



    public PathFindingModelFactory()
    {
        Model = new Target();
        Model2 = new VirtualObjectList();
        Model3 = new TargetPathSteps();
        Model4 = new FloorSwitcherPointList();
        Model5 = new CategoryList();
        Model6 = new TargetPathStepsList();
    }
}



public interface IPathfindingViewFactory
{
    ITargetInfoView View { get; }
    IVirtualObjectsListView View2 { get; }
    ITargetPathStepsInfoView View3 { get; }
    IFloorSwitcherPointsListView View4 { get; }
    ICategoriesListView View5 { get; }
    ITargetPathStepsListView View6 { get; }
    IFloorSwitcherPointsListView View7 { get; }
    ITargetPathStepsInfoView View8 { get; }

}
public class PathfindingViewFactory : IPathfindingViewFactory
{
    public ITargetInfoView View { get; private set; }
    public IVirtualObjectsListView View2 { get; private set; }
    public ITargetPathStepsInfoView View3 { get; private set; }
    public IFloorSwitcherPointsListView View4 { get; private set; }
    public ICategoriesListView View5 { get; private set; }
    public ITargetPathStepsListView View6 { get; private set; }
    public IFloorSwitcherPointsListView View7 { get; private set; }
    public ITargetPathStepsInfoView View8 { get; private set; }

    public PathfindingViewFactory()
    {
        //var prefab = Resources.Load<GameObject>("Prefabs/PlacesListCanvas");
        //var instance = UnityEngine.Object.Instantiate(prefab);
        var instance = GameObject.Find("QR Code Reader");
        View = instance.GetComponent<ITargetInfoView>();
        View2 = instance.GetComponent<IVirtualObjectsListView>();
        View3 = instance.GetComponent<ITargetPathStepsInfoView>();
        View4 = instance.GetComponent<IFloorSwitcherPointsListView>();

        var instance2 = GameObject.Find("TargetsCanvas").transform.Find("SearchCanvas").transform.Find("Places Panel").transform.Find("List Panel").transform.Find("List Panel").transform.Find("Scrollable List");
        View5 = instance2.GetComponent<ICategoriesListView>();
        View6 = instance2.GetComponent<ITargetPathStepsListView>();
        View7 = instance2.GetComponent<IFloorSwitcherPointsListView>();
        View8 = instance2.GetComponent<ITargetPathStepsInfoView>();


    }
}


public interface IPathfindingControllerFactory
{
    ITargetInfoController Controller { get; }
    IVirtualObjectsListController Controller2 { get; }
    ITargetPathStepsInfoController Controller3 { get; }
    IFloorSwitcherPointsListController Controller4 { get; }
    ICategoriesListController Controller5 { get; }
    ITargetPathStepsListController Controller6 { get; }
    IFloorSwitcherPointsListController Controller7 { get; }
    ITargetPathStepsInfoController Controller8 { get; }

}
public class PathfindingControllerFactory : IPathfindingControllerFactory
{
    public ITargetInfoController Controller { get; private set; }
    public IVirtualObjectsListController Controller2 { get; private set; }
    public ITargetPathStepsInfoController Controller3 { get; private set; }
    public IFloorSwitcherPointsListController Controller4 { get; private set; }
    public ICategoriesListController Controller5 { get; private set; }
    public ITargetPathStepsListController Controller6 { get; private set; }
    public IFloorSwitcherPointsListController Controller7 { get; private set; }
    public ITargetPathStepsInfoController Controller8 { get; private set; }
    public PathfindingControllerFactory(ITarget model, IVirtualObjectList model2, ITargetPathSteps model3, IFloorSwitcherPointList model4, ICategoryList model5, 
                                        ITargetPathStepsList model6, ITargetInfoView view, IVirtualObjectsListView view2, ITargetPathStepsInfoView view3, 
                                        IFloorSwitcherPointsListView view4, ICategoriesListView view5, ITargetPathStepsListView view6, IFloorSwitcherPointsListView view7,
                                        ITargetPathStepsInfoView view8)
    {

        Controller = new TargetInfoController(model, view);
        Controller2 = new VirtualObjectsListController(model2, view2);
        Controller3 = new TargetPathStepsInfoController(model3, view3);
        Controller4 = new FloorSwitcherPointsListController(model4, view4);
        Controller5 = new CategoriesListController(model5, view5);
        Controller6 = new TargetPathStepsListController(model6, view6);
        Controller7 = new FloorSwitcherPointsListController(model4, view7);
        Controller8 = new TargetPathStepsInfoController(model3, view8);

    }

    public PathfindingControllerFactory() : this(new Target(), new VirtualObjectList(), new TargetPathSteps(), new FloorSwitcherPointList(), new CategoryList(),
                                                new TargetPathStepsList(), new QRScript(), new QRScript(), new QRScript(), new QRScript(), new TargetsList(),
                                                new TargetsList(), new TargetsList(), new TargetsList()) { }
}


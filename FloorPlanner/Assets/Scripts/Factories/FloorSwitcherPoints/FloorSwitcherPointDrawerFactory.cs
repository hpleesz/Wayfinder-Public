using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorSwitcherPointDrawerModelFactory
{
    IFloorSwitcherPoint Model { get; }
    IFloorSwitcherPointList Model2 { get; }
    IFloorSwitcherPointDistanceList Model3 { get; }
    ITargetList Model4 { get; }
    ITargetDistanceList Model5 { get; }
}
public class FloorSwitcherPointDrawerModelFactory : IFloorSwitcherPointDrawerModelFactory
{
    public IFloorSwitcherPoint Model { get; private set; }
    public IFloorSwitcherPointList Model2 { get; private set; }
    public IFloorSwitcherPointDistanceList Model3 { get; private set; }
    public ITargetList Model4 { get; private set; }
    public ITargetDistanceList Model5 { get; private set; }


    public FloorSwitcherPointDrawerModelFactory()
    {
        Model = new FloorSwitcherPoint();
        Model2 = new FloorSwitcherPointList();
        Model3 = new FloorSwitcherPointDistanceList();
        Model4 = new TargetList();
        Model5 = new TargetDistanceList();

    }
}

//IFloorSwitcherPointsListView, IFloorsListView, ICategoriesListView, IFloorSwitcherPointEditView
public interface IFloorSwitcherPointDrawerViewFactory
{
    IFloorSwitcherPointInfoView View { get; }
    IFloorSwitcherPointEditView View2 { get; }
    IFloorSwitcherPointsListView View3 { get; }
    IFloorSwitcherPointDistancesListView View4 { get; }
    ITargetsListView View5 { get; }
    ITargetDistancesListView View6 { get; }


}
public class FloorSwitcherPointDrawerViewFactory : IFloorSwitcherPointDrawerViewFactory
{
    public IFloorSwitcherPointInfoView View { get; private set; }
    public IFloorSwitcherPointEditView View2 { get; private set; }
    public IFloorSwitcherPointsListView View3 { get; private set; }
    public IFloorSwitcherPointDistancesListView View4 { get; private set; }
    public ITargetsListView View5 { get; private set; }
    public ITargetDistancesListView View6 { get; private set; }

    public FloorSwitcherPointDrawerViewFactory()
    {
        var instance = GameObject.Find("FloorSwitcherPointDrawerCanvas");
        var instance2 = GameObject.Find("FloorSwitcherPointDrawer");

        /*
        var prefab = Resources.Load<GameObject>("Prefabs/FloorSwitcherPointDrawerCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        var prefab2 = Resources.Load<GameObject>("Prefabs/FloorSwitcherPointDrawer");
        var instance2 = UnityEngine.Object.Instantiate(prefab2);
        */
        View = instance.transform.Find("Main Panel").GetComponent<IFloorSwitcherPointInfoView>();
        View2 = instance2.transform.Find("Grid").GetComponent<IFloorSwitcherPointEditView>();
        View3 = instance2.transform.Find("Grid").GetComponent<IFloorSwitcherPointsListView>();
        View4 = instance2.transform.Find("Grid").GetComponent<IFloorSwitcherPointDistancesListView>();
        View5 = instance2.transform.Find("Grid").GetComponent<ITargetsListView>();
        View6 = instance2.transform.Find("Grid").GetComponent<ITargetDistancesListView>();

    }
}


public interface IFloorSwitcherPointDrawerControllerFactory
{
    IFloorSwitcherPointInfoController Controller { get; }
    IFloorSwitcherPointEditController Controller2 { get; }
    IFloorSwitcherPointsListController Controller3 { get; }
    IFloorSwitcherPointDistancesListController Controller4 { get; }
    ITargetsListController Controller5 { get; }
    ITargetDistancesListController Controller6 { get; }


}
public class FloorSwitcherPointDrawerControllerFactory : IFloorSwitcherPointDrawerControllerFactory
{
    public IFloorSwitcherPointInfoController Controller { get; private set; }
    public IFloorSwitcherPointEditController Controller2 { get; private set; }

    public IFloorSwitcherPointsListController Controller3 { get; private set; }

    public IFloorSwitcherPointDistancesListController Controller4 { get; private set; }
    public ITargetsListController Controller5 { get; private set; }
    public ITargetDistancesListController Controller6 { get; private set; }


    public FloorSwitcherPointDrawerControllerFactory(IFloorSwitcherPoint model, ITargetList model2, IFloorSwitcherPointList model3, IFloorSwitcherPointDistanceList model4, ITargetDistanceList model5, IFloorSwitcherPointInfoView view, IFloorSwitcherPointEditView view2, IFloorSwitcherPointsListView view3, IFloorSwitcherPointDistancesListView view4, ITargetsListView view5, ITargetDistancesListView view6)
    {

        Controller = new FloorSwitcherPointInfoController(model, view);
        Controller2 = new FloorSwitcherPointEditController(model, view2);
        Controller3 = new FloorSwitcherPointsListController(model3, view3);
        Controller4 = new FloorSwitcherPointDistancesListController(model4, view4);
        Controller5 = new TargetsListController(model2, view5);
        Controller6 = new TargetDistancesListController(model5, view6);

    }

    public FloorSwitcherPointDrawerControllerFactory() : this(new FloorSwitcherPoint(), new TargetList(), new FloorSwitcherPointList(), new FloorSwitcherPointDistanceList(), new TargetDistanceList(), new FloorSwitcherPointDrawer(), new FloorSwitcherPointPlacer(), new FloorSwitcherPointPlacer(), new FloorSwitcherPointPlacer(), new FloorSwitcherPointPlacer(), new FloorSwitcherPointPlacer()) { }
}



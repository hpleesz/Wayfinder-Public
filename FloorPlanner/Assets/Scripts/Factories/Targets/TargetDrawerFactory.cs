using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetDrawerModelFactory
{
    ITarget Model { get; }
    IFloorSwitcherPointList Model2 { get; }
    ITargetDistanceList Model3 { get; }

}
public class TargetDrawerModelFactory : ITargetDrawerModelFactory
{
    public ITarget Model { get; private set; }
    public IFloorSwitcherPointList Model2 { get; private set; }
    public ITargetDistanceList Model3 { get; private set; }

    public TargetDrawerModelFactory()
    {
        Model = new Target();
        Model2 = new FloorSwitcherPointList();
        Model3 = new TargetDistanceList();

    }
}

//ITargetsListView, IFloorsListView, ICategoriesListView, ITargetEditView
public interface ITargetDrawerViewFactory
{
    ITargetInfoView View { get; }
    ITargetEditView View2 { get; }
    IFloorSwitcherPointsListView View3 { get; }
    ITargetDistancesListView View4 { get; }


}
public class TargetDrawerViewFactory : ITargetDrawerViewFactory
{
    public ITargetInfoView View { get; private set; }
    public ITargetEditView View2 { get; private set; }
    public IFloorSwitcherPointsListView View3 { get; private set; }
    public ITargetDistancesListView View4 { get; private set; }
    public TargetDrawerViewFactory()
    {
        var instance = GameObject.Find("TargetDrawerCanvas");
        var instance2 = GameObject.Find("TargetDrawer");

        /*
        var prefab = Resources.Load<GameObject>("Prefabs/TargetDrawerCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        var prefab2 = Resources.Load<GameObject>("Prefabs/TargetDrawer");
        var instance2 = UnityEngine.Object.Instantiate(prefab2);
        */
        View = instance.transform.Find("Main Panel").GetComponent<ITargetInfoView>();
        View2 = instance2.transform.Find("Grid").GetComponent<ITargetEditView>();
        View3 = instance2.transform.Find("Grid").GetComponent<IFloorSwitcherPointsListView>();
        View4 = instance2.transform.Find("Grid").GetComponent<ITargetDistancesListView>();

    }
}


public interface ITargetDrawerControllerFactory
{
    ITargetInfoController Controller { get; }
    ITargetEditController Controller2 { get; }
    IFloorSwitcherPointsListController Controller3 { get; }
    ITargetDistancesListController Controller4 { get; }


}
public class TargetDrawerControllerFactory : ITargetDrawerControllerFactory
{
    public ITargetInfoController Controller { get; private set; }
    public ITargetEditController Controller2 { get; private set; }

    public IFloorSwitcherPointsListController Controller3 { get; private set; }

    public ITargetDistancesListController Controller4 { get; private set; }

    public TargetDrawerControllerFactory(ITarget model, IFloorSwitcherPointList model3, ITargetDistanceList model4, ITargetInfoView view, ITargetEditView view2, IFloorSwitcherPointsListView view3, ITargetDistancesListView view4)
    {

        Controller = new TargetInfoController(model, view);
        Controller2 = new TargetEditController(model, view2);
        Controller3 = new FloorSwitcherPointsListController(model3, view3);
        Controller4 = new TargetDistancesListController(model4, view4);

    }

    public TargetDrawerControllerFactory() : this(new Target(), new FloorSwitcherPointList(), new TargetDistanceList(), new TargetDrawer(), new TargetPlacer(), new TargetPlacer(), new TargetPlacer()) { }
}



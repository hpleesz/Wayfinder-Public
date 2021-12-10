using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorSwitcherPointModelListFactory
{
    IFloorSwitcherPointList Model { get; }
    IFloorSwitcherPoint Model2 { get; }
    IFloorList Model3 { get; }

}
public class FloorSwitcherPointModelListFactory : IFloorSwitcherPointModelListFactory
{
    public IFloorSwitcherPointList Model { get; private set; }
    public IFloorSwitcherPoint Model2 { get; private set; }
    public IFloorList Model3 { get; private set; }

    public FloorSwitcherPointModelListFactory()
    {
        Model = new FloorSwitcherPointList();
        Model2 = new FloorSwitcherPoint();
        Model3 = new FloorList();

    }
}


public interface IFloorSwitcherPointsListViewFactory
{
    IFloorSwitcherPointsListView View { get; }
    IFloorSwitcherPointEditView View2 { get; }
    IFloorsListView View3 { get; }

}
public class FloorSwitcherPointsListViewFactory : IFloorSwitcherPointsListViewFactory
{
    public IFloorSwitcherPointsListView View { get; private set; }
    public IFloorSwitcherPointEditView View2 { get; private set; }
    public IFloorsListView View3 { get; private set; }

    public FloorSwitcherPointsListViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/FloorSwitcherPointsListCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.transform.Find("Floor Switcher Points Panel").GetComponent<IFloorSwitcherPointsListView>();
        View2 = instance.transform.Find("Floor Switcher Points Panel").GetComponent<IFloorSwitcherPointEditView>();
        View3 = instance.transform.Find("Floor Switcher Points Panel").GetComponent<IFloorsListView>();

    }
}


public interface IFloorSwitcherPointsListControllerFactory
{
    IFloorSwitcherPointsListController Controller { get; }
    IFloorSwitcherPointEditController Controller2 { get; }
    IFloorsListController Controller3 { get; }

}
public class FloorSwitcherPointsListControllerFactory : IFloorSwitcherPointsListControllerFactory
{
    public IFloorSwitcherPointsListController Controller { get; private set; }
    public IFloorSwitcherPointEditController Controller2 { get; private set; }
    public IFloorsListController Controller3 { get; private set; }


    public FloorSwitcherPointsListControllerFactory(IFloorSwitcherPointList model, IFloorSwitcherPoint model2, IFloorList model3, IFloorSwitcherPointsListView view, IFloorSwitcherPointEditView view2, IFloorsListView view3)
    {

        Controller = new FloorSwitcherPointsListController(model, view);
        Controller2 = new FloorSwitcherPointEditController(model2, view2);
        Controller3 = new FloorsListController(model3, view3);

    }

    public FloorSwitcherPointsListControllerFactory() : this(new FloorSwitcherPointList(), new FloorSwitcherPoint(), new FloorList(), new FloorSwitcherPointsListView(), new FloorSwitcherPointsListView(), new FloorSwitcherPointsListView()) { }
}



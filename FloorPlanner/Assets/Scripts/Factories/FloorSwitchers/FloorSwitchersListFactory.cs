using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorSwitcherModelListFactory
{
    IFloorSwitcherList Model { get; }
    IFloorSwitcher Model2 { get; }
    IFloorSwitcherPointList Model3 { get; }

}
public class FloorSwitcherModelListFactory : IFloorSwitcherModelListFactory
{
    public IFloorSwitcherList Model { get; private set; }
    public IFloorSwitcher Model2 { get; private set; }
    public IFloorSwitcherPointList Model3 { get; private set; }

    public FloorSwitcherModelListFactory()
    {
        Model = new FloorSwitcherList();
        Model2 = new FloorSwitcher();
        Model3 = new FloorSwitcherPointList();

    }
}


public interface IFloorSwitchersListViewFactory
{
    IFloorSwitchersListView View { get; }
    IFloorSwitcherEditView View2 { get; }
    IFloorSwitcherPointsListView View3 { get; }

}
public class FloorSwitchersListViewFactory : IFloorSwitchersListViewFactory
{
    public IFloorSwitchersListView View { get; private set; }
    public IFloorSwitcherEditView View2 { get; private set; }
    public IFloorSwitcherPointsListView View3 { get; private set; }

    public FloorSwitchersListViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/FloorSwitchersListCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.transform.Find("Floor Switchers Panel").GetComponent<IFloorSwitchersListView>();
        View2 = instance.transform.Find("Floor Switchers Panel").GetComponent<IFloorSwitcherEditView>();
        View3 = instance.transform.Find("Floor Switchers Panel").GetComponent<IFloorSwitcherPointsListView>();

    }
}


public interface IFloorSwitchersListControllerFactory
{
    IFloorSwitchersListController Controller { get; }
    IFloorSwitcherEditController Controller2 { get; }
    IFloorSwitcherPointsListController Controller3 { get; }

}
public class FloorSwitchersListControllerFactory : IFloorSwitchersListControllerFactory
{
    public IFloorSwitchersListController Controller { get; private set; }
    public IFloorSwitcherEditController Controller2 { get; private set; }
    public IFloorSwitcherPointsListController Controller3 { get; private set; }


    public FloorSwitchersListControllerFactory(IFloorSwitcherList model, IFloorSwitcher model2, IFloorSwitcherPointList model3, IFloorSwitchersListView view, IFloorSwitcherEditView view2, IFloorSwitcherPointsListView view3)
    {

        Controller = new FloorSwitchersListController(model, view);
        Controller2 = new FloorSwitcherEditController(model2, view2);
        Controller3 = new FloorSwitcherPointsListController(model3, view3);

    }

    public FloorSwitchersListControllerFactory() : this(new FloorSwitcherList(), new FloorSwitcher(), new FloorSwitcherPointList(), new FloorSwitchersListView(), new FloorSwitchersListView(), new FloorSwitchersListView()) { }
}



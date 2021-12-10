using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorModelListFactory
{
    IFloorList Model { get; }
    IFloor Model2 { get; }
}
public class FloorModelListFactory : IFloorModelListFactory
{
    public IFloorList Model { get; private set; }
    public IFloor Model2 { get; private set; }

    public FloorModelListFactory()
    {
        Model = new FloorList();
        Model2 = new Floor();
    }
}


public interface IFloorsListViewFactory
{
    IFloorsListView View { get; }
    IFloorEditView View2 { get; }

}
public class FloorsListViewFactory : IFloorsListViewFactory
{
    public IFloorsListView View { get; private set; }
    public IFloorEditView View2 { get; private set; }

    public FloorsListViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/FloorsListCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.transform.Find("Floors Panel").GetComponent<IFloorsListView>();
        View2 = instance.transform.Find("Floors Panel").GetComponent<IFloorEditView>();

    }
}


public interface IFloorsListControllerFactory
{
    IFloorsListController Controller { get; }
    IFloorEditController Controller2 { get; }

}
public class FloorsListControllerFactory : IFloorsListControllerFactory
{
    public IFloorsListController Controller { get; private set; }
    public IFloorEditController Controller2 { get; private set; }


    public FloorsListControllerFactory(IFloorList model, IFloor model2, IFloorsListView view, IFloorEditView view2)
    {

        Controller = new FloorsListController(model, view);
        Controller2 = new FloorEditController(model2, view2);

    }

    public FloorsListControllerFactory() : this(new FloorList(), new Floor(), new FloorsListView(), new FloorsListView()) { }
}



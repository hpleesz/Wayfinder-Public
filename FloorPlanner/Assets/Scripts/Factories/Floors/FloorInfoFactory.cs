using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloorInfoModelFactory
{
    IFloor Model { get; }
}
public class FloorInfoModelFactory : IFloorInfoModelFactory
{
    public IFloor Model { get; private set; }

    public FloorInfoModelFactory()
    {
        Model = new Floor();
    }
}


public interface IFloorInfoViewFactory
{
    IFloorInfoView View { get; }
    IFloorEditView View2 { get; }

}
public class FloorInfoViewFactory : IFloorInfoViewFactory
{
    public IFloorInfoView View { get; private set; }
    public IFloorEditView View2 { get; private set; }

    public FloorInfoViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/FloorInfoCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.transform.Find("Main Panel").GetComponent<IFloorInfoView>();
        View2 = instance.transform.Find("Main Panel").GetComponent<IFloorEditView>();

    }
}


public interface IFloorInfoControllerFactory
{
    IFloorInfoController Controller { get; }
    IFloorEditController Controller2 { get; }
}
public class FloorInfoControllerFactory : IFloorInfoControllerFactory
{
    public IFloorInfoController Controller { get; private set; }
    public IFloorEditController Controller2 { get; private set; }


    public FloorInfoControllerFactory(IFloor model, IFloorInfoView view, IFloorEditView view2)
    {

        Controller = new FloorInfoController(model, view);
        Controller2 = new FloorEditController(model, view2);

    }

    public FloorInfoControllerFactory() : this(new Floor(), new FloorInfoView(), new FloorInfoView()) { }
}



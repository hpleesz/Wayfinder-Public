using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceEditModelFactory
{
    IPlace Model { get; }
}
public class PlaceEditModelFactory : IPlaceEditModelFactory
{
    public IPlace Model { get; private set; }

    public PlaceEditModelFactory()
    {
        Model = new Place();
    }
}



public interface IPlaceEditViewFactory
{
    IPlaceEditView View { get; }
    IPlaceInfoView View2 { get; }

}
public class PlaceEditViewFactory : IPlaceEditViewFactory
{
    public IPlaceEditView View { get; private set; }
    public IPlaceInfoView View2 { get; private set; }

    public PlaceEditViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/PlaceEditCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.transform.Find("Main Panel").GetComponent<IPlaceEditView>();
        View2 = instance.transform.Find("Main Panel").GetComponent<IPlaceInfoView>();
    }
}


public interface IPlaceEditControllerFactory
{
    IPlaceEditController Controller { get; }
    IPlaceInfoController Controller2 { get; }
}
public class PlaceEditControllerFactory : IPlaceEditControllerFactory
{
    public IPlaceEditController Controller { get; private set; }
    public IPlaceInfoController Controller2 { get; private set; }


    public PlaceEditControllerFactory(IPlace model, IPlaceEditView view, IPlaceInfoView view2)
    {
        Controller = new PlaceEditController(model, view);
        Controller2 = new PlaceInfoController(model, view2);

    }

    public PlaceEditControllerFactory() : this(new Place(), new PlaceEditView(), new PlaceInfoView()) { }
}



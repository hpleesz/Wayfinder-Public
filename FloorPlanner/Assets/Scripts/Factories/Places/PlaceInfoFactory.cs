using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceInfoModelFactory
{
    IPlace Model { get; }
}
public class PlaceInfoModelFactory : IPlaceInfoModelFactory
{
    public IPlace Model { get; private set; }

    public PlaceInfoModelFactory()
    {
        Model = new Place();
    }
}



public interface IPlaceInfoViewFactory
{
    IPlaceInfoView View { get; }
}
public class PlaceInfoViewFactory : IPlaceInfoViewFactory
{
    public IPlaceInfoView View { get; private set; }

    public PlaceInfoViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/PlaceCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.transform.Find("Main Panel").GetComponent<IPlaceInfoView>();
    }
}


public interface IPlaceInfoControllerFactory
{
    IPlaceInfoController Controller { get; }
}
public class PlaceInfoControllerFactory : IPlaceInfoControllerFactory
{
    public IPlaceInfoController Controller { get; private set; }

    public PlaceInfoControllerFactory(IPlace model, IPlaceInfoView view)
    {
        Controller = new PlaceInfoController(model, view);
    }

    public PlaceInfoControllerFactory() : this(new Place(), new PlaceInfoView()) { }
}



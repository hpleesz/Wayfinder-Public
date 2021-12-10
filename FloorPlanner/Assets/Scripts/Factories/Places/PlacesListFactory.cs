using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceModelListFactory
{
    IPlaceList Model { get; }
}
public class PlaceModelListFactory : IPlaceModelListFactory
{
    public IPlaceList Model { get; private set; }

    public PlaceModelListFactory()
    {
        Model = new PlaceList();
    }
}



public interface IPlacesListViewFactory
{
    IPlacesListView View { get; }
}
public class PlacesListViewFactory : IPlacesListViewFactory
{
    public IPlacesListView View { get; private set; }

    public PlacesListViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/PlacesListCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.GetComponent<IPlacesListView>();
    }
}


public interface IPlacesListControllerFactory
{
    IPlacesListController Controller { get; }
}
public class PlacesListControllerFactory : IPlacesListControllerFactory
{
    public IPlacesListController Controller { get; private set; }

    public PlacesListControllerFactory(IPlaceList model, IPlacesListView view)
    {

        Controller = new PlacesListController(model, view);
    }

    public PlacesListControllerFactory() : this(new PlaceList(), new PlacesListView()) { }
}



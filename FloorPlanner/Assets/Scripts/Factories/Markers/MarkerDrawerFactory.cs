using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarkerDrawerModelFactory
{
    IMarker Model { get; }
}
public class MarkerDrawerModelFactory : IMarkerDrawerModelFactory
{
    public IMarker Model { get; private set; }

    public MarkerDrawerModelFactory()
    {
        Model = new Marker();

    }
}

//IMarkersListView, IFloorsListView, ICategoriesListView, IMarkerEditView
public interface IMarkerDrawerViewFactory
{
    IMarkerInfoView View { get; }
    IMarkerEditView View2 { get; }


}
public class MarkerDrawerViewFactory : IMarkerDrawerViewFactory
{
    public IMarkerInfoView View { get; private set; }
    public IMarkerEditView View2 { get; private set; }
    public MarkerDrawerViewFactory()
    {
        var instance = GameObject.Find("MarkerDrawerCanvas");
        var instance2 = GameObject.Find("MarkerDrawer");

        /*
        var prefab = Resources.Load<GameObject>("Prefabs/MarkerDrawerCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        var prefab2 = Resources.Load<GameObject>("Prefabs/MarkerDrawer");
        var instance2 = UnityEngine.Object.Instantiate(prefab2);
        */
        View = instance.transform.Find("Main Panel").GetComponent<IMarkerInfoView>();
        View2 = instance2.transform.Find("Grid").GetComponent<IMarkerEditView>();

    }
}


public interface IMarkerDrawerControllerFactory
{
    IMarkerInfoController Controller { get; }
    IMarkerEditController Controller2 { get; }


}
public class MarkerDrawerControllerFactory : IMarkerDrawerControllerFactory
{
    public IMarkerInfoController Controller { get; private set; }
    public IMarkerEditController Controller2 { get; private set; }


    public MarkerDrawerControllerFactory(IMarker model, IMarkerEditView view, IMarkerInfoView view2)
    {

        Controller = new MarkerInfoController(model, view2);
        Controller2 = new MarkerEditController(model, view);

    }

    public MarkerDrawerControllerFactory() : this(new Marker(), new MarkerPlacer(), new MarkerDrawer()) { }
}



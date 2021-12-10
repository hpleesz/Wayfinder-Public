using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarkerModelListFactory
{
    IMarkerList Model { get; }
    IMarker Model2 { get; }
    IFloorList Model3 { get; }

}
public class MarkerModelListFactory : IMarkerModelListFactory
{
    public IMarkerList Model { get; private set; }
    public IMarker Model2 { get; private set; }
    public IFloorList Model3 { get; private set; }

    public MarkerModelListFactory()
    {
        Model = new MarkerList();
        Model2 = new Marker();
        Model3 = new FloorList();

    }
}

//IMarkersListView, IFloorsListView, ICategoriesListView, IMarkerEditView
public interface IMarkersListViewFactory
{
    IMarkersListView View { get; }
    IMarkerEditView View2 { get; }
    IFloorsListView View3 { get; }


}
public class MarkersListViewFactory : IMarkersListViewFactory
{
    public IMarkersListView View { get; private set; }
    public IMarkerEditView View2 { get; private set; }
    public IFloorsListView View3 { get; private set; }
    public MarkersListViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/MarkersListCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.transform.Find("Markers Panel").GetComponent<IMarkersListView>();
        View2 = instance.transform.Find("Markers Panel").GetComponent<IMarkerEditView>();
        View3 = instance.transform.Find("Markers Panel").GetComponent<IFloorsListView>();

    }
}


public interface IMarkersListControllerFactory
{
    IMarkersListController Controller { get; }
    IMarkerEditController Controller2 { get; }
    IFloorsListController Controller3 { get; }


}
public class MarkersListControllerFactory : IMarkersListControllerFactory
{
    public IMarkersListController Controller { get; private set; }
    public IMarkerEditController Controller2 { get; private set; }

    public IFloorsListController Controller3 { get; private set; }

    public MarkersListControllerFactory(IMarkerList model, IMarker model2, IFloorList model4, IMarkersListView view, IMarkerEditView view2, IFloorsListView view3)
    {

        Controller = new MarkersListController(model, view);
        Controller2 = new MarkerEditController(model2, view2);
        Controller3 = new FloorsListController(model4, view3);

    }

    public MarkersListControllerFactory() : this(new MarkerList(), new Marker(), new FloorList(), new MarkersListView(), new MarkersListView(), new FloorsListView()) { }
}



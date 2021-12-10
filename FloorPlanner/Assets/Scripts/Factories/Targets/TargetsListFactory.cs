using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetModelListFactory
{
    ITargetList Model { get; }
    ITarget Model2 { get; }
}
public class TargetModelListFactory : ITargetModelListFactory
{
    public ITargetList Model { get; private set; }
    public ITarget Model2 { get; private set; }
    public ICategoryList Model3 { get; private set; }
    public IFloorList Model4 { get; private set; }

    public TargetModelListFactory()
    {
        Model = new TargetList();
        Model2 = new Target();
        Model3 = new CategoryList();
        Model4 = new FloorList();

    }
}

//ITargetsListView, IFloorsListView, ICategoriesListView, ITargetEditView
public interface ITargetsListViewFactory
{
    ITargetsListView View { get; }
    ITargetEditView View2 { get; }
    ICategoriesListView View3 { get; }
    IFloorsListView View4 { get; }


}
public class TargetsListViewFactory : ITargetsListViewFactory
{
    public ITargetsListView View { get; private set; }
    public ITargetEditView View2 { get; private set; }
    public ICategoriesListView View3 { get; private set; }
    public IFloorsListView View4 { get; private set; }
    public TargetsListViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/TargetsListCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.transform.Find("Targets Panel").GetComponent<ITargetsListView>();
        View2 = instance.transform.Find("Targets Panel").GetComponent<ITargetEditView>();
        View3 = instance.transform.Find("Targets Panel").GetComponent<ICategoriesListView>();
        View4 = instance.transform.Find("Targets Panel").GetComponent<IFloorsListView>();

    }
}


public interface ITargetsListControllerFactory
{
    ITargetsListController Controller { get; }
    ITargetEditController Controller2 { get; }
    ICategoriesListController Controller3 { get; }
    IFloorsListController Controller4 { get; }


}
public class TargetsListControllerFactory : ITargetsListControllerFactory
{
    public ITargetsListController Controller { get; private set; }
    public ITargetEditController Controller2 { get; private set; }

    public ICategoriesListController Controller3 { get; private set; }

    public IFloorsListController Controller4 { get; private set; }

    public TargetsListControllerFactory(ITargetList model, ITarget model2, ICategoryList model3, IFloorList model4, ITargetsListView view, ITargetEditView view2, ICategoriesListView view3, IFloorsListView view4)
    {

        Controller = new TargetsListController(model, view);
        Controller2 = new TargetEditController(model2, view2);
        Controller3 = new CategoriesListController(model3, view3);
        Controller4 = new FloorsListController(model4, view4);

    }

    public TargetsListControllerFactory() : this(new TargetList(), new Target(), new CategoryList(), new FloorList(), new TargetsListView(), new TargetsListView(), new CategoriesListView(), new FloorsListView()) { }
}



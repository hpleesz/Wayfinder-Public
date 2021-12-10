using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICategoryModelListFactory
{
    ICategoryList Model { get; }
    ICategory Model2 { get; }
}
public class CategoryModelListFactory : ICategoryModelListFactory
{
    public ICategoryList Model { get; private set; }
    public ICategory Model2 { get; private set; }

    public CategoryModelListFactory()
    {
        Model = new CategoryList();
        Model2 = new Category();
    }
}


public interface ICategoriesListViewFactory
{
    ICategoriesListView View { get; }
    ICategoryEditView View2 { get; }

}
public class CategoriesListViewFactory : ICategoriesListViewFactory
{
    public ICategoriesListView View { get; private set; }
    public ICategoryEditView View2 { get; private set; }

    public CategoriesListViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/CategoriesListCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.transform.Find("Categories Panel").GetComponent<ICategoriesListView>();
        View2 = instance.transform.Find("Categories Panel").GetComponent<ICategoryEditView>();

    }
}


public interface ICategoriesListControllerFactory
{
    ICategoriesListController Controller { get; }
    ICategoryEditController Controller2 { get; }

}
public class CategoriesListControllerFactory : ICategoriesListControllerFactory
{
    public ICategoriesListController Controller { get; private set; }
    public ICategoryEditController Controller2 { get; private set; }


    public CategoriesListControllerFactory(ICategoryList model, ICategory model2, ICategoriesListView view, ICategoryEditView view2)
    {

        Controller = new CategoriesListController(model, view);
        Controller2 = new CategoryEditController(model2, view2);

    }

    public CategoriesListControllerFactory() : this(new CategoryList(), new Category(), new CategoriesListView(), new CategoriesListView()) { }
}



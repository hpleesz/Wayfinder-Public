using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoriesListManager : MonoBehaviour
{
    private void Awake()
    {
        var modelFactory = new CategoryModelListFactory();
        var model = modelFactory.Model;
        var model2 = modelFactory.Model2;

        var viewFactory = new CategoriesListViewFactory();
        var view = viewFactory.View;
        var view2 = viewFactory.View2;

        var controllerFactory = new CategoriesListControllerFactory(model, model2, view, view2);
        var controller = controllerFactory.Controller;





    }
}

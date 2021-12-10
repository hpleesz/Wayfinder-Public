using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDrawerManager : MonoBehaviour
{
    private void Awake()
    {
        var modelFactory = new FloorDrawerModelFactory();
        var model = modelFactory.Model;
        var model2 = modelFactory.Model2;
        var model3 = modelFactory.Model3;

        var viewFactory = new FloorDrawerViewFactory();
        var view = viewFactory.View;
        var view2 = viewFactory.View2;
        var view3 = viewFactory.View3;

        var controllerFactory = new FloorDrawerControllerFactory(model, model2, model3, view, view3, view2);
        var controller = controllerFactory.Controller;

    }
}

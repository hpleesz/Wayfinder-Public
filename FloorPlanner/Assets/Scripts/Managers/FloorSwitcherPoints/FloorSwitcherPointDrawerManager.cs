using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitcherPointDrawerManager : MonoBehaviour
{
    private void Awake()
    {
        var modelFactory = new FloorSwitcherPointDrawerModelFactory();
        var model = modelFactory.Model;
        var model2 = modelFactory.Model2;
        var model3 = modelFactory.Model3;
        var model4 = modelFactory.Model4;
        var model5 = modelFactory.Model5;

        var viewFactory = new FloorSwitcherPointDrawerViewFactory();
        var view = viewFactory.View;
        var view2 = viewFactory.View2;
        var view3 = viewFactory.View3;
        var view4 = viewFactory.View4;
        var view5 = viewFactory.View5;
        var view6 = viewFactory.View6;

        var controllerFactory = new FloorSwitcherPointDrawerControllerFactory(model, model4, model2, model3, model5, view, view2, view3, view4, view5, view6);
        var controller = controllerFactory.Controller;


    }
}

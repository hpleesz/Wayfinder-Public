using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    private void Awake()
    {
        var modelFactory = new PathFindingModelFactory();
        var model = modelFactory.Model;
        var model2 = modelFactory.Model2;
        var model3 = modelFactory.Model3;
        var model4 = modelFactory.Model4;
        var model5 = modelFactory.Model5;
        var model6 = modelFactory.Model6;

        var viewFactory = new PathfindingViewFactory();
        var view = viewFactory.View;
        var view2 = viewFactory.View2;
        var view3 = viewFactory.View3;
        var view4 = viewFactory.View4;
        var view5 = viewFactory.View5;
        var view6 = viewFactory.View6;
        var view7 = viewFactory.View7;
        var view8 = viewFactory.View8;

        var controllerFactory = new PathfindingControllerFactory(model, model2, model3, model4, model5, model6, view, view2, view3, view4, view5, view6, view7, view8);
        var controller = controllerFactory.Controller;
    }
}

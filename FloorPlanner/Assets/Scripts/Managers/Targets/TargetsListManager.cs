using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsListManager : MonoBehaviour
{
    private void Awake()
    {
        var modelFactory = new TargetModelListFactory();
        var model = modelFactory.Model;
        var model2 = modelFactory.Model2;
        var model3 = modelFactory.Model3;
        var model4 = modelFactory.Model4;

        var viewFactory = new TargetsListViewFactory();
        var view = viewFactory.View;
        var view2 = viewFactory.View2;
        var view3 = viewFactory.View3;
        var view4 = viewFactory.View4;

        var controllerFactory = new TargetsListControllerFactory(model, model2, model3, model4, view, view2, view3, view4);
        var controller = controllerFactory.Controller;


    }
}

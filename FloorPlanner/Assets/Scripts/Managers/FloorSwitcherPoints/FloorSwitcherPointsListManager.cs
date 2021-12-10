using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitcherPointsListManager : MonoBehaviour
{
    private void Awake()
    {
        var modelFactory = new FloorSwitcherPointModelListFactory();
        var model = modelFactory.Model;
        var model2 = modelFactory.Model2;
        var model3 = modelFactory.Model3;

        var viewFactory = new FloorSwitcherPointsListViewFactory();
        var view = viewFactory.View;
        var view2 = viewFactory.View2;
        var view3 = viewFactory.View3;

        var controllerFactory = new FloorSwitcherPointsListControllerFactory(model, model2, model3, view, view2, view3);
        var controller = controllerFactory.Controller;





    }
}

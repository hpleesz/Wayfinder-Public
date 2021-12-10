using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorsListManager : MonoBehaviour
{
    private void Awake()
    {
        var modelFactory = new FloorModelListFactory();
        var model = modelFactory.Model;
        var model2 = modelFactory.Model2;

        var viewFactory = new FloorsListViewFactory();
        var view = viewFactory.View;
        var view2 = viewFactory.View2;

        var controllerFactory = new FloorsListControllerFactory(model, model2, view, view2);
        var controller = controllerFactory.Controller;





    }
}

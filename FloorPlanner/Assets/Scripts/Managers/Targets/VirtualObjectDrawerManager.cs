using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualObjectDrawerManager : MonoBehaviour
{
    private void Awake()
    {
        var modelFactory = new VirtualObjectDrawerModelFactory();
        var model = modelFactory.Model;
        var model2 = modelFactory.Model2;

        var viewFactory = new VirtualObjectDrawerViewFactory();
        var view = viewFactory.View;
        var view2 = viewFactory.View2;


        var controllerFactory = new VirtualObjectDrawerControllerFactory(model, model2, view, view2);
        var controller = controllerFactory.Controller;


    }
}

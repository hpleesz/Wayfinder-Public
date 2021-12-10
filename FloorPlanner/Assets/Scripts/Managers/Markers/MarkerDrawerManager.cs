using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerDrawerManager : MonoBehaviour
{
    private void Awake()
    {
        var modelFactory = new MarkerDrawerModelFactory();
        var model = modelFactory.Model;

        var viewFactory = new MarkerDrawerViewFactory();
        var view = viewFactory.View;
        var view2 = viewFactory.View2;

        var controllerFactory = new MarkerDrawerControllerFactory(model, view2, view);
        var controller = controllerFactory.Controller;


    }
}

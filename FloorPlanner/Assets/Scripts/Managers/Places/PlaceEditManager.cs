using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceEditManager : MonoBehaviour
{
    private void Awake()
    {
        var modelFactory = new PlaceEditModelFactory();
        var model = modelFactory.Model;

        var viewFactory = new PlaceEditViewFactory();
        var view = viewFactory.View;
        var view2 = viewFactory.View2;


        var controllerFactory = new PlaceEditControllerFactory(model, view, view2);
        var controller = controllerFactory.Controller;
    }
}

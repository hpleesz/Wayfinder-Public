using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorInfoManager : MonoBehaviour
{
    private void Awake()
    {
        var modelFactory = new FloorInfoModelFactory();
        var model = modelFactory.Model;

        var viewFactory = new FloorInfoViewFactory();
        var view = viewFactory.View;
        var view2 = viewFactory.View2;

        var controllerFactory = new FloorInfoControllerFactory(model, view, view2);
        var controller = controllerFactory.Controller;

    }
}

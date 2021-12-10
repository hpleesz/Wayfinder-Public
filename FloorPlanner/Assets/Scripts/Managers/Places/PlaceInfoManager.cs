using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceInfoManager : MonoBehaviour
{
    private void Awake()
    {
        var modelFactory = new PlaceInfoModelFactory();
        var model = modelFactory.Model;

        var viewFactory = new PlaceInfoViewFactory();
        var view = viewFactory.View;

        var controllerFactory = new PlaceInfoControllerFactory(model, view);
        var controller = controllerFactory.Controller;
    }
}

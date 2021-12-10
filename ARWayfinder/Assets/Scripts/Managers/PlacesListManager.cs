using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacesListManager : MonoBehaviour
{

    private void Awake()
    {
        var modelFactory = new PlaceModelListFactory();
        var model = modelFactory.Model;

        var viewFactory = new PlacesListViewFactory();
        var view = viewFactory.View;

        var controllerFactory = new PlacesListControllerFactory(model, view);
        var controller = controllerFactory.Controller;
    }

}

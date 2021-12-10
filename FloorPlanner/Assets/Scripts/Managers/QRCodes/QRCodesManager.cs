using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRCodesManager : MonoBehaviour
{
    private void Awake()
    {
        var modelFactory = new QRCodeModelListFactory();
        var model = modelFactory.Model;

        var viewFactory = new QRCodeListViewFactory();
        var view = viewFactory.View;

        var controllerFactory = new QRCodesListControllerFactory(model, view);
        var controller = controllerFactory.Controller;


    }
}

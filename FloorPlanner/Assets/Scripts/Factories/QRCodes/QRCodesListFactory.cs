using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQRCodeModelListFactory
{
    ITargetList Model { get; }
}
public class QRCodeModelListFactory : IQRCodeModelListFactory
{
    public ITargetList Model { get; private set; }

    public QRCodeModelListFactory()
    {
        Model = new TargetList();
    }
}

//ITargetsListView, IFloorsListView, ICategoriesListView, ITargetEditView
public interface IQRCodeListViewFactory
{
    IQRCodesListView View { get; }


}
public class QRCodeListViewFactory : IQRCodeListViewFactory
{
    public IQRCodesListView View { get; private set; }

    public QRCodeListViewFactory()
    {
        var prefab = Resources.Load<GameObject>("Prefabs/QRCodesCanvas");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.transform.Find("Main Panel").GetComponent<IQRCodesListView>();

    }
}


public interface IQRCodesListControllerFactory
{
    IQRCodesListController Controller { get; }


}
public class QRCodesListControllerFactory : IQRCodesListControllerFactory
{
    public IQRCodesListController Controller { get; private set; }

    public QRCodesListControllerFactory(ITargetList model, IQRCodesListView view)
    {

        Controller = new QRCodesListController(model, view);

    }

    public QRCodesListControllerFactory() : this(new TargetList(), new DownloadQRView()) { }
}



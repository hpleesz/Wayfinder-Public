using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICategoriesListController
{ }

public class CategoriesListController : ICategoriesListController
{
    private readonly ICategoryList categoryModelList;
    private readonly ICategoriesListView categoriesListView;

    public CategoriesListController(ICategoryList categoryModelList, ICategoriesListView categoriesListView)
    {
        this.categoryModelList = categoryModelList;
        this.categoriesListView = categoriesListView;

        this.categoryModelList.onGetAllCategoriesByPlaceResult += HandleGetAllCategoriesByPlaceResult;

        this.categoriesListView.onGetCategoriesByPlaceCall += HandleGetAllCategoriesByPlaceCall;

    }

    private void HandleGetAllCategoriesByPlaceCall(int placeId)
    {
        categoryModelList.GetAllCategoriesByPlace(placeId);
    }
    private void HandleGetAllCategoriesByPlaceResult(ICategoryList categoryModelList)
    {
        categoriesListView.CreateCategoriesList(categoryModelList);
    }


}

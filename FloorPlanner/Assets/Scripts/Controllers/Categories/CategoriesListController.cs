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
        this.categoryModelList.onSearchCategoriesByPlaceResult += HandleSearchCategoriesByPlaceResult;

        this.categoriesListView.onGetAllCategoriesByPlaceCall += HandleGetAllCategoriesByPlaceCall;
        this.categoriesListView.onSearchCategoriesByPlaceCall += HandleSearchCategoriesByPlaceCall;
        this.categoriesListView.onClickCategoryCall += HandleClickCategoryCall;

    }

    private void HandleGetAllCategoriesByPlaceCall(int placeId)
    {
        categoryModelList.GetAllCategoriesByPlace(placeId);
    }
    private void HandleGetAllCategoriesByPlaceResult(ICategoryList categoryModelList)
    {
        categoriesListView.CreateCategoriesList(categoryModelList);
    }

    private void HandleSearchCategoriesByPlaceCall(int placeId, string term)
    {
        categoryModelList.SearchCategoriesByPlace(placeId, term);
    }

    private void HandleSearchCategoriesByPlaceResult(ICategoryList categoryModelList)
    {
        categoriesListView.CreateCategoriesList(categoryModelList);
    }

    private void HandleClickCategoryCall(int categoryId)
    {
        ICategory categoryModel = categoryModelList.GetCategoryById(categoryId);
        categoriesListView.ClickCategory(categoryModel);
    }

}

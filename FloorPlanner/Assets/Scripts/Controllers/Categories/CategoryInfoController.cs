using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICategoryInfoController
{ }

public class CategoryInfoController : ICategoryInfoController
{
    private readonly ICategory categoryModel;
    private readonly ICategoryInfoView categoryInfoView;

    public CategoryInfoController(ICategory categoryModel, ICategoryInfoView categoryInfoView)
    {
        this.categoryModel = categoryModel;
        this.categoryInfoView = categoryInfoView;

        this.categoryModel.onGetCategoryResult += HandleGetCategoryResult;

        this.categoryInfoView.onGetCategoryCall += HandleGetCategoryCall;

    }


    //GET PLACE
    private void HandleGetCategoryCall(int categoryId)
    {
        categoryModel.GetCategory(categoryId);
    }
    private void HandleGetCategoryResult(ICategory category)
    {
        categoryInfoView.ShowCategory(category);
    }

}

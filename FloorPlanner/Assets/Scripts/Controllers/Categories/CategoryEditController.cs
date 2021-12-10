using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ICategoryEditController
{ }


public class CategoryEditController : ICategoryEditController
{
    private readonly ICategory categoryModel;
    private readonly ICategoryEditView categoryView;

    public CategoryEditController(ICategory categoryModel, ICategoryEditView categoryView)
    {
        this.categoryModel = categoryModel;
        this.categoryView = categoryView;

        this.categoryModel.onAddCategoryResult += HandleAddCategoryToPlaceResult;
        this.categoryModel.onEditCategoryResult += HandleEditCategoryResult;

        this.categoryView.onAddCategoryToPlaceCall += HandleAddCategoryToPlaceCall;
        this.categoryView.onEditCategoryCall += HandleEditCategoryCall;

    }


    private void HandleAddCategoryToPlaceCall(int placeId, ICategory category)
    {
        categoryModel.AddCategory(placeId, category);
    }

    private void HandleAddCategoryToPlaceResult(int categoryId)
    {
        categoryView.NewCategoryCreated(categoryId);
    }


    private void HandleEditCategoryCall(int categoryId, ICategory category)
    {
        categoryModel.EditCategory(categoryId, category);
    }

    private void HandleEditCategoryResult(int categoryId)
    {
        categoryView.CategoryUpdated(categoryId);
    }
}

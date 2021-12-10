using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HttpRequestSingleton;

public interface ICategory
{
    public delegate void CategoryResult(ICategory category);
    public delegate void IdResult(int id);
    public delegate void Result();

    event CategoryResult onGetCategoryResult;
    event IdResult onEditCategoryResult;
    event IdResult onAddCategoryResult;

    public int Id { get; set; }
    public string Name { get; set; }
    public List<Target> Targets { get; set; }

    public ICategory CategoryFromJson(string json);
    public string CategoryToJson();


    public void EditCategory(int categoryId, ICategory category);
    public void AddCategory(int placeId, ICategory category);
    public void GetCategory(int categoryId);
}

[System.Serializable]
public class Category : ICategory
{
    private event Result onGetCategory;
    private event Result onAddCategory;
    private event Result onEditCategory;

    public event ICategory.CategoryResult onGetCategoryResult;
    public event ICategory.IdResult onEditCategoryResult;
    public event ICategory.IdResult onAddCategoryResult;


    public int id;
    public string name;
    public List<Target> targets;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public List<Target> Targets
    {
        get { return targets; }
        set { targets = value; }
    }

    public ICategory CategoryFromJson(string json)
    {
        return JsonUtility.FromJson<Category>(json);
    }

    public string CategoryToJson()
    {
        return JsonUtility.ToJson(this);
    }





    //EDIT CATEGORY
    public void EditCategory(int categoryId, ICategory category)
    {
        onEditCategory += onResultEditCategory;
        string json = category.CategoryToJson();
        HttpRequestSingleton.Instance.CallPut("/categories/" + categoryId, json, onEditCategory);
    }
    public void onResultEditCategory(byte[] result)
    {
        int categoryId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onEditCategoryResult(categoryId);
    }

    //ADD CATEGORY
    public void AddCategory(int placeId, ICategory category)
    {
        onAddCategory += onResultAddCategory;
        string json = category.CategoryToJson();
        HttpRequestSingleton.Instance.CallPost("/places/" + placeId + "/categories", json, onAddCategory);
    }
    public void onResultAddCategory(byte[] result)
    {
        int categoryId = Int32.Parse(System.Text.Encoding.UTF8.GetString(result));
        onAddCategoryResult(categoryId);
    }

    //GET CATEGORY
    public void GetCategory(int categoryId)
    {
        onGetCategory += onResultGetCategory;
        HttpRequestSingleton.Instance.CallGet("/categories/" + categoryId, onGetCategory);
    }
    private void onResultGetCategory(byte[] result)
    {
        string categoryJson = System.Text.Encoding.UTF8.GetString(result);
        ICategory category = new Category();
        category = category.CategoryFromJson(categoryJson);
        onGetCategoryResult(category);
    }
}

public interface ICategoryList
{

    public delegate void CategoriesResult(ICategoryList categoryModelList);
    public delegate void CategoryResult(ICategory category);


    event CategoriesResult onGetAllCategoriesByPlaceResult;
    event CategoriesResult onSearchCategoriesByPlaceResult;

    public List<Category> Categories { get; set; }

    public void GetAllCategoriesByPlace(int placeId);
    public void SearchCategoriesByPlace(int placeId, string term);

    //
    public ICategory GetCategoryById(int categoryId);



}

public class CategoryList : ICategoryList
{
    private event Result onGetAllCategoriesByPlace;
    private event Result onSearchCategoriesByPlace;


    public event ICategoryList.CategoriesResult onGetAllCategoriesByPlaceResult;
    public event ICategoryList.CategoriesResult onSearchCategoriesByPlaceResult;


    public List<Category> categories;
    public List<Category> Categories
    {
        get { return categories; }
        set { categories = value; }
    }

    public static CategoryList CategoryListFromJson(string json)
    {
        return JsonUtility.FromJson<CategoryList>(json);
    }

    //GET ALL CATEGORIES
    public void GetAllCategoriesByPlace(int placeId)
    {
        onGetAllCategoriesByPlace += onResultGetAllCategoriesByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/categories", onGetAllCategoriesByPlace);
    }

    private void onResultGetAllCategoriesByPlace(byte[] result)
    {
        string categoriesJson = System.Text.Encoding.UTF8.GetString(result);
        categories = CategoryListFromJson(categoriesJson).categories;
        onGetAllCategoriesByPlaceResult(this);

    }

    //SEARCH CATEGORIES
    public void SearchCategoriesByPlace(int placeId, string term)
    {
        onSearchCategoriesByPlace += onResultSearchCategoriesByPlace;
        HttpRequestSingleton.Instance.CallGet("/places/" + placeId + "/categories/search?term=" + term, onSearchCategoriesByPlace);
    }
    private void onResultSearchCategoriesByPlace(byte[] result)
    {
        string categoriesJson = System.Text.Encoding.UTF8.GetString(result);
        categories = CategoryListFromJson(categoriesJson).categories;
        onGetAllCategoriesByPlaceResult(this);
    }



    public ICategory GetCategoryById(int categoryId)
    {
        ICategory selectedCategory = categories.Where(i => i.Id == categoryId).FirstOrDefault();
        return selectedCategory;
    }
}

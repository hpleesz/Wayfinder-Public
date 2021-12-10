using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HttpRequestSingleton;

public interface ICategory
{


    public int Id { get; set; }
    public string Name { get; set; }
    public List<Target> Targets { get; set; }

    public ICategory CategoryFromJson(string json);
    public string CategoryToJson();

}

[System.Serializable]
public class Category : ICategory
{


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

}

public interface ICategoryList
{

    public delegate void CategoriesResult(ICategoryList categoryModelList);
    public delegate void CategoryResult(ICategory category);


    event CategoriesResult onGetAllCategoriesByPlaceResult;

    public List<Category> Categories { get; set; }

    public void GetAllCategoriesByPlace(int placeId);


}

public class CategoryList : ICategoryList
{
    //ez a http request resultra hívódik
    private event Result onGetAllCategoriesByPlace;


    public event ICategoryList.CategoriesResult onGetAllCategoriesByPlaceResult;


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


}

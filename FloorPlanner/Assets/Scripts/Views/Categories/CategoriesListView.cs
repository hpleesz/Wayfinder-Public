using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface ICategoryInfoView
{
    public delegate void GetCategoryCall(int categoryId);

    public event GetCategoryCall onGetCategoryCall;
    void ShowCategory(ICategory category);

}

public interface ICategoryEditView
{
    public delegate void AddCategoryToPlaceCall(int placeId, ICategory category);
    public delegate void EditCategoryCall(int categoryId, ICategory category);

    public event AddCategoryToPlaceCall onAddCategoryToPlaceCall;
    public event EditCategoryCall onEditCategoryCall;

    void NewCategoryCreated(int categoryId);
    void CategoryUpdated(int categoryId);


}
public interface ICategoriesListView
{

    public delegate void GetAllCategoriesByPlaceCall(int placeId);
    public delegate void SearchCategoriesByPlaceCall(int placeId, string term);
    public delegate void ClickCategoryCall(int id);


    public event GetAllCategoriesByPlaceCall onGetAllCategoriesByPlaceCall;
    public event SearchCategoriesByPlaceCall onSearchCategoriesByPlaceCall;
    public event ClickCategoryCall onClickCategoryCall;

    void CreateCategoriesList(ICategoryList categoriesList);
    void ClickCategory(ICategory selectedCategory);

}

public class CategoriesListView : MonoBehaviour, ICategoriesListView, ICategoryEditView
{

    public Image listElement;
    public GameObject listParent;

    public Image listElement2;
    public GameObject listParent2;

    public GameObject detailsPanel;
    public GameObject newPanel;
    public InputField searchTerm;

    public GameObject titlePanel;
    public GameObject editPanel;
    public GameObject savePanel;

    public event ICategoriesListView.GetAllCategoriesByPlaceCall onGetAllCategoriesByPlaceCall;
    public event ICategoriesListView.SearchCategoriesByPlaceCall onSearchCategoriesByPlaceCall;
    public event ICategoriesListView.ClickCategoryCall onClickCategoryCall;

    public event ICategoryEditView.AddCategoryToPlaceCall onAddCategoryToPlaceCall;
    public event ICategoryEditView.EditCategoryCall onEditCategoryCall;

    // Start is called before the first frame update
    void Start()
    {

        onGetAllCategoriesByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
    }


    public void searchCategories()
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);
        savePanel.SetActive(false);

        string text = searchTerm.transform.Find("Text").GetComponent<Text>().text.ToString();
        //httpRequest.SearchCategories(PlayerPrefs.GetInt("PlaceId"), text);
        onSearchCategoriesByPlaceCall(PlayerPrefs.GetInt("PlaceId"), text);
    }

    public void EditCategory()
    {
        titlePanel.SetActive(false);
        editPanel.SetActive(true);
        savePanel.SetActive(true);

        editPanel.transform.Find("Title Input").GetComponent<InputField>().text = titlePanel.transform.Find("Title Text").GetComponent<Text>().text;
    }

    public void SaveCategory()
    {
        ICategory category = SetCategoryValues();
        //string json = category.CategoryToJson();

        //httpRequest.EditCategory(PlayerPrefs.GetInt("CategoryId"), json);
        onEditCategoryCall(PlayerPrefs.GetInt("CategoryId"), category);
    }
    
    public ICategory SetCategoryValues()
    {
        string categoryName = editPanel.transform.Find("Title Input").Find("Text").GetComponent<Text>().text.ToString();

        ICategory category = new Category();
        category.Name = categoryName;
        return category;
    }

    public ICategory SetNewCategoryValues()
    {
        string categoryName = newPanel.transform.Find("Data Panel").Find("Category Name Input").Find("Text").GetComponent<Text>().text.ToString();

        ICategory category = new Category();
        category.Name = categoryName;
        return category;
    }

    public void CreateNewCategory()
    {
        ICategory category = SetNewCategoryValues();


        onAddCategoryToPlaceCall(PlayerPrefs.GetInt("PlaceId"), category);
    }


    public void CreateCategoriesList(ICategoryList categoriesList)
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        foreach (Transform child in listParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < categoriesList.Categories.Count; i++)
        {
            Image prefabInstance = Instantiate(listElement) as Image;
            Debug.Log(categoriesList.Categories[i].id);
            int id = categoriesList.Categories[i].id;
            prefabInstance.GetComponent<Button>().onClick.AddListener(delegate { onClickCategoryCall(id); });

            prefabInstance.transform.Find("Category Text").GetComponent<Text>().text = categoriesList.Categories[i].name;
            prefabInstance.transform.Find("Category Targets Value Text").GetComponent<Text>().text = categoriesList.Categories[i].targets.Count.ToString();


            prefabInstance.rectTransform.SetParent(listParent.transform);
            prefabInstance.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        }
        if (categoriesList.Categories.Count > 0)
        {
            if(onClickCategoryCall != null)
            {
                onClickCategoryCall(categoriesList.Categories[0].id);

            }

        }

        //Debug.Log(placeList.places.Count);    
    }

        public void ClickCategory(ICategory selectedCategory)
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);
        savePanel.SetActive(false);

        PlayerPrefs.SetInt("CategoryId", selectedCategory.Id);

        detailsPanel.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>().text = selectedCategory.Name;

        foreach (Transform child in listParent2.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < selectedCategory.Targets.Count; i++)
        {
            Image prefabInstance = Instantiate(listElement2) as Image;
            Debug.Log(selectedCategory.Targets[i].id);
            int selectedId = selectedCategory.Targets[i].id;

            prefabInstance.transform.Find("Target Name Text").GetComponent<Text>().text = selectedCategory.Targets[i].name;

            prefabInstance.rectTransform.SetParent(listParent2.transform);
            prefabInstance.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        }
    }

    public void NewCategoryCreated(int categoryId)
    {
        PlayerPrefs.SetInt("CategoryId", categoryId);
        onGetAllCategoriesByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
    }

    public void CategoryUpdated(int categoryId)
    {
        titlePanel.transform.Find("Title Text").GetComponent<Text>().text = editPanel.transform.Find("Title Input").Find("Text").GetComponent<Text>().text;

        titlePanel.SetActive(true);
        editPanel.SetActive(false);

        onGetAllCategoriesByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
    }
}

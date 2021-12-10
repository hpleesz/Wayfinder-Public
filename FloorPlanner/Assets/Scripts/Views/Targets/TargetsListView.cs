using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public interface ITargetInfoView
{
    public delegate void GetTargetCall(int targetId);

    public event GetTargetCall onGetTargetCall;
    void ShowTarget(ITarget target);

}

public interface ITargetEditView
{
    public delegate void AddTargetToFloorCall(int floorId, ITarget target);
    public delegate void EditTargetCall(int targetId, ITarget target);

    public event AddTargetToFloorCall onAddTargetToFloorCall;
    public event EditTargetCall onEditTargetCall;

    void NewTargetCreated(int targetId);
    void TargetUpdated(int targetId);
}
public interface ITargetsListView
{

    public delegate void GetAllTargetsByPlaceCall(int placeId);
    public delegate void GetAllTargetsByFloorCall(int floorId);

    public delegate void SearchTargetsByPlaceCall(int placeId, string term);
    public delegate void SearchTargetsByFloorAndCategoryCall(int floorId, string term, int category);

    public delegate void ClickTargetCall(int id);


    public event GetAllTargetsByPlaceCall onGetAllTargetsByPlaceCall;
    public event GetAllTargetsByFloorCall onGetAllTargetsByFloorCall;

    public event SearchTargetsByPlaceCall onSearchTargetsByPlaceCall;
    public event SearchTargetsByFloorAndCategoryCall onSearchTargetsByFloorAndCategoryCall;

    public event ClickTargetCall onClickTargetCall;

    void CreateTargetsList(ITargetList targetsList);

    void ClickTarget(ITarget selectedTarget);

}


public class TargetsListView : MonoBehaviour, ITargetsListView, IFloorsListView, ICategoriesListView, ITargetEditView
{
    //private TargetList targetList;
    //public HttpRequest httpRequest;
    public Image listElement;
    public GameObject listParent;
    public GameObject detailsPanel;
    public GameObject newPanel;
    public InputField searchTerm;



    public Dropdown floorDropdown;
    public Dropdown categoryDropdown;

    private List<int> categories = new List<int>();
    private List<int> floors = new List<int>();




    public event ITargetsListView.GetAllTargetsByPlaceCall onGetAllTargetsByPlaceCall;
    public event ITargetsListView.GetAllTargetsByFloorCall onGetAllTargetsByFloorCall;
    public event ITargetsListView.SearchTargetsByPlaceCall onSearchTargetsByPlaceCall;
    public event ITargetsListView.SearchTargetsByFloorAndCategoryCall onSearchTargetsByFloorAndCategoryCall;
    public event ITargetsListView.ClickTargetCall onClickTargetCall;


    public event IFloorsListView.GetAllFloorsByPlaceCall onGetAllFloorsByPlaceCall;
    public event IFloorsListView.SearchFloorsByPlaceCall onSearchFloorsByPlaceCall;
    public event IFloorsListView.ClickFloorCall onClickFloorCall;


    public event ICategoriesListView.GetAllCategoriesByPlaceCall onGetAllCategoriesByPlaceCall;
    public event ICategoriesListView.SearchCategoriesByPlaceCall onSearchCategoriesByPlaceCall;
    public event ICategoriesListView.ClickCategoryCall onClickCategoryCall;


    public event ITargetEditView.AddTargetToFloorCall onAddTargetToFloorCall;
    public event ITargetEditView.EditTargetCall onEditTargetCall;

    // Start is called before the first frame update
    void Start()
    {
        onGetAllTargetsByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
        onGetAllFloorsByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
        onGetAllCategoriesByPlaceCall(PlayerPrefs.GetInt("PlaceId"));

    }


    public void searchTargets()
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        string text = searchTerm.transform.Find("Text").GetComponent<Text>().text.ToString();
        //httpRequest.SearchTargets(PlayerPrefs.GetInt("PlaceId"), text);
        onSearchTargetsByPlaceCall(PlayerPrefs.GetInt("PlaceId"), text);
    }

    public void CreateTargetsList(ITargetList targetsList)
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        foreach (Transform child in listParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }


        for (int i = 0; i < targetsList.Targets.Count; i++)
        {
            Image prefabInstance = Instantiate(listElement) as Image;
            Debug.Log(targetsList.Targets[i].id);
            int id = targetsList.Targets[i].id;
            prefabInstance.GetComponent<Button>().onClick.AddListener(delegate { onClickTargetCall(id); });

            prefabInstance.transform.Find("Target Name Text").GetComponent<Text>().text = targetsList.Targets[i].name;
            prefabInstance.transform.Find("Target Floor Value Text").GetComponent<Text>().text = targetsList.Targets[i].floor.name;

            prefabInstance.rectTransform.SetParent(listParent.transform);
            prefabInstance.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        }
        if (targetsList.Targets.Count > 0)
        {
            onClickTargetCall(targetsList.Targets[0].id);
        }
    }

    public void ClickTarget(ITarget selectedTarget)
    {
        detailsPanel.SetActive(true);
        newPanel.SetActive(false);

        PlayerPrefs.SetInt("TargetId", selectedTarget.Id);


        detailsPanel.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>().text = selectedTarget.Name;
        //detailsPanel.transform.Find("Title Panel").Find("Number Text").GetComponent<Text>().text = selectedFloor.number.ToString();
        detailsPanel.transform.Find("Details Panel").Find("Category Value Text").GetComponent<Text>().text = selectedTarget.Category.name;
        detailsPanel.transform.Find("Details Panel").Find("Floor Value Text").GetComponent<Text>().text = selectedTarget.Floor.name;
        detailsPanel.transform.Find("Details Panel").Find("X Value Text").GetComponent<Text>().text = selectedTarget.XCoordinate.ToString();
        detailsPanel.transform.Find("Details Panel").Find("Y Value Text").GetComponent<Text>().text = selectedTarget.YCoordinate.ToString();
        detailsPanel.transform.Find("Details Panel").Find("Z Value Text").GetComponent<Text>().text = selectedTarget.ZCoordinate.ToString();
    }

    public void CreateFloorsList(IFloorList floorsList)
    {
        for (int i = 0; i < floorsList.Floors.Count; i++)
        {

            floorDropdown.options.Add(new Dropdown.OptionData() { text = floorsList.Floors[i].Number + ". " + floorsList.Floors[i].Name });
            floors.Add(floorsList.Floors[i].Id);

        }
        if (floorsList.Floors.Count > 0)
        {
            floorDropdown.value = 0;
        }
    }

    public void ClickFloor(IFloor selectedFloor)
    {
        throw new System.NotImplementedException();
    }

    public void CreateCategoriesList(ICategoryList categoriesList)
    {

        for (int i = 0; i < categoriesList.Categories.Count; i++)
        {
            categoryDropdown.options.Add(new Dropdown.OptionData() { text = categoriesList.Categories[i].name });
            categories.Add(categoriesList.Categories[i].id);
        }
        if (categoriesList.Categories.Count > 0)
        {
            categoryDropdown.value = 0;

        }
    }

    public void CreateNewTarget()
    {
        string name = gameObject.transform.Find("New Panel").Find("Title Panel Edit").Find("Title Input").Find("Text").GetComponent<Text>().text.ToString();

        Target target = new Target();
        Category category = new Category();
        category.id = categories[categoryDropdown.value];
        target.name = name;
        target.category = category;

        onAddTargetToFloorCall(floors[floorDropdown.value], target);

    }
    public void ClickCategory(ICategory selectedCategory)
    {
        throw new System.NotImplementedException();
    }

    public void NewTargetCreated(int targetId)
    {
        PlayerPrefs.SetInt("TargetId", targetId);
        onGetAllTargetsByPlaceCall(PlayerPrefs.GetInt("PlaceId"));
    }

    public void TargetUpdated(int targetId)
    {
        throw new System.NotImplementedException();
    }
}

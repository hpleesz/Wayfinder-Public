using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HttpRequestSingleton;

public interface ITargetPathSteps
{
    public delegate void IdResult(int id);
    public delegate void TargetPathStepsResult(ITargetPathSteps targetPathSteps);

    event TargetPathStepsResult onGetTargetPathResult;

    public Target Target { get; set; }
    public List<FloorSwitcherPointStep> FloorSwitcherPointSteps { get; set; }


    public ITargetPathSteps TargetPathStepsFromJson(string json);
    public string TargetPathStepsToJson();

    public void GetTargetPath(int targetId, PathStepList pathStepsList);

}

[System.Serializable]
public class TargetPathSteps: ITargetPathSteps
{
    private event Result onGetTargetPath;
    public event ITargetPathSteps.TargetPathStepsResult onGetTargetPathResult;


    public Target target;

    public List<FloorSwitcherPointStep> floorSwitcherPointSteps;

    public Target Target
    {
        get { return target; }
        set { target = value; }
    }
    public List<FloorSwitcherPointStep> FloorSwitcherPointSteps
    {
        get { return floorSwitcherPointSteps; }
        set { floorSwitcherPointSteps = value; }
    }

    public ITargetPathSteps TargetPathStepsFromJson(string json)
    {
        return JsonUtility.FromJson<TargetPathSteps>(json);
    }

    public string TargetPathStepsToJson()
    {
        return JsonUtility.ToJson(this);
    }


    //GET TARGET
    public void GetTargetPath(int targetId, PathStepList pathStepsList)
    {

        onGetTargetPath += onResultGetTargetPath;
        string json = pathStepsList.PathStepListToJson();
        HttpRequestSingleton.Instance.CallPost("/targets/" + targetId + "/path",json, onGetTargetPath);
    }

    private void onResultGetTargetPath(byte[] result)
    {
        string targetJson = System.Text.Encoding.UTF8.GetString(result);
        ITargetPathSteps target = new TargetPathSteps();
        target = target.TargetPathStepsFromJson(targetJson);
        onGetTargetPathResult(target);
    }

}


public interface ITargetPathStepsList
{
    public delegate void TargetPathStepsListResult(ITargetPathStepsList targetModelList);


    event TargetPathStepsListResult onGetAllTargetDistancesByPlaceResult;
    event TargetPathStepsListResult onSearchTargetDistancesByPlaceAndCategoryResult;


    public List<TargetPathSteps> TargetPathSteps { get; set; }

    public ITargetPathStepsList TargetPathStepsListFromJson(string json);
    public string TargetPathStepsListToJson();


    public void GetAllTargetDistancesByPlace(int placeId, PathStepList pathStepList);
    public void SearchTargetDistancesByPlaceAndCategory(int placeId, string term, string category, PathStepList pathStep);

}


public class TargetPathStepsList : ITargetPathStepsList
{
    private event Result onGetAllTargetDistancesByPlace;
    private event Result onSearchTargetDistancesByPlaceAndCategory;

    public List<TargetPathSteps> targetPathSteps;

    public List<TargetPathSteps> TargetPathSteps
    {
        get { return targetPathSteps; }
        set { targetPathSteps = value; }
    }

    public event ITargetPathStepsList.TargetPathStepsListResult onGetAllTargetDistancesByPlaceResult;
    public event ITargetPathStepsList.TargetPathStepsListResult onSearchTargetDistancesByPlaceAndCategoryResult;

    public ITargetPathStepsList TargetPathStepsListFromJson(string json)
    {
        return JsonUtility.FromJson<TargetPathStepsList>(json);
    }


    public void GetAllTargetDistancesByPlace(int placeId, PathStepList pathStepList)
    {
        onGetAllTargetDistancesByPlace += onResultGetAllTargetDistancesByPlace;
        string json = pathStepList.PathStepListToJson();
        HttpRequestSingleton.Instance.CallPost("/places/" + placeId + "/target-distances", json, onGetAllTargetDistancesByPlace);
    }

    private void onResultGetAllTargetDistancesByPlace(byte[] result)
    {
        string targetsJson = System.Text.Encoding.UTF8.GetString(result);
        targetPathSteps = TargetPathStepsListFromJson(targetsJson).TargetPathSteps;
        onGetAllTargetDistancesByPlaceResult(this);

    }

    public void SearchTargetDistancesByPlaceAndCategory(int placeId, string term, string category, PathStepList pathStepList)
    {
        onSearchTargetDistancesByPlaceAndCategory += onResultSearchTargetDistancesByPlaceAndCategory;
        string json = pathStepList.PathStepListToJson();
        HttpRequestSingleton.Instance.CallPost("/places/" + placeId + "/target-distances/category-search?term=" + term + "&category=" + category, json, onSearchTargetDistancesByPlaceAndCategory);
    }
    private void onResultSearchTargetDistancesByPlaceAndCategory(byte[] result)
    {
        string targetsJson = System.Text.Encoding.UTF8.GetString(result);
        targetPathSteps = TargetPathStepsListFromJson(targetsJson).TargetPathSteps;
        onSearchTargetDistancesByPlaceAndCategoryResult(this);
    }

    public string TargetPathStepsListToJson()
    {
        return JsonUtility.ToJson(this);
    }
}


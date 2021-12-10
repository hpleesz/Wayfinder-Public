using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NavPlace()
    {
        SceneManager.LoadScene("Place");
    }

    public void NavFloors()
    {
        SceneManager.LoadScene("Floors");
    }
    public void NavTargets()
    {
        SceneManager.LoadScene("Targets");
    }
    public void NavMarkers()
    {
        SceneManager.LoadScene("Markers");
    }
    public void NavCategories()
    {
        SceneManager.LoadScene("Categories");
    }
    public void NavDownload()
    {
        SceneManager.LoadScene("QrDownload");
    }

    public void NavFloorSwitchers()
    {
        SceneManager.LoadScene("Floor Switchers");
    }

    public void NavFloorSwitcherPoints()
    {
        SceneManager.LoadScene("Floor Switcher Points");


    }
    public void NavBack()
    {
        SceneManager.LoadScene("Places");
    }

}

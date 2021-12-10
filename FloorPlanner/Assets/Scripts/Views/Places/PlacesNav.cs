using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlacesNav : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewPlace()
    {
        PlayerPrefs.SetInt("PlaceId", 0);
        SceneManager.LoadScene("Place Edit");
    }

    public void EditPlace()
    {
        SceneManager.LoadScene("Place Edit");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ViewPlace()
    {
        SceneManager.LoadScene("Menu");
    }

}

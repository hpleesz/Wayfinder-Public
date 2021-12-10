using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorsNav : MonoBehaviour
{
    public GameObject detailsPanel;
    public GameObject newPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewFloor()
    {
        //PlayerPrefs.SetInt("FloorId", 0);
        detailsPanel.SetActive(false);
        newPanel.SetActive(true);
        //SceneManager.LoadScene("Place Edit");
    }

    public void ViewFloor()
    {
        SceneManager.LoadScene("Floor");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetsNav : MonoBehaviour
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

    public void NewTarget()
    {
        //PlayerPrefs.SetInt("FloorId", 0);
        detailsPanel.SetActive(false);
        newPanel.SetActive(true);
        //SceneManager.LoadScene("Place Edit");
    }

    public void ViewTarget()
    {
        SceneManager.LoadScene("Target Drawer");
    }

    public void EditVirtualObject()
    {
        SceneManager.LoadScene("Virtual Object Drawer");
    }


    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCast : MonoBehaviour
{
    int finalmask;

    // Start is called before the first frame update
    void Start()
    {

        //path layer
        int layer0 = 0;
        int layer1 = 1;
        int layer2 = 2;
        int layer3 = 3;
        int layer4 = 4;
        int layer5 = 5;
        int layer6 = 6;
        int layer7 = 7;
        int layer8 = 8;
        int layer9 = 9;
        int layer10 = 10;
        int layer11 = 11;

        int layermask0 = 1 << layer0;
        int layermask1 = 1 << layer1;
        int layermask2 = 1 << layer2;
        int layermask3 = 1 << layer3;
        int layermask4 = 1 << layer4;
        int layermask5 = 1 << layer5;
        int layermask6 = 1 << layer6;
        int layermask7 = 1 << layer7;
        int layermask8 = 1 << layer8;
        int layermask9 = 1 << layer9;
        int layermask10 = 1 << layer10;
        int layermask11 = 1 << layer11;

        finalmask = layermask0 | layermask1 | layermask2 | layermask3 | layermask4 | layermask5 | layermask6 | layermask7 | layermask8 | layermask9 | layermask10 | layermask11;
    }
    
    // Update is called once per frame
    void Update()
    {
        GameObject[] linecastObjects = GameObject.FindGameObjectsWithTag("Linecast");
        RaycastHit hit;
        foreach (GameObject linecastObject in linecastObjects)
        {
            string virtualName = linecastObject.name.Replace("CubeM", "CubeV");
            if (Physics.Linecast(transform.position, linecastObject.transform.position, out hit, ~finalmask))
            {
                GameObject.Find(virtualName).transform.Find("Quad").GetComponent<MeshRenderer>().enabled = false;

            }
            else
            {

                GameObject.Find(virtualName).transform.Find("Quad").GetComponent<MeshRenderer>().enabled = true;

            }
        }
        GameObject[] linecastObjects2 = GameObject.FindGameObjectsWithTag("Linecast2");
        RaycastHit hit2;
        foreach (GameObject linecastObject in linecastObjects2)
        {
            string virtualName = linecastObject.name.Replace("FloorSwitcherPointM", "FloorSwitcherPointV");
            if (Physics.Linecast(transform.position, linecastObject.transform.position, out hit, ~finalmask))
            {
                GameObject.Find(virtualName).transform.GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find(virtualName).transform.Find("Canvas").gameObject.SetActive(false);

            }
            else
            {
                GameObject.Find(virtualName).transform.GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find(virtualName).transform.Find("Canvas").gameObject.SetActive(true);
            }
        }
    }
    
    public Material invisibleMaterial;
    public void Linecast()
    {
        int layer1 = 11;
        //path layer
        int layer2 = 6;
        int layermask1 = 1 << layer1;
        int layermask2 = 1 << layer2;
        int finalmask = layermask1 | layermask2;

        GameObject[] linecastObjects = GameObject.FindGameObjectsWithTag("Linecast");
        RaycastHit hit;
        foreach (GameObject linecastObject in linecastObjects)
        {
            Debug.DrawLine(GameObject.Find("User").transform.position, linecastObject.transform.position, Color.red, 100, true);

            if (Physics.Linecast(GameObject.Find("User").transform.position, linecastObject.transform.position, out hit, ~finalmask))
            {
                Debug.Log(linecastObject.transform.parent.name + " " + linecastObject.name + " blocked" + "     " + hit.collider.name);

                linecastObject.GetComponent<Renderer>().material.color = Color.white;

            }
            else
            {
                linecastObject.GetComponent<Renderer>().material.color = Color.green;

            }
        }
    }
}

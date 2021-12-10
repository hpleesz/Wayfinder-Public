using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Parabox.Stl;
using UnityEngine.AI;

public class FloorSwitcherPointDrawer : MonoBehaviour, IFloorSwitcherPointInfoView
{
    public Material material;
    public FloorSwitcherPointPlacer floorSwitcherPointPlacer;
    public static IFloorSwitcherPoint floorSwitcherPoint;
    public NavMeshSurface surface;

    public event IFloorSwitcherPointInfoView.GetFloorSwitcherPointCall onGetFloorSwitcherPointCall;



    // Start is called before the first frame update
    void Start()
    {
        onGetFloorSwitcherPointCall(PlayerPrefs.GetInt("FloorSwitcherPointId"));
    }

    public void Back()
    {
        SceneManager.LoadScene("Floor Switcher Points");

    }

    void Import(string path)
    {

        Mesh[] meshes = Importer.Import(HttpRequestSingleton.SERVER_URL + "/" + path);
        //Debug.Log(HttpRequest.SERVER_URL + "/Models/3DModel-13.stl");
        Debug.Log(meshes.Length);
        GameObject[] gos = new GameObject[1];
        foreach (Mesh mesh in meshes)
        {
            GameObject go = new GameObject();
            go.AddComponent<MeshRenderer>().sharedMaterial = material;
            go.AddComponent<MeshFilter>().sharedMesh = mesh;
            go.tag = "Model";
            go.layer = 6;
            go.AddComponent<MeshCollider>();

            Debug.Log(go.GetComponent<MeshRenderer>().bounds.size);
            float y = go.GetComponent<MeshRenderer>().bounds.size.y / 2;
            go.transform.localPosition = new Vector3(0, y, 0);
            go.name = "Floor Model";

            floorSwitcherPointPlacer.floorSwitcherPoint = go;

            go.AddComponent<NavMeshModifier>();
            go.GetComponent<NavMeshModifier>().overrideArea = true;
            go.GetComponent<NavMeshModifier>().area = 1;

            
            float xFloor = go.GetComponent<MeshRenderer>().bounds.size.x;
            float zFloor = go.GetComponent<MeshRenderer>().bounds.size.z;
            float yFloor = go.GetComponent<MeshRenderer>().bounds.size.y;

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.position = new Vector3(0, -yFloor/2, 0);
            cube.transform.position = new Vector3(go.GetComponent<Renderer>().bounds.center.x, -yFloor / 2, go.GetComponent<Renderer>().bounds.center.z);

            cube.transform.localScale = new Vector3(xFloor * 1.2f, 0.1f, zFloor * 1.2f);
            cube.layer = 7;
            //cube.GetComponent<MeshRenderer>().material.color = Color.clear;

            //GameObject.Find("Plane").transform.localScale = new Vector3(xFloor * 1.2f, 1f, zFloor * 1.2f);

            surface.BuildNavMesh();
            //Debug.Log(surface.navMeshData.sourceBounds);
        }

    }


    public void ShowFloorSwitcherPoint(IFloorSwitcherPoint floorSwitcherPoint)
    {
        FloorSwitcherPointDrawer.floorSwitcherPoint = floorSwitcherPoint;
        gameObject.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>().text = floorSwitcherPoint.Name;
        //gameObject.transform.Find("Title Panel").Find("Number Text").GetComponent<Text>().text = floor.number.ToString();
        Import(floorSwitcherPoint.Floor.FloorPlan3D.FileLocation);
    }
}

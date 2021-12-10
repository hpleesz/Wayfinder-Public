using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Parabox.Stl;
using UnityEngine.AI;

public class TargetDrawer : MonoBehaviour, ITargetInfoView
{
    public Material material;
    public TargetPlacer targetPlacer;
    public NavMeshSurface surface;
    public static ITarget target;

    public event ITargetInfoView.GetTargetCall onGetTargetCall;

    // Start is called before the first frame update
    void Start()
    {

        onGetTargetCall(PlayerPrefs.GetInt("TargetId"));
    }

    public void Back()
    {
        SceneManager.LoadScene("Targets");

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

            targetPlacer.target = go;

            go.AddComponent<NavMeshModifier>();
            go.GetComponent<NavMeshModifier>().overrideArea = true;
            go.GetComponent<NavMeshModifier>().area = 1;


            float xFloor = go.GetComponent<MeshRenderer>().bounds.size.x;
            float zFloor = go.GetComponent<MeshRenderer>().bounds.size.z;
            float yFloor = go.GetComponent<MeshRenderer>().bounds.size.y;

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.position = new Vector3(0, -yFloor / 2, 0);
            cube.transform.position = new Vector3(go.GetComponent<Renderer>().bounds.center.x, -yFloor / 2, go.GetComponent<Renderer>().bounds.center.z);

            cube.transform.localScale = new Vector3(xFloor * 1.2f, 0.1f, zFloor * 1.2f);
            cube.layer = 7;
            //cube.GetComponent<MeshRenderer>().material.color = Color.clear;

            //GameObject.Find("Plane").transform.localScale = new Vector3(xFloor * 1.2f, 1f, zFloor * 1.2f);

            surface.BuildNavMesh();
            //Debug.Log(surface.navMeshData.sourceBounds);
        }

    }

    public void ShowTarget(ITarget target)
    {
        TargetDrawer.target = target;
        gameObject.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>().text = target.Name;
        //gameObject.transform.Find("Title Panel").Find("Number Text").GetComponent<Text>().text = floor.number.ToString();
        Import(target.Floor.floorPlan3D.fileLocation);
    }
}

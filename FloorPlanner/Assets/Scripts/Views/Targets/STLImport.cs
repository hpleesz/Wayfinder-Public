using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.Stl;

public class STLImport : MonoBehaviour
{
    // Start is called before the first frame update
    public Material material;
    public TargetPlacer targetPlacer;
    
    void Start()
    {
        Debug.Log("https://localhost:44397/Models/3DModel-13.stl" + "/Models/3DModel-13.stl");

        Mesh[] meshes = Importer.Import(HttpRequestSingleton.SERVER_URL + "/Models/3DModel-13.stl");
        //Debug.Log(HttpRequest.SERVER_URL + "/Models/3DModel-13.stl");
        Debug.Log(meshes.Length);
        GameObject[] gos = new GameObject[1];
        foreach (Mesh mesh in meshes)
        {
            GameObject go = new GameObject();
            go.AddComponent<MeshRenderer>().sharedMaterial = material;
            go.AddComponent<MeshFilter>().sharedMesh = mesh;
            go.tag = "Model";
            go.AddComponent<MeshCollider>();

            Debug.Log(go.GetComponent<MeshRenderer>().bounds.size);
            float y = go.GetComponent<MeshRenderer>().bounds.size.y / 2;
            go.transform.localPosition = new Vector3(0, y, 0);
            targetPlacer.target = go;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

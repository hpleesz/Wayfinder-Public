using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera Cam;
    public NavMeshAgent agent;
    public Material material;
    public GameObject prefab;
    public GameObject virtualPathPrefab;



    public void setPath(Vector3 targetPoint)
    {

        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(agent.gameObject.transform.position, targetPoint, NavMesh.AllAreas, path);
        Vector3[] corners = path.corners;



        //QR kód
        GameObject dummyUser = GameObject.Find("UserDummy");


        GameObject trackedImageDummy = GameObject.Find("TrackedImageDummy");


        if (corners.Length >= 2)
        {
            Vector3 previousCorner = corners[0];

            float lengthSoFar = 0.0F;

            //float lengthSoFar = 0.0F;
            int i = 1;
            while (i < corners.Length)
            {
                Vector3 currentCorner = path.corners[i];

                
                //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                
                GameObject cube = GameObject.Instantiate(prefab);
                Vector3 between = previousCorner - currentCorner;
                float distance = between.magnitude;
                cube.transform.localScale = new Vector3(0.1f, 0.1f, distance);
                cube.transform.position = currentCorner + new Vector3(between.x / 2.0f, between.y / 2.0f, between.z / 2.0f);
                cube.transform.LookAt(previousCorner);
                cube.layer = 6;
                foreach (Transform child in cube.transform)
                {
                    child.gameObject.layer = 6;
                }
                cube.GetComponent<MeshRenderer>().material = material;
                cube.name = "Path " + i;
                cube.tag = "PathMap";

                cube.transform.SetParent(dummyUser.transform);


                GameObject go = GameObject.Instantiate(cube);
                go.transform.SetParent(trackedImageDummy.transform);

                go.transform.localPosition = cube.transform.localPosition;
                go.transform.localRotation = cube.transform.localRotation;
                go.transform.localScale = new Vector3(cube.transform.localScale.x / trackedImageDummy.transform.localScale.x, cube.transform.localScale.y / trackedImageDummy.transform.localScale.y, cube.transform.localScale.z / trackedImageDummy.transform.localScale.z);
                go.layer = 0;
                go.name = "Path " + i;
                go.tag = "PathVirtual";
                go.GetComponent<MeshRenderer>().enabled = false;

                foreach (Transform child in go.transform)
                {
                    child.gameObject.layer = 0;
                    child.GetComponent<MeshRenderer>().enabled = false;

                }
                lengthSoFar += Vector3.Distance(previousCorner, currentCorner);
               
                previousCorner = currentCorner;

                Transform[] children = dummyUser.transform.GetComponentsInChildren<Transform>().Where(t => t.name == "Path " + i).ToArray();
                Debug.Log(children.Length);
                if(children.Length > 1)
                {
                    Destroy(cube);
                    Destroy(go);
                }
                i++;
            }

        }

        setPathOcclusion();

        //targetPoint = new Vector3();
    }


    public void setPathOcclusion()
    {
        setMapLinecastCubes();
        setVirtualLinecastCubes();
    }

    public void setVirtualLinecastCubes()
    {
        GameObject trackedImageDummy = GameObject.Find("TrackedImageDummy");

        Transform[] children = trackedImageDummy.GetComponentsInChildren<Transform>();

        int i = 0;

        foreach (Transform child in children)
        {
            if(child.name.Contains("Path"))
            {
                double diff = 1;
                for (double d = 0; d < 0.5; d = d + diff)
                {
                    GameObject mySphereAsChild = Instantiate(virtualPathPrefab);
                    mySphereAsChild.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    mySphereAsChild.transform.rotation = child.transform.rotation;
                    mySphereAsChild.transform.parent = child.transform;

                    mySphereAsChild.transform.localPosition = new Vector3(0, 0, (float)d);
                    mySphereAsChild.layer = 0;
                    mySphereAsChild.tag = "Mask";
                    mySphereAsChild.name = "CubeV" + i;
                    //mySphereAsChild.GetComponent<Renderer>().material = invisibleMaterial;

                    Transform[] c = child.transform.GetComponentsInChildren<Transform>().Where(t => t.name == "CubeV" + i).ToArray();
                    if (c.Length > 1)
                    {
                        Destroy(mySphereAsChild);
                    }

                    i++;

                    if (d != 0)
                    {
                        GameObject mySphereAsChildNeg = Instantiate(virtualPathPrefab);
                        mySphereAsChildNeg.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        mySphereAsChildNeg.transform.rotation = child.transform.rotation;
                        mySphereAsChildNeg.transform.parent = child.transform;

                        mySphereAsChildNeg.transform.localPosition = new Vector3(0, 0, -(float)d);
                        mySphereAsChildNeg.layer = 0;
                        mySphereAsChildNeg.tag = "Mask";
                        mySphereAsChildNeg.name = "CubeV" + i;
                        //mySphereAsChildNeg.GetComponent<Renderer>().material = invisibleMaterial;

                        Transform[] c2 = child.transform.GetComponentsInChildren<Transform>().Where(t => t.name == "CubeV" + i).ToArray();
                        if (c2.Length > 1)
                        {
                            Destroy(mySphereAsChildNeg);
                        }

                        i++;

                    }


                    diff = mySphereAsChild.transform.localScale.z;
                }
            }

        }

    }

    public void setMapLinecastCubes()
    {
        GameObject dummyUser = GameObject.Find("UserDummy");

        Transform[] children = dummyUser.GetComponentsInChildren<Transform>();
        int i = 0;

        foreach (Transform child in children)
        {
            if (child.name.Contains("Path"))
            {
                double diff = 1;
                for (double d = 0; d < 0.5; d = d + diff)
                {
                    GameObject mySphereAsChild = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    mySphereAsChild.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    mySphereAsChild.transform.rotation = child.transform.rotation;
                    mySphereAsChild.transform.parent = child.transform;

                    mySphereAsChild.transform.localPosition = new Vector3(0, 0, (float)d);
                    mySphereAsChild.layer = 11;
                    mySphereAsChild.tag = "Linecast";
                    mySphereAsChild.name = "CubeM" + i;
                    i++;

                    if (d != 0)
                    {
                        GameObject mySphereAsChildNeg = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        mySphereAsChildNeg.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        mySphereAsChildNeg.transform.rotation = child.transform.rotation;
                        mySphereAsChildNeg.transform.parent = child.transform;

                        mySphereAsChildNeg.transform.localPosition = new Vector3(0, 0, -(float)d);
                        mySphereAsChildNeg.layer = 11;
                        mySphereAsChildNeg.tag = "Linecast";
                        mySphereAsChildNeg.name = "CubeM" + i;
                        i++;

                    }


                    diff = mySphereAsChild.transform.localScale.z;
                }
            }

        }

    }

    public float CalculatePathLength(Vector3 target)
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(agent.gameObject.transform.position, target, NavMesh.AllAreas, path);
        Vector3[] corners = path.corners;

        float lengthSoFar = 0.0F;


        if (corners.Length >= 2)
        {
            Vector3 previousCorner = corners[0];


            //float lengthSoFar = 0.0F;
            int i = 1;
            while (i < corners.Length)
            {
                Vector3 currentCorner = path.corners[i];

                lengthSoFar += Vector3.Distance(previousCorner, currentCorner);

                previousCorner = currentCorner;
                i++;
            }

        }

        return lengthSoFar;


    }
}

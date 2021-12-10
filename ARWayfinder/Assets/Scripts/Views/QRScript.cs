using Dummiesman;
using Parabox.Stl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ZXing;
using static UnityEngine.XR.ARSubsystems.XRCpuImage;

public interface ITargetInfoView
{
    public delegate void GetQrCodeCall(int placeId, string qrValue);

    public event GetQrCodeCall onGetQrCodeCall;
    void QrCodeResult(ITarget target);
}

public interface IVirtualObjectsListView
{
    public delegate void GetAllVirtualObjectsByTargetCall(int targetId);

    public event GetAllVirtualObjectsByTargetCall onGetAllVirtualObjectsByTargetCall;

    void CreateVirtualObjectsList(IVirtualObjectList virtualObjects);
}

public interface ITargetPathStepsInfoView
{
    public delegate void GetTargetPathStepsCall(int targetId, PathStepList pathStepList);

    public event GetTargetPathStepsCall onGetTargetPathStepsCall;
    void TargetPathResult(ITargetPathSteps targetPathSteps);
}

public interface IFloorSwitcherPointsListView
{
    public delegate void GetAllFloorSwitcherPointsByFloorCall(int floorId);

    public event GetAllFloorSwitcherPointsByFloorCall onGetAllFloorSwitcherPointsByFloorCall;

    void CreateFloorSwitcherPointsList(IFloorSwitcherPointList floorSwitcherPointsList);

}

public class QRScript : MonoBehaviour, ITargetInfoView, IVirtualObjectsListView, ITargetPathStepsInfoView, IFloorSwitcherPointsListView
{

    public event ITargetInfoView.GetQrCodeCall onGetQrCodeCall;
    public event IVirtualObjectsListView.GetAllVirtualObjectsByTargetCall onGetAllVirtualObjectsByTargetCall;
    public event ITargetPathStepsInfoView.GetTargetPathStepsCall onGetTargetPathStepsCall;
    public event IFloorSwitcherPointsListView.GetAllFloorSwitcherPointsByFloorCall onGetAllFloorSwitcherPointsByFloorCall;

    public GameObject arObjectOnQRCode; //Reference to the AR Gameobject which needs to be placed on scanning the QRCode
    IBarcodeReader reader; //QRCode reading library
    ARCameraManager aRCamera;
    ARRaycastManager arRaycastManager;
    private Texture2D arCameraTexture; //Texture to hold the processed AR Camera frame
    private bool onlyonce;
    private bool requesting = false;

    private Gyroscope gyro;
    public NavMeshSurface surface;


    public static Vector3 CAMLOCALPOS;
    public static Vector3 CAMFORWARD;
    public static Vector3 CAMUP;
    public static Vector3 CAMRIGHT;
    public static Quaternion CAMROT;

    public static ITarget target;
    public static bool ready = false;

    void Start()
    {
        aRCamera = FindObjectOfType<ARCameraManager>(); //Load the ARCamera
        arRaycastManager = FindObjectOfType<ARRaycastManager>(); //Load the Raycast Manager
                                                                 //Get the ZXing Barcode/QRCode reader
        reader = new BarcodeReader();
        //Subscribe to read AR camera frames: Make sure this statement runs only once
        aRCamera.frameReceived += OnCameraFrameReceived;


    }

    void Awake()
    {
        
        gyro = Input.gyro;
        gyro.enabled = true;

        
    }

    public void t()
    {
        onGetQrCodeCall(6, "TARGET_25");

    }

    unsafe void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
    {
        if ((Time.frameCount % 15) == 0)
        { //You can set this number based on the frequency to scan the QRCode
            XRCpuImage image;
            if (aRCamera.TryAcquireLatestCpuImage(out image))
            {
                StartCoroutine(ProcessQRCode(image));
                image.Dispose();
            }
        }
    }

    //Asynchronously Convert to Grayscale and Color : https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@1.0/manual/cpu-camera-image.html
    IEnumerator ProcessQRCode(XRCpuImage image)
    {
        // Create the async conversion request
        var request = image.ConvertAsync(new ConversionParams
        {
            inputRect = new RectInt(0, 0, image.width, image.height),
            outputDimensions = new Vector2Int(image.width / 2, image.height / 2),
            // Color image format
            outputFormat = TextureFormat.RGB24,
            // Flip across the Y axis
            //  transformation = CameraImageTransformation.MirrorY
        });
        while (!request.status.IsDone())
            yield return null;
        // Check status to see if it completed successfully.



        if (request.status != AsyncConversionStatus.Ready)
        {
            // Something went wrong
            Debug.LogErrorFormat("Request failed with status {0}", request.status);
            // Dispose even if there is an error.
            request.Dispose();
            yield break;
        }



        // Image data is ready. Let's apply it to a Texture2D.
        var rawData = request.GetData<byte>();
        // Create a texture if necessary
        if (arCameraTexture == null)
        {
            arCameraTexture = new Texture2D(
request.conversionParams.outputDimensions.x,
request.conversionParams.outputDimensions.y,
request.conversionParams.outputFormat,
false);
        }
        // Copy the image data into the texture
        arCameraTexture.LoadRawTextureData(rawData);
        arCameraTexture.Apply();
        byte[] barcodeBitmap = arCameraTexture.GetRawTextureData();
        LuminanceSource source = new RGBLuminanceSource(barcodeBitmap, arCameraTexture.width, arCameraTexture.height);
        //Send the source to decode the QRCode using ZXing
        if(true)//if (!onlyonce)
        { //Check if a frame is already being decoded for QRCode. If not, get inside the block.
            onlyonce = true; //Now frame is being decoded for a QRCode
                             //decode QR Code
            var result = reader.Decode(source);
            if (result != null && result.Text != "")
            { //If QRCode found inside the frame
                var QRContents = result.Text;


                if (!requesting)
                {
                    requesting = true;
                    //string[] words = result.Text.Split('_');
                    //httpRequest.GetTarget(Int32.Parse(words[0]));
                    //httpRequest.GetQRCodeByPlace(PlayerPrefs.GetInt("PlaceId"), result.Text);
                    onGetQrCodeCall(PlayerPrefs.GetInt("PlaceId"), result.Text);
                }



                // Get the resultsPoints of each qr code contain the following points in the following order: index 0: bottomLeft index 1: topLeft index 2: topRight
                //Note this depends on the oreintation of the QRCode. The below part is mainly finding the mid of the QRCode using result points and making a raycast hit from that pose.
                ResultPoint[] resultPoints = result.ResultPoints;
                ResultPoint a = resultPoints[1];
                ResultPoint b = resultPoints[2];
                ResultPoint c = resultPoints[0];
                Vector2 pos1 = new Vector2((float)a.X, (float)a.Y);
                Vector2 pos2 = new Vector2((float)b.X, (float)b.Y);
                Vector2 pos3 = new Vector2((float)c.X, (float)c.Y);
                Vector2 pos4 = new Vector2(((float)b.X - (float)a.X) / 2.0f, ((float)c.Y - (float)a.Y) / 2.0f);
                List<ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();
                //Make a raycast hit to get the pose of the QRCode detected to place an object around it.

      

                onlyonce = false;
            }
            else
            {

                onlyonce = false;  //QRCode not found in the frame. Continue processing next frame for QRCode
            }
        }
        else
        {

        }
    }



    public Material material;
    public GameObject parent;

    public static bool NewQrCode = false;


    public static List<VirtualObject> virtualObjects;









    //CHECK

    private PathStepList pathStepList;
    private Dictionary<int, FloorSwitcherPoint> floorSwitchers;
    public PlayerController playerController;
    public GameObject floorSwitcherPrefab;


    public void QrCodeResult(ITarget target)
    {
        QRScript.target = target;

        NewQrCode = true;
        int floorId = PlayerPrefs.GetInt("FloorId");

        GameObject nav = GameObject.Find("Nav");
        GameObject nav2 = GameObject.Find("Nav2");

        PlayerPrefs.SetInt("CurrentTargetId", target.Id);

        onGetAllVirtualObjectsByTargetCall(target.Id);
        floorId = 0;
        if (target.Floor.id != floorId)
        {
            if (parent.transform.Find("Walls") != null)
            {
                Destroy(GameObject.Find("Walls").gameObject);
            }

            if (parent.transform.Find("Walls2") != null)
            {
                Destroy(GameObject.Find("Walls2").gameObject);
            }
            if (parent.transform.Find("Floor") != null)
            {
                Destroy(GameObject.Find("Floor").gameObject);
            }

            PlayerPrefs.SetInt("FloorId", target.Floor.id);
            //Mesh[] meshes = Importer.Import(HttpRequestSingleton.SERVER_URL + "/Models/3DModel-" + target.Floor.id + ".stl");
            Mesh[] meshes = Importer.Import(HttpRequestSingleton.SERVER_URL + "/" + target.Floor.floorPlan3D.fileLocation);
            foreach (Mesh mesh in meshes)
            {
                GameObject go = new GameObject();
                go.AddComponent<MeshRenderer>().sharedMaterial = material;
                go.AddComponent<MeshFilter>().sharedMesh = mesh;
                go.tag = "Model";
                go.layer = 6;
                go.AddComponent<MeshCollider>();
                go.name = "Walls";

                float y = go.GetComponent<MeshRenderer>().bounds.size.y / 2;
                go.transform.position = new Vector3(0, y, 0);
                go.transform.SetParent(parent.transform);


                go.AddComponent<NavMeshModifier>();
                go.GetComponent<NavMeshModifier>().overrideArea = true;
                go.GetComponent<NavMeshModifier>().area = 1;

                float xFloor = go.GetComponent<MeshRenderer>().bounds.size.x;
                float zFloor = go.GetComponent<MeshRenderer>().bounds.size.z;
                float yFloor = go.GetComponent<MeshRenderer>().bounds.size.y;

                //Floor
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //cube.transform.position = new Vector3(0, -yFloor / 2, 0);
                cube.transform.position = new Vector3(go.GetComponent<Renderer>().bounds.center.x, -yFloor / 2, go.GetComponent<Renderer>().bounds.center.z);
                cube.transform.localScale = new Vector3(xFloor * 1.2f, 0.1f, zFloor * 1.2f);
                cube.transform.SetParent(parent.transform);
                cube.layer = 6;
                cube.name = "Floor";


                nav.transform.SetParent(go.transform);





















                GameObject goV = new GameObject();
                goV.AddComponent<MeshRenderer>().sharedMaterial = material;
                goV.AddComponent<MeshFilter>().sharedMesh = mesh;
                //goV.tag = "Model";
                goV.layer = 12;
                goV.AddComponent<MeshCollider>();
                goV.name = "Walls2";
                goV.transform.localScale = new Vector3(goV.transform.localScale.x, 100, goV.transform.localScale.z);
                goV.GetComponent<MeshRenderer>().enabled = false;

                float y2 = goV.GetComponent<MeshRenderer>().bounds.size.y / 2;
                goV.transform.position = new Vector3(0, 0, 0);
                goV.transform.SetParent(parent.transform);


                goV.AddComponent<NavMeshModifier>();
                goV.GetComponent<NavMeshModifier>().overrideArea = true;
                goV.GetComponent<NavMeshModifier>().area = 1;


            }
            //return;
            surface.BuildNavMesh();
        }

        GameObject user = GameObject.Find("User Arrow");
        nav.transform.position = new Vector3(target.XCoordinate, target.YCoordinate, target.ZCoordinate);
        nav2.transform.position = new Vector3(target.XCoordinate, target.YCoordinate, target.ZCoordinate);



        GameObject userDummy = GameObject.Find("UserDummy");
        userDummy.transform.position = nav2.transform.position;
        GameObject.Find("TrackParent").transform.position = nav2.transform.position;
        GameObject.Find("TrackParentPos").transform.position = nav2.transform.position;

        CAMLOCALPOS = GameObject.Find("AR Camera").transform.localPosition;
        CAMFORWARD = GameObject.Find("AR Camera").transform.forward;
        CAMUP = GameObject.Find("AR Camera").transform.up;
        CAMRIGHT = GameObject.Find("AR Camera").transform.right;
        CAMROT = GameObject.Find("AR Camera").transform.rotation;

        ARTrackTest2.targetHeightY = GameObject.Find("HeightTest").transform.position.y;

        userDummy.transform.eulerAngles = new Vector3(target.XRotation, target.YRotation, target.ZRotation);
        GameObject.Find("TrackParent").transform.eulerAngles = new Vector3(target.XRotation, target.YRotation + 180, target.ZRotation);
        GameObject.Find("TrackParentPos").transform.eulerAngles = new Vector3(target.XRotation, target.YRotation + 180, target.ZRotation);


        ARTrackTest2.first = true;

        if (PlayerPrefs.GetInt("TargetId") > 0)
        {
            foreach (Transform child in GameObject.Find("UserDummy").transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (Transform child in GameObject.Find("TrackedImageDummy").transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            onGetAllFloorSwitcherPointsByFloorCall(PlayerPrefs.GetInt("FloorId"));
        }

        foreach (Transform child in GameObject.Find("TargetCanvas").transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        requesting = false;
        ready = true;
    }

    public static bool NewVirtualObjects = false;
    public void CreateVirtualObjectsList(IVirtualObjectList virtualObjectsList)
    {
        virtualObjects = virtualObjectsList.VirtualObjects;
        NewVirtualObjects = true;
    }

    public void TargetPathResult(ITargetPathSteps targetPathSteps)
    {

        //IF target is on same floor
        if (targetPathSteps.Target.floor.id == PlayerPrefs.GetInt("FloorId"))
        {
            Target target = targetPathSteps.Target;
            playerController.setPath(new Vector3(target.xCoordinate, target.yCoordinate, target.zCoordinate));
        }
        //IF target not on same floor
        else
        {
            //start point of floor switcher
            FloorSwitcherPoint point = targetPathSteps.FloorSwitcherPointSteps[1].floorSwitcherPoint;
            int floorSwitcherId = targetPathSteps.FloorSwitcherPointSteps[1].floorSwitcherPoint.floorSwitcher.id;



            playerController.setPath(new Vector3(point.xCoordinate, point.yCoordinate, point.zCoordinate));

            //point.floor.number == current floor number
            int goToFloor = point.floor.number;
            int i = 0;
            foreach (FloorSwitcherPointStep floorSwitcherPointStep in targetPathSteps.FloorSwitcherPointSteps)
            {
                if (i > 0)
                {

                    if (floorSwitcherPointStep.floorSwitcherPoint.floorSwitcher.id == floorSwitcherId)
                    {
                        goToFloor = floorSwitcherPointStep.floorSwitcherPoint.floor.number;
                    }
                    else
                    {
                        break;
                    }
                }
                i++;
            }


            //MAP FLOOR SWITCHER INFO
            GameObject dummyUser = GameObject.Find("UserDummy");

            GameObject go = Instantiate(floorSwitcherPrefab);
            go.transform.position = new Vector3(point.xCoordinate, 1f, point.zCoordinate);
            go.transform.SetParent(dummyUser.transform);
            go.layer = 6;

            GameObject[] mapPaths = GameObject.FindGameObjectsWithTag("PathMap");
            Vector3 targetPostitionMap = new Vector3(mapPaths[mapPaths.Length - 1].transform.position.x,
                            go.transform.position.y,
                             mapPaths[mapPaths.Length - 1].transform.position.z);
            go.transform.LookAt(targetPostitionMap);
            go.tag = "Linecast2";
            go.name = "FloorSwitcherPointM" + point.id;

            //VIRTUAL FOOR SWITCHER INFO
            GameObject trackedImageDummy = GameObject.Find("TrackedImageDummy");
            GameObject go2 = Instantiate(floorSwitcherPrefab);

            go2.transform.SetParent(trackedImageDummy.transform);

            go2.transform.localPosition = go.transform.localPosition;
            go2.transform.localScale = new Vector3(go.transform.localScale.x / trackedImageDummy.transform.localScale.x, go.transform.localScale.y / trackedImageDummy.transform.localScale.y, go.transform.localScale.z / trackedImageDummy.transform.localScale.z);
            go2.layer = 0;

            go2.transform.Find("Canvas").Find("Text").GetComponent<Text>().text = "Go to floor: " + goToFloor;

            GameObject[] virtualPaths = GameObject.FindGameObjectsWithTag("PathVirtual");

            Vector3 targetPostitionVirtual = new Vector3(virtualPaths[virtualPaths.Length - 1].transform.position.x,
                                        go2.transform.position.y,
                                         virtualPaths[virtualPaths.Length - 1].transform.position.z);
            go2.transform.LookAt(targetPostitionVirtual);
            go2.name = "FloorSwitcherPointV" + point.id;

        }
    }

    public void CreateFloorSwitcherPointsList(IFloorSwitcherPointList floorSwitcherPointList)
    {


        floorSwitchers = new Dictionary<int, FloorSwitcherPoint>();


        pathStepList = new PathStepList();
        List<PathStep> pathSteps = new List<PathStep>();

        for (int i = 0; i < floorSwitcherPointList.FloorSwitcherPoints.Count; i++)
        {
            floorSwitchers.Add(floorSwitcherPointList.FloorSwitcherPoints[i].id, floorSwitcherPointList.FloorSwitcherPoints[i]);

            float pathLength = playerController.CalculatePathLength(new Vector3(floorSwitcherPointList.FloorSwitcherPoints[i].xCoordinate, floorSwitcherPointList.FloorSwitcherPoints[i].yCoordinate, floorSwitcherPointList.FloorSwitcherPoints[i].zCoordinate));
            PathStep pathStep = new PathStep();
            pathStep.stepId = floorSwitcherPointList.FloorSwitcherPoints[i].id;
            pathStep.distance = double.Parse(pathLength.ToString());
            pathSteps.Add(pathStep);
        }

        pathStepList.pathSteps = pathSteps;


        onGetTargetPathStepsCall(PlayerPrefs.GetInt("TargetId"), pathStepList);

    }
}

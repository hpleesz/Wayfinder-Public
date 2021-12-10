using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using ZXing;
using ZXing.QrCode;
using Parabox.Stl;
using System.Net;
using UnityEngine.Video;
using System.IO;
using System.Text;
using Dummiesman;

namespace UnityEngine.XR.ARFoundation.Samples
{


    /// This component listens for images detected by the <c>XRImageTrackingSubsystem</c>
    /// and overlays some information as well as the source Texture2D on top of the
    /// detected image.
    /// </summary>
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class TrackedImageInfoManager : MonoBehaviour
    {
        public ARSessionOrigin arSessionOrigin;

        //private Gyroscope gyro;
        public GameObject ImagePrefab;
        public GameObject VideoPrefab;
        public Material standardMaterial;

        public static Vector3 positionDifference = new Vector3(0, 0, 0);
        public static Quaternion rotationDifference = new Quaternion();
        public static Vector3 cubeRot = Vector3.zero;
        public static Vector3 trImage = new Vector3(0, 0, 0);
        GameObject trackedImageDummy;
        GameObject target;

        public static Vector3 FORWARD = Vector3.zero;
        public static Vector3 UP = Vector3.zero;
        public static Vector3 RIGHT = Vector3.zero;
        public static Vector3 DIFF = Vector3.zero;

        [SerializeField]
        [Tooltip("The camera to set on the world space UI canvas for each instantiated image info.")]
        Camera m_WorldSpaceCanvasCamera;

        /// <summary>
        /// The prefab has a world space UI canvas,
        /// which requires a camera to function properly.
        /// </summary>
        public Camera worldSpaceCanvasCamera
        {
            get { return m_WorldSpaceCanvasCamera; }
            set { m_WorldSpaceCanvasCamera = value; }
        }

        [SerializeField]
        [Tooltip("If an image is detected but no source texture can be found, this texture is used instead.")]
        Texture2D m_DefaultTexture;

        /// <summary>
        /// If an image is detected but no source texture can be found,
        /// this texture is used instead.
        /// </summary>
        public Texture2D defaultTexture
        {
            get { return m_DefaultTexture; }
            set { m_DefaultTexture = value; }
        }

        ARTrackedImageManager m_TrackedImageManager;

        private void Start()
        {
            trackedImageDummy = GameObject.Find("TrackedImageDummy");
            target = GameObject.Find("Target");
        }
        void Awake()
        {
            m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
            //gyro = Input.gyro;
            //gyro.enabled = true;


        }

        void OnEnable()
        {
            m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }

        void OnDisable()
        {
            m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }

        private void Update()
        {


        }
        void UpdateInfo(ARTrackedImage trackedImage, bool newTarget)
        {

            // Set canvas camera
            var canvas = trackedImage.GetComponentInChildren<Canvas>();
            canvas.worldCamera = worldSpaceCanvasCamera;

            canvas.worldCamera = worldSpaceCanvasCamera;

            // Update information about the tracked image
            var text = canvas.GetComponentInChildren<Text>();
            

            text.text = string.Format(
                "{0}\ntrackingState: {1}\nGUID: {2}\nReference size: {3} cm\nDetected size: {4} cm",
                trackedImage.referenceImage.name,
                trackedImage.trackingState,
                trackedImage.referenceImage.guid,
                trackedImage.referenceImage.size * 100f,
                trackedImage.size * 100f);

            var planeParentGo = trackedImage.transform.GetChild(0).gameObject;

            var planeGo = planeParentGo.transform.GetChild(0).gameObject;


            trackedImageDummy.transform.position = trackedImage.transform.position;
            trackedImageDummy.transform.rotation = trackedImage.transform.rotation;

            target.transform.position = trackedImage.transform.position;
            target.transform.rotation = trackedImage.transform.rotation;
            target.transform.eulerAngles = new Vector3(target.transform.eulerAngles.x, target.transform.eulerAngles.y, target.transform.eulerAngles.z);


            trackedImageDummy.transform.eulerAngles = new Vector3(trackedImageDummy.transform.eulerAngles.x, trackedImageDummy.transform.eulerAngles.y + 180, trackedImageDummy.transform.eulerAngles.z);


            //if (positionDifference == new Vector3(0, 0, 0))
            if (QRScript.NewQrCode)
            {

                GameObject nav = GameObject.Find("Nav");

                positionDifference = trackedImage.gameObject.transform.position - nav.transform.position;

                trImage = trackedImage.gameObject.transform.position;

                GameObject userDummy = GameObject.Find("UserDummy");

                Quaternion q = Quaternion.Euler(trackedImage.transform.localRotation.x, trackedImage.transform.localRotation.y + 180, trackedImage.transform.localRotation.z);

                arSessionOrigin.MakeContentAppearAt(userDummy.transform, trackedImage.transform.position, GameObject.Find("ZAxis").transform.rotation);
                arSessionOrigin.transform.forward = trackedImage.transform.forward;
                arSessionOrigin.transform.up = trackedImage.transform.up;
                arSessionOrigin.transform.right = trackedImage.transform.right;


                cubeRot = GameObject.Find("TrackParent").transform.eulerAngles;

                FORWARD = GameObject.Find("AR Camera").transform.forward - userDummy.transform.forward;
                UP = GameObject.Find("AR Camera").transform.up - userDummy.transform.up;
                RIGHT = GameObject.Find("AR Camera").transform.right - userDummy.transform.right;


                QRScript.NewQrCode = false;

            }

            

            if(QRScript.NewVirtualObjects)
            {


                GameObject targetCanvas = GameObject.Find("TargetCanvas");
                foreach (VirtualObject virtualObject in QRScript.virtualObjects)
                {

                    if (virtualObject.type.id == 1)
                    {
                        Debug.Log("VIRTUALOBJECT TYPE 1");
                        GameObject img = Instantiate(ImagePrefab, new Vector3(0, 0, 0), new Quaternion(), targetCanvas.gameObject.transform);

                        img.transform.localPosition = new Vector3(virtualObject.xCoordinate, virtualObject.yCoordinate, virtualObject.zCoordinate);
                        img.transform.localEulerAngles = new Vector3(virtualObject.xRotation, virtualObject.yRotation, virtualObject.zRotation);
                        img.transform.localScale = new Vector3(virtualObject.xScale, virtualObject.yScale, virtualObject.zScale);

                        if (virtualObject.fileLocation != null)
                        {
                            string someUrl = HttpRequestSingleton.SERVER_URL + "" + virtualObject.fileLocation;
                            Debug.Log(someUrl);
                            using (var webClient = new WebClient())
                            {
                                byte[] imageBytes = webClient.DownloadData(someUrl);

                                Texture2D tex = new Texture2D(2, 2);
                                tex.LoadImage(imageBytes);
                                Sprite sprite2 = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));

                                img.GetComponent<Image>().sprite = sprite2; //Image is a defined reference to an image component
                                img.GetComponent<Image>().preserveAspect = true;
                            }
                        }
                    }
                    else if (virtualObject.type.id == 2)
                    {
                        Debug.Log("VIRTUALOBJECT TYPE 2");

                        GameObject img = Instantiate(VideoPrefab, new Vector3(0, 0, 0), new Quaternion(), targetCanvas.gameObject.transform);

                        img.transform.localPosition = new Vector3(virtualObject.xCoordinate, virtualObject.yCoordinate, virtualObject.zCoordinate);
                        img.transform.localEulerAngles = new Vector3(virtualObject.xRotation, virtualObject.yRotation, virtualObject.zRotation);
                        img.transform.localScale = new Vector3(virtualObject.xScale, virtualObject.yScale, virtualObject.zScale);

                        if (virtualObject.fileLocation != null)
                        {
                            string someUrl = HttpRequestSingleton.SERVER_URL + "" + virtualObject.fileLocation;
                            Debug.Log(someUrl);

                            img.GetComponent<VideoPlayer>().url = someUrl;
                        }
                    }
                    else if (virtualObject.type.id == 3)
                    {
                        Debug.Log("VIRTUALOBJECT TYPE 3");


                        using (var webClient = new WebClient())
                        {
                            byte[] fileContent = webClient.DownloadData(HttpRequestSingleton.SERVER_URL + virtualObject.fileLocation);
                            var textStream = new MemoryStream(fileContent);

                            var loadedObj = new OBJLoader().Load(textStream);
                            loadedObj.transform.parent = targetCanvas.gameObject.transform;
                            loadedObj.transform.localPosition = new Vector3(virtualObject.xCoordinate, virtualObject.yCoordinate, virtualObject.zCoordinate);
                            loadedObj.transform.localEulerAngles = new Vector3(virtualObject.xRotation, virtualObject.yRotation, virtualObject.zRotation);
                            loadedObj.transform.localScale = new Vector3(virtualObject.xScale, virtualObject.yScale, virtualObject.zScale);


                            
                            byte[] textureContent = webClient.DownloadData(HttpRequestSingleton.SERVER_URL + virtualObject.textureLocation);
                            Texture2D tex2 = new Texture2D(2, 2, TextureFormat.ARGB32, false);
                            tex2.LoadImage(textureContent);

                            Color[] pix = tex2.GetPixels();
                            for (int j = 0; j < pix.Length; j++)
                                tex2.SetPixels(pix);
                            tex2.Apply();
                            
                            standardMaterial.SetTexture("_MainTex", tex2);

                            foreach (Transform child in loadedObj.transform)
                            {
                                //WavefrontObject
                                child.parent = targetCanvas.gameObject.transform;
                                child.gameObject.layer = 0;
                                child.gameObject.tag = "VirtualObject";
                                child.GetComponent<MeshRenderer>().material = standardMaterial;

                            }
                            Destroy(GameObject.Find("WavefrontObject"));

                        }




                    }
                }
                QRScript.NewVirtualObjects = false;
            }

            

            if (trackedImage.trackingState != TrackingState.None)
            {
                planeGo.SetActive(true);

                // The image extents is only valid when the image is being tracked
                trackedImage.transform.localScale = new Vector3(trackedImage.size.x, 1f, trackedImage.size.y);

                // Set the texture
                var material = planeGo.GetComponentInChildren<MeshRenderer>().material;
                material.mainTexture = (trackedImage.referenceImage.texture == null) ? defaultTexture : trackedImage.referenceImage.texture;


            }
            else
            {
                planeGo.SetActive(false);

            }
        }

        private bool requesting = false;
        void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            //CHECK
            if (QRScript.target != null)
            {
                foreach (var trackedImage in eventArgs.added)
                {

                    // Give the initial image a reasonable default scale
                    trackedImage.transform.localScale = new Vector3(0.01f, 1f, 0.01f);

                    UpdateInfo(trackedImage, true);

                }

                foreach (var trackedImage in eventArgs.updated)
                    //var trackedImage2 = eventArgs.updated[eventArgs.updated.Count - 1];
                    UpdateInfo(trackedImage, false);
            }
        }

        public Material material;
        public GameObject parent;

    }

}
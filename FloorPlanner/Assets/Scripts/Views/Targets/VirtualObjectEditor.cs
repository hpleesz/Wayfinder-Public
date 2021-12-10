using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public interface IVirtualObjectEditView
{
    public delegate void AddVirtualObjectToTargetCall(int targetId, IVirtualObject virtualObject);
    public delegate void EditVirtualObjectCall(int virtualObjectId, IVirtualObject virtualObject);

    public delegate void AddImageToVirtualObjectCall(int floorId, byte[] form);
    public delegate void AddVideoToVirtualObjectCall(int floorId, byte[] form);
    public delegate void AddTextureToVirtualObjectCall(int floorId, byte[] form);
    public delegate void AddObjToVirtualObjectCall(int floorId, byte[] form);

    public event AddVirtualObjectToTargetCall onAddVirtualObjectToTargetCall;
    public event EditVirtualObjectCall onEditVirtualObjectCall;
    public event AddImageToVirtualObjectCall onAddImageToVirtualObjectCall;
    public event AddVideoToVirtualObjectCall onAddVideoToVirtualObjectCall;
    public event AddTextureToVirtualObjectCall onAddTextureToVirtualObjectCall;
    public event AddObjToVirtualObjectCall onAddObjToVirtualObjectCall;

    void VirtualObjectAdded(int virtualObjectId);
    void VirtualObjectUpdated(int virtualObjectId);
    void ImageAddedToVirtualObject();
    void VideoAddedToVirtualObject();
    void ObjAddedToVirtualObject(int virtualObjectId);
    void TextureAddedToVirtualObject();

}

public interface IVirtualObjectTypesListView
{
    public delegate void GetAllVirtualObjectTypesCall();

    public event GetAllVirtualObjectTypesCall onGetAllVirtualObjectTypesCall;


    void CreateVirtualObjectTypesList(IVirtualObjectTypeList virtualObjectTypeList);


}
public class VirtualObjectEditor : MonoBehaviour, IVirtualObjectEditView, IVirtualObjectTypesListView
{

    public event IVirtualObjectEditView.AddVirtualObjectToTargetCall onAddVirtualObjectToTargetCall;
    public event IVirtualObjectEditView.EditVirtualObjectCall onEditVirtualObjectCall;
    public event IVirtualObjectEditView.AddImageToVirtualObjectCall onAddImageToVirtualObjectCall;
    public event IVirtualObjectEditView.AddVideoToVirtualObjectCall onAddVideoToVirtualObjectCall;
    public event IVirtualObjectEditView.AddTextureToVirtualObjectCall onAddTextureToVirtualObjectCall;
    public event IVirtualObjectEditView.AddObjToVirtualObjectCall onAddObjToVirtualObjectCall;

    public event IVirtualObjectTypesListView.GetAllVirtualObjectTypesCall onGetAllVirtualObjectTypesCall;

    public Dropdown dropdown;
    public GameObject ImagePrefab;
    public GameObject VideoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //httpRequest.GetAllVirtualObjectTypes();
        onGetAllVirtualObjectTypesCall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<VirtualObjectType> virtualTypesList;

    public void AddVirtualObject()
    {
        switch(dropdown.value)
        {
            case 0:
                GameObject img = Instantiate(ImagePrefab, new Vector3(0,0,0), new Quaternion(), GameObject.Find("TargetCanvas").transform);
                img.transform.localPosition = new Vector3(0,0,-0.1f);
                img.transform.localEulerAngles = new Vector3(0,0,180);
            break;
            case 1:
                GameObject video = Instantiate(VideoPrefab, new Vector3(0, 0, 0), new Quaternion(), GameObject.Find("TargetCanvas").transform);
                video.transform.localPosition = new Vector3(0, 0, -0.1f);
                video.transform.localEulerAngles = new Vector3(0, 0, 180);
                VideoPlayer vp = video.GetComponent<VideoPlayer>();
                vp.aspectRatio = VideoAspectRatio.FitInside;
            break;
            case 2:
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.parent = GameObject.Find("TargetCanvas").transform;
                cube.transform.localPosition = new Vector3(0, 0, -0.1f);
                cube.transform.localEulerAngles = new Vector3(0, 0, 180);
                cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                cube.tag = "VirtualObject";
                cube.layer = 5;
                cube.name = "Placeholder OBJ";
                break;
        }
    }
    public void SetVirtualObjectPos()
    {
        InputField xpos = GameObject.Find("X Pos Input").GetComponent<InputField>();
        InputField ypos = GameObject.Find("Y Pos Input").GetComponent<InputField>();
        InputField zpos = GameObject.Find("Z Pos Input").GetComponent<InputField>();

        TargetSelectionHandler.selection.localPosition = new Vector3(-float.Parse(xpos.text, CultureInfo.InvariantCulture), -float.Parse(ypos.text, CultureInfo.InvariantCulture), -float.Parse(zpos.text, CultureInfo.InvariantCulture));

    }

    public void SetVirtualObjectRot()
    {
        InputField xrot = GameObject.Find("X Rot Input").GetComponent<InputField>();
        InputField yrot = GameObject.Find("Y Rot Input").GetComponent<InputField>();
        InputField zrot = GameObject.Find("Z Rot Input").GetComponent<InputField>();

        TargetSelectionHandler.selection.localEulerAngles = new Vector3(-float.Parse(xrot.text, CultureInfo.InvariantCulture), -float.Parse(yrot.text, CultureInfo.InvariantCulture), -float.Parse(zrot.text, CultureInfo.InvariantCulture)+180);

    }

    public void SetVirtualObjectScale()
    {
        InputField xscale = GameObject.Find("X Scale Input").GetComponent<InputField>();
        InputField yscale = GameObject.Find("Y Scale Input").GetComponent<InputField>();
        InputField zscale = GameObject.Find("Z Scale Input").GetComponent<InputField>();

        TargetSelectionHandler.selection.localScale = new Vector3(float.Parse(xscale.text, CultureInfo.InvariantCulture), float.Parse(yscale.text, CultureInfo.InvariantCulture), float.Parse(zscale.text, CultureInfo.InvariantCulture));


    }
    public void SaveTarget()
    {
        Transform parent = GameObject.Find("TargetCanvas").transform;
        foreach (Transform child in parent)
        {
            VirtualObject vo = new VirtualObject();
            vo.xCoordinate = child.localPosition.x;
            vo.yCoordinate = child.localPosition.y;
            vo.zCoordinate = child.localPosition.z;
            vo.xRotation = child.localEulerAngles.x;
            vo.yRotation = child.localEulerAngles.y;
            vo.zRotation = child.localEulerAngles.z;
            vo.xScale = child.localScale.x;
            vo.yScale = child.localScale.y;
            vo.zScale = child.localScale.z;
            vo.type = new VirtualObjectType();
            
            vo.type.id = virtualTypesList[dropdown.value].id;

            onAddVirtualObjectToTargetCall(PlayerPrefs.GetInt("TargetId"), vo);
        }


    }

    public void Back()
    {
        SceneManager.LoadScene("Targets");
    }

    public void VirtualObjectAdded(int virtualObjectId)
    {
        Transform parent = GameObject.Find("TargetCanvas").transform;
        foreach (Transform child in parent)
        {
            if (child.GetComponent<Image>() != null)
            {
                if (child.GetComponent<Image>().sprite != null)
                {
                    Texture2D texture = child.GetComponent<Image>().sprite.texture;
                    byte[] itemBytes = texture.EncodeToPNG();

                    onAddImageToVirtualObjectCall(virtualObjectId, itemBytes);
                }
            }
            else if (child.GetComponent<VideoPlayer>() != null)
            {
                byte[] videoBytes = File.ReadAllBytes(child.GetComponent<VideoPlayer>().url);
                onAddVideoToVirtualObjectCall(virtualObjectId, videoBytes);


            }
            else
            {
                string fileName = child.name;
                byte[] itemBytes = File.ReadAllBytes(fileName);

                onAddObjToVirtualObjectCall(virtualObjectId, itemBytes);

            }

        }
    }

    public void VirtualObjectUpdated(int virtualObjectId)
    {
        throw new System.NotImplementedException();
    }

    public void ImageAddedToVirtualObject()
    {
        SceneManager.LoadScene("Targets");
    }

    public void VideoAddedToVirtualObject()
    {
        SceneManager.LoadScene("Targets");
    }

    public void ObjAddedToVirtualObject(int virtualObjectId)
    {
        string fullname = TargetSelectionHandler.selection.GetComponent<MeshRenderer>().material.name;
        string name = "";
        string[] namesplit = fullname.Split(' ');
        for (int i = 0; i < namesplit.Length - 1; i++)
        {
            if (i > 0)
            {
                name += " ";
            }
            name += namesplit[i];
        }

        if (name != "ObjPlaceholder")
        {
            //int virtualObjectId = int.Parse(System.Text.Encoding.UTF8.GetString(result));

            byte[] itemBytes = File.ReadAllBytes(name);
            //WWWForm form = new WWWForm();
            //form.AddBinaryData("file", itemBytes);

            //httpRequest.AddObjImageToVirtualObject(virtualObjectId, form);
            onAddTextureToVirtualObjectCall(virtualObjectId, itemBytes);
        }
        else
        {
            SceneManager.LoadScene("Targets");
        }
    }

    public void TextureAddedToVirtualObject()
    {
        SceneManager.LoadScene("Targets");
    }

    public void CreateVirtualObjectTypesList(IVirtualObjectTypeList virtualObjectTypeList)
    {

        virtualTypesList = virtualObjectTypeList.VirtualObjectTypes;

        List<string> m_DropOptions = new List<string>();
        foreach (var type in virtualObjectTypeList.VirtualObjectTypes)
        {
            m_DropOptions.Add(type.name);
        }

        dropdown.ClearOptions();
        //Add the options created in the List above
        dropdown.AddOptions(m_DropOptions);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.Stl;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface IFloorPlan2DEditView
{
    public delegate void Add2DImageToFloorCall(int floorId, byte[] form);
    public delegate void AddFloorTemplate(int floorId, byte[] form);

    public event AddFloorTemplate onAddFloorTemplate;
    public event Add2DImageToFloorCall onAdd2DImageToFloorCall;

    void FloorTemplateAdded();
    void FloorPlan2DAdded();
}

public interface IFloorPlan3DEditView
{
    public delegate void AddFloorPlan3DToFloorCall(int floorId, string fileContent);

    public event AddFloorPlan3DToFloorCall onAddFloorPlan3DToFloorCall;

    void FloorPlan3DAdded();
}

public class FloorDrawer : MonoBehaviour, IFloorPlan2DEditView, IFloorPlan3DEditView, IFloorInfoView
{
    public GameObject savePanel;
    // Start is called before the first frame update
    //public HttpRequest httpRequest;
    public GameObject image;

    public event IFloorPlan2DEditView.AddFloorTemplate onAddFloorTemplate;
    public event IFloorPlan2DEditView.Add2DImageToFloorCall onAdd2DImageToFloorCall;

    public event IFloorPlan3DEditView.AddFloorPlan3DToFloorCall onAddFloorPlan3DToFloorCall;

    public event IFloorInfoView.GetFloorCall onGetFloorCall;

    //private Exporter pb_Stl;

    void Start()
    {
        onGetFloorCall(PlayerPrefs.GetInt("FloorId"));
    }

    public void SaveModel()
    {
        savePanel.SetActive(true);
        GameObject.Find("Main Panel").SetActive(false);
        GameObject.Find("FloorTemplateCanvas").SetActive(false);
        image.SetActive(false);
        EditorOptions.Instance.ChangeMove();
    }

    public void OnClickScreenCaptureButton()
    {
        Debug.Log("ScreenCapture");
        gameObject.SetActive(true);
        StartCoroutine(CaptureScreen());

    }
    public IEnumerator CaptureScreen()
    {
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        //savePanel.SetActive(false);
        Debug.Log("start coroutine");
        GameObject.Find("FloorDrawerCanvas").GetComponent<Canvas>().enabled = false;
        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        // Take screenshot
        //ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/screenshot.png");//use this
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        //Read the pixels in the Rect starting at 0,0 and ending at the screen's width and height
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        byte[] itemBytes = texture.EncodeToPNG();

        Debug.Log(itemBytes.Length);

        //WWWForm form = new WWWForm();
        //form.AddBinaryData("file", itemBytes);

        //httpRequest.Add2DImageToFloor(PlayerPrefs.GetInt("FloorId"), form);
        onAdd2DImageToFloorCall(PlayerPrefs.GetInt("FloorId"), itemBytes);
        gameObject.SetActive(false);

        //SEND

        // Show UI after we're done
        //GameObject.Find("Canvas (2)").GetComponent<Canvas>().enabled = true;
    }

    private void AddFloorTemplate()
    {
        if (image.GetComponent<Image>().sprite != null)
        {
            Texture2D texture = image.GetComponent<Image>().sprite.texture;
            byte[] itemBytes = texture.EncodeToPNG();

            //WWWForm form = new WWWForm();
            //form.AddBinaryData("file", itemBytes);

            //httpRequest.AddFloorTemplate(PlayerPrefs.GetInt("FloorId"), form);
            onAddFloorTemplate(PlayerPrefs.GetInt("FloorId"), itemBytes);
        }

    }

    public void ExportSTL()
    {
        Debug.Log(Application.persistentDataPath);

        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        List<Mesh> meshArr = new List<Mesh>();
        foreach (var wall in walls)
        {
            meshArr.Add(wall.GetComponent<MeshFilter>().mesh);
        }
        Debug.Log(meshArr.Count);

        string fileContent = ExporterToString.ExportToString("", walls, FileType.Ascii);
        fileContent = "\"" + fileContent + "\"";
        fileContent = fileContent.Replace(',', '.');
        //fileContent.Replace("0x0D","\n");
        //httpRequest.AddModelToFloor(PlayerPrefs.GetInt("FloorId"), fileContent);
        onAddFloorPlan3DToFloorCall(PlayerPrefs.GetInt("FloorId"), fileContent);

        Debug.Log(fileContent);
    }

    public void FloorTemplateAdded()
    {
        SceneManager.LoadScene("Floor");
    }

    public void FloorPlan2DAdded()
    {
        ExportSTL();
    }

    public void FloorPlan3DAdded()
    {
        if (image.GetComponent<Image>().sprite != null)
        {
            AddFloorTemplate();
        }
        else
        {
            SceneManager.LoadScene("Floor");
        }
    }

    public void ShowFloor(IFloor floor)
    {
        //Floor Drawerhez képest van
        gameObject.transform.Find("Title Panel").Find("Title Text").GetComponent<Text>().text = floor.Name;
        gameObject.transform.Find("Title Panel").Find("Number Text").GetComponent<Text>().text = floor.Number.ToString();
    }

    public void Back()
    {
        SceneManager.LoadScene("Floor");
    }
}

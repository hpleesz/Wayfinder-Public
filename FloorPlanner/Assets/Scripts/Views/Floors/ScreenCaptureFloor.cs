using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCaptureFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickScreenCaptureButton()
    {
        StartCoroutine(CaptureScreen());
    }
    public IEnumerator CaptureScreen()
    {
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        GameObject.Find("Canvas (2)").GetComponent<Canvas>().enabled = false;

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        // Take screenshot
        //ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/screenshot.png");//use this
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        //Read the pixels in the Rect starting at 0,0 and ending at the screen's width and height
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        byte[] itemBytes = texture.EncodeToPNG();

        //SEND

        // Show UI after we're done
        GameObject.Find("Canvas (2)").GetComponent<Canvas>().enabled = true;
    }

}

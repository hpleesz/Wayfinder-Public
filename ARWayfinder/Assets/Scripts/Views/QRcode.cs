using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
public class QRcode : MonoBehaviour
{


private Camera cam;
private Rect screenRect;
void Start()
{

    }

void OnGUI()
{
        var currentRT = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;

        // Render the camera's view.
        cam.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D camTexture = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        camTexture.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        camTexture.Apply();

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;

        // drawing the camera on screen
        GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
    // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
    try
    {
        IBarcodeReader barcodeReader = new BarcodeReader();
        // decode the current frame
        var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);
        if (result != null)
        {
            Debug.Log("DECODED TEXT FROM QR: " +result.Text);
        }
    }
    catch (Exception ex) { Debug.LogWarning(ex.Message); }
        
        //Texture2D myQR = generateQR("test");
        //if (GUI.Button(new Rect(300, 300, 256, 256), myQR, GUIStyle.none)) { }
    }

    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    public static Texture2D generateQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }

}


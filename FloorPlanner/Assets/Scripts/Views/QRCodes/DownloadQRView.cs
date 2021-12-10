using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface IQRCodesListView
{

    public delegate void GetQRCodesByPlaceCall(int placeId);


    public event GetQRCodesByPlaceCall onGetQRCodesByPlaceCall;


    void CreateQRCodesList(ITargetList targetsList);


}

public class DownloadQRView : MonoBehaviour, IQRCodesListView
{
    public InputField inputField;
    private string folderPath;

    public event IQRCodesListView.GetQRCodesByPlaceCall onGetQRCodesByPlaceCall;

    public void StartSave()
    {
        setFolderPath();
        onGetQRCodesByPlaceCall(PlayerPrefs.GetInt("PlaceId"));

    }

    public void setFolderPath()
    {
        folderPath = inputField.transform.Find("Text").GetComponent<Text>().text.ToString();
    }

    public void CreateQRCodesList(ITargetList targetsList)
    {
        string path = folderPath + "/QrCodes/";
        var folder = Directory.CreateDirectory(path);

        Texture2D textureQR;
        DateTime foo = DateTime.Now;
        long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
        foreach (var target in targetsList.Targets)
        {
            //textureQR = QRcode.generateQR(target.id.ToString()+"_"+target.name+"_"+target.xCoordinate+"_"+target.yCoordinate+"_"+target.zCoordinate);
            textureQR = QRcode.generateQR(target.qrCode);
            byte[] bytes = textureQR.EncodeToPNG();

            File.WriteAllBytes(path + target.id.ToString() + "_" + target.name + ".png", bytes);
        }

        SceneManager.LoadScene("Menu");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");

    }
}

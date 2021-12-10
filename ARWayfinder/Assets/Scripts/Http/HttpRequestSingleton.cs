using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpRequestSingleton : MonoBehaviour
{
    public static HttpRequestSingleton Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public delegate void Result(byte[] results);  //declare the method types that can be registered to the event


    //public const string SERVER_URL = "https://localhost:44397";
    public const string SERVER_URL = "https://wayfinder-server.azurewebsites.net/";
    private byte[] resultData = new byte[] { };


    public void CallGet(string url, Result resultEvent)
    {
        var result = StartCoroutine(Get(url, resultEvent));
    }

    public void CallPut(string url, string json, Result resultEvent)
    {
        var result = StartCoroutine(Put(url, json, resultEvent));
    }

    public void CallPost(string url, string json, Result resultEvent)
    {
        var result = StartCoroutine(Post(url, json, resultEvent));
    }

    public void CallPostForm(string url, WWWForm form, Result resultEvent)
    {
        var result = StartCoroutine(PostForm(url, form, resultEvent));
    }

    IEnumerator Get(string url, Result resultEvent)
    {

        UnityWebRequest www = UnityWebRequest.Get(SERVER_URL + url);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            var results = www.downloadHandler.data;
            Debug.Log(System.Text.Encoding.UTF8.GetString(results));

            resultEvent(results);
        }
    }


    IEnumerator Post(string url, string json, Result resultEvent)
    {
        //JSON
        var request = new UnityWebRequest(SERVER_URL + url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            var results = request.downloadHandler.data;
            Debug.Log(System.Text.Encoding.UTF8.GetString(results));

            resultEvent(results);
        }

    }

    IEnumerator Put(string url, string json, Result resultEvent)
    {
        //JSON
        var request = new UnityWebRequest(SERVER_URL + url, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            var results = request.downloadHandler.data;
            Debug.Log(System.Text.Encoding.UTF8.GetString(results));

            resultEvent(results);
        }

    }

    IEnumerator PostForm(string url, WWWForm form, Result resultEvent)
    {



        UnityWebRequest www = UnityWebRequest.Post(SERVER_URL + url, form);
        //www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            byte[] results = www.downloadHandler.data;
            Debug.Log(System.Text.Encoding.UTF8.GetString(results));
            Debug.Log("Form upload complete!");
            resultEvent(results);
        }
    }
}

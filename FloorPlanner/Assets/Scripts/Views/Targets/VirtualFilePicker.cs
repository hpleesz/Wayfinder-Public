using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using SimpleFileBrowser;
using UnityEngine.UI;
using UnityEngine.Video;
using Dummiesman;
using System.Text;

public class VirtualFilePicker : MonoBehaviour
{
	// Warning: paths returned by FileBrowser dialogs do not contain a trailing '\' character
	// Warning: FileBrowser can only show 1 dialog at a time

	public void ChooseObj()
	{
		FileBrowser.SetFilters(true, new FileBrowser.Filter("3D Objects", ".obj"));

		FileBrowser.SetDefaultFilter(".obj");
		FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

		FileBrowser.AddQuickLink("Users", "C:\\Users", null);

		StartCoroutine(ShowLoadDialogOptionsCoroutine("OBJ"));
	}

	public void ChooseObjImg()
	{
		FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));

		FileBrowser.SetDefaultFilter(".jpg");
		FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

		FileBrowser.AddQuickLink("Users", "C:\\Users", null);

		StartCoroutine(ShowLoadDialogOptionsCoroutine("OBJIMG"));

	}
	public void ChooseImg()
	{
		FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));

		FileBrowser.SetDefaultFilter(".jpg");
		FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

		FileBrowser.AddQuickLink("Users", "C:\\Users", null);

		StartCoroutine(ShowLoadDialogOptionsCoroutine("IMG"));
	}

	public void ChooseVideo()
	{
		FileBrowser.SetFilters(true, new FileBrowser.Filter("Videos", "..mp4"));

		FileBrowser.SetDefaultFilter("..mp4");
		FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

		FileBrowser.AddQuickLink("Users", "C:\\Users", null);

		StartCoroutine(ShowLoadDialogOptionsCoroutine("VIDEO"));

	}

	public void ChooseFile()
	{
		Debug.Log("file");
		// Set filters (optional)
		// It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
		// if all the dialogs will be using the same filters
		FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png", ".mp4"), new FileBrowser.Filter("Text Files", ".txt", ".pdf"), new FileBrowser.Filter("3D Objects", ".obj"));

		// Set default filter that is selected when the dialog is shown (optional)
		// Returns true if the default filter is set successfully
		// In this case, set Images filter as the default filter
		FileBrowser.SetDefaultFilter(".jpg");

		// Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
		// Note that when you use this function, .lnk and .tmp extensions will no longer be
		// excluded unless you explicitly add them as parameters to the function
		FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

		// Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
		// It is sufficient to add a quick link just once
		// Name: Users
		// Path: C:\Users
		// Icon: default (folder icon)
		FileBrowser.AddQuickLink("Users", "C:\\Users", null);

		// Show a save file dialog 
		// onSuccess event: not registered (which means this dialog is pretty useless)
		// onCancel event: not registered
		// Save file/folder: file, Allow multiple selection: false
		// Initial path: "C:\", Initial filename: "Screenshot.png"
		// Title: "Save As", Submit button text: "Save"
		// FileBrowser.ShowSaveDialog( null, null, FileBrowser.PickMode.Files, false, "C:\\", "Screenshot.png", "Save As", "Save" );

		// Show a select folder dialog 
		// onSuccess event: print the selected folder's path
		// onCancel event: print "Canceled"
		// Load file/folder: folder, Allow multiple selection: false
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Select Folder", Submit button text: "Select"
		// FileBrowser.ShowLoadDialog( ( paths ) => { Debug.Log( "Selected: " + paths[0] ); },
		//						   () => { Debug.Log( "Canceled" ); },
		//						   FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select" );

		// Coroutine example
		StartCoroutine(ShowLoadDialogCoroutine());
	}









	IEnumerator ShowLoadDialogOptionsCoroutine(string OPTION)
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: both, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

		// Dialog is closed
		// Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
		Debug.Log(FileBrowser.Success);

		if (FileBrowser.Success)
		{
			// Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
			for (int i = 0; i < FileBrowser.Result.Length; i++)
				Debug.Log(FileBrowser.Result[i]);

			// Read the bytes of the first file via FileBrowserHelpers
			// Contrary to File.ReadAllBytes, this function works on Android 10+, as well
			//byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);


			switch(OPTION)
            {
				case "IMG":
					Texture2D tex = null;

					tex = new Texture2D(2, 2);
					byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
					tex.LoadImage(bytes); //..this will auto-resize the texture dimensions.
										  //GetComponent<Renderer>().material.mainTexture = tex;
					Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));

					Image img = TargetSelectionHandler.selection.GetComponent<Image>();
					img.sprite = sprite; //Image is a defined reference to an image component
					img.preserveAspect = true;
					img.rectTransform.sizeDelta = new Vector2(0.1f, 10000);
					img.gameObject.SetActive(true);
					//img.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
					//img.color = new Color(255,255,255,100);
					img.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
					break;
				case "OBJIMG":
					var fileContent = File.ReadAllBytes(FileBrowser.Result[0]);
					Texture2D tex2 = new Texture2D(2, 2, TextureFormat.ARGB32, false);
					tex2.LoadImage(fileContent);

					Color[] pix = tex2.GetPixels();
					for (int j = 0; j < pix.Length; j++)
						tex2.SetPixels(pix);
					tex2.Apply();

					//Find the Standard Shader
					Material myNewMaterial = new Material(Shader.Find("Standard"));
					myNewMaterial.name = FileBrowser.Result[0];
					//Set Texture on the material
					myNewMaterial.SetTexture("_MainTex", tex2);
					//Apply to GameObject
					TargetSelectionHandler.selection.GetComponent<MeshRenderer>().material = myNewMaterial;
					break;
				case "VIDEO":
					VideoPlayer vp = TargetSelectionHandler.selection.GetComponent<VideoPlayer>();
					vp.url = FileBrowser.Result[0];
					break;
				case "OBJ":
					string[] lines = File.ReadAllLines(FileBrowser.Result[0]);

					string text = "";
					int i = 0;
					foreach (string line in lines)
					{
						text += line;
						text += "\n";

						i++;
					}

					Debug.Log(text);

					var textStream = new MemoryStream(Encoding.UTF8.GetBytes(text));

					var loadedObj = new OBJLoader().Load(textStream);
					loadedObj.transform.parent = GameObject.Find("TargetCanvas").transform;
					loadedObj.transform.localPosition = new Vector3(0, 0, -0.1f);
					loadedObj.transform.localEulerAngles = new Vector3(0, 0, 0);
					loadedObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

					foreach (Transform child in loadedObj.transform)
					{
						//WavefrontObject
						child.parent = GameObject.Find("TargetCanvas").transform;
						child.tag = "VirtualObject";
						child.gameObject.layer = 5;
						child.gameObject.name = FileBrowser.Result[0];
						TargetSelectionHandler.selection = child;
					}
					Destroy(GameObject.Find("WavefrontObject"));
					Destroy(GameObject.Find("Placeholder OBJ"));
					break;
			}

		}

	}











		IEnumerator ShowLoadDialogCoroutine()
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: both, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

		// Dialog is closed
		// Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
		Debug.Log(FileBrowser.Success);

		if (FileBrowser.Success)
		{
			// Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
			for (int i = 0; i < FileBrowser.Result.Length; i++)
				Debug.Log(FileBrowser.Result[i]);

			// Read the bytes of the first file via FileBrowserHelpers
			// Contrary to File.ReadAllBytes, this function works on Android 10+, as well
			byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);


			Texture2D tex = null;

			tex = new Texture2D(2, 2);
			tex.LoadImage(bytes); //..this will auto-resize the texture dimensions.
								  //GetComponent<Renderer>().material.mainTexture = tex;
			Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));

			Image img = TargetSelectionHandler.selection.GetComponent<Image>();
			VideoPlayer vp = TargetSelectionHandler.selection.GetComponent<VideoPlayer>();

			if (img != null)
            {
				img.sprite = sprite; //Image is a defined reference to an image component
				img.preserveAspect = true;
				img.rectTransform.sizeDelta = new Vector2(0.1f, 10000);
				img.gameObject.SetActive(true);
				//img.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
				//img.color = new Color(255,255,255,100);
				img.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);

				// Or, copy the first file to persistentDataPath
				/*
				string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
				FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);*/
            }
			else if (vp != null)
            {
				vp.url = FileBrowser.Result[0];

			}
			else
            {
				string[] lines = File.ReadAllLines(FileBrowser.Result[0]);

				string text = "";
				int i = 0;
				foreach (string line in lines)
				{
					text += line;
					text += "\n";

					i++;
				}

				Debug.Log(text);

				var textStream = new MemoryStream(Encoding.UTF8.GetBytes(text));

				var loadedObj = new OBJLoader().Load(textStream);
				loadedObj.transform.parent = GameObject.Find("TargetCanvas").transform;
				loadedObj.transform.localPosition = new Vector3(0, 0, -0.1f);
				loadedObj.transform.localEulerAngles = new Vector3(0, 0, 0);
				loadedObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

				foreach(Transform child in loadedObj.transform)
                {
					//WavefrontObject
					child.parent = GameObject.Find("TargetCanvas").transform;
					child.tag = "VirtualObject";
					child.gameObject.layer = 5;
					child.gameObject.name = FileBrowser.Result[0];
					TargetSelectionHandler.selection = child;
				}
				Destroy(GameObject.Find("WavefrontObject"));
				Destroy(GameObject.Find("Placeholder OBJ"));

				var fileContent = File.ReadAllBytes("C:\\Users\\plees\\Desktop\\7_3.png");
				Texture2D tex2 = new Texture2D(2, 2, TextureFormat.ARGB32, false);
				tex2.LoadImage(fileContent);


				Color[] pix = tex2.GetPixels();
				for (int j = 0; j < pix.Length; j++)
					tex2.SetPixels(pix);
				tex2.Apply();

				//Find the Standard Shader
				Material myNewMaterial = new Material(Shader.Find("Standard"));
				myNewMaterial.name = "C:\\Users\\plees\\Desktop\\7_3.png";
				//Set Texture on the material
				myNewMaterial.SetTexture("_MainTex", tex2);
				//Apply to GameObject
				TargetSelectionHandler.selection.GetComponent<MeshRenderer>().material = myNewMaterial;
			}

		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using SimpleFileBrowser;
using UnityEngine.UI;

public class FolderPicker : MonoBehaviour
{
	// Warning: paths returned by FileBrowser dialogs do not contain a trailing '\' character
	// Warning: FileBrowser can only show 1 dialog at a time
	public InputField inputField;
    private void Start()
    {
	}
    public void ChooseFile()
	{
		Debug.Log("file");
		// Set filters (optional)
		// It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
		// if all the dialogs will be using the same filters
		FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"), new FileBrowser.Filter("Text Files", ".txt", ".pdf"));

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

	IEnumerator ShowLoadDialogCoroutine()
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: both, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Folders, true, null, null, "Load Files and Folders", "Load");

		// Dialog is closed
		// Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
		Debug.Log(FileBrowser.Success);

		if (FileBrowser.Success)
		{
			// Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
			for (int i = 0; i < FileBrowser.Result.Length; i++)
			{
				Debug.Log(FileBrowser.Result[i]);
				inputField.GetComponent<InputField>().text = FileBrowser.Result[i];

				//downloadQr.StartSave(FileBrowser.Result[i]);
				//var folder = Directory.CreateDirectory(FileBrowser.Result[i]+"/dir"); // returns a DirectoryInfo object
			}
			// Read the bytes of the first file via FileBrowserHelpers
			// Contrary to File.ReadAllBytes, this function works on Android 10+, as well
			/*
			byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);


			Texture2D tex = null;

			tex = new Texture2D(2, 2);
			tex.LoadImage(bytes); //..this will auto-resize the texture dimensions.
								  //GetComponent<Renderer>().material.mainTexture = tex;
			Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));

			img.sprite = sprite; //Image is a defined reference to an image component
			img.preserveAspect = true;
			img.rectTransform.sizeDelta = new Vector2(10, 10000);
			img.gameObject.SetActive(true);
			imageData.SetActive(true);
			//img.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
			//img.color = new Color(255,255,255,100);
			img.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
			// Or, copy the first file to persistentDataPath
			/*
			string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
			FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);*/
		}
	}
}

static class CanvasExtensions
{
	public static Vector2 SizeToParent(this RawImage image, float padding = 0)
	{
		var parent = image.transform.parent.GetComponent<RectTransform>();
		var imageTransform = image.GetComponent<RectTransform>();
		if (!parent) { return imageTransform.sizeDelta; } //if we don't have a parent, just return our current width;
		padding = 1 - padding;
		float w = 0, h = 0;
		float ratio = image.texture.width / (float)image.texture.height;
		var bounds = new Rect(0, 0, parent.rect.width, parent.rect.height);
		if (Mathf.RoundToInt(imageTransform.eulerAngles.z) % 180 == 90)
		{
			//Invert the bounds if the image is rotated
			bounds.size = new Vector2(bounds.height, bounds.width);
		}
		//Size by height first
		h = bounds.height * padding;
		w = h * ratio;
		if (w > bounds.width * padding)
		{ //If it doesn't fit, fallback to width;
			w = bounds.width * padding;
			h = w / ratio;
		}
		imageTransform.sizeDelta = new Vector2(w, h);
		return imageTransform.sizeDelta;
	}

	

}

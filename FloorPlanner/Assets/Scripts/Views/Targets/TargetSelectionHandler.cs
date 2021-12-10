using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class TargetSelectionHandler : MonoBehaviour
{
    public Material defaultMaterial;
    public Material selectMaterial;

    private Transform currentSelect;

    public GameObject selectedMenu;
    public GameObject notSelectedMenu;
    public static Transform selection;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0) && EditorOptions.Instance.SelectWall)
            {
                if (currentSelect != null)
                {
                    var selectionRenderer = currentSelect.GetComponent<Renderer>();
                    selectionRenderer.material = defaultMaterial;
                    currentSelect = null;
                    selectedMenu.SetActive(false);
                    notSelectedMenu.SetActive(true);
                }
                //else
                //{
                int layer_mask = LayerMask.GetMask("UI");

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
                {

                    if (hit.transform.CompareTag("VirtualObject"))
                    {
                        selection = hit.transform;

                        Debug.Log(selection.name);

                        //if img
                        //var selectionRenderer = selection.GetComponent<Image>();
                        //selectionRenderer.material = selectMaterial;
                        //

                        currentSelect = selection;
                        selectedMenu.SetActive(true);

                        GameObject chooseImg = selectedMenu.transform.Find("Choose Img").gameObject;
                        GameObject chooseObjImg = selectedMenu.transform.Find("Choose ObjImg").gameObject;
                        GameObject chooseObj = selectedMenu.transform.Find("Choose Obj").gameObject;
                        GameObject chooseVideo = selectedMenu.transform.Find("Choose Video").gameObject;

                        chooseImg.SetActive(false);
                        chooseObjImg.SetActive(false);
                        chooseObj.SetActive(false);
                        chooseVideo.SetActive(false);


                        if (selection.GetComponent<Image>() != null)
                        {
                            chooseImg.SetActive(true);
                        }
                        else if (selection.GetComponent<VideoPlayer>() != null)
                        {
                            chooseVideo.SetActive(true);
                        }
                        else
                        {
                            chooseObj.SetActive(true);
                            chooseObjImg.SetActive(true);
                        }

                        notSelectedMenu.SetActive(false);

                    }

                }
                //}

            }
        }
    }

    public void DeleteWall()
    {
        if (currentSelect != null)
        {
            Destroy(currentSelect.gameObject);
            currentSelect = null;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionHandler : MonoBehaviour
{
    public Material defaultMaterial;
    public Material selectMaterial;

    private Transform currentSelect;

    public GameObject selectedMenu;
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
                if(currentSelect != null)
                {
                    var selectionRenderer = currentSelect.GetComponent<Renderer>();
                    selectionRenderer.material = defaultMaterial;
                    currentSelect = null;
                    selectedMenu.SetActive(false);
                }
                //else
                //{
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        var selection = hit.transform;
                        if(selection.CompareTag("Wall"))
                        {
                            var selectionRenderer = selection.GetComponent<Renderer>();
                            selectionRenderer.material = selectMaterial;
                            currentSelect = selection;
                            selectedMenu.SetActive(true);

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

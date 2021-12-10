using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorOptions : MonoBehaviour
{
    private static EditorOptions _instance;

    public static EditorOptions Instance { get { return _instance; } }

    private bool _selectWall;
    public bool SelectWall { get { return _selectWall; } }

    private bool _drawWall;
    public bool DrawWall { get { return _drawWall; } }

    private bool _moveFloor;
    public bool MoveFloor { get { return _moveFloor; } }

    private bool _rotateFloor;
    public bool RotateFloor { get { return _rotateFloor; } }

    private Image drawButton;
    private Image selectButton;
    private Image moveButton;
    private Image rotateButton;
    private Image deleteButton;

    private void Start()
    {

    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        if(transform.Find("Draw Button") != null)
        {
            Instance.drawButton = transform.Find("Draw Button").GetComponent<Image>();
        }
        Instance.selectButton = transform.Find("Select Button").GetComponent<Image>();
        Instance.moveButton = transform.Find("Move Button").GetComponent<Image>();
        Instance.rotateButton = transform.Find("Rotate Button").GetComponent<Image>();
        if(transform.Find("Wall Selected Menu") != null)
        {
            Instance.deleteButton = transform.Find("Wall Selected Menu").Find("Delete Button").GetComponent<Image>();
        }
        InactivateAllButtons();
    }

    public void ChangeSelect()
    {
        Instance._selectWall = true;
        Instance._drawWall = false;
        Instance._moveFloor = false;
        Instance._rotateFloor = false;
        InactivateAllButtons();
        Instance.selectButton.color = new Color(0.9058824f, 0.4156863f, 0.1647059f, 1f);
    }

    public void ChangeMove()
    {
        Instance._selectWall = false;
        Instance._drawWall = false;
        Instance._moveFloor = true;
        Instance._rotateFloor = false;
        InactivateAllButtons();
        Instance.moveButton.color = new Color(0.9058824f, 0.4156863f, 0.1647059f, 1f);
        Debug.Log(MoveFloor);
    }

    public void ChangeDraw()
    {
        Instance._selectWall = false;
        Instance._drawWall = true;
        Instance._moveFloor = false;
        Instance._rotateFloor = false;
        InactivateAllButtons();
        Instance.drawButton.color = new Color(0.9058824f, 0.4156863f, 0.1647059f, 1f);
    }

    public void ChangeRotate()
    {
        Instance._selectWall = false;
        Instance._drawWall = false;
        Instance._moveFloor = false;
        Instance._rotateFloor = true;
        InactivateAllButtons();
        Instance.rotateButton.color = new Color(0.9058824f, 0.4156863f, 0.1647059f, 1f);
    }

    private void InactivateAllButtons()
    {
        if (Instance.drawButton != null)
        {
            Instance.drawButton.color = Color.white;
        }
        Instance.selectButton.color = Color.white;
        Instance.moveButton.color = Color.white;
        Instance.rotateButton.color = Color.white;
        if(Instance.deleteButton != null)
        {
            Instance.deleteButton.color = Color.white;
        }
    }
}
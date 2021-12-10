using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class customGrid : MonoBehaviour
{
    public GameObject target;
    public GameObject structure;
    public GameObject target2;
    public GameObject structure2;
    Vector3 truePos;
    Vector3 truePos2;

    public InputField heightInput;
    public InputField widthInput;


    public float gridSize;
    public GameObject wallPrefab;

    //Runs after the update function
    void LateUpdate()
    {
        var offset = 0.1f / 2;
        //float fractionalPortion = target.transform.localScale.x - Mathf.Floor(target.transform.localScale.x);
        decimal fractionalPortion = new decimal(target.transform.localScale.x);
        fractionalPortion = fractionalPortion * 10;
        //int fractionalPortion = int.Parse(widthInput.text.Split('.')[1]);
        if (fractionalPortion % 2 == 0)
        {
            offset = 0;
        }
        truePos.x = Mathf.Floor(target.transform.position.x / gridSize) * gridSize + offset;
        truePos.z = Mathf.Floor(target.transform.position.z / gridSize) * gridSize + offset; //Y
        truePos.y = Mathf.Floor(target.transform.position.y / gridSize) * gridSize + target.transform.localScale.y / 2; //Z

        structure.transform.position = truePos;
        
        truePos2.x = Mathf.Floor(target2.transform.position.x / gridSize) * gridSize + offset;
        truePos2.z = Mathf.Floor(target2.transform.position.z / gridSize) * gridSize + offset; //Y
        truePos2.y = Mathf.Floor(target2.transform.position.y / gridSize) * gridSize + target2.transform.localScale.y / 2; //Z

        structure2.transform.position = truePos2;
    }

    public void DrawWall()
    {
        
        float wallX = structure.transform.position.x + (structure2.transform.position.x - structure.transform.position.x) / 2;
        float wallY = structure.transform.position.y + (structure2.transform.position.y - structure.transform.position.y) / 2;
        float wallZ = structure.transform.position.z + (structure2.transform.position.z - structure.transform.position.z) / 2;

        GameObject wall = Instantiate(wallPrefab, new Vector3(wallX, wallY, wallZ), Quaternion.identity);

        var length = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(structure2.transform.position.x - structure.transform.position.x), 2) + Mathf.Pow(Mathf.Abs(structure2.transform.position.z - structure.transform.position.z), 2)); //Y
        /*
        wall.transform.localScale = new Vector3(length + structure.transform.localScale.x,
            Mathf.Abs(0) + structure.transform.localScale.y,
            Mathf.Abs(structure2.transform.position.z - structure.transform.position.z) + 0.1f);
        */
        wall.transform.localScale = new Vector3(length + structure.transform.localScale.x,
            Mathf.Abs(structure2.transform.position.y - structure.transform.position.y) + 0.1f,
            Mathf.Abs(0) + structure.transform.localScale.z
            );

        /*wall.transform.localScale = new Vector3(length + 0.1f,
            Mathf.Abs(0) + 0.1f,
            Mathf.Abs(structure2.transform.position.z - structure.transform.position.z) + 0.1f);
        */
        //Vector3 WallDirection = structure2.transform.position - structure.transform.position;

        float alpha = 0;
        if(Mathf.Abs(structure2.transform.position.x - structure.transform.position.x) > 0)
        {
            alpha =  Mathf.Atan(Mathf.Abs(structure2.transform.position.z - structure.transform.position.z) / Mathf.Abs(structure2.transform.position.x - structure.transform.position.x)) * Mathf.Rad2Deg;
        }
        Debug.Log(alpha);
        if(structure.transform.position.x < structure2.transform.position.x && structure.transform.position.z < structure2.transform.position.z)
        {
            alpha = -alpha;
        }

        else if (structure.transform.position.x > structure2.transform.position.x && structure.transform.position.z > structure2.transform.position.z)
        {

            alpha = -alpha;
        }
        else if(structure.transform.position.x == structure2.transform.position.x)
        {

            alpha = 90;
        }
        wall.transform.rotation = Quaternion.Euler(new Vector3(0f,alpha,0f));

    }

    public void setWallsHeight()
    {
        float height = float.Parse(heightInput.text, CultureInfo.InvariantCulture);
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        Debug.Log(walls.Length);
        foreach (GameObject wall in walls)
        {
            wall.transform.localScale = new Vector3(wall.transform.localScale.x, height, wall.transform.localScale.z);
            wall.transform.position = new Vector3(wall.transform.position.x, height / 2f, wall.transform.position.z);

        }
    }

    public void setWallsWidth()
    {
        float width = float.Parse(widthInput.text, CultureInfo.InvariantCulture);
        target.transform.localScale = new Vector3(width, target.transform.localScale.y, width);
        target2.transform.localScale = new Vector3(width, target2.transform.localScale.y, width);
        structure.transform.localScale = new Vector3(width, structure.transform.localScale.y, width);
        structure2.transform.localScale = new Vector3(width, structure2.transform.localScale.y, width);

    }
}

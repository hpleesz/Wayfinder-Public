using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour
{

    public InputField widthInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImageWidth()
    {
        float width = float.Parse(widthInput.text, CultureInfo.InvariantCulture);
        gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(width, 1000);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerTextView : MonoBehaviour
{

    Text text;
    int layer;

    void Start()
    {
        text = GetComponent<Text>();
        layer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(layer != GameManager.ActiveGameManager.CameraManager.CurrentLayer)
        {
            layer = GameManager.ActiveGameManager.CameraManager.CurrentLayer;
            text.text = "Layer " + layer.ToString();
        }
    }
}

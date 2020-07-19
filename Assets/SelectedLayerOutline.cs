using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedLayerOutline : MonoBehaviour
{
    void Start()
    {
        float displayRatio = (float)Display.main.renderingWidth / Display.main.renderingHeight;
        transform.localScale = new Vector3(displayRatio, 1f, 1f) * 1.05f;
    }

}

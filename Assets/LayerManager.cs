using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    Layer[] layers;
    const int layerCount = 10;
    const float seperationDistance = 5f;
    Transform selectedLayerOutline;

    private void Awake()
    {
        layers = new Layer[layerCount];

        GameObject temp = transform.Find("LayerTemplate").gameObject;

        for (int i = 0; i < layerCount; i++)
        {
            Layer l = GameObject.Instantiate(temp).GetComponent<Layer>();
            l.transform.position = new Vector3(0, 0, -(seperationDistance * layerCount / 2) + i * seperationDistance);
            l.transform.SetParent(transform, true);

            SpriteRenderer gridSprite = l.transform.Find("Renderer").Find("Grid").GetComponent<SpriteRenderer>();
            l.LayerColor = gridSprite.color = new HSL_Color(((float)i / layerCount) * 360, 1f, 0.8f).ToRGB();
            layers[i] = l;
        }

        temp.gameObject.SetActive(false);

        selectedLayerOutline = transform.Find("SelectedLayerOutline");
    }

    private void Update()
    {
        int currentLayer = GameManager.ActiveGameManager.CameraManager.CurrentLayer;

        selectedLayerOutline.transform.position = layers[currentLayer].transform.position;

        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].transform.Find("Renderer").Find("Grid").gameObject.SetActive(
                GameManager.ActiveGameManager.ToggleGrid.GridActive);
        }


        if (GameManager.ActiveGameManager.CameraManager.CURRENT_CAM_MODE ==
            CameraManager.CAM_MODE.SINGLE_LAYER)
        {
            for (int i = 0; i < layerCount; i++)
                if (i == currentLayer)
                    layers[i].Active = true;
                else
                    layers[i].Active = false;

            return;
        }

        bool[] toggledLayers = GameManager.ActiveGameManager.LayerToggle.LayersActive;
        
        for(int i = 0; i < layerCount; i++)
            layers[i].Active = toggledLayers[i];
    }

    public Layer[] Layers
    {
        get { return layers; }
    }
   

}

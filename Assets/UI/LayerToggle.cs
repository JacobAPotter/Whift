using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerToggle : MonoBehaviour
{

    Toggle[] toggles;
    bool[] togglesOn;
    bool changed;

    void Start()
    {
        Toggle temp = transform.Find("Toggle").GetComponent<Toggle>();

        int count = GameManager.ActiveGameManager.LayerManager.Layers.Length;
        toggles = new Toggle[count];

        for (int i = 0; i < count; i++)
        {
            Toggle t = GameObject.Instantiate(temp).GetComponent<Toggle>();
            t.transform.SetParent( transform, false);
            t.transform.localPosition = Vector3.right * (-110 + 25 * i);
            ColorBlock cb = t.colors;
            cb.normalColor = cb.selectedColor = cb.highlightedColor = GameManager.ActiveGameManager.LayerManager.Layers[i].LayerColor;
            t.colors = cb;
            toggles[i] = t;

        }
        temp.gameObject.SetActive(false);

        togglesOn = new bool[toggles.Length];
    }

    private void Update()
    {
        changed = false;


        if (Input.GetKeyDown(KeyCode.Alpha1))
            toggles[0].isOn = !toggles[0].isOn;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            toggles[1].isOn = !toggles[1].isOn;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            toggles[2].isOn = !toggles[2].isOn;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            toggles[3].isOn = !toggles[3].isOn;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            toggles[4].isOn = !toggles[4].isOn;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            toggles[5].isOn = !toggles[5].isOn;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            toggles[6].isOn = !toggles[6].isOn;
        if (Input.GetKeyDown(KeyCode.Alpha8))
            toggles[7].isOn = !toggles[7].isOn;
        if (Input.GetKeyDown(KeyCode.Alpha9))
            toggles[8].isOn = !toggles[8].isOn;
        if (Input.GetKeyDown(KeyCode.Alpha0))
            toggles[9].isOn = !toggles[9].isOn;


        for (int i = 0; i < togglesOn.Length; i++)
        {
            if(togglesOn[i] != toggles[i].isOn)
                changed = true;

            togglesOn[i] = toggles[i].isOn;
        }
    }

    public bool[] LayersActive
    {
        get { return togglesOn; }
    }
    
    public bool Changed
    {
        get { return changed; }
    }
    
}

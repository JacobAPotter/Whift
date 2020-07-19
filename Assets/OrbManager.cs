using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrbManager : MonoBehaviour
{
    List<Orb> orbs;
    Orb selectedOrb;
    Orb templateOrb;
    int activeOrbs;
    bool orbWasDragged;
    const float minOrbDragDistance = 0.01f;
    SpriteRenderer selectedOrbOutline;
    Vector3 mouseClickPos;

    //if the click was off-center, maintain that offset
    Vector3 mouseToOrbOffset;

    bool heldSinceSelected;
    bool initialPanelOpened;
    
    void Start()
    {
        orbs = new List<Orb>();
        templateOrb = transform.Find("OrbTemplate").GetComponent<Orb>();
        templateOrb.gameObject.SetActive(false);
        selectedOrbOutline = GameObject.Find("SelectedOrb").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //select an orb
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.ActiveGameManager.EventSystem.IsPointerOverGameObject())
            {
                if (GameManager.ActiveGameManager.CameraManager.CURRENT_CAM_MODE == CameraManager.CAM_MODE.ALL_LAYERS ||
                    GameManager.ActiveGameManager.CameraManager.CURRENT_CAM_MODE == CameraManager.CAM_MODE.SINGLE_LAYER)
                {
                    if (!GameManager.ActiveGameManager.RenderViewPanel.RenderViewOn)
                    {
                        Camera cam = GameManager.ActiveGameManager.CameraManager.MainCamera;

                        Vector2 click = cam.ScreenToWorldPoint(Input.mousePosition);

                        bool orbFound = false;
                        foreach (Orb o in orbs)
                        {
                            if (!o.gameObject.activeSelf)
                                continue;

                            float dist = Vector2.Distance(click, o.transform.position);
                            if (dist < o.Radius)
                            {
                                selectedOrb = o;
                                orbWasDragged = false;
                                orbFound = true;
                                mouseClickPos = click;
                                heldSinceSelected = true;
                                initialPanelOpened = false;
                                mouseToOrbOffset = click -  new Vector2(o.transform.position.x , o.transform.position.y);
                                break;
                            }
                        }

                        if (!orbFound)
                            selectedOrb = null;
                    }
                }
            }
        }

        //after clicking and releasing an orb, open the panel
        if (!Input.GetMouseButton(0) && selectedOrb != null && !heldSinceSelected && !orbWasDragged && !initialPanelOpened)
        {
            GameManager.ActiveGameManager.BrushEditor.gameObject.SetActive(true);
            GameManager.ActiveGameManager.BrushEditor.SetSelectedOrb(selectedOrb);

            //find the preset name for this orb in the dropdown list and set the dropdown list to that value
            List<UnityEngine.UI.Dropdown.OptionData> presets = GameManager.ActiveGameManager.BrushEditor.presetDropdownList.options;
            int currentPresetValue = 0;

            foreach(UnityEngine.UI.Dropdown.OptionData option in presets)
                if(option.text == selectedOrb.presetName)
                    break;
                else
                    currentPresetValue++;
            if (currentPresetValue >= presets.Count)
                Debug.Log("Preset Name attatched to Orb not found in preset list");
            else 
                GameManager.ActiveGameManager.BrushEditor.presetDropdownList.value = currentPresetValue;


            initialPanelOpened = true;
        }

        if (!Input.GetMouseButton(0))
        {
            heldSinceSelected = false;

            if (selectedOrb == null)
                orbWasDragged = false;
        }

        //drag the orb
        if (Input.GetMouseButton(0)  && selectedOrb != null && heldSinceSelected)
        {
            Camera cam = GameManager.ActiveGameManager.CameraManager.MainCamera;
            Vector2 click = cam.ScreenToWorldPoint(Input.mousePosition);
           
            if (Vector3.Distance(mouseClickPos, click) > minOrbDragDistance)
            {
                selectedOrb.transform.position = new Vector3(click.x, click.y, selectedOrb.transform.position.z) - mouseToOrbOffset;
                orbWasDragged = true;
            }
        }

        //glowing outline on selected orb
        if (selectedOrb != null &&  !GameManager.ActiveGameManager.RenderViewPanel.RenderViewOn &&
            GameManager.ActiveGameManager.LayerManager.Layers[selectedOrb.layer].Active )
        {
            selectedOrbOutline.gameObject.SetActive(true);
            selectedOrbOutline.gameObject.transform.position = selectedOrb.transform.position + Vector3.forward;
        }
        else
            selectedOrbOutline.gameObject.SetActive(false);


        activeOrbs = 0;
        //activate only orbs in current layer
        if (GameManager.ActiveGameManager.CameraManager.CURRENT_CAM_MODE ==
            CameraManager.CAM_MODE.SINGLE_LAYER)
        {
            int currentLayer = GameManager.ActiveGameManager.CameraManager.CurrentLayer;

            for (int i = 0; i < orbs.Count; i++)
                if (orbs[i].layer == currentLayer)
                {
                    orbs[i].gameObject.SetActive(true);
                    activeOrbs++;
                }
                else
                    orbs[i].gameObject.SetActive(false);

            return;
        }


        //activate orbs in all active layers
        bool[] activeLayers = GameManager.ActiveGameManager.LayerToggle.LayersActive;

        foreach (Orb o in orbs)
        {
            o.gameObject.SetActive(activeLayers[o.layer]);

            if (activeLayers[o.layer])
                activeOrbs++;
        }
    }

    public void SpawnNewOrb()
    {
        Orb newOrb = GameObject.Instantiate(templateOrb).GetComponent<Orb>();
        newOrb.gameObject.SetActive(true);
        newOrb.transform.SetParent(transform);

        int currentLayer = GameManager.ActiveGameManager.CameraManager.CurrentLayer;

        newOrb.transform.position =
            GameManager.ActiveGameManager.LayerManager.
            Layers[currentLayer].transform.position + Vector3.up + new Vector3(Random.value, Random.value, 0) * 0.01f;
        newOrb.layer = currentLayer;
        newOrb.name = "Orb " + orbs.Count.ToString();
        orbs.Add(newOrb);

        GameManager.ActiveGameManager.LayerManager.Layers[currentLayer].AddOrb(newOrb);
        newOrb.Initialize(GameManager.ActiveGameManager.LayerManager.Layers[currentLayer].LayerColor);
        selectedOrb = newOrb;
    }

    public int DrawPerFrame
    {
        get { return 8192 / ((activeOrbs > 0) ? activeOrbs : 1); }
    }

    public void SetSelectedToNull()
    {
        selectedOrb = null;
    }
        
    public void DeleteSelectedOrb()
    {
        if(selectedOrb != null)
        {
            GameManager.ActiveGameManager.LayerManager.Layers[selectedOrb.layer].RemoveOrb(selectedOrb);
            orbs.Remove(selectedOrb);
            GameManager.ActiveGameManager.BrushEditor.SetSelectedOrb(null);
            GameObject.Destroy(selectedOrb.gameObject);
        }
    }

    public Orb CurrentOrb
    {
        get { return selectedOrb; }
    }
}

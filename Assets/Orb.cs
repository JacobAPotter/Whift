using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public int layer;
    GameObject orbDisplay;
    bool rendering;
    Brush currentBrush;
    PolarTrig2D presetPolarTrig;
    PolarTrig2D customPolarTrig;
    float radius;
    SpriteRenderer glow;
    public string presetName;

    private void Start()
    {
        //dont init if its already been called
        if (orbDisplay == null)
            Initialize(GameManager.ActiveGameManager.LayerManager.Layers[layer].LayerColor);

        GameManager.ActiveGameManager.PresetManager.LoadPresetToOrb("default", this);
    }

    public void Initialize(Color glowColor)
    {
        orbDisplay = transform.Find("OrbModel").gameObject;
        presetPolarTrig = new PolarTrig2D();
        customPolarTrig = new PolarTrig2D();
        currentBrush = customPolarTrig;
        UseCustomPolarBrush(true);
        radius = GetComponent<SphereCollider>().bounds.extents.x;
        glow = transform.Find("OrbModel").Find("glow").GetComponent<SpriteRenderer>();
        glow.color = glowColor;
    }

    private void Update()
    {
        if (orbDisplay == null)
            Debug.Log(name);

        if (GameManager.ActiveGameManager.RenderViewPanel.RenderViewOn &&
            GameManager.ActiveGameManager.CameraManager.CURRENT_CAM_MODE != CameraManager.CAM_MODE.FLY_CAM)
        {
            if (orbDisplay.activeSelf)
                orbDisplay.SetActive(false);

            if (!rendering)
            {
                rendering = true;
                currentBrush.Init();
            }

            currentBrush.Update();
            currentBrush.Paint(layer,  transform.position.x, transform.position.y);
        }
        else
        {
            if (!orbDisplay.activeSelf)
                orbDisplay.SetActive(true);

            rendering = false;
        }

    }

    public Brush CurrentBrush
    {
        get { return currentBrush; }
    }

    public PolarTrig2D PolarBrush
    {
        get
        {
            return (currentBrush == customPolarTrig) ? customPolarTrig : presetPolarTrig;
        }
    }

    public float Radius
    {
        get { return radius; }
    }

    public void UseCustomPolarBrush(bool useCustom)
    {
        if (useCustom)
        {
            currentBrush = customPolarTrig;
            presetName = "Custom";
        }
        else
            currentBrush = customPolarTrig;

    }

    public bool UsingCustomPolarTrig
    {
        get { return currentBrush == customPolarTrig; }
    }

}


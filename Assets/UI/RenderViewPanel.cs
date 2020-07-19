using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderViewPanel : MonoBehaviour
{
    Toggle orbView;
    Toggle renderView;

    bool renderViewSet;

    private void Start()
    {
        orbView = transform.Find("ToggleOrbView").GetComponent<Toggle>();
        renderView = transform.Find("ToggleRenderView").GetComponent<Toggle>();
        orbView.isOn = true;
        renderView.isOn = false;
        renderViewSet = false;
    }

    private void Update()
    {

        if(GameManager.ActiveGameManager.CameraManager.CURRENT_CAM_MODE == CameraManager.CAM_MODE.FLY_CAM)
        {
            renderViewSet = false;
            orbView.interactable = false;
            renderView.interactable = false;
        }
        else  
        {
            //just exited fly cam mode.
            if (!orbView.interactable)
            {
                //turn buttons back on
                orbView.interactable = true;
                renderView.interactable = true;

                //set renderSet to whatever its supposed to be.
                renderViewSet = renderView.isOn;
            }
        }

        //prevent unchecking current toggle
        if(!renderView.isOn && !orbView.isOn)
        {
            if (renderViewSet)
                renderView.isOn = true;
            else
                orbView.isOn = true;
        }

        if(renderViewSet && orbView.isOn)
        {
            renderViewSet = false;
            renderView.isOn = false;
        }
        else if(!renderViewSet && renderView.isOn)
        {
            renderViewSet = true;
            orbView.isOn = false;
        }

    }

    public bool RenderViewOn
    {
        get { return renderViewSet; }
    }

}

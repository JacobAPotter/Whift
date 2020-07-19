using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPanel : MonoBehaviour
{
    Toggle flyCam;
    Toggle singleLayer;
    Toggle allLayers;

    CameraManager.CAM_MODE CAM_MODE;

    // Start is called before the first frame update
    void Start()
    {
        flyCam = transform.Find("FlyViewToggle").GetComponent<Toggle>();
        singleLayer = transform.Find("SingleViewToggle").GetComponent<Toggle>();
        allLayers = transform.Find("AllViewToggle").GetComponent<Toggle>();
        flyCam.isOn = true;
        singleLayer.isOn = false;
        allLayers.isOn = false;
        CAM_MODE = CameraManager.CAM_MODE.FLY_CAM;
    }

    private void Update()
    {
        //prevent toggle off of the current mode
        if(!flyCam.isOn && !allLayers.isOn && !singleLayer.isOn)
        {
            switch (CAM_MODE)
            {
                case CameraManager.CAM_MODE.FLY_CAM:
                    flyCam.isOn = true;
                    break;
                case CameraManager.CAM_MODE.SINGLE_LAYER:
                    singleLayer.isOn = true;
                    break;
                case CameraManager.CAM_MODE.ALL_LAYERS:
                    allLayers.isOn = true;
                    break;
            }

            }


        switch(CAM_MODE)
        {
            case CameraManager.CAM_MODE.FLY_CAM:

                if(singleLayer.isOn)
                {
                    CAM_MODE = CameraManager.CAM_MODE.SINGLE_LAYER;
                    flyCam.isOn = false;
                    allLayers.isOn = false;
                }
                else if (allLayers.isOn)
                {
                    CAM_MODE = CameraManager.CAM_MODE.ALL_LAYERS;
                    flyCam.isOn = false;
                    singleLayer.isOn = false;
                }

                break;
            case CameraManager.CAM_MODE.SINGLE_LAYER:
                if (flyCam.isOn)
                {
                    CAM_MODE = CameraManager.CAM_MODE.FLY_CAM;
                    singleLayer.isOn = false;
                    allLayers.isOn = false;
                }
                else if (allLayers.isOn)
                {
                    CAM_MODE = CameraManager.CAM_MODE.ALL_LAYERS;
                    flyCam.isOn = false;
                    singleLayer.isOn = false;
                }

                break;
            case CameraManager.CAM_MODE.ALL_LAYERS:
                if (flyCam.isOn)
                {
                    CAM_MODE = CameraManager.CAM_MODE.FLY_CAM;
                    allLayers.isOn = false;
                    singleLayer.isOn = false;
                }
                else if (singleLayer.isOn)
                {
                    CAM_MODE = CameraManager.CAM_MODE.SINGLE_LAYER;
                    flyCam.isOn = false;
                    allLayers.isOn = false;
                }

                break;
        }


    }


    public CameraManager.CAM_MODE CURRENT_TOGGLE
    {
        get { return CAM_MODE; }
    }

}

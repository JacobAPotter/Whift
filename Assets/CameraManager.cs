using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public enum CAM_MODE
    {
        FLY_CAM,
        SINGLE_LAYER,
        ALL_LAYERS
    }


    Camera mainCamera;

    float layerChangeTimeStamp;
    int currentLayer;

    float zoom;

    CAM_MODE CURRENT_MODE;

    Vector3 flyCamRotation = new Vector3(15, 30, 0);
    Vector3 flyCamLayerOffset = new Vector3(-5, 2, -7);
    Vector3 defaultRotation = new Vector3(0, 0, 0);
    Vector3 defaultPosition = new Vector3(0, 1, -30);


    const float MovementSpeed = 15f;
    const float RotationSpeed = 30f;

    void Start()
    {
        mainCamera = transform.Find("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Time.timeSinceLevelLoad - layerChangeTimeStamp > 0.15f)
            {
                currentLayer++;

                if (currentLayer >= GameManager.ActiveGameManager.LayerManager.
                                 Layers.Length)
                    currentLayer = GameManager.ActiveGameManager.LayerManager.
                                 Layers.Length - 1;


                layerChangeTimeStamp = Time.timeSinceLevelLoad;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Time.timeSinceLevelLoad - layerChangeTimeStamp > 0.15f)
            {
                currentLayer--;

                if (currentLayer < 0) currentLayer = 0;

                layerChangeTimeStamp = Time.timeSinceLevelLoad;
            }
        }
        else
            layerChangeTimeStamp = 0;


        //snap in to position
        if ((CURRENT_MODE == CAM_MODE.ALL_LAYERS || CURRENT_MODE == CAM_MODE.SINGLE_LAYER)
            && GameManager.ActiveGameManager.CameraViewToggle.CURRENT_TOGGLE == CAM_MODE.FLY_CAM)
        {
            mainCamera.transform.position = GameManager.ActiveGameManager.LayerManager.
                                Layers[currentLayer].transform.position +
                                flyCamLayerOffset;
        }

        CURRENT_MODE = GameManager.ActiveGameManager.CameraViewToggle.CURRENT_TOGGLE;

        switch (CURRENT_MODE)
        {
            case CAM_MODE.FLY_CAM:

                
                mainCamera.transform.position = Vector3.MoveTowards( 
                    mainCamera.transform.position,
                    GameManager.ActiveGameManager.LayerManager.
                                 Layers[currentLayer].transform.position +
                                 flyCamLayerOffset, 
                                Time.deltaTime * MovementSpeed);

                mainCamera.transform.rotation = Quaternion.Euler(flyCamRotation);


                if (mainCamera.orthographic)
                    mainCamera.orthographic = false;

                break;

            case CAM_MODE.SINGLE_LAYER:
            case CAM_MODE.ALL_LAYERS:

                if (!mainCamera.orthographic)
                    mainCamera.orthographic = true;

                mainCamera.transform.position = defaultPosition;
                mainCamera.transform.rotation = Quaternion.Euler(defaultRotation);

                break;
        }

    }

    public CAM_MODE CURRENT_CAM_MODE
    {
        get { return CURRENT_MODE; }
    }

    public int CurrentLayer
    {
        get { return currentLayer; }
    }

    public Camera MainCamera
    {
        get { return mainCamera; }
    }
}

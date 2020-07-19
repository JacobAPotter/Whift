using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnOrbButton : MonoBehaviour
{
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        button.interactable = GameManager.ActiveGameManager.CameraManager.
            CURRENT_CAM_MODE != 
            CameraManager.CAM_MODE.FLY_CAM;
    }
}

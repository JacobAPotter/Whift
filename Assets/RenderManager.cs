using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
    float renderStartTimeStamp;
    bool rendering;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!rendering && GameManager.ActiveGameManager.RenderViewPanel.RenderViewOn)
        {
            rendering = true;
            renderStartTimeStamp = Time.timeSinceLevelLoad;
        }
        else if(rendering && !GameManager.ActiveGameManager.RenderViewPanel.RenderViewOn)
        {
            rendering = false;
        }
            
    }

    public float RenderTime
    {
        get { return Time.timeSinceLevelLoad - renderStartTimeStamp; }
    }
}

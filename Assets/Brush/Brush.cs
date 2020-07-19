using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush  
{
    protected Orb orb;
    protected ParameterMap parameterMap;

    public virtual void Update()
    { }
    public virtual void Init()
    { }
    public virtual void Paint(int layer, float x, float y)
    { }
    public ParameterMap ParameterMap
    {
        get { return parameterMap; }
    }
}

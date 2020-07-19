using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterMap
{
    List<int> floatKeys;
    List<int> boolKeys;

    List<FloatParameter> floatParameters;
    List<BooleanParameter> boolParameters;

    public ParameterMap()
    {
        floatKeys = new List<int>();
        boolKeys = new List<int>();
        floatParameters = new List<FloatParameter>();
        boolParameters = new List<BooleanParameter>();
    }

    public void AddFloatParameter(FloatParameter p, int key)
    {
        floatKeys.Add(key);
        floatParameters.Add(p);
    }

    public FloatParameter GetFloatParameterByKey(int key)
    {
        int i = floatKeys.IndexOf(key);
        
        if (i >= 0 && i < floatParameters.Count)
            return floatParameters[i];

        return null;
    }

    public void AddBoolParameter(BooleanParameter p, int key)
    {
        boolKeys.Add(key);
        boolParameters.Add(p);
    }

    public BooleanParameter GetBoolParameterByKey(int key)
    {
        int i = boolKeys.IndexOf(key);

        if (i >= 0 && i < boolParameters.Count)
            return boolParameters[i];

        return null;
    }

    public List<FloatParameter> FloatParameters
    {
        get { return floatParameters; }
    }

    public List<BooleanParameter> BoolParameters
    {
        get { return boolParameters; }
    }

    public int GetFloatKeyByIndex(int i)
    {
        return floatKeys[i];
    }
}

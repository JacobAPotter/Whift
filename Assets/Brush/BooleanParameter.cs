using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooleanParameter : Parameter
{
    bool val;

    public BooleanParameter(string name, int key, bool initialVal)
    {
        this.name = name;
        this.ParamKey = key;
        val = initialVal;
    }

    public bool Value
    {
        get { return val; }
        set { val = value; }
    }


}

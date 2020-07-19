using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushParticle  : FloatParameter
{
    string textureName;

    public BrushParticle(string name) : base(name, new float[] { }, new ParameterRange(0,1), 1, -1)
    {
    }
}

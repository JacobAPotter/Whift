using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatParameter : Parameter
{

    float val;
    float rate;
    float min;
    float max;

    float[] lockToValues;

    //nullable range
    ParameterRange constantRange;

    //starting value
    float offset;
    //if not cyclical, should it ascend or descend?
    bool ascend;
    //if cyclical, should it start going up or down?
    bool startAscending;
    //should it go alternate up and down or just go in one direction
    bool cyclical;
    //if cyclical, are we currently going up or down?
    bool ascending;
    //max random amount added each frame
    float randomness;

    public FloatParameter(string name, float[] lockTo, ParameterRange clampRange, float initialValue, int parameterKey)
    {
        val = initialValue;
        min = clampRange.min;
        max = clampRange.max;
        rate = (max - min) * 0.017f;
        lockToValues = lockTo;
        this.name = name;
        constantRange = clampRange;
        cyclical = true;
        startAscending = true;
        ascending = true;
        ParamKey = parameterKey;
    }

    public override void Init()
    {
        val = Offset;

        if (cyclical)
            ascending = startAscending;
        else
            ascending = ascend;

    }

    public override void Update()
    {
        if (ascending)
        {
            val += rate;

            if (randomness > 0)
                val += Random.value * randomness;

            if (val >= max)
            {
                val = max;

                if (cyclical)
                    ascending = false;
            }

        }
        else
        {
            val -= rate;

            if (randomness > 0)
                val -= Random.value * randomness;

            if (val < min)
            {
                val = min;

                if (cyclical)
                    ascending = true;
            }
        }

    }

    void EnforeceParamaterRange(ParameterRange range)
    {
        foreach (float l in lockToValues)
        {
            if (l < range.min || l > range.max)
                Debug.Log("Lock values are out of range: " + name);
        }

        offset = Mathf.Clamp(Offset, range.min, range.max);
        val = Mathf.Clamp(val, range.min, range.max);
        randomness = Mathf.Clamp(randomness, range.min, range.max);
    }

    void ClampRanges()
    {
        offset = Mathf.Clamp(Offset, constantRange.min, constantRange.max);
        val = Mathf.Clamp(val, constantRange.min, constantRange.max);
        randomness = Mathf.Clamp(randomness, constantRange.min, constantRange.max);
    }

    public float GetValue
    {
        get { return val; }
    }

    public ParameterRange ClampRange
    {
        get { return constantRange; }
    }

    public void SetMin(float val)
    {
        min = Mathf.Clamp(val, constantRange.min, constantRange.max);
        max = Mathf.Clamp(max, min, constantRange.max);
    }

    public void SetMax(float val)
    {
        max = Mathf.Clamp(val, constantRange.min, constantRange.max);
        min = Mathf.Clamp(min, constantRange.min, max);
    }

    public void SetRate(float val)
    {
        rate = Mathf.Clamp(val, min, max);
    }

    public void SetOffset(float val)
    {
        offset = Mathf.Clamp(val, min, max);
    }

    public void SetRandom(float val)
    {
        randomness = Mathf.Clamp(val, min, max);
    }

    public void SetCyclical(bool val)
    {
        cyclical = val;
    }

    public void SetStartAscending(bool val)
    {
        startAscending = val;
    }

    public void SetAscend(bool val)
    {
        ascend = val;
    }

    public float Min
    {
        get { return min; }
    }
    public float Max
    {
        get { return max; }
    }
    public float Rate
    {
        get { return rate; }
    }
    public float Offset
    {
        get { return offset; }
    }
    public float Randomness
    {
        get { return randomness; }
    }
    public bool IsCyclical
    {

        get { return cyclical; }
    }
    public bool Ascend
    {
        get { return ascend; }
    }
    public bool StartAscending
    {
        get { return startAscending; }
    }

}

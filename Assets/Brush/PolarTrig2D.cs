using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarTrig2D : Brush
{
    BrushParticle particle;
    FloatParameter emission;
    FloatParameter XRadius;
    FloatParameter YRadius;
    BooleanParameter SyncRadius;
    FloatParameter SineCoefficient;
    FloatParameter CosineCoefficient;
    FloatParameter SineExponent;
    FloatParameter CosineExponent;
    FloatParameter Hue;
    FloatParameter Saturation;
    FloatParameter Brightness;
    FloatParameter Alpha;
    FloatParameter Theta;


    public PolarTrig2D() : base()
    {
        parameterMap = new ParameterMap();

        emission = new FloatParameter("Emission", new float[] { }, new ParameterRange(0, 60), 10, ParameterDictionary.EmissionKey);
        XRadius = new FloatParameter("X Radius", new float[] { 1f, 2f, 3f, 4f }, new ParameterRange(0, 5f), 1, ParameterDictionary.X_RadiusKey);
        YRadius = new FloatParameter("Y Radius", new float[] { 1f, 2f, 3f, 4f }, new ParameterRange(0, 5f), 1, ParameterDictionary.Y_RadiusKey);
        SyncRadius = new BooleanParameter("Synch Radius", ParameterDictionary.SyncRadiusKey , true);
        SineCoefficient = new FloatParameter("Sine Coeffecient", new float[] { }, new ParameterRange(0, 50), 5, ParameterDictionary.SineCoefKey);
        CosineCoefficient = new FloatParameter("Cosine Coeffecient", new float[] { }, new ParameterRange(0, 50), 19, ParameterDictionary.CosCoefKey);
        SineExponent = new FloatParameter("Sine Exponent", new float[] { 0.5f, 1f, 2f, 3f, 4f }, new ParameterRange(0.25f, 4f), 2, ParameterDictionary.SineExponentKey);
        CosineExponent = new FloatParameter("Cosine Exponent", new float[] { 0.5f, 1f, 2f, 3f, 4f }, new ParameterRange(0.25f, 4f), 2, ParameterDictionary.CosExponentKey);
        Hue = new FloatParameter("Hue", new float[] { }, new ParameterRange(0, 360), 0, ParameterDictionary.HueKey);
        Saturation = new FloatParameter("Saturation", new float[] { }, new ParameterRange(0, 1), 0.8f, ParameterDictionary.SaturationKey);
        Brightness = new FloatParameter("Brightness", new float[] { }, new ParameterRange(0, 1), 0.8f, ParameterDictionary.BrightnessKey);
        Alpha = new FloatParameter("Alpha", new float[] { }, new ParameterRange(0, 1f), 0.5f, ParameterDictionary.AlphaKey);
        Theta = new FloatParameter("Theta", new float[] { Mathf.PI / 2, Mathf.PI, 3 * Mathf.PI / 2 }, new ParameterRange(0, 2 * Mathf.PI), 3.14f, ParameterDictionary.ThetaKey);

        parameterMap.AddFloatParameter(emission, ParameterDictionary.EmissionKey);
        parameterMap.AddFloatParameter(XRadius, ParameterDictionary.X_RadiusKey);
        parameterMap.AddFloatParameter(YRadius, ParameterDictionary.Y_RadiusKey);
        parameterMap.AddBoolParameter(SyncRadius, ParameterDictionary.SyncRadiusKey);
        parameterMap.AddFloatParameter(SineCoefficient, ParameterDictionary.SineCoefKey);
        parameterMap.AddFloatParameter(CosineCoefficient, ParameterDictionary.CosCoefKey);
        parameterMap.AddFloatParameter(SineExponent, ParameterDictionary.SineExponentKey);
        parameterMap.AddFloatParameter(CosineExponent, ParameterDictionary.CosExponentKey);
        parameterMap.AddFloatParameter(Hue, ParameterDictionary.HueKey);
        parameterMap.AddFloatParameter(Saturation, ParameterDictionary.SaturationKey);
        parameterMap.AddFloatParameter(Brightness, ParameterDictionary.BrightnessKey);
        parameterMap.AddFloatParameter(Alpha, ParameterDictionary.AlphaKey);
        parameterMap.AddFloatParameter(Theta, ParameterDictionary.ThetaKey);
    }

    public override void Init()
    {
        foreach (FloatParameter p in parameterMap.FloatParameters)
            p.Init();
        
    }

    public void SetParameterValues(FloatParameter f)
    {
        FloatParameter param = parameterMap.GetFloatParameterByKey(f.ParamKey);

        if (param == null)
            return;

        param.SetMin(f.Min);
        param.SetMax(f.Max);
        param.SetRate(f.Rate);
        param.SetOffset(f.Offset);
        param.SetRandom(f.Randomness);
        param.SetCyclical(f.IsCyclical);
        param.SetAscend(f.Ascend);
        param.SetStartAscending(f.StartAscending);
    }

    public override void Update()
    {
        foreach (FloatParameter p in parameterMap.FloatParameters)
            p.Update();
    }

    public override void Paint(int layer, float x, float y)
    {
        LayerTexture layerTexture = GameManager.ActiveGameManager.LayerManager.Layers[layer].LayerTexture;

        float xPixelsPerUnit = layerTexture.Size.x / layerTexture.GetComponent<Renderer>().bounds.extents.x;
        float yPixelPerUnit = layerTexture.Size.y / layerTexture.GetComponent<Renderer>().bounds.extents.y;

        //get the physical size of the layer panel 
        //Why does x need tex.width/2 added to it, 
        //but y doesnt: the center of the render texture is at 0,1
        float xPixel = (layerTexture.Size.x / 2) + (x * xPixelsPerUnit * 0.5f);
        float yPixel =  (y * yPixelPerUnit * 0.5f);

        //if (emission.GetValue < GameManager.ActiveGameManager.RenderManager.RenderTime)
        { 
            int drawCount = GameManager.ActiveGameManager.OrbManager.DrawPerFrame;
            
            int i = 0;
            const float pixelCoef = 56;

            while (i++ < drawCount)
            {
                float r = Mathf.Pow(Mathf.Sin(SineCoefficient.GetValue * Theta.GetValue), SineExponent.GetValue) +
                          Mathf.Pow(Mathf.Cos(CosineCoefficient.GetValue * Theta.GetValue), CosineExponent.GetValue) * pixelCoef;
                 
                int xPos = (int)(xPixel + Mathf.Cos(Theta.GetValue) * Mathf.Clamp(r * XRadius.GetValue, XRadius.Min * pixelCoef, XRadius.Max * pixelCoef) );
                int yPos = (int)(yPixel + Mathf.Sin(Theta.GetValue) * Mathf.Clamp(r * YRadius.GetValue, YRadius.Min * pixelCoef, YRadius.Max * pixelCoef) );

                HSL_Color h = new HSL_Color(Mathf.Clamp(Theta.GetValue * Hue.Rate *(Hue.Max - Hue.Min) , Hue.Min, Hue.Max), Mathf.Clamp(Theta.GetValue * Saturation.Rate, Saturation.Min, Saturation.Max),Brightness.GetValue);
                Color col = h.ToRGB();
                col.a = Alpha.GetValue;


                Hue.Update();
                Saturation.Update();
                Brightness.Update();
                Alpha.Update();
                Theta.Update();

                layerTexture.WriteToTexture(xPos, yPos, col);
            }
        }
    }

    Vector2 NextCoordinate()
    {
        float xRad = XRadius.GetValue;
        float yRad;

        if (SyncRadius.Value)
            yRad = xRad;
        else
            yRad = YRadius.GetValue;

        float r = Mathf.Pow(Mathf.Sin(SineCoefficient.GetValue * Theta.GetValue), SineExponent.GetValue) +
                  Mathf.Pow(Mathf.Cos(CosineCoefficient.GetValue * Theta.GetValue), CosineExponent.GetValue);

        if (float.IsNaN(r) || float.IsInfinity(r))
            return Vector2.zero;

        return new Vector2(Mathf.Cos(Theta.GetValue) * xRad * r,
                            Mathf.Sin(Theta.GetValue) * yRad * r);
    }

    public void SetSyncRadius(bool s)
    {
        SyncRadius.Value = true;
    }
}

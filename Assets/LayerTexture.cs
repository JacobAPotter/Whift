using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTexture : MonoBehaviour
{
    Texture2D renderTexture;
    Material renderMaterial;
    bool modified;

    private void Start()
    {
        renderTexture = new Texture2D(Display.main.renderingWidth, Display.main.renderingHeight);

        for (int x = 0; x < renderTexture.width; x++)
            for (int y = 0; y < renderTexture.height; y++)
                renderTexture.SetPixel(x, y, new Color(0f, 0f, 0f, 0f));
        renderTexture.Apply();
        renderMaterial = GetComponent<Renderer>().material;
        renderMaterial.mainTexture = renderTexture;
    }

    private void LateUpdate()
    {
        if (modified)
        {
            ApplyTexture();
            modified = false;
        }
    }

    public void WriteToTexture(Vector2Int point, Color col)
    {
        WriteToTexture(point.x, point.y, col);
    }

    public void WriteToTexture(int x, int y, Color col)
    {
        ////1-(1-a)(1-b)
        //col *= 0.1f;
        Color old = renderTexture.GetPixel(x, y);

        //Color newCol = new Color(1f - ((1f - col.r) * (1f - old.r)),
        //                        1f - ((1f - col.g) * (1f - old.g)),
        //                        1f - ((1f - col.b) * (1f - old.b)),
        //           

        float r;
        if (old.r < 0.5)
            r = (2 * col.r * old.r) + ((col.r * col.r) * (1f - (2 * old.r)));
        else
            r = ((2 * col.r) * (1f - old.r)) + (Mathf.Sqrt(col.r) * ((2 * old.r) - 1f));

        float g;
        if (old.g < 0.5)
            g = (2 * col.g * old.g) + ((col.g * col.g) * (1f - (2 * old.g)));
        else
            g = ((2 * col.g) * (1f - old.g)) + (Mathf.Sqrt(col.g) * ((2 * old.g) - 1f));

        float b;
        if (old.b < 0.5)
            b = (2 * col.b * old.b) + ((col.b * col.b) * (1f - (2 * old.b)));
        else
            b = ((2 * col.b) * (1f - old.b)) + (Mathf.Sqrt(col.b) * ((2 * old.b) - 1f));

        float a = col.a + old.a;

        renderTexture.SetPixel(x, y, new Color(r,g,b,a));
        modified = true;
    }

    public void ApplyTexture()
    {
        renderTexture.Apply();
    }

    public Vector2Int Size
    {
        get { return new Vector2Int(renderTexture.width, renderTexture.height); }
    }
}

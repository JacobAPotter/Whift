using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueCycleSprite : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float hue;
    public float rate;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hue = Random.value * 360;
    }

    void Update()
    {
        spriteRenderer.color = new HSL_Color(hue,0.3f,0.3f).ToRGB();
        hue += Time.deltaTime * rate;
    }
}

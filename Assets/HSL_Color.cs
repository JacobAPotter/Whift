using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSL_Color 
{
        float h;
        float s;
        float l;

        public HSL_Color()
        {
            h = 0;
            s = 0;
            l = 0;
        }

        //color in hsl space, H in 0-360, S in 0-1, L in 0-1
        public HSL_Color(float h, float s, float l)
        {
            this.h = ClampDegrees(h);
            this.s = Mathf.Clamp(s, 0f, 1);
            this.l = Mathf.Clamp(l, 0f, 1f);
        }

        public float H
        {
            get { return h; }
            set { h = ClampDegrees(value); }
        }

        public float S
        {
            get { return s; }
            set { s = Mathf.Clamp(value, 0f, 1f); }
        }

        public float L
        {
            get { return l; }
            set
            {
                l = Mathf.Clamp(value, 0f, 1f);
            }
        }

        public Color ToRGB_LowAlpha()
        {
            float p2;

            if (l <= 0.5)
                p2 = l * (1 + s);
            else
                p2 = l + s - l * s;

            float p1 = 2 * l - p2;

            float r, g, b;

            if (s == 0)
            {
                r = l;
                g = l;
                b = l;
            }
            else
            {
                r = QqhToRgb(p1, p2, h + 120);
                g = QqhToRgb(p1, p2, h);
                b = QqhToRgb(p1, p2, h - 120);
            }

            // Convert RGB to the 0 to 255 range.
            return new Color(r, g, b, 0.1f);
        }
        public Color ToRGB()
        {
            float p2;

            if (l <= 0.5)
                p2 = l * (1 + s);
            else
                p2 = l + s - l * s;

            float p1 = 2 * l - p2;

            float r, g, b;

            if (s == 0)
            {
                r = l;
                g = l;
                b = l;
            }
            else
            {
                r = QqhToRgb(p1, p2, h + 120);
                g = QqhToRgb(p1, p2, h);
                b = QqhToRgb(p1, p2, h - 120);
            }

            // Convert RGB to the 0 to 255 range.
            return new Color(r, g, b);
        }
        private static float QqhToRgb(float q1, float q2, float hue)
        {

            hue = ClampDegrees(hue);

            if (hue < 60) return q1 + (q2 - q1) * hue / 60;
            if (hue < 180) return q2;
            if (hue < 240) return q1 + (q2 - q1) * (240 - hue) / 60;
            return q1;
        }

        public static float ClampDegrees(float d)
        {
            if (d < 0)
                return 359.999f + (d % 360);
            else
                return d % 360;
        }
    }

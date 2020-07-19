using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    Transform grid;
    LayerTexture layerTexture;
    List<Orb> orbs;
    bool layerActive;
    Color layerColor;
    Vector3 volume;

    private void Awake()
    {
        orbs = new List<Orb>();
    }

    void Start()
    {
        grid = transform.Find("Renderer");
        layerTexture = grid.Find("LayerTexture").GetComponent<LayerTexture>();
        float displayRatio = (float)Display.main.renderingWidth / Display.main.renderingHeight;
        grid.localScale = new Vector3(displayRatio, 1f, 1f);

    }

    private void Update()
    {
        float displayRatio = (float)Display.main.renderingWidth / Display.main.renderingHeight;
        grid.localScale = new Vector3(displayRatio, 1f, 1f);

        //make sure orbs arent inside eachother 
        if (layerActive)
        { 
            
            foreach (Orb o in orbs)
            {
                //TO DO: FIX O(N^2)
                //if o has moved..

                foreach (Orb o2 in orbs)
                    {
                        if(o != o2)
                        {
                            Vector3 diff = o.transform.position - o2.transform.position;

                            float dist = diff.magnitude;

                            diff.z = 0;

                            if (dist < 0.001f)
                                diff = new Vector3(1,0, 0);

                            if (dist < o.Radius * 2)
                                o.transform.Translate(diff.normalized * Mathf.Max((o.Radius - dist) * 0.004f, 0.001f));
                        }
                }
            }
        }
    }

    public bool Active
    {
        get { return layerActive; }
        set { layerActive = value;

            if (layerActive && !grid.gameObject.activeSelf)
                grid.gameObject.SetActive(true);
            else if (!layerActive && grid.gameObject.activeSelf)
                grid.gameObject.SetActive(false);
            }
    }

    public void AddOrb(Orb o)
    {
        orbs.Add(o);
    }

    public void RemoveOrb(Orb o)
    {
        orbs.Remove(o);
    }

    public Color LayerColor
    {
        get { return layerColor; }
        set { layerColor = value; }
    }

    public LayerTexture LayerTexture
    {
        get { return layerTexture; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolarBrushWindow : MonoBehaviour
{
    List<ParameterInput> parameterInput;
    List<Button> buttons;
    Toggle syncToggle;
    PolarTrig2D polarBrush;
    ParameterWindow parameterWindow;

    private void Start()
    {
        parameterWindow = transform.parent.Find("ParameterWindow").GetComponent<ParameterWindow>();
        parameterWindow.gameObject.SetActive(false);
    }

    public void Init(PolarTrig2D brush)
    {
        polarBrush = brush;
        buttons = new List<Button>();


        for (int i = 0; i < transform.childCount; i++)
        {
            //assuming this child is a ui element on the window, get the elements parameter key
            ParameterInput p = transform.GetChild(i).GetComponent<ParameterInput>();

            if (p != null) //if it has a ParameterInput, proceed
            {
                //find the parameter in this brush that corrisponds to the parameter key for this button
                FloatParameter f = polarBrush.ParameterMap.GetFloatParameterByKey(p.parameterKey);

                //if its not boolean then its a floatParameter
                if (f != null && !p.isBoolen)
                {
                    Button btn = transform.GetChild(i).Find("Button").GetComponent<Button>();
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(delegate { OpenParameterWindow(p.parameterKey); });
                    buttons.Add(btn);
                    continue;
                }

                BooleanParameter b = polarBrush.ParameterMap.GetBoolParameterByKey(p.parameterKey);
                Toggle tog = transform.GetChild(i).GetComponent<Toggle>();
                tog.onValueChanged.RemoveAllListeners();

                if (p.parameterKey == ParameterDictionary.SyncRadiusKey)
                {
                    syncToggle = tog;
                    syncToggle.isOn = brush.ParameterMap.GetBoolParameterByKey(ParameterDictionary.SyncRadiusKey).Value;
                    tog.onValueChanged.AddListener(delegate { SetSyncRadius(); });

                }
            }
        }
    }

    void OpenParameterWindow(int paramKey)
    {
        parameterWindow.gameObject.SetActive(true);
        parameterWindow.InitWithParameter(polarBrush.ParameterMap.GetFloatParameterByKey(paramKey));
    }

    //should x and y radius be synced
    void SetSyncRadius()
    {
        foreach (Button b in buttons)
        {
            ParameterInput p = b.transform.parent.GetComponent<ParameterInput>();

            if (p != null)
            {
                int k = p.parameterKey;

                if (k == ParameterDictionary.Y_RadiusKey)
                {
                    b.interactable = !syncToggle.isOn;
                    polarBrush.SetSyncRadius(syncToggle.isOn);
                    return;
                }
            }
        }
    }
}

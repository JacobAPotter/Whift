using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushEditor : MonoBehaviour
{
    Text windowLabel;
    PolarBrushWindow polarBrushWidow;
    ParameterWindow parameterWindow;
    Dropdown brushDropdownList;
    public Dropdown presetDropdownList { get; private set; }
    public InputField presetInputField { get; private set; }
    public Button savePresetButton { get; private set; }
    Orb selectedOrb;

    private void Start()
    {
        gameObject.SetActive(false);
        polarBrushWidow = transform.Find("PolarBrushWindow").GetComponent<PolarBrushWindow>();
        parameterWindow = transform.Find("ParameterWindow").GetComponent<ParameterWindow>();
        brushDropdownList = transform.Find("TypeLabel").Find("Dropdown").GetComponent<Dropdown>();
        windowLabel = transform.Find("DragPanel").Find("BrushWindowLabel").GetComponent<Text>();
        presetDropdownList = transform.Find("PresetLabel").Find("Dropdown").GetComponent<Dropdown>();
        presetInputField = transform.Find("PresetInputField").GetComponent<InputField>();
        savePresetButton = transform.Find("SavePresetButton").GetComponent<Button>();

        presetDropdownList.onValueChanged.AddListener(delegate { OnPresetDropdownChanged(); });
    }

    private void Update()
    {
        if (GameManager.ActiveGameManager.CameraViewToggle.CURRENT_TOGGLE == CameraManager.CAM_MODE.FLY_CAM)
        {
            GameManager.ActiveGameManager.OrbManager.SetSelectedToNull();
            gameObject.SetActive(false);
        }
    }

    public void SetSelectedOrb(Orb o)
    {
        //orb manager will pass in a null orb when the selected orb is deleted.
        if (o == null)
        {
            selectedOrb = null;
            gameObject.SetActive(false);
            return;
        }

        if (selectedOrb == o)
            return;

        windowLabel.text = o.name;

        //open the appropriate window
        if (brushDropdownList.options[brushDropdownList.value].text == "Polar Brush")
        {
            //see if we can keep the parameter window open to the same param but initialize it with our new orb
            if (parameterWindow.gameObject.activeSelf)
            {
                if (parameterWindow.CurrentParameter != null)
                {
                    FloatParameter fp = o.PolarBrush.ParameterMap.GetFloatParameterByKey(parameterWindow.CurrentParameter.ParamKey);

                    if (fp != null)
                        parameterWindow.InitWithParameter(fp);
                    else
                        parameterWindow.gameObject.SetActive(false);
                }
                else
                    parameterWindow.gameObject.SetActive(false);
            }

            polarBrushWidow.gameObject.SetActive(true);
            polarBrushWidow.Init(o.PolarBrush);
        }

        selectedOrb = o;
    }

    public Orb CurrentOrb
    {
        get { return selectedOrb; }
    }

    public string BrushType
    {
        get { return brushDropdownList.options[brushDropdownList.value].text; }
    }

    public string PresetInputField
    {
        get { return presetInputField.text; }
    }

    public void OnPresetDropdownChanged()
    {
        if(presetDropdownList.options[presetDropdownList.value].text == "Custom")
        {
            GameManager.ActiveGameManager.OrbManager.CurrentOrb.UseCustomPolarBrush(true);

            //init the window with the custom parameters
            if(GameManager.ActiveGameManager.BrushEditor.polarBrushWidow.gameObject.activeSelf)
                GameManager.ActiveGameManager.BrushEditor.polarBrushWidow.Init(GameManager.ActiveGameManager.
                OrbManager.CurrentOrb.PolarBrush);
        }
        else
        {
            GameManager.ActiveGameManager.OrbManager.CurrentOrb.UseCustomPolarBrush(false);
            GameManager.ActiveGameManager.PresetManager.LoadPresetToOrb(presetDropdownList.options[presetDropdownList.value].text,
            GameManager.ActiveGameManager.OrbManager.CurrentOrb);
        }
    }

}

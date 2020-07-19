using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePresetButton : MonoBehaviour
{
    Button button;
    InputField inputField;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { SavePreset(); });
        inputField = GameManager.ActiveGameManager.BrushEditor.presetInputField;
    }

    void SavePreset()
    {
        if(inputField.text.Length > 0)
            GameManager.ActiveGameManager.PresetManager.SavePreset(inputField.text);
    }

}

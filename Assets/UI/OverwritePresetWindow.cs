using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverwritePresetWindow : MonoBehaviour
{
    Button yesButton;
    Button noButton;
    Text windowText;

    void Start()
    {
        
        yesButton = transform.Find("YesButton").GetComponent<Button>();
        noButton = transform.Find("NoButton").GetComponent<Button>();

        yesButton.onClick.AddListener(delegate { OverWritePreset(); });
        noButton.onClick.AddListener(delegate{ CloseWindow(); });

        windowText = transform.Find("Text").GetComponent<Text>();

        gameObject.SetActive(false);
    }

    public void OpenWindow()
    {
        windowText.text = "Overwrite parameter " + GameManager.ActiveGameManager.BrushEditor.PresetInputField + "?";
    }

    public void OverWritePreset()
    {
        GameManager.ActiveGameManager.PresetManager.OverwritePreset(GameManager.ActiveGameManager.BrushEditor.PresetInputField);
        gameObject.SetActive(false);
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}

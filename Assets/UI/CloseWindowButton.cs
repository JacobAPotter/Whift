using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseWindowButton : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { CloseWindow(); });
    }

    

    public void CloseWindow()
    {
        transform.parent.gameObject.SetActive(false);
    }
}

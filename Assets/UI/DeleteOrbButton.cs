using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeleteOrbButton : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { DeleteOrb(); });
    }

    void DeleteOrb()
    {
        GameManager.ActiveGameManager.OrbManager.DeleteSelectedOrb();
    }

}

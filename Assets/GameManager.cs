using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    public CameraManager CameraManager { get; private set; }
    public ViewPanel CameraViewToggle { get; private set; }
    public LayerManager LayerManager { get; private set; }
    public LayerToggle LayerToggle { get; private set; }
    public ToggleGrid ToggleGrid { get; private set; }
    public RenderViewPanel RenderViewPanel { get; private set; }
    public RenderManager RenderManager { get; private set; }
    public OrbManager OrbManager { get; private set; }
    public BrushEditor BrushEditor { get; private set; }
    public EventSystem EventSystem { get; private set; }
    public PresetManager PresetManager { get; private set; }
    public OverwritePresetWindow OverwritePresetWindow { get; private set; }
    public MessageQueue MessageQueue { get; private set; }


    private void Awake()
    {
        gameManager = this;

        CameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();

        CameraViewToggle = GameObject.Find("UI").
                           transform.Find("Canvas").
                           transform.Find("ViewPanel").GetComponent<ViewPanel>();

        LayerManager = GameObject.Find("Layers").GetComponent<LayerManager>();

        LayerToggle = GameObject.Find("UI").
                           transform.Find("Canvas").
                           transform.Find("LayerTogglePanel").GetComponent<LayerToggle>();

        ToggleGrid = GameObject.Find("UI").
                           transform.Find("Canvas").
                           transform.Find("ToggleGrid").GetComponent<ToggleGrid>();

        RenderViewPanel = GameObject.Find("UI").
                           transform.Find("Canvas").
                           transform.Find("RenderViewPanel").GetComponent<RenderViewPanel>();

        RenderManager = GameObject.Find("RenderManager").GetComponent<RenderManager>();

        OrbManager = GameObject.Find("OrbManager").GetComponent<OrbManager>();

        BrushEditor = GameObject.Find("UI").
                           transform.Find("Canvas").
                           transform.Find("BrushWindow").GetComponent<BrushEditor>();

        EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        PresetManager = GameObject.Find("PresetManager").GetComponent<PresetManager>();

        OverwritePresetWindow = GameObject.Find("UI").
                                   transform.Find("Canvas").
                                   transform.Find("OverwritePresetWindow").GetComponent<OverwritePresetWindow>();

        MessageQueue = GameObject.Find("UI").transform.Find("Canvas")
                      .transform.Find("MessageQueue").GetComponent<MessageQueue>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    public static GameManager ActiveGameManager
    {
        get { return gameManager; }
    }
}

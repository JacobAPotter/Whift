using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragPanelManager : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    DragPanel currentDragPanel;
    Vector3 mouseDown;
    Vector3 dragPanelStart;

    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GameManager.ActiveGameManager.EventSystem;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            foreach (RaycastResult result in results)
            {

                DragPanel dragPanel = result.gameObject.GetComponent<DragPanel>();

                if (dragPanel != null)
                {
                    currentDragPanel = dragPanel;
                    mouseDown = Input.mousePosition;
                    dragPanelStart = currentDragPanel.transform.parent.GetComponent<RectTransform>().anchoredPosition;
                }
            }
        }


        if(Input.GetMouseButton(0) && currentDragPanel != null)
        {
            currentDragPanel.transform.parent.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition - mouseDown + dragPanelStart;
        }
        else
        {
            currentDragPanel = null;
        }

    }
}

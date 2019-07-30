using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour
{
    public static TabController instance;
    private GameObject activePanel;
    private GameObject activeTab;

    private void Start()
    {
        instance = this;
    }

    public void SetPanelActive(GameObject tab, GameObject panel)
    {
        if(activePanel != null)
        {
            activePanel.SetActive(false);
            activeTab.GetComponent<Canvas>().sortingOrder = 1;
        }
        if (panel != null)
        {
            panel.SetActive(true);
            activePanel = panel;
            activeTab = tab;
            activeTab.GetComponent<Canvas>().sortingOrder = 3;
        }
    }
}

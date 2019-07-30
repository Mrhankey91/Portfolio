using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    public bool startTab = false;
    public GameObject panel;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(TabSelect);
        if (startTab)
        {
            TabSelect();
        }
        else
        {
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }
    }

    private void TabSelect()
    {
        TabController.instance.SetPanelActive(this.gameObject, panel);
    }
}

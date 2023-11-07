using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;
using System;
using UnityEngine.UI;
using TMPro;

public class Navigation : MonoBehaviour
{
    [Header("Parameters")]
    public MapSelection[] mapSelections;

    [Header("Components")]
    public Panel startingPanel;
    public NavigationSwipePanel navigationSwipePanel;
    public MapNavigationPanel mapNavigationPanel;
    public TextMeshProUGUI bottomBarText;

    [Header("Debug")]
    public Panel currentPanel;
    public List<Panel> panels = new List<Panel>();

    protected virtual void Awake()
    {
        HandlePanelSetup();
    }

    private void HandlePanelSetup()
    {
        if (navigationSwipePanel)
        {
            panels.Add(navigationSwipePanel);
        }

        if (mapNavigationPanel)
        {
            panels.Add(mapNavigationPanel);
        }

        foreach (Panel panel in panels)
        {
            panel.SetupPanel();
        }

        OpenPanel(startingPanel);
    }

    public virtual void OpenPanel(Panel panel)
    {
        foreach(Panel p in panels)
        {
            if(panel == p) 
            {
                p.Open();
            }

            else
            {
                p.Close();
            }
        }

        currentPanel = panel;
    }
}

[System.Serializable]
public class MapSelection
{
    public string locationName;
    public Sprite mapSprite;
    public Sprite mapSelectionSprite;
    public int min;
    public float dist;
    public float defaultZoom = 0;
}


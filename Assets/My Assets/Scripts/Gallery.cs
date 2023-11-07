using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Gallery : MonoBehaviour
{
    [Header("Components")]
    public Panel startingPanel;
    public GallerySwipePanel gallerySwipePanel;
    public GalleryViewPanel galleryViewPanel;
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
        if (gallerySwipePanel)
        {
            gallerySwipePanel.Gallery = this;
            panels.Add(gallerySwipePanel);
        }

        if (galleryViewPanel)
        {
            galleryViewPanel.Gallery = this;
            panels.Add(galleryViewPanel);
        }

        foreach (Panel panel in panels)
        {
            panel.SetupPanel();
        }

        OpenPanel(startingPanel);
    }

    public virtual void OpenPanel(Panel panel)
    {
        foreach (Panel p in panels)
        {
            if (panel == p)
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

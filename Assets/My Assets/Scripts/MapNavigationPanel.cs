using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapNavigationPanel : Panel
{
    [Header("Parameters")]
    public MapNavigationState[] mapNavigationStates;

    [Header("Components")]
    public Slider zoomSlider;
    public Map map;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI locationText;
    public GameObject infoPanel;
    public TextMeshProUGUI bottomBarText;

    [Header("Debug")]
    public MapNavigationState currentMapNavigationState;
    public NavigationState currentNavState;
  
    private void Awake()
    {              
        if (zoomSlider)
        {
            zoomSlider.onValueChanged.AddListener(OnValueChanged);
        }
    }

    public override void SetupPanel()
    {
        base.SetupPanel();

        SetMapNavigationState(NavigationState.InfoState);
    }

    public virtual void SetTimeText(int min)
    {
        if (timeText)
        {
            timeText.text = min.ToString() + " min";
        }
    }

    public virtual void SetDistText(float dist)
    {
        if (distanceText)
        {
            distanceText.text = dist.ToString() + " mi";
        }
    }

    public virtual void SetLocationName(string locationName)
    {
        if (locationText)
        {
            locationText.text = locationName;
        }
    }

    public virtual void SetMapNavigationState(NavigationState navigationState)
    {
        foreach(MapNavigationState mapNavigationState in mapNavigationStates)
        {
            if (mapNavigationState.navigationState == navigationState)
            {             
                map.mapMask.rectTransform.sizeDelta = mapNavigationState.sizeDelta;               
                map.mapMask.rectTransform.anchoredPosition = mapNavigationState.anchorPos;
                currentMapNavigationState = mapNavigationState;
                bottomBarText.text = mapNavigationState.bottomBarString;
            }
        }

        switch (navigationState)
        {
            case NavigationState.InfoState:

                zoomSlider.gameObject.SetActive(false);
                infoPanel.SetActive(true);
                
                break;

            case NavigationState.ZoomState:

                zoomSlider.gameObject.SetActive(true);
                infoPanel.SetActive(false);

                if (zoomSlider && map)
                {
                    zoomSlider.value = map.zoomScale;
                }

                break;

            case NavigationState.PanState:

                zoomSlider.gameObject.SetActive(false);
                infoPanel.SetActive(false);

                break;                 
        }

        currentNavState = navigationState;
    }
    private void OnValueChanged(float value)
    {
        if (map)
        {
            map.SetMapZoom(value);
        }
    }

    [System.Serializable]
    public class MapNavigationState
    {
        public NavigationState navigationState;
        public Vector2 anchorPos;
        public Vector2 sizeDelta;
        public string bottomBarString;
    }

    public enum NavigationState
    {
        InfoState,
        ZoomState,
        PanState
    }
}

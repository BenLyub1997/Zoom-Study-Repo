using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class NavigationSwipePanel : Panel
{
    [Header("Components")]
    public Navigation navigation;

    [Header("Parameters")]
    public int startingSelection = 0;
    public SelectionEllipse selectionEllipse;
    public Transform ellipseLayoutGroup;
    public SimpleScrollSnap scrollSnap;
    public MapSelectionPrefab mapSelectionPrefab;

    [Header("Debug")]
    public int hoverIndex;
    public List<MapSelectionPrefab> mapSelectionPrefabs = new List<MapSelectionPrefab>();
    public List<SelectionEllipse> selectionEllipses = new List<SelectionEllipse>();

    MapSelection[] mapSelections;

    InputModule inputModule;

    public MapSelection[] MapSelections
    {
        get { return mapSelections; }
     
        set
        {    
            if (mapSelectionPrefabs.Count > 0)
            {
                ClearMapSelections();
            }

            mapSelections = value;         
        }
    }

    protected virtual void Start()
    {
        inputModule = InputModule.instance;
        inputModule.inputEvent.AddListener(OnInputEvent);
    }

    private void OnInputEvent(InputModule.InputType arg0)
    {
        switch (arg0)
        {
            case InputModule.InputType.TWO_FINGERS_RELEASE:

                GoToPreviousMapSelection();
                
                break;
            case InputModule.InputType.TWO_FINGERS_SWIPE:

                GoToNextMapSelection();
                
                break;
        }
    }

    public override void SetupPanel()
    {
        base.SetupPanel();

        if (scrollSnap)
        {
            scrollSnap.OnPanelSelected.AddListener(OnPanelSelected);
        }

        MapSelections = navigation.mapSelections;

        if (mapSelections.Length > 0)
        {
            GenerateMapSelections(mapSelections);
            HighlightMapSelection(startingSelection);
        }
    }

    public virtual void GoToPreviousMapSelection()
    {
        if (hoverIndex > 0)
        {
            HighlightMapSelection(hoverIndex -= 1);
        }
    }

    public virtual void GoToNextMapSelection()
    {
        if (hoverIndex < scrollSnap.NumberOfPanels - 1)
        {
            HighlightMapSelection(hoverIndex += 1);
        }
    }

    private void HighlightMapSelection(int selection)
    {
        scrollSnap.GoToPanel(selection);

        for(int i = 0; i < selectionEllipses.Count; i++)
        {
            if(i == selection)
            {
                selectionEllipses[selection].Select();
            }
        
            else
            {
                selectionEllipses[i].UnSelect();
            }
        }

        hoverIndex = selection;
    }

    public virtual void GenerateMapSelections(MapSelection[] mapSelections)
    {
        for (int i = 0; i < mapSelections.Length; i++)
        {
            GenerateMapSelection(mapSelections[i], i);
        }
    }

    public virtual void GenerateMapSelection(MapSelection mapSelection, int index)
    {
        GameObject mapSelectionObj = Instantiate(mapSelectionPrefab.gameObject);
        MapSelectionPrefab mapSelectionPFab = mapSelectionObj.GetComponent<MapSelectionPrefab>();

        mapSelectionObj.transform.localScale = Vector3.one;
        mapSelectionObj.transform.localRotation = Quaternion.Euler(0, 0, 0);

        mapSelectionPFab.SetMapSelection(mapSelection, this);

        mapSelectionPrefabs.Add(mapSelectionPFab);

        GenerateSelectionEllipse();

        AddPanel(mapSelectionObj, index);
    }

    private void GenerateSelectionEllipse()
    {
        GameObject selectionEllipseObj = Instantiate(selectionEllipse.gameObject);
        SelectionEllipse selectionEllipsePFab = selectionEllipseObj.GetComponent<SelectionEllipse>();

        selectionEllipseObj.transform.SetParent(ellipseLayoutGroup);
        selectionEllipseObj.transform.localPosition = Vector3.zero;
        selectionEllipseObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
        selectionEllipseObj.transform.localScale = Vector3.one;
        selectionEllipsePFab.UnSelect();

        selectionEllipses.Add(selectionEllipsePFab);
    }

    public virtual void ClearMapSelections()
    {
        foreach (MapSelectionPrefab mapSelectionPrefab in mapSelectionPrefabs)
        {
            Destroy(mapSelectionPrefab.gameObject);
        }

        mapSelectionPrefabs.Clear();
    }

    public virtual void AddPanel(GameObject obj, int index)
    {
        scrollSnap.Add(obj, index);
    }

    private void OnPanelSelected(int panel)
    {
       
    }

    public virtual void Select()
    {
        navigation.OpenPanel(navigation.mapNavigationPanel);
        navigation.mapNavigationPanel.map.SetMapImage(mapSelectionPrefabs[hoverIndex].CurrentMapSelection.mapSprite);
        navigation.mapNavigationPanel.map.SetMapZoom(mapSelectionPrefabs[hoverIndex].CurrentMapSelection.defaultZoom);
        navigation.mapNavigationPanel.SetDistText(mapSelectionPrefabs[hoverIndex].CurrentMapSelection.dist);
        navigation.mapNavigationPanel.SetLocationName(mapSelectionPrefabs[hoverIndex].CurrentMapSelection.locationName);             
        navigation.mapNavigationPanel.SetTimeText(mapSelectionPrefabs[hoverIndex].CurrentMapSelection.min);      
    }

    protected override void OnTap()
    {
        base.OnTap();

        Select();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectionPrefab : MonoBehaviour
{
    [Header("Components")]
    public Image mapSelectionImage;

    [Header("Debug")]
    private MapSelection currentMapSelection; 
    private NavigationSwipePanel navSwipePanel;
    private Navigation navigation;

    public MapSelection CurrentMapSelection
    {
        get { return currentMapSelection; }
      
        set
        {
            currentMapSelection = value;
        }
    }

    public NavigationSwipePanel NavSwipePanel
    {
        get { return navSwipePanel; }
       
        set
        {
            navSwipePanel = value;
        }
    }

    public Navigation Navigation
    {
        get { return navigation; }
     
        set
        {
            navigation = value;
        }
    }

    public virtual void SetMapSelection(MapSelection mapSelection, NavigationSwipePanel swipePanel)
    {
        mapSelectionImage.sprite = mapSelection.mapSelectionSprite;          
        CurrentMapSelection = mapSelection;
        NavSwipePanel = swipePanel;
        Navigation = swipePanel.navigation;
    }

    public virtual void ChooseMapSelection()
    {
        navigation.OpenPanel(navigation.mapNavigationPanel);

        navigation.mapNavigationPanel.map.SetMapImage(currentMapSelection.mapSprite);
        navigation.mapNavigationPanel.map.SetMapZoom(currentMapSelection.defaultZoom);

        navigation.mapNavigationPanel.SetTimeText(currentMapSelection.min);
        navigation.mapNavigationPanel.SetDistText(currentMapSelection.dist);
        navigation.mapNavigationPanel.SetLocationName(currentMapSelection.locationName);
    }
}

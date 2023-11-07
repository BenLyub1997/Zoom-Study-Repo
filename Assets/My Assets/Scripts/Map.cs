using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Map : MonoBehaviour
{
    [Header("Parameters")]
    public float panSpeed;   
    public Vector2 minScale;
    public Vector2 maxScale;

    [Header("Components")]
    public Image mapImage;
    public Mask mapMask;

    [Header("Debug")]
    [Range(-.5f, .5f)] public float xPan;
    [Range(-.5f, .5f)] public float yPan;
    [Range(0, 1)] public float zoomScale;
    public float ratio;
    private float xPanRange;
    private float yPanRange;
 
    private void Update()
    {
        TestPan();
        TestZoom();
    }

    private void TestPan()
    {
        Vector2 panVector = new Vector2(xPan, yPan);
       
        Pan(panVector);
    }

    private void TestZoom()
    {
        SetMapZoom(zoomScale);
    }

    public virtual void SetMapImage(Sprite map)
    {
        if (mapImage.sprite != map)
        {
            mapImage.sprite = map;
        }
    }

    public virtual void SetMapZoom(float scale)
    {             
        zoomScale = Mathf.Clamp01(scale);        
        mapImage.rectTransform.sizeDelta = Vector2.Lerp(minScale, maxScale, zoomScale);        
    }

    float xPos;
    float yPos;

    public virtual void Pan(Vector2 panDir)
    {  
        xPan = panDir.x;
        yPan = panDir.y;

        ratio = mapImage.rectTransform.rect.width / mapMask.rectTransform.rect.width;

        xPanRange = mapImage.rectTransform.rect.width - (mapMask.rectTransform.rect.width);
        yPanRange = mapImage.rectTransform.rect.height - (mapMask.rectTransform.rect.height);

        if(xPan < 0)
        {          
            xPos = Mathf.Lerp(0, -xPanRange, Mathf.Abs(xPan));
        }

        else if(xPan > 0)
        {
            xPos = Mathf.Lerp(0, xPanRange, xPan);
        }

        if (yPan < 0)
        {
            yPos = Mathf.Lerp(0, -yPanRange, Mathf.Abs(yPan));
        }

        else if (yPan > 0)
        {
            yPos = Mathf.Lerp(0, yPanRange, yPan);
        }

        mapImage.rectTransform.anchoredPosition = new Vector2(xPos, yPos);
    }
}

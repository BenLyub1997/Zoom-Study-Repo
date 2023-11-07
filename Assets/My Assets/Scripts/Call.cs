using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Call : MonoBehaviour
{
    [Header("Parameters")]
    public Vector2 userScreenSize;
    public Vector2 userZoomScreenSize;
    public Vector2 minCallScreenUserBox;
    public Vector2 minZoomScreenUserBox;
    public Vector2 maxUserBoxZoom;

    [Header("Components")]
    public Image userBox;

    [Header("Debug")]  
    [Range(0,1)] public float zoomPercent;

    private Image img;

    protected virtual void Awake()
    {
        img = GetComponent<Image>();
    }

    public virtual void SetZoom(float zoom)
    {
        zoomPercent = Mathf.Clamp01(zoom);

        if (!userBox) 
            return;

        userBox.rectTransform.sizeDelta = new Vector2(

            Mathf.Lerp(minZoomScreenUserBox.x, maxUserBoxZoom.x, zoomPercent),
            Mathf.Lerp(minZoomScreenUserBox.y, maxUserBoxZoom.y, zoomPercent));
    }

    public virtual void SetState(CallScreen callState)
    {
        switch (callState)
        {
            case CallScreen.CALL_USERS_SCREEN:

                userBox.rectTransform.sizeDelta = new Vector2(minCallScreenUserBox.x, minCallScreenUserBox.y);
                img.rectTransform.sizeDelta = new Vector2(userScreenSize.x, userScreenSize.y);

                break;
          
            case CallScreen.CALL_ZOOM_SCREEN:

                userBox.rectTransform.sizeDelta = new Vector2(minZoomScreenUserBox.x, minZoomScreenUserBox.y);
                img.rectTransform.sizeDelta = new Vector2(userZoomScreenSize.x, userZoomScreenSize.y);

                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class VideoCalling : MonoBehaviour
{
    [Header("Components")]
    public RectTransform navigationBar;
    public Slider zoomSlider;
    public Call inCall;
    public Call outCall;

    [Header("Input")]
    public InputAction zoomMenuTestInput;
    public InputAction callScreenMenuTestInput;

    [Header("Debug")]
    [Range(0,1)] public float zoomPercent;

    protected virtual void Awake()
    {
        SetCallScreenState(CallScreen.CALL_USERS_SCREEN);

        if (zoomSlider)
        {
            zoomSlider.onValueChanged.AddListener(OnZoomValueChanged);
        }

        if (zoomMenuTestInput != null)
        {
            zoomMenuTestInput.Enable();
            zoomMenuTestInput.performed += OnZoomMenuInputPerformed;
        }

        if (callScreenMenuTestInput != null)
        {
            callScreenMenuTestInput.Enable();
            callScreenMenuTestInput.performed += CallScreenMenuTestInputPerformed;
        }
    }

    private void CallScreenMenuTestInputPerformed(InputAction.CallbackContext obj)
    {
        SetCallScreenState(CallScreen.CALL_USERS_SCREEN);
    }

    private void OnZoomMenuInputPerformed(InputAction.CallbackContext obj)
    {
        SetCallScreenState(CallScreen.CALL_ZOOM_SCREEN);
    }

    protected virtual void Update()
    {
#if UNITY_EDITOR
        OnZoomValueChanged(zoomPercent);
        zoomSlider.value = zoomPercent;
#endif
    }

    private void OnZoomValueChanged(float zoom)
    {
        zoomPercent = Mathf.Clamp01(zoom);
    }

    public void SetCallScreenState(CallScreen callScreen)
    {
        switch (callScreen)
        {
            case CallScreen.CALL_USERS_SCREEN:

                inCall.gameObject.SetActive(true);
                outCall.gameObject.SetActive(true);
                zoomSlider.gameObject.SetActive(false);
                navigationBar.gameObject.SetActive(true);

                break;
        
            case CallScreen.CALL_ZOOM_SCREEN:

                inCall.gameObject.SetActive(false);
                outCall.gameObject.SetActive(true);
                zoomSlider.gameObject.SetActive(true);
                navigationBar.gameObject.SetActive(false);

                break;
        }

        inCall.SetState(callScreen);
        outCall.SetState(callScreen);
    }
}

public enum CallScreen
{
    CALL_USERS_SCREEN,
    CALL_ZOOM_SCREEN
}

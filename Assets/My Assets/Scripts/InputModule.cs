using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputModule : MonoBehaviour
{
    [Header("Inputs")]
    public InputAction TAP_INPUT;
    public InputAction ONE_FINGER_SWIPE_INPUT;
    public InputAction TWO_FINGERS_TAP_INPUT;
    public InputAction TWO_FINGERS_HOLD_INPUT;
    public InputAction TWO_FINGERS_SWIPE_INPUT;
    public InputAction TWO_FINGERS_RELEASE_INPUT;

    [Header("Events")]
    public InputEvent inputEvent = new InputEvent();

    public static InputModule instance;

    protected virtual void Awake()
    {
        if(instance is null)
        {
            instance = this;
        }

        SetupInputs();
    }

    protected virtual void SetupInputs()
    {
        if (TAP_INPUT != null)
        {
            TAP_INPUT.Enable();
            TAP_INPUT.performed += OnTap;
        }

        if (ONE_FINGER_SWIPE_INPUT != null)
        {
            ONE_FINGER_SWIPE_INPUT.Enable();
            ONE_FINGER_SWIPE_INPUT.performed += OnSingleSwipe;
        }

        if (TWO_FINGERS_TAP_INPUT != null)
        {
            TWO_FINGERS_TAP_INPUT.Enable();
            TWO_FINGERS_TAP_INPUT.performed += OnDoubleTap;
        }

        if(TWO_FINGERS_HOLD_INPUT != null)
        {
            TWO_FINGERS_HOLD_INPUT.Enable();
            TWO_FINGERS_HOLD_INPUT.performed += OnDoubleHold;
        }

        if (TWO_FINGERS_SWIPE_INPUT != null)
        {
            TWO_FINGERS_SWIPE_INPUT.Enable();
            TWO_FINGERS_SWIPE_INPUT.performed += OnDoubleSwipe;
        }

        if (TWO_FINGERS_RELEASE_INPUT != null)
        {
            TWO_FINGERS_RELEASE_INPUT.Enable();
            TWO_FINGERS_RELEASE_INPUT.performed += OnDoubleRelease;
        }

        Debug.Log("Inputs Set");
    }

    private void OnTap(InputAction.CallbackContext obj)
    {
        inputEvent.Invoke(InputType.TAP);
        Debug.Log("1");
    }

    private void OnSingleSwipe(InputAction.CallbackContext obj)
    {
        inputEvent.Invoke(InputType.ONE_FINGER_SWIPE);
        Debug.Log("2");
    }

    private void OnDoubleTap(InputAction.CallbackContext obj)
    {
        inputEvent.Invoke(InputType.TWO_FINGERS_TAP);
        Debug.Log("3");
    }

    private void OnDoubleHold(InputAction.CallbackContext obj)
    {
        inputEvent.Invoke(InputType.TWO_FINGERS_HOLD);
        Debug.Log("4");
    }

    private void OnDoubleSwipe(InputAction.CallbackContext obj)
    {
        inputEvent.Invoke(InputType.TWO_FINGERS_SWIPE);
        Debug.Log("5");
    }

    private void OnDoubleRelease(InputAction.CallbackContext obj)
    {
        inputEvent.Invoke(InputType.TWO_FINGERS_RELEASE);
        Debug.Log("6");
    }

    public enum InputType 
    { 
        TAP,
        ONE_FINGER_SWIPE,
        TWO_FINGERS_TAP,
        TWO_FINGERS_HOLD,
        TWO_FINGERS_SWIPE,
        TWO_FINGERS_RELEASE,
    }

    [System.Serializable]
    public class InputEvent: UnityEvent<InputType> { }
}

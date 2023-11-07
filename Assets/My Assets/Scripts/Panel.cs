using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [Header("Panel:")]
    [Header("Debug")]
    public bool isOpen = false;
    
    private InputModule inputModule;

    public InputModule InputModule
    {
        get { return inputModule; }
   
        set
        {
            inputModule = value;
        }
    }

    public virtual void SetupPanel()
    {
        InputModule = InputModule.instance;
        InputModule.inputEvent.AddListener(OnInputEvent);
    }

    public virtual Panel Open()
    {
        isOpen = true;
        gameObject.SetActive(true);
        return this;
    }

    public virtual Panel Close()
    {
        isOpen = false;
        gameObject.SetActive(false);
        return this;
    }

    private void OnInputEvent(InputModule.InputType input)
    {
        if (!isOpen) 
            return;

        switch (input)
        {
            case InputModule.InputType.TAP:
              
                OnTap();
               
                break;

            case InputModule.InputType.ONE_FINGER_SWIPE:
               
                OnOneFingerSwipe();
               
                break;

            case InputModule.InputType.TWO_FINGERS_TAP:

                OnTwoFingerTap();
 
                break;

            case InputModule.InputType.TWO_FINGERS_SWIPE:

                OnTwoFingerSwipe();

                break;

            case InputModule.InputType.TWO_FINGERS_HOLD:

                OnTwoFingerHold();

                break;

            case InputModule.InputType.TWO_FINGERS_RELEASE:

                OnTwoFingerRelease();

                break;
        }
    }

    protected virtual void OnTap()
    {

    }

    protected virtual void OnOneFingerSwipe()
    {

    }

    protected virtual void OnTwoFingerSwipe()
    {

    }

    protected virtual void OnTwoFingerTap()
    {

    }

    protected virtual void OnTwoFingerHold()
    {

    }

    protected virtual void OnTwoFingerRelease()
    {

    }
}

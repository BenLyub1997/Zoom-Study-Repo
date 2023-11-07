using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoNavigation : MonoBehaviour
{
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

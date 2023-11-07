using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SelectionEllipse : MonoBehaviour
{
    [Header("Parameters")]
    public Sprite unSelectedSprite;
    public Sprite selectedSprite;

    [Header("Debug")]
    public bool isSelected = false;

    private Image img;

    protected virtual void Awake()
    {
        img = GetComponent<Image>();
    }

    public virtual void Select()
    {
        if (!isSelected)
        {
            img.sprite = selectedSprite;
            isSelected = true;
        }
    }

    public virtual void UnSelect()
    {
        if (isSelected)
        {
            img.sprite = unSelectedSprite;
            isSelected = false;
        }
    }
}

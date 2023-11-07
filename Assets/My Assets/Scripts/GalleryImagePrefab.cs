using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryImagePrefab : MonoBehaviour
{
    [Header("Components")]
    public Image img;

    public void SetImageUI(Sprite sprite)
    {
        img.sprite = sprite;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryViewPanel : Panel
{
    [Header("Parameters")]
    [SerializeField] private Vector2 _minViewBoxSize;
    [SerializeField] private Vector2 _maxViewBoxSize;
    [SerializeField] private Vector2 _minViewImageSize;
    [SerializeField] private Vector2 _maxViewImageSize;

    [Header("Components")]
    [SerializeField] private Image _viewBox;
    [SerializeField] private Image _viewImage;
    [SerializeField] private Slider _zoomSlider;

    [Header("Debug")] 
    [Range(0,1)] public float zoomPercent;
    [Range(-1, 1)] public float xPan;
    
    private Gallery gallery;

    public Gallery Gallery
    {
        get { return gallery; }

        set
        {
            gallery = value;
        }
    }

    private float xPanRange;
    private float xPos;

    private void Update()
    {
#if UNITY_EDITOR
        OnZoomValueChanged(zoomPercent);

        _zoomSlider.value = zoomPercent;
        Pan(xPan);
#endif
    }

    public override void SetupPanel()
    {
        base.SetupPanel();

        if (_zoomSlider)
        {
            _zoomSlider.onValueChanged.AddListener(OnZoomValueChanged);
        }

        if (_viewBox)
        {
            _viewBox.rectTransform.sizeDelta = new Vector2(_minViewBoxSize.x, _minViewBoxSize.y);
        }

        if (_viewImage)
        {
            _viewImage.rectTransform.sizeDelta = new Vector2(_minViewImageSize.x, _minViewImageSize.y);
        }

        Pan(0);
    }

    private void OnZoomValueChanged(float zoom)
    {     
        zoomPercent = Mathf.Clamp01(zoom);
       
        _viewBox.rectTransform.sizeDelta = new Vector2(
          
            Mathf.Lerp(_minViewBoxSize.x, _maxViewBoxSize.x, zoomPercent),                     
            Mathf.Lerp(_minViewBoxSize.y, _maxViewBoxSize.y, zoomPercent));
          
        _viewImage.rectTransform.sizeDelta = new Vector2(
                          
            Mathf.Lerp(_minViewImageSize.x, _maxViewImageSize.x, zoomPercent),                 
            Mathf.Lerp(_minViewImageSize.y, _maxViewImageSize.y, zoomPercent));
    }

    public virtual void Pan(float pan)
    {
        float xRange = _viewBox.rectTransform.rect.width / 2;
        xPanRange = xRange * zoomPercent;
    
        if (xPan < 0)
        {
            xPos = Mathf.Lerp(0, -xPanRange, Mathf.Abs(pan));
        }

        else if (xPan > 0)
        {
            xPos = Mathf.Lerp(0, xPanRange, pan);
        }

        _viewImage.rectTransform.anchoredPosition = new Vector2(xPos, _viewImage.rectTransform.anchoredPosition.y);
    }

    public virtual void SetZoomableImage(Sprite sprite)
    {
        _viewImage.sprite = sprite;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine.InputSystem;

public class GallerySwipePanel : Panel
{
    [Header("Parameters")]
    public int startingSelection;

    [Header("Components")]
    public SimpleScrollSnap scrollSnap;
    public Sprite[] galleryImages;
    public GalleryImagePrefab galleryImagePrefab;

    [Header("Input")]
    public InputAction leftSwipeInput;
    public InputAction rightSwipeInput;
    public InputAction selectSwipeInput;

    [Header("Debug")]
    public List<GalleryImagePrefab> galleryImgPFabs = new List<GalleryImagePrefab>();
    public int hoverIndex;

    private Gallery gallery;

    public Gallery Gallery
    {
        get { return gallery; }
    
        set
        {
            gallery = value;
        }
    }

    public override void SetupPanel()
    {
        base.SetupPanel();

        if(galleryImages.Length > 0)
        {
            GenerateGallerySelections(galleryImages);
            HighlightSelection(startingSelection);
        }

        if (leftSwipeInput != null)
        {
            leftSwipeInput.Enable();
            leftSwipeInput.performed += OnSwipeLeftPerformed;
        }

        if(rightSwipeInput != null)
        {
            rightSwipeInput.Enable();
            rightSwipeInput.performed += OnSwipeRightPerformed;
        }

        if(selectSwipeInput != null)
        {
            selectSwipeInput.Enable();
            selectSwipeInput.performed += OnSelectSwipePerformed;
        }

        Debug.Log("Panel Setup");
    }

    private void OnSwipeLeftPerformed(InputAction.CallbackContext obj)
    {
        GoToPreviousMapSelection();

        Debug.Log("Left Input");
    }

    private void OnSwipeRightPerformed(InputAction.CallbackContext obj)
    {
        GoToNextMapSelection();

        Debug.Log("Right Input");
    }

    private void OnSelectSwipePerformed(InputAction.CallbackContext obj)
    {
        Select();

        Debug.Log("Select");
    }

    private void HighlightSelection(int selection)
    {
        scrollSnap.GoToPanel(selection);
        hoverIndex = selection;
    }

    private void GenerateGallerySelections(Sprite[] imgs)
    {
        for(int i = 0; i < imgs.Length; i++)
        {
            GenerateGallerySelection(imgs[i], i);
        }
    }

    private void GenerateGallerySelection(Sprite img, int index)
    {
        GameObject galleryImageObj = Instantiate(galleryImagePrefab.gameObject);
        GalleryImagePrefab galleryImagePFab = galleryImageObj.GetComponent<GalleryImagePrefab>();

        galleryImageObj.transform.localScale = Vector3.one;
        galleryImageObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
        galleryImagePFab.SetImageUI(img);
        galleryImgPFabs.Add(galleryImagePFab);

        AddPanel(galleryImageObj, index);
    }

    public virtual void AddPanel(GameObject obj, int index)
    {
        scrollSnap.Add(obj, index);
    }

    public virtual void Select()
    {
        gallery.OpenPanel(gallery.galleryViewPanel);
        gallery.galleryViewPanel.SetZoomableImage(galleryImgPFabs[hoverIndex].img.sprite);
    }

    public virtual void GoToPreviousMapSelection()
    {
        if (hoverIndex > 0)
        {
            HighlightMapSelection(hoverIndex -= 1);
        }
    }

    public virtual void GoToNextMapSelection()
    {
        if (hoverIndex < scrollSnap.NumberOfPanels - 1)
        {
            HighlightMapSelection(hoverIndex += 1);
        }
    }

    private void HighlightMapSelection(int selection)
    {
        scrollSnap.GoToPanel(selection);
        hoverIndex = selection;
    }

    protected override void OnTap()
    {
        base.OnTap();
        Select();
    }
}

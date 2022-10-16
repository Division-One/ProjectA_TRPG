using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PageIndicator : MonoBehaviour
{
    PageIndicatorControl collectionBookPage;
    public Image background;

    public UnityEvent onSelected;
    public UnityEvent onDeselected;
    private void Awake()
    {
        background = GetComponent<Image>();
        collectionBookPage = GetComponentInParent<PageIndicatorControl>();
        collectionBookPage.Subscribe(this);
    }
    
    public void Select()
    {
        if (onSelected != null)
        {
            onSelected.Invoke();
        }
    }
    public void Deselect()
    {
        if (onDeselected != null)
        {
            onDeselected.Invoke();
        }
    }
}

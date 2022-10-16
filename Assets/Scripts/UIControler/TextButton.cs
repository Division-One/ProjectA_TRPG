using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextButton : ClickableText, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Inspector

    public Color HoverColor = Color.black;

    #endregion Inspector

    private bool _isHover;


    protected override void Updatecolor()
    {
        if (!_isInteractive)
        {
            TextComponent.color = DisabledColor;
            return;
        }

        if (_isPressed)
        {
            TextComponent.color = PressColor;
            return;
        }

        if (_isHover)
        {
            TextComponent.color = HoverColor;
            return;
        }

        TextComponent.color = NormalColor;
    }

    #region IPointer Callbacks

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        // invoke your event
        onClick.Invoke();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isHover) return;
        _isPressed = true;
        Updatecolor();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isHover) return;
        _isPressed = false;
        Updatecolor();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHover = true;
        Updatecolor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHover = false;
        _isPressed = false;
        Updatecolor();
    }

    #endregion IPointer Callbacks
}

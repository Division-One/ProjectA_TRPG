using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DualToggleTextButton : ClickableText, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool _isSelected = false;
    [SerializeField]
    DualToggleTextButton pairTextButton;
    protected override void Updatecolor()
    {
        if (!_isInteractive)
        {
            TextComponent.color = DisabledColor;
            return;
        }

        if(_isSelected)
        {
            TextComponent.color = PressColor;
            return;
        }

        TextComponent.color = NormalColor;
    }

    public void SetUnSelected()
    {
        _isSelected = false;
        Updatecolor();
    }
    public void SetSelected()
    {
        _isSelected =true;
        pairTextButton.OnPairSelected();
        Updatecolor();
    }
    public void OnPairSelected()
    {
        _isSelected = false;
        Updatecolor();
    }
    #region IPointer Callbacks

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        SetSelected();
        // invoke your event
        onClick.Invoke();
    }

    #endregion IPointer Callbacks
}

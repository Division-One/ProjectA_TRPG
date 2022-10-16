using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ClickableText : MonoBehaviour, IPointerClickHandler
{
    #region Inspector
    public Color NormalColor = Color.white;
    public Color PressColor = Color.yellow;
    public Color DisabledColor = Color.gray;

    // add callbacks in the inspector like for buttons
    public UnityEvent onClick;

    #endregion Inspector

    protected bool _isInteractive = true;
    public bool interactive
    {
        get
        {
            return _isInteractive;
        }
        set
        {
            _isInteractive = value;
            Updatecolor();
        }
    }

    protected bool _isPressed;

    protected TextMeshProUGUI _textComponent;
    protected TextMeshProUGUI TextComponent
    {
        get
        {
            if (!_textComponent) _textComponent = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();
            return _textComponent;
        }
    }

    protected virtual void Updatecolor()
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
        TextComponent.color = NormalColor;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region IPointer Callbacks

    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        // invoke your event
        onClick.Invoke();
    }
    #endregion IPointer Callbacks
}

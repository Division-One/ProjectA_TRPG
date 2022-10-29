using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnOffDualTogglelControl : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    DualToggleTextButton onButton;
    [SerializeField]
    DualToggleTextButton offButton;
    [SerializeField]
    bool defaultSelected = true;
    #endregion Inspector
    private void Awake()
    {
        if (defaultSelected)
        {
            onButton.SetSelected();
            onButton.onClick.Invoke();
        }
        else
        {
            offButton.SetSelected();
            offButton.onClick.Invoke();
        }

    }

}

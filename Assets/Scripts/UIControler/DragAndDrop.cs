using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public delegate void CommonDelegate();
    public event CommonDelegate onDropHit;
    public event CommonDelegate onDragStart;
    public float dragPosModifyX;
    public float dragPosModifyY;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    #region OnDrag
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (onDragStart != null)
            onDragStart.Invoke();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position;
        this.transform.position = currentPos + new Vector2(dragPosModifyX, dragPosModifyY);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (onDropHit != null)
            onDropHit.Invoke();
    }
    #endregion OnDrag
}

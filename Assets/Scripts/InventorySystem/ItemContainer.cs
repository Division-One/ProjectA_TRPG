using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemContainer : MonoBehaviour
{
    public Vector2 DefaultPos;
    public ItemInfo item;

    int rowLocation = -1;
    int colLocation = -1;
    int index;
    Inventory inventory;
    RectTransform rectTransform;
    ItemInfoUI ui;
    DragAndDrop dragAndDrop;

    void Start()
    {

    }
    public void Initialize(ItemInfo item, Inventory inven)
    {
        if (item == null)
            return;
        if(inven == null)
            return ;

        DefaultPos = this.transform.position;
        rectTransform = GetComponent<RectTransform>();
        ui = GetComponent<ItemInfoUI>();
        dragAndDrop = GetComponent<DragAndDrop>();
        dragAndDrop.dragPosModifyX = -this.rectTransform.sizeDelta.x / 3;
        dragAndDrop.dragPosModifyY = +this.rectTransform.sizeDelta.y / 3;
        dragAndDrop.onDropHit += OnDrop;
        inventory = inven;
        rowLocation = -1;
        colLocation = -1;
        index = -1;
        SetItem(item); 
    }
    #region getset
    public int GetRow()
    {
        return rowLocation;
    }
    public int GetCol()
    {
        return colLocation;
    }
    public int GetIndex()
    {
        return index;
    }
    public void SetItem(ItemInfo item)
    {
        this.item = item;
        ui.Display(item);
    }
    public void SetPosition(Vector3 transform, float cellSize)
    {
        rectTransform.position = transform + new Vector3(-cellSize / 2, cellSize / 2, 0);
        DefaultPos = this.transform.position;
    }

    /// <summary>
    ///  아이템 컨테이너의 크기를 칸에 맞게 맞추는 함수. 
    /// </summary>
    /// <param name="cellSize"></param>
    public void SetSize(float cellSize)
    {
        rectTransform.sizeDelta = new Vector2(cellSize * item.itemShape.colSize, cellSize * item.itemShape.rowSize);
    }
    #endregion getset
    public bool IsInInventory()
    {
        return index >= 0;
    }
    public bool IsSame(ItemContainer item)
    {
        return item.GetIndex() == index;
    }
    public void InsertedToInventory(int row, int col, int index)
    {
        if (row < 0 || col < 0)
            return;
        rowLocation = row;
        colLocation = col;
        this.index = index;
        SetPosition(inventory.GetSlot(row,col).transform.position,inventory.cellSize);
    }
    public void RemovedFromInventory()
    {
        colLocation = -1;
        rowLocation = -1;
        index = -1;
    }

    public void ToDefaultPos()
    {
        this.transform.position = DefaultPos;
    }
    public void OnDrop()
    {
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, transform.forward, 15f);
        if (hit.collider != null && hit.collider.CompareTag("InventorySlot"))//인벤토리 안에 드랍
        {
            InventorySlot slot = hit.collider.GetComponent<InventorySlot>();
            slot.inventory.TryInsertItem(this, slot.row, slot.col);
        }
        else
        {
            inventory.TrtRemoveItem(this);
            DefaultPos = this.transform.position;
        }
    }

}

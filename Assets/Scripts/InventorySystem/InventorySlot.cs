using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int row;
    public int col;
    public Inventory inventory;
    int itemIdx = -1;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = this.GetComponentInParent<GridLayoutGroup>().cellSize;
    }
    public void Initialize(int row, int col, Inventory inv)
    {
        this.row = row;
        this.col = col;
        this.inventory = inv;
        itemIdx = -1;
    }
    public bool IsEmpty()
    {
        return itemIdx == -1; 
    }
    /// <summary>
    /// 인벤토리의 아이템리스트의 인덱스
    /// </summary>
    /// <returns></returns>
    public int GetItemIdx()
    {
        return this.itemIdx;
    }
    public void SetItemIdx(int idx)
    {
        itemIdx = idx;
    }
}
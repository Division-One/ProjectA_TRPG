using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory: MonoBehaviour
{
    [SerializeField]
    RectTransform slotsParent;
    [SerializeField]
    InventorySlot slotPrefab;
    GridLayoutGroup gridLayout;

    public int columnCount = 6;
    public int rowCount = 2;
    public float cellSize;

    List<List<InventorySlot>> slots = new List<List<InventorySlot>>();
    Dictionary<int,ItemContainer> itemList =new Dictionary<int,ItemContainer>();
    int itemIndexInc = 0;
    private void Awake()
    {
        SetUISize();
        CreateSlotObjects();
    }
    public InventorySlot GetSlot(int row, int col)
    {
        return slots[row][col];
    }
    void SetUISize()
    {
        gridLayout = GetComponentInChildren<GridLayoutGroup>();
        cellSize = gridLayout.cellSize.x;
        slotsParent.sizeDelta = new Vector2(columnCount * cellSize + (columnCount - 1) * gridLayout.spacing.x,
                                                rowCount * cellSize + (rowCount - 1) * gridLayout.spacing.y);
    }
    void CreateSlotObjects()
    {
        for (int i = 0; i < rowCount; i++)
        {
            slots.Add(new List<InventorySlot>());
            for (int j = 0; j < columnCount; j++)
            {
                InventorySlot invSlot = Instantiate(slotPrefab, slotsParent);
                invSlot.Initialize(i,j,this);
                slots[i].Add(invSlot);
            }
        }
    }
    /// <summary>
    /// 해당 자리에 이미 아이템이 존재하거나 칸이 부족한지 여부
    /// </summary>
    /// <param name="itemContainer"></param>
    /// <param name="col"></param>
    /// <param name="row"></param>
    /// <returns>이미 존재하면 true, 아니면 false</returns>
    bool CheckCollision(ItemContainer itemContainer, int row, int col)
    {
        ItemShape shape = itemContainer.item.itemShape;
        foreach(var cell in shape.cells)
        {
            int r = cell.Key + row;
            int c = cell.Value + col;
            if (c >= columnCount || r >= rowCount)
                return true;
            if (!slots[r][c].IsEmpty()) 
                if(!itemList[slots[r][c].GetItemIdx()].IsSame(itemContainer))
                    return true;

        }
        return false;
    }
    /// <summary>
    /// 해당 위치에 인벤토리에 삽입 시도. 
    /// </summary>
    /// <param name="itemContainer"></param>
    /// <param name="wantRow"></param>
    /// <param name="wantCol"></param>
    /// <returns>해당 위치 삽입 성공 시 true, 실패 시 false </returns>
    public bool TryInsertItem(ItemContainer itemContainer, int wantRow, int wantCol)
    {
        if (itemContainer == null)
            return false;
        if (wantRow < 0 || wantCol < 0)
            return false;

        if (CheckCollision(itemContainer, wantRow, wantCol)) // 해당 칸에 이미 뭔가 있으면
        {
            itemContainer.ToDefaultPos();
            return false;
        }
        else // 해당 칸이 비어있으면
        {
            if (itemContainer.IsInInventory())
            {
                TrtRemoveItem(itemContainer);
            }
            InsertItem(itemContainer, wantRow, wantCol);
            return true;
        }

    }
    public void InsertItem(ItemContainer itemContainer, int wantRow, int wantCol)
    {
        if (itemContainer == null)
            return;
        if (wantRow < 0 || wantCol < 0)
            return ;

        //슬롯 채우기
        ItemShape shape = itemContainer.item.itemShape;
        foreach (var cell in shape.cells)
        {
            int r = cell.Key + wantRow;
            int c = cell.Value + wantCol;
            slots[r][c].SetItemIdx(itemIndexInc);
        }
        //아이템 리스트에 넣기
        itemList[itemIndexInc] = itemContainer;
        //아이템 컨테이너 row col 설정
        itemContainer.InsertedToInventory(wantRow, wantCol, itemIndexInc);
        itemIndexInc++;
    }
    public void TrtRemoveItem(ItemContainer itemContainer)
    {
        if (itemContainer == null || !itemContainer.IsInInventory())
            return;

        //슬롯 비우기
        ItemShape shape = itemContainer.item.itemShape;
        foreach (var cell in shape.cells)
        {
            int r = cell.Key + itemContainer.GetRow();
            int c = cell.Value + itemContainer.GetCol();
            slots[r][c].SetItemIdx(-1);
        }
        //아이템 리스트에서 빼기
        itemList.Remove(itemContainer.GetIndex());
        //아이템 컨테이너 row col 설정
        itemContainer.RemovedFromInventory();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Store : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI heldGold;
    [SerializeField]
    ItemInfoUI selectedItemInfo;
    [SerializeField]
    Button purchaseButton;
    [SerializeField]
    List<StoreSlot> slots;
    [SerializeField]
    ItemContainer ItemContainerPrefab;
    [SerializeField]
    RectTransform purchasedItemParent;
    [SerializeField]
    Inventory inventory;

    ItemInfo selectedItem;
    int goodsCount = 0;
    private void Start()
    {
        SetGold();
    }
    /// <summary>
    /// 상점에 물건을 입고시킨다는 개념
    /// </summary>
    /// <param name="item">입고시킬 물건</param>
    public void Stock(ItemInfo item)
    {
        if (slots.Count < goodsCount + 1) return;
        goodsCount++;
        slots[goodsCount - 1].SetItem(item);
    }
    public void OnSlotClick(StoreSlot slot)
    {
        if(slot == null)
            return;
        if(slot.item == null)
            return;

        selectedItemInfo.Display(slot.item);
        selectedItem = slot.item;

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].DeHightLIght();
        }
        slot.HighLight();

    }
    public void OnPurchase()
    {
        if (selectedItem == null) 
            return;
        if (DataManager.Instance.playerProperty.GetGold()< selectedItem.itemPrice)
            return;

        DataManager.Instance.playerProperty.UseGold(selectedItem.itemPrice);
        SetGold();

        ItemContainer instance = Instantiate(ItemContainerPrefab, purchasedItemParent);
        instance.Initialize(selectedItem,inventory);
        instance.SetPosition(purchasedItemParent.position,inventory.cellSize);
        instance.SetSize(inventory.cellSize);
    }
    public void SetGold()
    {
        heldGold.text = DataManager.Instance.playerProperty.GetGold().ToString();
    }
}

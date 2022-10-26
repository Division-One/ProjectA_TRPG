using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour
{
    public ItemInfo item;
    [SerializeField]
    ItemInfoUI itemInfoUI;
    [SerializeField]
    Image highlighter;
    public void SetItem(ItemInfo i)
    {
        item = i;
        itemInfoUI.Display(i);

    }
    public void HighLight()
    {
        highlighter.gameObject.SetActive(true);
    }
    public void DeHightLIght()
    {
        highlighter.gameObject.SetActive(false);
    }
}

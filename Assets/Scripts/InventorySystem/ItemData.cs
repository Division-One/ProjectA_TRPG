using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Pair = System.Collections.Generic.KeyValuePair<int, int>;
public class ItemData : MonoBehaviour
{
    #region singletone
    private static ItemData instance = null;
    public static ItemData Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Initiate();
        }
        else
            Destroy(this.gameObject);
    }
    #endregion singletone
    public List<ItemInfo> items = new List<ItemInfo>();
    public int itemCount = 12;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Initiate()
    {
        ItemShape shape1 = new ItemShape();
        shape1.AddCell(0, 0);
        shape1.AddCell(0, 1);
        ItemShape shape2 = new ItemShape();
        shape2.AddCell(0, 0);
        shape2.AddCell(1, 0);
        ItemShape shape3 = new ItemShape();
        shape3.AddCell(0, 0);
        shape3.AddCell(0, 1);
        shape3.AddCell(1, 0);
        shape3.AddCell(1, 1);
        for (int i = 0; i < itemCount; i++)
        {
            ItemInfo item = new ItemInfo(i, i + "th", i + "th 아이템 입니다", "sampleItem", i * 100, shape1);
            items.Add(item);
        }
    }

}
public class ItemShape
{
    public List<Pair> cells = new List<Pair>();
    public int rowSize=0;
    public int colSize=0;
    public void AddCell(int x,int y)
    {
        cells.Add(new Pair(x,y));
        foreach (var item in cells)
        {
            rowSize = Math.Max(item.Key+1, rowSize);
            colSize = Math.Max(item.Value+1, colSize);
        }
    }
}
public class ItemInfo
{
    public int itemId;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public int itemPrice;
    public ItemShape itemShape;

    public ItemInfo(int id, string name, string desc, string icon, int price, ItemShape shape)
    {
        itemId = id;
        itemName = name;
        itemDescription = desc;
        itemIcon = Resources.Load<Sprite>("Sprites/" + icon);
        itemPrice = price;
        itemShape = shape;
    }

}
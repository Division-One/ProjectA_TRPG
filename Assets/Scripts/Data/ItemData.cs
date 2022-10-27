using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Text.RegularExpressions;

using Pair = System.Collections.Generic.KeyValuePair<int, int>;
public class ItemData
{
    public List<ItemInfo> items = new List<ItemInfo>();
    public int itemCount;

    public void GenerateData()
    {
        List<List<object>> data = CSVReader.Parsing("Data/ItemData");
        itemCount = data.Count;
        for (int i = 0; i < itemCount; i++)
        {
            int itemID = Int32.Parse(data[i][Constants.CSV_ITEM_ID_IDX].ToString());
            string itemName = data[i][Constants.CSV_ITEM_NAME_IDX].ToString();
            string desc = data[i][Constants.CSV_ITEM_DESC_IDX].ToString();
            int price = Int32.Parse(data[i][Constants.CSV_ITEM_PRICE_IDX].ToString());
            string icon = data[i][Constants.CSV_ITEM_ICON_IDX].ToString();
            string shapeString = data[i][Constants.CSV_ITEM_SHAPE_IDX].ToString();

            ItemShape shape = new ItemShape();
            var shapeBlocks = Regex.Split(shapeString, @"/");
            foreach (var block in shapeBlocks)
            {
                var blockSplit = Regex.Split(block, @",");
                shape.AddCell(Int32.Parse(blockSplit[0]), Int32.Parse(blockSplit[1]));
            }

            ItemInfo item = new ItemInfo(itemID, itemName, desc, icon, price, shape);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


static class Constants
{
    //EventData 관련 Constant
    public const int CSV_EVENT_ID_IDX = 0;
    public const int CSV_EVENT_BLOCK_IDX = 1;
    public const int CSV_EVENT_ELEMTYPE_IDX = 2;
    public const int CSV_EVENT_CONTENT_IDX = 3;
    public const int CSV_EVENT_CONNNUMBER_IDX = 4;
    public const int CSV_EVENT_ISENDOP_IDX = 5;

    //TipData 관련 Constant
    public const int CSV_TIPTEXT_ID_IDX = 0;
    public const int CSV_TIPTEXT_CONTENT_IDX = 1;

    //ItemData 관련 
    public const int CSV_ITEM_ID_IDX = 0;
    public const int CSV_ITEM_NAME_IDX = 1;
    public const int CSV_ITEM_DESC_IDX = 2;
    public const int CSV_ITEM_PRICE_IDX = 3;
    public const int CSV_ITEM_ICON_IDX = 4;
    public const int CSV_ITEM_SHAPE_IDX = 5;


    public static void CreateUnDuplicateRandom(List<int> pool, int min, int max, int n)
    {
        int currentNumber = Random.Range(min, max);

        for (int i = 0; i < n;)
        {
            if (pool.Contains(currentNumber))
            {
                currentNumber = Random.Range(min, max);
            }
            else
            {
                pool.Add(currentNumber);
                i++;
            }
        }
    }
}



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


    /// <summary>
    /// 중복되지 않은 랜덤 뽑기. pool에 n개의 min~max 정수가 담기게 됨.
    /// </summary>
    /// <param name="pool">뽑은 결과를 담을 리스트</param>
    /// <param name="min">랜덤 정수 최소 값</param>
    /// <param name="max">랜덤 정수 최대 값</param>
    /// <param name="n">랜덤 정수 갯수</param>
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



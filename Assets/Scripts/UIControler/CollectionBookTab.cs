using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionBookTab : MonoBehaviour
{
    [SerializeField]
    int collectionButtonHorizCount;
    [SerializeField]
    int collectionButtonVerticCount;
    [SerializeField]
    PageIndicatorControl pageIndicatorControl;
    [SerializeField]
    CollectionButtonsControl buttonsPaging;
    [SerializeField]
    CollectionType type;

    public List<CollectionData> collectionDataList;
    int collectionCount;
    int itemCountPerPage;
    int pageCount;
    int currentPage = 0;

    private void Awake()
    { 
    }
    private void Start()
    {
        collectionDataList = CollectionBookManager.Instance.dataList[(int)type];
        collectionCount = collectionDataList.Count;
        itemCountPerPage = collectionButtonHorizCount * collectionButtonVerticCount;
        pageCount = (collectionCount - 1) / itemCountPerPage + 1;

        pageIndicatorControl.Instantiate(pageCount);
        buttonsPaging.Instantiate(itemCountPerPage);

        ToPage(currentPage);
    }
    public void ToNextPage()
    {
        if ((currentPage + 1) >= pageCount)
            return;
        ToPage(++currentPage);
    }
    public void ToPrevPage()
    {
        if ((currentPage - 1) < 0)
            return;
        ToPage(--currentPage);
    }
    void ToPage(int pageNum)
    {
        pageIndicatorControl.SwitchToPage(pageNum);
        int rangeStart = itemCountPerPage * pageNum;
        int rangeCount = rangeStart + itemCountPerPage >= collectionCount ? collectionCount- rangeStart : itemCountPerPage;
        buttonsPaging.DisplayPage(collectionDataList.GetRange(rangeStart, rangeCount));
    }
}


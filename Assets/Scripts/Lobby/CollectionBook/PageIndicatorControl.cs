using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageIndicatorControl : MonoBehaviour
{

    public GameObject pageIndicatorPrefab;
    [SerializeField]
    List<PageIndicator> pageIndicators;
    [SerializeField]
    Sprite indicatorActive;
    [SerializeField]
    Sprite IndicatorIdle;

    public void Instantiate(int pageCount)
    {
        for (int i = 0; i < pageCount; i++)
        {
            Instantiate(pageIndicatorPrefab, transform);
        }
    }

    public void SwitchToPage(int p)
    {
        int pageCount = pageIndicators.Count;
        for (int i = 0; i < pageCount; i++)
        {
            if (i == p)
                pageIndicators[i].background.sprite = indicatorActive;
            else
                pageIndicators[i].background.sprite = IndicatorIdle;

        }
    }
    public void Subscribe(PageIndicator indicator)
    {
        if (pageIndicators == null)
        {
            pageIndicators = new List<PageIndicator>();
        }
        pageIndicators.Add(indicator);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    [SerializeField]
    Sprite tabIdle;
    [SerializeField]
    Sprite tabHover;
    [SerializeField]
    Sprite tabActive;

    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;

    private void Start()
    {
        OnTabSelected(tabButtons[0]);
    }
    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
    }

    public GameObject GetTab(TabButton t)
    {
        return objectsToSwap[t.transform.GetSiblingIndex()];
    }
    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
            button.background.sprite = tabHover;
    }
    public void OnTabExit(TabButton button)
    {
        ResetTabs();

    }
    public void OnTabSelected(TabButton button)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }
        selectedTab = button;
        selectedTab.Select();

        selectedTab = button;
        ResetTabs();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        int count = objectsToSwap.Count;
        for (int i = 0; i < count; i++)
        {
            if (i == index)
                objectsToSwap[i].SetActive(true);
            else
                objectsToSwap[i].SetActive(false);
        }
    }

    public void ResetTabs()
    { 
        foreach(TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) continue;
            button.background.sprite = tabIdle;
        }
    }
}

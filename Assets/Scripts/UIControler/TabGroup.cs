using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [SerializeField]
    Sprite tabIdle;
    [SerializeField]
    Sprite tabHover;
    [SerializeField]
    Sprite tabActive;

    public List<TabButton> tabButtons;
    public List<GameObject> objectsToSwap;
    public TabButton selectedTab;
    public int selectedTabIndex = 0;

    private void Awake()
    {
        tabButtons = new List<TabButton>();
        var collection = GetComponentsInChildren<TabButton>();
        int idx = 0;
        foreach (var item in collection)
        {
            tabButtons.Add(item);
            item.myIndex = idx;
            idx++;
        }


    }
    private void Start()
    {
        OnTabSelected(tabButtons[selectedTabIndex]);
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
        selectedTabIndex = button.myIndex;
        selectedTab = button;
        selectedTab.Select();

        ResetTabs();
        button.background.sprite = tabActive;
        int count = objectsToSwap.Count;
        for (int i = 0; i < count; i++)
        {
            if (i == button.myIndex)
                objectsToSwap[i].SetActive(true);
            else
                objectsToSwap[i].SetActive(false);
        }
    }

    public void ResetTabs()
    { 
        foreach(TabButton button in tabButtons)
        {
            if (selectedTab != null && button.myIndex == selectedTabIndex) continue;
            button.background.sprite = tabIdle;
        }
    }
}

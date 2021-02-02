using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabActive;
    public Sprite tabHover;
    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;

    public void Subscribe(TabButton _button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(_button);
    }

    public void OnTabEnter(TabButton _button)
    {
        ResetTabs();
        if(selectedTab == null || _button != selectedTab)
            _button.backGround.sprite = tabHover;
    }

    public void OnTabExit(TabButton _button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton _button)
    {
        if (selectedTab != null)
            selectedTab.DeSelectTab();
        selectedTab = _button;
        selectedTab.SelectTab();
        ResetTabs();

        _button.backGround.sprite = tabActive;
        int index = _button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            objectsToSwap[i].SetActive(i == index);
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab)
                continue;
            button.backGround.sprite = tabIdle;
        }
    }
}

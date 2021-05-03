using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//tab group for the world editor
public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    public TabButton SelectedTab;

    public List<GameObject> tabs;

    public void AddToList(TabButton t_tabButton)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(t_tabButton);
    }


    public void OnTabEnter(TabButton t_tabButton)
    {
        ResetTabs();
        if(SelectedTab == null || t_tabButton != SelectedTab)
        {
            t_tabButton.GetComponent<Image>().color = new Color(0, 255, 0);
        } 
    }

    public void OnTabExit(TabButton t_tabButton)
    {
        ResetTabs();
    }

    public void OnTabSelect(TabButton t_tabButton)
    {
        SelectedTab = t_tabButton;
        ResetTabs();
        t_tabButton.GetComponent<Image>().color = new Color(255, 0, 0);

        int index = t_tabButton.transform.GetSiblingIndex();

        for (int i = 0; i < tabs.Count; i++)
        {
            if(i == index)
            {
                tabs[i].SetActive(true);
            }
            else
            {
                tabs[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if(SelectedTab != null && button == SelectedTab)
            {
                continue;
            }

            button.GetComponent<Image>().color = new Color(255, 255, 255);
        }
    }
}

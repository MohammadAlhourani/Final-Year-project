using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;



    public void OnPointerClick(PointerEventData t_eventData)
    {
        tabGroup.OnTabSelect(this);
    }

    public void OnPointerEnter(PointerEventData t_eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData t_eventData)
    {
        tabGroup.OnTabExit(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        tabGroup.AddToList(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

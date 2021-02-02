using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public Image backGround;
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeSelected;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    private void Start()
    {
        backGround = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public void SelectTab()
    {
        onTabSelected?.Invoke();
    }

    public void DeSelectTab()
    {
        onTabDeSelected?.Invoke();
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragTest : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private RectTransform dragRect;
    [SerializeField] private Canvas canvas;

    private Vector2 targetPos;
    private Vector2 currPos;
    private Vector2 beginPos;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        beginPos = Camera.main.ScreenToWorldPoint(eventData.position);

    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //dragRect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        targetPos = Camera.main.ScreenToWorldPoint(eventData.position) ;
    }

    private void Update()
    {
        var dist = targetPos - beginPos;
        dragRect.anchoredPosition += dist;
    }

    //private void OnMouseDrag()
    //{
    // dragRect.anchoredPosition += 
    //}

}

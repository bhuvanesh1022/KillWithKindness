﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Transform return_to_parent = null;
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        return_to_parent = this.transform.parent;
        
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().alpha = 0.6f;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        this.transform.position = new Vector3(pos.x,pos.y,0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
        this.transform.SetParent(return_to_parent);

        // if (return_to_parent.name == "ChoosePanel")
        // {
        //     GetComponent<CanvasGroup>().blocksRaycasts = false;
        // }
        
        GetComponent<CanvasGroup>().alpha = 1f;
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
    
    
    public void Update()
    {
        this.transform.localScale = new Vector2(1,1);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(return_to_parent);

        if (return_to_parent.name == "ChoosePanel")
        {
            GetComponent<Image>().raycastTarget = false;
        }
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    
}

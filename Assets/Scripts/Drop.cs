using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drop : MonoBehaviour,IDropHandler,IPointerEnterHandler,IPointerExitHandler
{
    public List<CardDisply> card_choose = new List<CardDisply>();

    private void Update()
    {
        
        if (this.transform.childCount == 3)
        {
            GetComponent<Image>().raycastTarget = false;
        }
        if (this.transform.childCount < 3)
        {
            GetComponent<Image>().raycastTarget = true;
        }
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Drag d = eventData.pointerDrag.GetComponent<Drag>();
        
        Debug.Log(eventData.pointerDrag.name + "dropped on" + gameObject.name);

        if (d != null)
        {
            d.return_to_parent = this.transform;
            card_choose.add(eventData.pointerDrag);
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("onexit");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drop : MonoBehaviour,IDropHandler,IPointerEnterHandler,IPointerExitHandler
{
    public List<CardManager> cardmanager = new List<CardManager>();
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
        CardManager cm = eventData.pointerDrag.GetComponent<CardManager>();
        Debug.Log(eventData.pointerDrag.name + "dropped on" + gameObject.name);

        if (d != null)
        {
            d.return_to_parent = this.transform;
            cm.cardState = CardManager.CardState.Inchoosen;
        }
        
        

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Drag d = eventData.pointerDrag.GetComponent<Drag>();

        //if (this.gameObject.transform.childCount > 3)
        //{
        //    d.transform.SetParent(d.return_to_parent);
        //}
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("onexit");
    }
}

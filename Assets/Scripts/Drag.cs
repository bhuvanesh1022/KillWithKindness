using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Transform return_to_parent = null;
    public bool onHover = false;
    public bool isSelected = false;
    public Ray2D ray2D;
    public RaycastHit2D raycastHit2D;

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
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
        this.transform.SetParent(return_to_parent);

        if (return_to_parent.name == "ChoosePanel")
        {
            GetComponent<Image>().raycastTarget = false;
        }
        
        GetComponent<CanvasGroup>().alpha = 1f;
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.transform.parent.name == "HandPanel")
        {
            onHover = true;
            this.gameObject.transform.localScale = new Vector2(1.2f,1.2f);
        }
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.transform.localScale = new Vector2(1,1);
    }

    public void CardClicked()
    {
        this.gameObject.transform.localScale = new Vector2(1,1);
    }

    public void Update()
    {
        //if(Input.GetMouseButtonDown(0) == true)
       // {
         //   Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         //   Vector2 mousePos2D = new Vector2(mousePos.x,mousePos.y);
          //  ray2D = new Ray2D(mousePos2D,Vector2.down);
          //  raycastHit2D = Physics2D.Raycast(mousePos2D, Vector2.down);

          //  if (raycastHit2D.transform != null)
          //  {
          //      Debug.Log(raycastHit2D.transform.gameObject.name);
          //  }
        }
    // }

}

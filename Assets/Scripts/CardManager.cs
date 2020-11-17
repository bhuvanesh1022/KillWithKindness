using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public enum CardState {InDeck, InHand, InBin , Inchoosen};
    public CardState cardState;
    //public bool onover;
    //public int value;
    //public enum CardPower { Attack , Heal};
    //public CardPower cardPower;

    void Start()
    {
        gameObject.SetActive(true);
    }

    void Update()
    {
       
        this.transform.localScale = new Vector2(1, 1);
        
        switch (cardState)
        {
            case CardState.InBin:
                gameObject.SetActive(false);
                transform.parent = null;
                break;

            case CardState.Inchoosen:
                GetComponent<CanvasGroup>().blocksRaycasts = false;
                break;

            case CardState.InDeck:
                gameObject.SetActive(true);
                transform.parent = null;
                break;

            case CardState.InHand:
                gameObject.SetActive(true);
                break;

            default:
                break;
        }
    }
}

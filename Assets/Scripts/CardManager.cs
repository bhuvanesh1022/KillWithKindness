using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public enum CardState {InDeck, InHand, InBin , Inchoosen};
    public CardState cardState;
    private Ray ray;
  
    void Start()
    {
        gameObject.SetActive(true);
    }

    void Update()
    {

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

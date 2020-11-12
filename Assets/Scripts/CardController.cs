using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public int noOfCards;

    [SerializeField] Button shuffleBtn, discardBtn;
    public List<GameObject> cardsInDeck = new List<GameObject>();
    [SerializeField] List<GameObject> cardsInHand = new List<GameObject>();
    public List<GameObject> cardsInBin = new List<GameObject>();
    [SerializeField] GameObject cardSample;
    [SerializeField] GameObject HandPanel;
    [SerializeField] List<GameObject> cardHolder = new List<GameObject>();
    [SerializeField] Transform t;
    
    // Start is called before the first frame update
    void Start()
    {
        shuffleBtn.onClick.RemoveAllListeners();
        shuffleBtn.onClick.AddListener(OnShuffle);

        discardBtn.onClick.RemoveAllListeners();
        discardBtn.onClick.AddListener(OnDiscard);

        for (int i = 0; i < noOfCards; i++)
        {
            GameObject card = Instantiate(cardSample,t, false);
            card.GetComponent<CardManager>().value = i+1;
            card.GetComponent<CardManager>().cardState = CardManager.CardState.InDeck;
            card.GetComponentInChildren<TextMeshProUGUI>().text = card.GetComponent<CardManager>().value.ToString();
            cardHolder.Add(card);
            cardsInDeck.Add(card);
        }   
    }

    private void Update()
    {
        //for (int i = 0; i < cardHolder.Count; i++)
        //{

        //    switch (cardHolder[i].GetComponent<CardManager>().cardState)
        //    {
        //        case CardManager.CardState.InBin:
        //            gameObject.SetActive(false);
        //            transform.parent = null;
        //            break;

        //        case CardManager.CardState.InDeck:
        //            gameObject.SetActive(true);
        //            transform.parent = t;
        //            break;

        //        case CardManager.CardState.InHand:
        //            gameObject.SetActive(true);
        //            break;

        //        default:
        //            break;
        //    }
        //}
    }

    void OnShuffle()
    {
        if (cardsInDeck.Count == 0)
        {
            cardsInDeck.AddRange(cardsInBin);
            for (int i = 0; i < cardsInDeck.Count; i++)
            {
                cardsInDeck[i].gameObject.SetActive(true);
                cardsInDeck[i].GetComponent<CardManager>().cardState = CardManager.CardState.InDeck;
                
            }
            cardsInBin.Clear();
        }

        if (cardsInHand.Count == 0)
        {
            List<int> r = new List<int>();

            if (cardsInDeck.Count > 5)
            {
                for (int i = 0; i < 5; i++) r.Add(Random.Range(0, cardsInDeck.Count));

                for (int j = 0; j < r.Count; j++)
                {
                    cardsInHand.Add(cardsInDeck[j]);
                    for (int k = 0; k < cardsInHand.Count; k++)
                    {
                        cardsInHand[k].GetComponent<CardManager>().cardState = CardManager.CardState.InHand;
                        cardsInHand[k].transform.SetParent(HandPanel.transform, false) ;
                        
                        cardsInHand[k].GetComponent<CanvasGroup>().blocksRaycasts = true;
                    }
                    cardsInDeck.RemoveAt(j);
                }
            }
            else
            {
                cardsInHand.AddRange(cardsInDeck);
                for (int k = 0; k < cardsInHand.Count; k++)
                {
                    cardsInHand[k].GetComponent<CardManager>().cardState = CardManager.CardState.InHand;
                    cardsInHand[k].transform.SetParent(HandPanel.transform, false);
                }
                cardsInDeck.Clear();
            }
        }
    }

    void OnDiscard()
    {

        cardsInBin.AddRange(cardsInHand);
        for (int i = 0; i < cardsInBin.Count; i++)
            cardsInBin[i].GetComponent<CardManager>().cardState = CardManager.CardState.InBin;
        cardsInHand.Clear();
    }

}

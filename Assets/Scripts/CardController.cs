using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public int noOfCards;

    [SerializeField] Button shuffleBtn, discardBtn;
    [SerializeField] List<GameObject> cardsInDeck = new List<GameObject>();
    [SerializeField] List<GameObject> cardsInHand = new List<GameObject>();
    [SerializeField] List<GameObject> cardsInBin = new List<GameObject>();
    [SerializeField] GameObject cardSample;
    
    // Start is called before the first frame update
    void Start()
    {
        shuffleBtn.onClick.RemoveAllListeners();
        shuffleBtn.onClick.AddListener(OnShuffle);

        discardBtn.onClick.RemoveAllListeners();
        discardBtn.onClick.AddListener(OnDiscard);

        for (int i = 0; i < noOfCards; i++)
        {
            GameObject card = Instantiate(cardSample);
            card.GetComponent<CardManager>().value = i;
            card.GetComponent<CardManager>().cardState = CardManager.CardState.InDeck;
            cardsInDeck.Add(Instantiate(card));
        }    
    }

    void OnShuffle()
    {
        if (cardsInDeck.Count == 0)
        {
            cardsInDeck.AddRange(cardsInBin);
            for (int i = 0; i < cardsInDeck.Count; i++)
                cardsInDeck[i].GetComponent<CardManager>().cardState = CardManager.CardState.InDeck;

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
                        cardsInHand[k].GetComponent<CardManager>().cardState = CardManager.CardState.InHand;
                    cardsInDeck.RemoveAt(j);
                }
            }
            else
            {
                cardsInHand.AddRange(cardsInDeck);
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

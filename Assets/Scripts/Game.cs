using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    public List<GameObject> cardsInDeck = new List<GameObject>();
    public List<GameObject> cardsInHand = new List<GameObject>();
    public List<GameObject> cardsInBin = new List<GameObject>();
    public int numberofcardsNeeded = 12;
    public int noOfCardsNeedToBeDrawn = 5;
    public GameObject inst;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberofcardsNeeded; i++)
        {
            GameObject gameObject = Instantiate(inst, Vector3.one, Quaternion.identity);
            cardsInDeck.Add(gameObject);
        }
    }

    public void Draw()
    {
        if (cardsInDeck.Count >= noOfCardsNeedToBeDrawn)
        {
            for (int i = 0; i < noOfCardsNeedToBeDrawn; i++)
            {
                int r = Random.Range(0, cardsInDeck.Count);
                cardsInHand.Add(cardsInDeck[r]);
                cardsInDeck.Remove(cardsInDeck[r]);
            }
        }
        else if (cardsInDeck.Count < noOfCardsNeedToBeDrawn)
        {
            for (int i = 0; i < cardsInDeck.Count; i++) //2
            {
                cardsInHand.Add(cardsInDeck[i]);
            }
            cardsInDeck.Clear();
            
            if (cardsInHand.Count < noOfCardsNeedToBeDrawn)
            {
                for (int i = 0; i < cardsInBin.Count; i++)
                {
                    cardsInDeck.Add(cardsInBin[i]);
                }
                
                cardsInBin.Clear();
                Debug.Log(cardsInHand.Count );
                Debug.Log(cardsInDeck.Count );
                var inHandCards = cardsInHand.Count;
                for (int i = 0; i <noOfCardsNeedToBeDrawn - inHandCards; i++)  //5-2 = 3 
                {
                    Debug.Log("run");
                    int h = Random.Range(0, cardsInDeck.Count);
                    cardsInHand.Add(cardsInDeck[h]);
                    cardsInDeck.Remove(cardsInDeck[h]);
                }
            }
        }
    }
    
    public void Discard()
    {
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInBin.Add(cardsInHand[i]);
        }
        cardsInHand.Clear();
        Draw();
    }
}
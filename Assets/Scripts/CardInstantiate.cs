using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInstantiate : MonoBehaviour
{
    public List<Card> card_list = new List<Card>();
    public List<GameObject> card_in_hand = new List<GameObject>();
    public List<GameObject> Deck = new List<GameObject>();
    public GameObject card_prefab;
    public int Cards_in_hand = 5;
    public Transform Holder;
    public Transform card_Hand;

    public Drop drop;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < card_list.Count; i++)
        {
            GameObject gameObject = Instantiate(card_prefab,Holder,false);
            gameObject.GetComponent<CardDisply>().card = card_list[i];
            gameObject.GetComponent<Image>().color = card_list[i].color;
            Deck.Add(gameObject);
            gameObject.SetActive(false);

        }
    }

    private void Start()
    {
        StartCoroutine(DrawCards());
    }

    IEnumerator DrawCards()
    {
        for (int i = 0; i < Cards_in_hand; i++)
        {
            
            int randomnumber = Random.Range(0, Deck.Count);
            card_in_hand.Add(Deck[randomnumber].gameObject);
            Deck.RemoveAt(randomnumber);
            card_in_hand[i].gameObject.transform.SetParent(card_Hand);
            card_in_hand[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Onclick()
    {
        StartCoroutine(ChooseCards());
    }

    IEnumerator ChooseCards()
    {
        for (int i = 0; i == drop.card_choose.Count ; i++)
        {
            yield return new WaitForSeconds(1f);
            //drop.cardmanager[i].
        }
        
    }
}

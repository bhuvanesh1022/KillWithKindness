using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public CardController CardController;
    public TextMeshProUGUI one;
    public TextMeshProUGUI two;
    public TextMeshProUGUI three;
    public TextMeshProUGUI Ans;

    public TextMeshProUGUI deck_count;
    public TextMeshProUGUI bin_count;
    public GameObject choose_panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (choose_panel.transform.childCount <= 0)
        {
            one.text = ""+00;
            two.text = "" +00;
            three.text = "" +00;
            Ans.text = "" + 00;
        }

            if (choose_panel.transform.childCount == 1)
            {
                one.text = choose_panel.transform.GetChild(0).GetComponent<CardManager>().value.ToString();
            }
            if (choose_panel.transform.childCount == 2)
            {
                two.text = choose_panel.transform.GetChild(1).GetComponent<CardManager>().value.ToString();
            }
            if (choose_panel.transform.childCount == 3)
            {
                three.text = choose_panel.transform.GetChild(2).GetComponent<CardManager>().value.ToString();
                int i = int.Parse( one.text) +int.Parse( two.text )+int.Parse( three.text);
                Ans.text = i.ToString();
            }


        bin_count.text = CardController.cardsInBin.Count.ToString();
        deck_count.text = CardController.cardsInDeck.Count.ToString();
       

    }
}

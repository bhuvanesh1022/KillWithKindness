using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisply : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI name_text;
    public TextMeshProUGUI int_text;
    public TextMeshProUGUI multiply;
    public TextMeshProUGUI power;
    public Image img;

    public Texture texture;
    // Start is called before the first frame update
    void Start()
    {
        name_text.text = card.power_name;
        texture = card.texture;
        //int_text.text = card.power_number.ToString();
        img.sprite = card.card_image;
        power.text = card.cardPower;
        
        if (card.impacton == Card.ImpactOn.Assetiveness)
        {
            multiply.text = "Assertiveness";
            int_text.text = card.power_number+ " X";
        }
        if (card.impacton == Card.ImpactOn.Empathy)
        {
            multiply.text = "Empathy";
            int_text.text = card.power_number+ " X";
        }
        if (card.impacton == Card.ImpactOn.none)
        {
            if (card.onassetiveness == true)
            {
                multiply.text = "Assertiveness";
                int_text.text = "+ "+card.power_number;
            }
            if (card.onempathy == true)
            {
                multiply.text = "Empathy";
                int_text.text = "+ "+card.power_number;
            }
            if (card.onassetiveness && card.onempathy)
            {
                multiply.text = "Assertiveness" + "+ Empathy";
                int_text.text = "+ "+card.power_number;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

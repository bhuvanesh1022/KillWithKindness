﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisply : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI name_text;
    public TextMeshProUGUI int_text;
    // Start is called before the first frame update
    void Start()
    {
        name_text.text = card.power_name;
        int_text.text = card.power_number.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
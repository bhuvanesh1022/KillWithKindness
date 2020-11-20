using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New_card",menuName ="card")]

public class Card : ScriptableObject
{
    public string power_name;
    public int power_number;
    public Sprite card_image;
    
}

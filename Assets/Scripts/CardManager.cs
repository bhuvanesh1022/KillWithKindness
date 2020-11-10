using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public enum CardState {InDeck, InHand, InBin};
    public CardState cardState;

    public int value;
}

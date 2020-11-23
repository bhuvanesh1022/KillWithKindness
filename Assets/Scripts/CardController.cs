using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardController : MonoBehaviour
{
    public int noOfCards;

    [SerializeField] Button shuffleBtn;
    public Button discardBtn;
    public  List<Card> cardsScriptableObjects = new  List<Card>();
    public List<Card> enemyScriptableObjects = new List<Card>();
    public List<GameObject> cardsInDeck = new List<GameObject>();
    [SerializeField] List<GameObject> cardsInHand = new List<GameObject>();
    public List<GameObject> cardsInBin = new List<GameObject>();
    [SerializeField] GameObject cardSample;
    [SerializeField] GameObject HandPanel;
    [SerializeField] List<GameObject> cardHolder = new List<GameObject>();
    [SerializeField] Transform t;

    public TextMeshProUGUI cardsInDeckCount;
    //public TextMeshProUGUI cardsInBinCount;
    public Slider PlayerSlider;
    public Slider EnemySlider;
    public Drop drop;
    
    public GameObject panel;
    public GameObject card;
    public GameObject gameOverPanel;
    public TextMeshProUGUI attackPoint;
    public TextMeshProUGUI healPoint;

    public GameObject enemy_attack;
    public List<GameObject> enemy_card = new List<GameObject>();
    public TextMeshProUGUI winnerText;
    
    public TextMeshProUGUI maxValueResistance;
    public TextMeshProUGUI maxValueEnthusiasm;
    public TextMeshProUGUI currentEnthusiasm;
    public TextMeshProUGUI currentResistance;

    public Slider Assertivness;
    public Slider Empathy;

    public Animator HandPanelAnim;

    public Animator ChoosePanelAnim;
    // Start is called before the first frame update
    void Start()
    {
        Assertivness.value = 2f;
        Empathy.value = 3f;
        EnemySlider.value = 10f;
        PlayerSlider.value = 10f;
        shuffleBtn.onClick.RemoveAllListeners();
        shuffleBtn.onClick.AddListener(OnShuffle);
        panel.SetActive(false);
        attackPoint.gameObject.SetActive(false);
        healPoint.gameObject.SetActive(false);
        enemy_attack.SetActive(false);
        for (int i = 0; i < noOfCards; i++)
        {
            GameObject card = Instantiate(cardSample,t, false);
            card.GetComponent<CardManager>().cardState = CardManager.CardState.InDeck;
            card.GetComponent<CardDisply>().card = cardsScriptableObjects[i];
            cardHolder.Add(card);
            cardsInDeck.Add(card);
        }
    }
    

    private void Update()
    {
        cardsInDeckCount.text = cardsInDeck.Count.ToString();
        Debug.Log(cardsInDeckCount.text + "Total cards in Deck");
        maxValueEnthusiasm.text = PlayerSlider.maxValue.ToString();
        currentEnthusiasm.text = PlayerSlider.value.ToString();
        maxValueResistance.text = EnemySlider.maxValue.ToString();
        currentResistance.text = EnemySlider.value.ToString();
        if (EnemySlider.value == 0)
        {
            enemy_attack.SetActive(false);
            gameOverPanel.SetActive(true);
            winnerText.text=
                "You Have Won the Game";
            
            Time.timeScale = 0;
        }
        else if(PlayerSlider.value == 0)
        {
            enemy_attack.SetActive(false);
            gameOverPanel.SetActive(true);
            winnerText.text =
                "Opponent Won the Game";
            
            Time.timeScale = 0;
        }
        else
        {
            gameOverPanel.SetActive(false);
           
        }

    }
    public void Restart()
    {
       Application.Quit();
       
    }
    void OnShuffle()
    {
        if (cardsInDeck.Count < 3)
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
                for (int i = 0; i < 5; i++)
                {
                    r.Add(Random.Range(0, cardsInDeck.Count));
                    Debug.Log(r[i]);
                }
                
                for (int j = 0; j < r.Count; j++)
                {
                    cardsInHand.Add(cardsInDeck[j]);
                    for (int k = 0; k < cardsInHand.Count; k++)
                    {
                        cardsInHand[k].GetComponent<CardManager>().cardState = CardManager.CardState.InHand;
                        cardsInHand[k].transform.SetParent(HandPanel.transform, false) ;
                        cardsInHand[k].GetComponent<CanvasGroup>().alpha = 1f;
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

    public void OnDiscard()
    {
        discardBtn.gameObject.SetActive(false);
        HandPanelAnim.SetBool("choose",true);
        StartCoroutine(CardChoose());
    }

    IEnumerator CardChoose()
    {
        Debug.Log("Clicked");
        ChoosePanelAnim.SetBool("choose",true);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < drop.card_choose.Count; i++)
        {
            panel.SetActive(true);
  
            string name = drop.card_choose[i].GetComponent<CardDisply>().card.power_name;
            int power = drop.card_choose[i].GetComponent<CardDisply>().card.power_number;
            Debug.Log(name + power);
            card.GetComponent<Image>().sprite = drop.card_choose[i].GetComponent<Image>().sprite;
            card.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = name;
            card.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = power.ToString();
            drop.card_choose[i].GetComponent<CanvasGroup>().alpha = 0f;
            
            if (drop.card_choose[i].GetComponent<CardDisply>().card.cardPower == "Attack")
            {
                if (drop.card_choose[i].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.Assetiveness)
                {
                    power = power * (int)Assertivness.value;
                }
                if (drop.card_choose[i].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.Empathy)
                {
                    power = power * (int)Empathy.value;
                }
                if (drop.card_choose[i].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.none)
                {
                    power = power;
                }
                EnemySlider.value -= power;
                Debug.Log("HIT"+EnemySlider.value);
                attackPoint.gameObject.SetActive(true);
                attackPoint.color = new Color(255,0,0,255);
                attackPoint.text = "-" + power.ToString();
                yield return new WaitForSeconds(0.3f);
            }
            else if (drop.card_choose[i].GetComponent<CardDisply>().card.cardPower == "Defence")
            {
                if (drop.card_choose[i].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.Assetiveness)
                {
                    power = power * (int)Assertivness.value;
                }
                if (drop.card_choose[i].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.Empathy)
                {
                    power = power * (int)Empathy.value;
                }
                if (drop.card_choose[i].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.none)
                {
                    power = power;
                }
                PlayerSlider.value += power;
                healPoint.gameObject.SetActive(true);
                healPoint.color = new Color32(54,198,32,255);
                healPoint.text = "+"+power.ToString();
                yield return new WaitForSeconds(0.3f);
            }
            
            yield return new WaitForSeconds(1f);
        }
        
        cardsInBin.AddRange(cardsInHand);
        
        for (int i = 0; i < cardsInBin.Count; i++)
        {
            //cardsInBinCount.text = cardsInBin.Count.ToString();
            cardsInBin[i].GetComponent<CardManager>().cardState = CardManager.CardState.InBin;
            cardsInBin[i].GetComponent<CardManager>().gameObject.SetActive(false);
            cardsInBin[i].GetComponent<Drag>().return_to_parent = null;
            cardsInBin[i].GetComponent<Transform>().SetParent(null);
        }
        cardsInHand.Clear();
        drop.card_choose.Clear();
        discardBtn.interactable = true;
        panel.SetActive(false);
        attackPoint.gameObject.SetActive(false);
        healPoint.gameObject.SetActive(false);
        shuffleBtn.interactable = false;
        StartCoroutine(Enemy_Card());
    }

    IEnumerator Enemy_Card()
    {
        enemy_attack.SetActive(true);
        yield return new WaitForSeconds(2f);
        
        for (int i = 0; i < 1; i++)
        {
            int random = Random.Range(0, enemyScriptableObjects.Count);
            panel.SetActive(true);
            string power =enemyScriptableObjects[random].cardPower ;
            string name = enemyScriptableObjects[random].power_name;
            int number =enemyScriptableObjects[random].power_number ;
            Debug.Log(name+"  number"+number);
            card.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text=name;
            card.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text=number.ToString();
            card.GetComponent<Image>().sprite = enemyScriptableObjects[random].card_image;
            
            if (power.ToString() == "Attack")
            {
                PlayerSlider.value -= number;
                Debug.Log("HIT"+EnemySlider.value);
                healPoint.gameObject.SetActive(true);
                healPoint.color = new Color(124,252,0,255);
                healPoint.text = "-"+ number.ToString();
                yield return new WaitForSeconds(0.3f);
            }
            else if (power.ToString() == "Defence")
            {
                EnemySlider.value += number;
                attackPoint.gameObject.SetActive(true);
                attackPoint.color = new Color32(54,198,32,255);
                attackPoint.text = "+"+number.ToString();
                yield return new WaitForSeconds(0.3f);
            }
            healPoint.gameObject.SetActive(false);
            attackPoint.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
        panel.SetActive(false);
        enemy_attack.SetActive(false);
        shuffleBtn.interactable = true;
        ChoosePanelAnim.SetBool("choose",false);
        yield return new WaitForSeconds(0.6f);
        HandPanelAnim.SetBool("choose",false);
    }
    
}

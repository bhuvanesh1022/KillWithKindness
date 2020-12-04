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
    public int noOfCardsNeedToBeDrawn = 5;
    //[SerializeField] Button shuffleBtn;
    public Button discardBtn;
    public  List<Card> cardsScriptableObjects = new  List<Card>();
    public List<Card> NerdScriptableObjects = new List<Card>();
    public List<Card> JockScriptableObjects = new List<Card>();
    public List<Card> OldTeacherScriptableObjects = new List<Card>();
    public List<Card> ReallyScriptableObjects = new List<Card>();
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
    public TextMeshProUGUI currentEmpathy;
    public TextMeshProUGUI currentAssertiveness;
    public Slider Assertivness;
    public Slider Empathy;
    public List<Sprite> Enemy_image = new List<Sprite>();
    public Image Enemy;
    public List<String> enemy_name = new List<String>();
    public TextMeshProUGUI enemyName;

    public Image enemy_comment_img;
    public TextMeshProUGUI enemy_comment;
    
    public Image player_comment_img;
    public TextMeshProUGUI player_comment;
    void Start()
    {
        Assertivness.value = 1f;
        Empathy.value = 1f;
        EnemySlider.value = 20f;
        PlayerSlider.value = 10f;
        panel.SetActive(false);
        attackPoint.gameObject.SetActive(false);
        healPoint.gameObject.SetActive(false);
        enemy_attack.SetActive(false);
        for (int i = 0; i < noOfCards; i++)
        {
            GameObject card = Instantiate(cardSample,t, false);
            card.GetComponent<CardManager>().cardState = CardManager.CardState.InDeck;
            card.GetComponent<CardDisply>().card = cardsScriptableObjects[i];
            cardsInDeck.Add(card);
        }

        int enemy_number = Random.Range(0, Enemy_image.Count);
        Enemy.sprite = Enemy_image[enemy_number];
        enemyName.text = enemy_name[enemy_number];
        enemy_comment_img.gameObject.SetActive(false);
        player_comment_img.gameObject.SetActive(false);
        DrawCards();
    }
    
    private void Update()
    {
        currentEmpathy.text = Empathy.value.ToString();
        currentAssertiveness.text = Assertivness.value.ToString();
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
        SceneManager.LoadScene("InitialScene");

    }

    public void Quit()
    {
        Application.Quit();
    }

    private void DrawCards()
    {
        StartCoroutine(Draw());
    }
    IEnumerator  Draw()
    {
        if (cardsInDeck.Count >= noOfCardsNeedToBeDrawn)
        {
            for (int i = 0; i < noOfCardsNeedToBeDrawn; i++)
            {
                int r = Random.Range(0, cardsInDeck.Count);
                cardsInHand.Add(cardsInDeck[r]);
                cardsInHand[i].SetActive(true);
                cardsInHand[i].GetComponent<CardManager>().cardState = CardManager.CardState.InHand;
                cardsInHand[i].transform.SetParent(HandPanel.transform, false);
                cardsInHand[i].GetComponent<CanvasGroup>().alpha = 1f;
                cardsInHand[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
                cardsInDeck.Remove(cardsInDeck[r]);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (cardsInDeck.Count < noOfCardsNeedToBeDrawn)
        {
            for (int i = 0; i < cardsInDeck.Count; i++)
            {
                cardsInHand.Add(cardsInDeck[i]);
                cardsInHand[i].SetActive(true);
                cardsInHand[i].GetComponent<CardManager>().cardState = CardManager.CardState.InHand;
                cardsInHand[i].transform.SetParent(HandPanel.transform, false) ;
                cardsInHand[i].GetComponent<CanvasGroup>().alpha = 1f;
                cardsInHand[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
                yield return new WaitForSeconds(0.1f);
            }
            cardsInDeck.Clear();
            
            if (cardsInHand.Count < noOfCardsNeedToBeDrawn)
            {
                for (int i = 0; i < cardsInBin.Count; i++)
                {
                    cardsInDeck.Add(cardsInBin[i]);
                    cardsInDeck[i].GetComponent<CardManager>().cardState = CardManager.CardState.InDeck;
                    cardsInDeck[i].SetActive(true);
                }
                cardsInBin.Clear();
                
                int inHandCards = cardsInHand.Count;
                
                for (int i = 0; i <noOfCardsNeedToBeDrawn - inHandCards; i++)
                {
                    int h = Random.Range(0, cardsInDeck.Count);
                    cardsInDeck[h].SetActive(true);
                    cardsInHand.Add(cardsInDeck[h]);
                    for (int j = 0; j < cardsInHand.Count; j++)
                    {
                        cardsInHand[j].GetComponent<CardManager>().cardState = CardManager.CardState.InHand;
                        cardsInHand[j].transform.SetParent(HandPanel.transform, false) ;
                        cardsInHand[j].GetComponent<CanvasGroup>().alpha = 1f;
                        cardsInHand[j].GetComponent<CanvasGroup>().blocksRaycasts = true;
                        yield return new WaitForSeconds(0.1f);
                    }
                    cardsInDeck.Remove(cardsInDeck[h]);
                }
            }
        }

        yield return null;
    }
    
    public void OnDiscard()
    {
        discardBtn.interactable = false;
        discardBtn.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
        StartCoroutine(CardChoose());
    }

    IEnumerator CardChoose()
    {
        Debug.Log("Clicked");
        for (int i = 0; i < 3; i++)//i=0
        {
            panel.SetActive(true);
  
            string name = drop.card_choose[i].GetComponent<CardDisply>().card.power_name;
            int power = drop.card_choose[i].GetComponent<CardDisply>().card.power_number;
            Debug.Log(name + power);
            card.GetComponent<Image>().sprite = drop.card_choose[i].GetComponent<Image>().sprite;
            card.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = name;
            card.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = power.ToString();
            card.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text =
                drop.card_choose[i].GetComponent<CardDisply>().card.cardPower;
            string multiply = card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text;
            player_comment_img.gameObject.SetActive(true);
            player_comment.text = drop.card_choose[i].GetComponent<CardDisply>().card.comment;
            drop.card_choose[i].GetComponent<CanvasGroup>().alpha = 0f;
            
            if (drop.card_choose[i].GetComponent<CardDisply>().card.cardPower == "Attack")
            {
                if (drop.card_choose[i].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.Assetiveness)
                {
                    power = power * (int)Assertivness.value;
                    card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = power+ " X Assetiveness";
                }
                if (drop.card_choose[i].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.Empathy)
                {
                    power = power * (int)Empathy.value;
                    card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = power+ " X Empathy";
                }
                EnemySlider.value -= power;
                Debug.Log("HIT"+EnemySlider.value);
                attackPoint.gameObject.SetActive(true);
                
                attackPoint.color = new Color(255,0,0,255);
                attackPoint.text = "-" + power.ToString();
                yield return new WaitForSeconds(0.6f);

            }
            else if (drop.card_choose[i].GetComponent<CardDisply>().card.cardPower == "Heal")
            {
                if (drop.card_choose[i].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.Assetiveness)
                {
                    power = power * (int)Assertivness.value;
                    card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text =
                        power + "X Assetiveness";
                }
                if (drop.card_choose[i].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.Empathy)
                {
                    power = power * (int)Empathy.value;
                    card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = power+ "X Empathy";
                }
                
                PlayerSlider.value += power;
                healPoint.gameObject.SetActive(true);
                healPoint.color = new Color32(54,198,32,255);
                healPoint.text = "+"+power.ToString();
                yield return new WaitForSeconds(0.6f);
            }
            else if (drop.card_choose[i].GetComponent<CardDisply>().card.cardPower == "Effects")
            {
                power = power;
                if (drop.card_choose[i].card.onassetiveness == true && drop.card_choose[i].card.onempathy == false)
                {
                    card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = power+ "+ Assertiveness";
                    Assertivness.value = Assertivness.value+1;
                    yield return new WaitForSeconds(0.6f);
                }
                else if (drop.card_choose[i].card.onempathy == true && drop.card_choose[i].card.onassetiveness == false)
                {
                    card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = power+ "+ Empathy";
                    Empathy.value = Empathy.value+1;
                    yield return new WaitForSeconds(0.6f);
                }
                else if (drop.card_choose[i].card.onassetiveness && drop.card_choose[i].card.onempathy)
                {
                    card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = power+ "+ Assertiveness" + power+ "+ Empathy";
                    Assertivness.value = Assertivness.value+1;
                    Empathy.value = Empathy.value+1;
                    yield return new WaitForSeconds(0.6f);
                }
            }

            healPoint.text = " ";
            attackPoint.text = " ";
            yield return new WaitForSeconds(2.5f);
            panel.gameObject.SetActive(false);
            player_comment_img.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.6f);
        }
        
        cardsInBin.AddRange(cardsInHand);
        
        for (int i = 0; i < cardsInBin.Count; i++)
        {
            cardsInBin[i].GetComponent<CardManager>().cardState = CardManager.CardState.InBin;
            cardsInBin[i].transform.position = t.localPosition;
            cardsInBin[i].GetComponent<Drag>().return_to_parent = null;
            cardsInBin[i].GetComponent<Transform>().SetParent(null);
        }
        cardsInHand.Clear();
        drop.card_choose.Clear();
        panel.SetActive(false);
        discardBtn.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        discardBtn.interactable = true;
        attackPoint.gameObject.SetActive(false);
        healPoint.gameObject.SetActive(false);
        player_comment_img.gameObject.SetActive(false);
        StartCoroutine(Enemy_Card());
    }

    IEnumerator Enemy_Card()
    {
        enemy_attack.SetActive(true);
        yield return new WaitForSeconds(2f);

        if (enemyName.text == "Nerd Bully")
        {
            for (int i = 0; i < NerdScriptableObjects.Count; i++)
            {
                enemyScriptableObjects.Add(NerdScriptableObjects[i]);
            }
        }
        if (enemyName.text == "Jock Bully")
        {
            for (int i = 0; i < JockScriptableObjects.Count; i++)
            {
                enemyScriptableObjects.Add(JockScriptableObjects[i]);
            }
        }
        if (enemyName.text == "Mean Teacher")
        {
            for (int i = 0; i < OldTeacherScriptableObjects.Count; i++)
            {
                enemyScriptableObjects.Add(OldTeacherScriptableObjects[i]);
            }
        }
        if (enemyName.text == "Really Bully")
        {
            for (int i = 0; i < ReallyScriptableObjects.Count; i++)
            {
                enemyScriptableObjects.Add(ReallyScriptableObjects[i]);
            }
        }
        
        for (int i = 0; i < 1; i++)
        {
            int random = Random.Range(0, enemyScriptableObjects.Count);
            panel.SetActive(true);
            string power =enemyScriptableObjects[random].cardPower ;
            string name = enemyScriptableObjects[random].power_name;
            int number =enemyScriptableObjects[random].power_number ;
            Debug.Log(name+"  number"+number);
            card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = " ";
            card.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text=name;
            card.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text=number.ToString();
            card.GetComponent<Image>().sprite = enemyScriptableObjects[random].card_image;
            
            if (power.ToString() == "Attack")
            {
                PlayerSlider.value -= number;
                Debug.Log("HIT"+EnemySlider.value);
                healPoint.gameObject.SetActive(true);
                //healPoint.color = new Color(124,252,0,255);
                healPoint.color = new Color(255,0,0,255);
                healPoint.text = "-"+ number.ToString();
                enemy_comment_img.gameObject.SetActive(true);
                enemy_comment.text = enemyScriptableObjects[random].comment;
                yield return new WaitForSeconds(0.6f);
            }
            else if (power.ToString() == "Heal")
            {
                EnemySlider.value += number;
                attackPoint.gameObject.SetActive(true);
                attackPoint.color = new Color32(54,198,32,255);
                attackPoint.text = "+"+number.ToString();
                enemy_comment_img.gameObject.SetActive(true);
                enemy_comment.text = enemyScriptableObjects[random].comment;
                yield return new WaitForSeconds(0.6f);
            }
            healPoint.gameObject.SetActive(false);
            attackPoint.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
        panel.SetActive(false);
        enemy_attack.SetActive(false);
        enemy_comment_img.gameObject.SetActive(false);
        enemyScriptableObjects.Clear();
        yield return new WaitForSeconds(0.6f);
        DrawCards();
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardController : MonoBehaviour
{
    public int numberofturnsTaken = 0;
    // animate the game object from -1 to +1 and back
    public float minimum = 0.01f;
    public float maximum = 0.20f;

    // starting value for the Lerp
    public float timeT = 0.2f;
    
    public Material mat;
    public Material assertive_glow,empathy_glow;
    public int noOfCards;
    public int noOfCardsNeedToBeDrawn = 5;
    //[SerializeField] Button shuffleBtn;
    public Button enemyconfirmbtn;
    public Button nextBtn;
    [FormerlySerializedAs("discardBtn")] public Button confirmBtn;
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
    //public TextMeshProUGUI winnerText;
    
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

    public Image player_gameover_panel,enemy_gameover_panel;
    
    void Start()
    {
        assertive_glow.SetFloat("_Outline",0f);
        empathy_glow.SetFloat("_Outline",0f);
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
        enemy_gameover_panel.sprite = Enemy.sprite;
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
            Time.timeScale = 0;
        }
        else if(PlayerSlider.value == 0)
        {
            enemy_attack.SetActive(false);
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            gameOverPanel.SetActive(false);
        }

        //StartCoroutine(Blink());
        
        //mat.SetFloat("_Outline", Mathf.Repeat(Time.time*Time.deltaTime,0.99f));
        mat.SetFloat("_Outline",Mathf.Lerp(minimum,maximum,timeT));
        timeT += 0.5f * Time.deltaTime;
        if (timeT >0.2f)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            timeT = 0.01f;
        }
    }

    IEnumerator Blink()
    {
        if (mat.GetFloat("_Outline") >= 0.99f)
        {
            for (int i = 0; i < 0.99f; i++)
            {
                mat.SetFloat("_Outline",mat.GetFloat("_Outline"));
                yield return new WaitForSeconds(0.05f);
            }
        }
        if (mat.GetFloat("_Outline") <= 0)
        {
            for (int i = 1; i < 0.01f; i--)
            {
                mat.SetFloat("_Outline",mat.GetFloat("_Outline"));
                yield return new WaitForSeconds(0.05f);
            }
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
                    }
                    cardsInDeck.Remove(cardsInDeck[h]);
                }
            }
        }

        yield return null;
    }
    public int currentIndex = 0;
    
    public void OnDiscard()  
    {
        Debug.Log("called");
        confirmBtn.interactable = false;
        confirmBtn.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
        assertive_glow.SetFloat("_Outline",0f);
        empathy_glow.SetFloat("_Outline",0f);
        StartCoroutine(I_OnCardChoose());
    }
    
    private IEnumerator I_OnCardChoose()
    {
        panel.SetActive(true);
        
        string name = drop.card_choose[currentIndex].GetComponent<CardDisply>().card.power_name;
        int power = drop.card_choose[currentIndex].GetComponent<CardDisply>().card.power_number;
        Debug.Log(name + power);
        mat.SetTexture("_MainTexture",drop.card_choose[currentIndex].GetComponent<CardDisply>().texture);
        //mat.mainTexture("_MainTexture", drop.card_choose[currentIndex].GetComponent<CardDisply>().texture);
        card.GetComponent<Image>().sprite = drop.card_choose[currentIndex].GetComponent<Image>().sprite;
        card.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = name;
        card.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = power.ToString();
        card.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text =
            drop.card_choose[currentIndex].GetComponent<CardDisply>().card.cardPower;
        string multiply = card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text;
        player_comment_img.gameObject.SetActive(true);
        player_comment.text = drop.card_choose[currentIndex].GetComponent<CardDisply>().card.comment;
        drop.card_choose[currentIndex].GetComponent<CanvasGroup>().alpha = 0f;

        if (drop.card_choose[currentIndex].GetComponent<CardDisply>().card.cardPower == "Attack")
        {
            if (drop.card_choose[currentIndex].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.Assetiveness)
            {
                assertive_glow.SetFloat("_Outline",1f);
                power = power * (int) Assertivness.value;
                card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = power + " X Assetiveness";
            }

            if (drop.card_choose[currentIndex].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.Empathy)
            {
                empathy_glow.SetFloat("_Outline",1f);
                power = power * (int) Empathy.value;
                card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = power + " X Empathy";
            }
            EnemySlider.value -= power;
            Debug.Log("HIT" + EnemySlider.value);
            attackPoint.gameObject.SetActive(true);

            attackPoint.color = new Color(255, 0, 0, 255);
            attackPoint.text = "-" + power.ToString();
        }
        else if (drop.card_choose[currentIndex].GetComponent<CardDisply>().card.cardPower == "Heal")
        {
            if (drop.card_choose[currentIndex].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.Assetiveness)
            {
                assertive_glow.SetFloat("_Outline",1f);
                power = power * (int) Assertivness.value;
                card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text =
                    power + "X Assetiveness";
            }

            if (drop.card_choose[currentIndex].GetComponent<CardDisply>().card.impacton == Card.ImpactOn.Empathy)
            {
                empathy_glow.SetFloat("_Outline",1f);
                power = power * (int) Empathy.value;
                card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = power + "X Empathy";
            }

            PlayerSlider.value += power;
            healPoint.gameObject.SetActive(true);
            healPoint.color = new Color32(54, 198, 32, 255);
            healPoint.text = "+" + power.ToString();
            yield return new WaitForSeconds(0.6f);
        }
        else if (drop.card_choose[currentIndex].GetComponent<CardDisply>().card.cardPower == "Effects")
        {
            power = power;
            if (drop.card_choose[currentIndex].card.onassetiveness == true &&
                drop.card_choose[currentIndex].card.onempathy == false)
            {
                card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = power + "+ Assertiveness";
                assertive_glow.SetFloat("_Outline",0.7f);
                Assertivness.value = Assertivness.value + 1;
            }
            else if (drop.card_choose[currentIndex].card.onempathy == true &&
                     drop.card_choose[currentIndex].card.onassetiveness == false)
            {
                card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = power + "+ Empathy";
                empathy_glow.SetFloat("_Outline",0.7f);
                Empathy.value = Empathy.value + 1;
            }
            else if (drop.card_choose[currentIndex].card.onassetiveness &&
                     drop.card_choose[currentIndex].card.onempathy)
            {
                card.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text =
                    power + "+ Assertiveness" + power + "+ Empathy";
                assertive_glow.SetFloat("_Outline",1f);
                empathy_glow.SetFloat("_Outline",1f);
                Assertivness.value = Assertivness.value + 1;
                Empathy.value = Empathy.value + 1;
            }

        }
        if (currentIndex < drop.card_choose.Count -1)
        {
            yield return new WaitForSeconds(0.2f);
            confirmBtn.interactable = true;
            confirmBtn.GetComponentInChildren<TextMeshProUGUI>().SetText("Next");
            confirmBtn.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            currentIndex++;
        }

        else
        {
            confirmBtn.interactable=false;
            confirmBtn.GetComponent<CanvasGroup>().alpha = 0f;
            yield return new WaitForSeconds(0.1f);
            currentIndex = 0;
            nextBtn.gameObject.SetActive(true);
        }
    }

    public void PlayerClose()
    {
        nextBtn.gameObject.SetActive(false);
        assertive_glow.SetFloat("_Outline",0f);
        empathy_glow.SetFloat("_Outline",0f);
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
        confirmBtn.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        confirmBtn.interactable = true;
        attackPoint.gameObject.SetActive(false);
        healPoint.gameObject.SetActive(false);
        player_comment_img.gameObject.SetActive(false);
        
        StartCoroutine(Enemy_Card());
    }
    IEnumerator Enemy_Card()
    {
        enemy_attack.SetActive(true);
        yield return new WaitForSeconds(1f);

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
            mat.SetTexture("_MainTexture",enemyScriptableObjects[random].texture);
            
            if (power.ToString() == "Attack")
            {
                PlayerSlider.value -= number;
                healPoint.gameObject.SetActive(true);
                healPoint.color = new Color(255,0,0,255);
                healPoint.text = "-"+ number.ToString();
                enemy_comment_img.gameObject.SetActive(true);
                enemy_comment.text = enemyScriptableObjects[random].comment;
                yield return new WaitForSeconds(1f);
            }
            else if (power.ToString() == "Heal")
            {
                EnemySlider.value += number;
                attackPoint.gameObject.SetActive(true);
                attackPoint.color = new Color32(54,198,32,255);
                attackPoint.text = "+"+number.ToString();
                enemy_comment_img.gameObject.SetActive(true);
                enemy_comment.text = enemyScriptableObjects[random].comment;
                yield return new WaitForSeconds(1f);
            }
            healPoint.gameObject.SetActive(false);
            attackPoint.gameObject.SetActive(false);
        }
        enemyconfirmbtn.gameObject.SetActive(true);
    }

    public void EnemyFinsih()
    {
        enemyconfirmbtn.gameObject.SetActive(false);
        panel.SetActive(false);
        enemy_attack.SetActive(false);
        enemy_comment_img.gameObject.SetActive(false);
        enemyScriptableObjects.Clear();
        numberofturnsTaken += 1;
        DrawCards();
    }

    IEnumerator BattleEndPoints()
    {
        yield return new WaitForSeconds(2f);
        
    }
}

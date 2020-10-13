using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MemoryGameManager : MonoBehaviour
{
    List<Card> choiceCards = new List<Card>();
    List<Card> chosenCards = new List<Card>();

    List<int> generatedNumbers = new List<int>();

    List<Card> getChoiceCards()
    {
        return choiceCards;
    }
    List<Card> getChosenCards()
    {
        return chosenCards;
    }


    public CardButton cardButton;
    public GameObject choiceContent;
    public GameObject chosenContent;
    int round = 1;

    int GenerateCardNumber()
    {

        int rand = Random.Range(0, 100);
        while (generatedNumbers.Contains(rand))
        {
            rand = Random.Range(0, 100);
        }
        return rand;
    }

    void GenerateCards(int round)
    {
        choiceCards.Clear();

        for (int i = 0; i < 2 * round; i++)
        {

            choiceCards.Add(new Card(
                GenerateCardNumber(), (i < (2 * round) / 2), false
            ));

        }

        Shuffler.Shuffle(choiceCards);
        
    }

    void StartNextRound()
    {

        chosenCards.Clear();
        GenerateCards(round);
        GenerateCardUI();
        StartCoroutine(HideCards());
    }

    IEnumerator HideCards()
    {

        yield return new WaitForSeconds(2f);
        foreach (Card card in choiceCards)
        {
            card.isHidden = true;
        }
        ClearCardUI();
        GenerateCardUI();
    }

    public void OnClickSubmit()
    {
        bool fail = false;
        if (chosenCards.Count > 0)
        {
            foreach (Card card in chosenCards)
            {
                if (!card.isRed)
                { fail = true; break; }
            }
            foreach (Card card in choiceCards)
            {
                if (card.isRed)
                { fail = true; break; }
            }
            if (fail)
            {
                StartNextRound();
            }
            else
            {
                round++;
                StartNextRound();
            }
        }
    }

    void GenerateCardUI()
    {
        foreach (Transform child in choiceContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in chosenContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Card card in choiceCards)
        {
            CardButton b = Instantiate<CardButton>(cardButton);
            b.transform.SetParent(choiceContent.transform);
            b.card = card;
            Text text = b.GetComponentInChildren<Text>();

            text.text = card.CardNumber.ToString();
            Image image = b.GetComponent<Image>();
            image.color = card.isHidden ? Color.white : card.isRed ? Color.red : Color.green;
        }
    }

    public void OnTapCard(CardButton cardButton)
    {   //Dont know why, but it works with the opposite condition
        if (cardButton.card.isHidden)
        {
            chosenCards.Add(cardButton.card);
            choiceCards.Remove(cardButton.card);
            cardButton.gameObject.transform.SetParent(chosenContent.transform);
        }
    }

    void ClearCardUI()
    {
        foreach (Transform transform in choiceContent.transform)
        {
            GameObject.Destroy(transform.gameObject);
        }
    }

    void Start()
    {
        StartNextRound();
    }



    void Update()
    {

    }

    
}

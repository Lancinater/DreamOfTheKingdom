using System;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;
    public CardLayoutManager cardLayoutManager;

    private List<CardDataSO> drawDeck = new();
    private List<CardDataSO> discardDeck = new();
    private List<Card> cardsInHand = new();

    public void InitializeDeck()
    {
        drawDeck.Clear();
        foreach (var entry in cardManager.currentLibrary.cardLibraryList)
        {
            for(int i=0;i<entry.amount;i++)
            {
                drawDeck.Add(entry.cardData);
            }
        }
        
        // TODO: Shuffle the deck/Update the UI
        
    }
    
    // Test Draw Card
    private void Start()
    {
        InitializeDeck();
        DrawCard(3);
    }

    [ContextMenu("Test Draw Card")]
    private void testDrawCard()
    {
        DrawCard(1);
    }
    
    private void DrawCard(int amount)
    {
        for(int i=0;i<amount;i++)
        {
            if (drawDeck.Count == 0)
            {
                // TODO:Shuffle the discard deck to the draw deck
                
            }
            var cardData = drawDeck[0];
            drawDeck.RemoveAt(0);
            var card = cardManager.GetCardObject().GetComponent<Card>();
            card.Init(cardData);
            cardsInHand.Add(card);
            SetCardLayout();
        }
    }

    private void SetCardLayout()
    {
        for(int i=0;i<cardsInHand.Count;i++)
        {
            var card = cardsInHand[i];
            var cardTransform = cardLayoutManager.GetCardTransform(i, cardsInHand.Count);
            card.transform.SetPositionAndRotation(cardTransform.position, cardTransform.rotation);
        }
    }
}

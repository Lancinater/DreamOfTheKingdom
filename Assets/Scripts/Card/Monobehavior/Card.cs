using System;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Card Data")]
    public SpriteRenderer cardImage;
    public CardDataSO cardData;
    public CardType cardType;
    public TextMeshPro costText, descriptionText, typeText;


    private void Start()
    {
        Init(cardData);
    }

    public void Init(CardDataSO data)
    {
        cardData = data;
        cardImage.sprite = data.cardImage;
        costText.text = data.cost.ToString();
        descriptionText.text = data.description;
        typeText.text = data.cardType.ToString();
    }
}

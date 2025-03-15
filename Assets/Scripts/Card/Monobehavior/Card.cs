using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Card : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("Card Data")]
    public SpriteRenderer cardImage;
    public CardDataSO cardData;
    public CardType cardType;
    public TextMeshPro costText, descriptionText, typeText;

    [Header("Original Position for Animation")]
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public int originalSortingOrder;
    public bool isAnimating;

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

    public void UpdatePositionRotation(Vector3 position, Quaternion rotation)
    {
        originalPosition = position;
        originalRotation = rotation;
        originalSortingOrder = GetComponent<SortingGroup>().sortingOrder;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isAnimating)
        {
            return;
        }
        transform.SetPositionAndRotation(originalPosition + Vector3.up, Quaternion.identity);
        GetComponent<SortingGroup>().sortingOrder = 20;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isAnimating)
        {
            return;
        }
        transform.SetPositionAndRotation(originalPosition, originalRotation);
        GetComponent<SortingGroup>().sortingOrder = originalSortingOrder;
    }
}

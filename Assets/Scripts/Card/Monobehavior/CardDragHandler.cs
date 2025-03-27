using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool canDrag;
    public Card card;
    public bool canExecute;
    private CharacterBase target;
    public GameObject dragArrowPrefab;
    private GameObject dragArrow;
    

    private void Awake()
    {
        card = GetComponent<Card>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            card.isAnimating = true;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            card.transform.position = worldPos;
            canExecute = worldPos.y > 1f;
        }
        else
        {
            if(eventData.pointerEnter == null)
                return;
            if (eventData.pointerEnter.CompareTag("Enemy"))
            {
                canExecute = true;
                target = eventData.pointerEnter.GetComponent<CharacterBase>();
                return;
            }
            canExecute = false;
            target = null;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        switch (card.cardData.cardType)
        {
            case CardType.Ability:
            case CardType.Defense:
                canDrag = true;
                break;
            case CardType.Attack:
                dragArrow = Instantiate(dragArrowPrefab, transform);
                break;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(dragArrow!=null)
            Destroy(dragArrow);
        if (canExecute)
        {
            // TODO: Execute the card
            card.ExecuteCardEffects(card.player, target);
        }
        else
        {
            card.transform.position = card.originalPosition;
            card.isAnimating = false;
        }
        
    }
}

using System.Collections.Generic;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    public bool isHorizontal;

    public float maxWidth = 7f;

    public float cardSpacing = 2f;

    public Vector3 centrePoint;
    
    [SerializeField]private List<Vector3> cardPositions = new List<Vector3>();
    private List<Quaternion> cardRotations = new List<Quaternion>();

    public CardTransform GetCardTransform(int index, int totalCards)
    {
        CalculatePositions(totalCards, isHorizontal);
        
        return new CardTransform(cardPositions[index], cardRotations[index]);
    }
    
    private void CalculatePositions(int numberOfCards, bool horizontal)
    {
        
        cardPositions.Clear();
        cardRotations.Clear();
        if (horizontal)
        {
            float currentWidth = cardSpacing * (numberOfCards - 1);
            float totalWidth = Mathf.Min(currentWidth, maxWidth);
            float currentSpacing = totalWidth>0?totalWidth/(numberOfCards-1):0;
            
            for(int i=0; i<numberOfCards; i++)
            {
                float x = 0 - (totalWidth/2) + (i*currentSpacing);
                var pos = new Vector3(x, centrePoint.y, 0);
                var rot = Quaternion.identity;
                cardPositions.Add(pos);
                cardRotations.Add(rot);
            }
            
            
        }
    }
    
    
}

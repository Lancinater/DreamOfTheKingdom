using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public MapConfigureSO mapConfigure;
    public Room roomPrefab;

    private float height;
    private float width;
    private float columnWidth;
    private Vector3 generatePosition;
    public float border;
    
    private void Awake()
    {
        height = Camera.main.orthographicSize * 2;
        width = height * Camera.main.aspect;
        columnWidth = width / (mapConfigure.roomBlueprints.Count + 1);
    }

    private void Start()
    {
        CreateMap();
    }

    public void CreateMap()
    {
        for(int column=0; column<mapConfigure.roomBlueprints.Count; column++)
        {
            var bluePrint = mapConfigure.roomBlueprints[column];
            
            var amount = Random.Range(bluePrint.min, bluePrint.max);

            var roomGapY = height / (amount + 1);

            var startHeight = height / 2 - roomGapY;
            
            generatePosition = new Vector3(-width/2 + border + columnWidth*column, startHeight, 0);
            
            var newPosition = generatePosition;
            
            if (column == mapConfigure.roomBlueprints.Count - 1)
            {
                newPosition.x = width/2 - border*2;
            }
            
            for(int j=0; j<amount; j++)
            {
                newPosition.y = startHeight - roomGapY*j;
                Instantiate(roomPrefab, newPosition, Quaternion.identity, transform);
            }
        }
    }
}

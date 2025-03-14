using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int row;
    public int column;
    private SpriteRenderer spriteRenderer;
    public RoomDataSO roomData;
    public RoomState roomState;
    public List<Vector2Int> linkTo = new();
    
    [Header ("Broadcasting on Room Click")]
    public ObjectEventSO loadRoomEvent;
    
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        // SetupRoom(0,0, roomData);
    }

    private void OnMouseUp()
    {
        // Debug.Log("Room Type: " + roomData.roomType);
        if(roomState == RoomState.Attainable)
        {
            loadRoomEvent.RaiseEvent(this, this);
        }
        else
        {
            Debug.Log("The room is currently locked");
        }
        
    }
    
    /// <summary>
    /// Setup the room when it is created
    /// </summary>
    /// <param name="column"></param>
    /// <param name="row"></param>
    /// <param name="roomData"></param>
    public void SetupRoom(int column, int row, RoomDataSO roomData)
    {
        this.column = column;
        this.row = row;
        this.roomData = roomData;
        spriteRenderer.sprite = roomData.roomIcon;
        spriteRenderer.color = roomState switch
        {
            RoomState.Locked => new Color(0.5f, 0.5f, 0.5f, 1f),
            RoomState.Visited => new Color(0.5f, 0.8f, 0.5f, 0.5f),
            RoomState.Attainable => Color.white,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

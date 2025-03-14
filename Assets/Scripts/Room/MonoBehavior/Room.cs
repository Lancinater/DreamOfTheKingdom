using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int row;
    public int column;
    private SpriteRenderer spriteRenderer;
    public RoomDataSO roomData;
    public RoomState roomState;
    
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
        Debug.Log("Room Type: " + roomData.roomType);
        loadRoomEvent.RaiseEvent(roomData,this);
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
    }
}

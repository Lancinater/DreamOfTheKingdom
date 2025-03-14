using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Map Layout")]
    public MapLayoutSO mapLayout;

    
    /// <summary>
    /// Function to listen to the event of the finish room
    /// </summary>
    /// <param name="value"></param>
    public void UpdateMapLayoutData(object value)
    {
        var roomVector = (Vector2Int) value;
        
        // Find the current room and update the room state
        var currentRoom = mapLayout.mapRoomDataList.Find(room => room.column == roomVector.x && room.row == roomVector.y);
        currentRoom.roomState = RoomState.Visited;
        
        // Lock rooms that are in the same column
        var otherRooms = mapLayout.mapRoomDataList.FindAll(room => room.column == roomVector.x);
        foreach (var room in otherRooms)
        {
            if(room.row != roomVector.y)
                room.roomState = RoomState.Locked;
        }
        
        // Update the room state of the linked room to attainable
        foreach (var link in currentRoom.linkTo)
        {
            var linkedRoom = mapLayout.mapRoomDataList.Find(room => room.column == link.x && room.row == link.y);
            linkedRoom.roomState = RoomState.Attainable;
        }
    }
}

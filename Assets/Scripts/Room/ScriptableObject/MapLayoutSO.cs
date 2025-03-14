using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapLayoutSO", menuName = "Map/MapLayoutSO")]
public class MapLayoutSO : ScriptableObject
{
    public List<mapRoomData> mapRoomDataList = new();
    public List<LinePosition> linePositionList = new();
}

[System.Serializable]
public class mapRoomData
{
    public float posX, poxY;
    public int column, row;
    public RoomDataSO roomData;
    public RoomState roomState;
}

[System.Serializable]
public class LinePosition
{
    public SerializeVector3 start, end;
}

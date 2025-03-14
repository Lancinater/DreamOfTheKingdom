using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{   
    [Header("Map Configuration")]
    public MapConfigureSO mapConfigure;
    
    [Header("Map Layout")]
    public MapLayoutSO mapLayout;
    
    [Header("Prefabs")]
    public Room roomPrefab;
    public LineRenderer linePrefab;

    [Header("Border")]
    private float height;
    private float width;
    private float columnWidth;
    private Vector3 generatePosition;
    public float border;
    
    [Header("Room Data")]
    private List<Room> rooms = new List<Room>();
    public List<RoomDataSO> roomDataList = new List<RoomDataSO>();
    // Dictionary to store room data list
    Dictionary<RoomType, RoomDataSO> roomDataDic = new Dictionary<RoomType, RoomDataSO>();

    private List<LineRenderer> lines = new();
    
    private void Awake()
    {
        height = Camera.main.orthographicSize * 2;
        width = height * Camera.main.aspect;
        columnWidth = width / (mapConfigure.roomBlueprints.Count + 1);
        
        foreach (var roomData in roomDataList)
        {
            roomDataDic.Add(roomData.roomType, roomData);
        }
    }

    // private void Start()
    // {
    //     CreateMap();
    // }
    
    private void OnEnable()
    {
        if(mapLayout.mapRoomDataList.Count > 0)
        {
            LoadMap();
        }
        else
        {
            CreateMap();
        }
    }

    public void CreateMap()
    {
        // List of rooms in the previous column
        List<Room> previousColumnRooms = new List<Room>();
        
        // Generate the rooms
        for(int column=0; column<mapConfigure.roomBlueprints.Count; column++)
        {
            var bluePrint = mapConfigure.roomBlueprints[column];
            
            var amount = Random.Range(bluePrint.min, bluePrint.max);

            var roomGapY = height / (amount + 1);

            var startHeight = height / 2 - roomGapY;
            
            generatePosition = new Vector3(-width/2 + border + columnWidth*column, startHeight, 0);
            
            var newPosition = generatePosition;
            
            // List of rooms in the current column
            List<Room> currentColumnRooms = new List<Room>();
            
            if (column == mapConfigure.roomBlueprints.Count - 1)
            {
                newPosition.x = width/2 - border*2;
            }
            
            for(int j=0; j<amount; j++)
            {
                newPosition.y = startHeight - roomGapY*j;
                // generate room
                var room = Instantiate(roomPrefab, newPosition, Quaternion.identity, transform);
                rooms.Add(room);
                RoomType roomType = GetRandomRoomType(mapConfigure.roomBlueprints[column].roomType);
                
                room.SetupRoom(column, j, GetRoomData(roomType));
                currentColumnRooms.Add(room);
            }
            
            // if current column is not the first column, connect the rooms with the previous column
            if(previousColumnRooms.Count > 0)
            {
                // Connect the rooms
                CreateConnections(previousColumnRooms, currentColumnRooms);
            }
            
            previousColumnRooms = currentColumnRooms;
        }
        SaveMap();
    }

    private void CreateConnections(List<Room> column1, List<Room> column2)
    {
        HashSet<Room> visited = new HashSet<Room>();

        foreach (var room in column1)
        {
            var targetRoom = ConnectToRandomRoom(room, column2);
            visited.Add(targetRoom);
        }
        
        foreach (var room in column2)
        {
            if (!visited.Contains(room))
            {
                ReverseConnectToRandomRoom(room, column1);
            }
        }
    }

    private Room ConnectToRandomRoom(Room room, List<Room> column2)
    {
        Room targetRoom;
        
        targetRoom = column2[Random.Range(0, column2.Count)];
        
        // Create a line between the rooms
        var line = Instantiate(linePrefab, transform);
        line.SetPosition(0, room.transform.position);
        line.SetPosition(1, targetRoom.transform.position);
        lines.Add(line);
        return targetRoom;
    }
    
    private Room ReverseConnectToRandomRoom(Room room, List<Room> column2)
    {
        Room targetRoom;
        
        targetRoom = column2[Random.Range(0, column2.Count)];
        
        // Create a line between the rooms
        var line = Instantiate(linePrefab, transform);
        line.SetPosition(0, targetRoom.transform.position);
        line.SetPosition(1, room.transform.position);

        return targetRoom;
    }
    
    private RoomDataSO GetRoomData(RoomType roomType)
    {
        return roomDataDic[roomType];
    }
    
    private RoomType GetRandomRoomType(RoomType flags)
    {
        string[] rooms = flags.ToString().Split(',');
        
        string randomRoom = rooms[Random.Range(0, rooms.Length)];
        
        RoomType roomType = (RoomType) Enum.Parse(typeof(RoomType), randomRoom);
        
        return roomType;
    }

    private void SaveMap()
    {
        mapLayout.mapRoomDataList = new();

        // Save all the rooms
        for(int i = 0; i < rooms.Count; i++)
        {
            var room = new mapRoomData()
            {
                posX = rooms[i].transform.position.x,
                poxY = rooms[i].transform.position.y,
                column = rooms[i].column,
                row = rooms[i].row,
                roomData = rooms[i].roomData,
                roomState = rooms[i].roomState
            };
            
            mapLayout.mapRoomDataList.Add(room);
        }

        mapLayout.linePositionList = new();
        // Save all the lines
        for(int i = 0; i < lines.Count; i++)
        {
            var line = new LinePosition()
            {
                start = new SerializeVector3(lines[i].GetPosition(0)),
                end = new SerializeVector3(lines[i].GetPosition(1))
            };
            
            mapLayout.linePositionList.Add(line);
        }
    }
    
    private void LoadMap()
    {
        // read all the rooms
        for(int i = 0; i < mapLayout.mapRoomDataList.Count; i++)
        {
            var roomData = mapLayout.mapRoomDataList[i];
            var room = Instantiate(roomPrefab, new Vector3(roomData.posX, roomData.poxY, 0), Quaternion.identity, transform);
            room.roomState = mapLayout.mapRoomDataList[i].roomState;
            room.SetupRoom(roomData.column, roomData.row, roomData.roomData);
            rooms.Add(room);
        }
        
        // read all the lines
        for(int i = 0; i < mapLayout.linePositionList.Count; i++)
        {
            var lineData = mapLayout.linePositionList[i];
            var line = Instantiate(linePrefab, transform);
            line.SetPosition(0, lineData.start.ToVector3());
            line.SetPosition(1, lineData.end.ToVector3());
            lines.Add(line);
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RoomDataSO", menuName = "Map/MapConfigureSO")]
public class MapConfigureSO : ScriptableObject
{
    public List<RoomBlueprint> roomBlueprints;
}

[System.Serializable]
public class RoomBlueprint
{
    public int min, max;
    public RoomType roomType;
}

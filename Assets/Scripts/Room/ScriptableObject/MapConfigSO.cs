
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapConfigSO",menuName = "Map/MapConfig")]
public class MapConfigSO : ScriptableObject
{
    public List<RoomBlueprint> roomBlueprints;
}




[System.Serializable]
public class RoomBlueprint 
{
    public int min, max;
    public RoomType roomType;

}
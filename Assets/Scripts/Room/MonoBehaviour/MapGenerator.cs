using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("地图配置表")]
    public MapConfigSO mapConfig;

    [Header("预制体")]
    public Room roomPrefab;
    public LineRenderer linePrefab;

    private float screenHeight;
    private float screenWidth;

    private float columnWidth;
    private Vector3 generatePoint;

    public float border;

    private List<Room> rooms = new List<Room>();
    private List<LineRenderer> lines = new List<LineRenderer>();
    private void Awake()
    {
        screenHeight = Camera.main.orthographicSize * 2;
        screenWidth = screenHeight * Camera.main.aspect;

        columnWidth = screenWidth / mapConfig.roomBlueprints.Count;
    }

    private void Start()
    {
        CreateMap();
    }

    public void CreateMap()
    {
        //创建前一列房间列表
        List<Room> previousColumnRooms = new List<Room>();

        for (int column = 0; column < mapConfig.roomBlueprints.Count; column++)
        {
            RoomBlueprint blueprint = mapConfig.roomBlueprints[column];
            var amount = Random.Range(blueprint.min, blueprint.max);

            var startHeight = screenHeight / 2 - screenHeight / (amount + 1);
            generatePoint = new Vector3(-screenWidth / 2 + border + columnWidth * column, startHeight, 0);

            var newPosition = generatePoint;


            //创建当前列房间
            List<Room> currentColumnRooms = new List<Room>();

            var roomGapY = screenHeight / (amount + 1);
            //循环当前列的所有房间数量生成房间
            for (int i = 0; i < amount; i++)
            {

                //最后一列，boss房间
                if (column == mapConfig.roomBlueprints.Count - 1)
                {
                    newPosition.x = screenWidth / 2 - border * 2;
                }
                else if (column != 0)
                {
                    newPosition.x = generatePoint.x + Random.Range(-border / 2, border / 2);
                }

                newPosition.y = startHeight - roomGapY * i;
                var room = Instantiate(roomPrefab, newPosition, Quaternion.identity, transform);


                rooms.Add(room);
                currentColumnRooms.Add(room);
            }

            //判断当前列是否为第一列，如果不是则连接到上一列
            if(previousColumnRooms.Count > 0)
            {
                //创建俩个列表的房间连线
                CreateConnections(previousColumnRooms, currentColumnRooms);

            }

            previousColumnRooms = currentColumnRooms;
        }
    }

    private void CreateConnections(List<Room> column1, List<Room> column2)
    {
        HashSet<Room> connectedColumn2Rooms = new HashSet<Room>();//用于判断第二列哪些房间没有被连

        foreach (var room in column1)
        {
            var targetRoom =  ConnectToRandomRoom(room, column2);
            connectedColumn2Rooms.Add(targetRoom);
        }

        foreach (var room in column2)
        {
            if (!connectedColumn2Rooms.Contains(room))
            {
                ConnectToRandomRoom(room, column1);
            }
        }

    }


    private Room ConnectToRandomRoom(Room room, List<Room> column2)
    {
        Room targetRoom;

        targetRoom = column2[Random.Range(0, column2.Count)];

        //创建房间之间的连线
        var line = Instantiate(linePrefab, transform);
        line.SetPosition(0, room.transform.position);
        line.SetPosition(1, targetRoom.transform.position);
        lines.Add(line);

        return targetRoom;
    }

    [ContextMenu("ReGeneratorRoom")]
    public void ReGeneratorRoom()
    {
        foreach (var room in rooms)
        {
            Destroy(room.gameObject);
        }


        foreach (var item in lines)
        {
            Destroy(item.gameObject);
        }

        rooms.Clear();
        lines.Clear();

        CreateMap();
    }


   

}

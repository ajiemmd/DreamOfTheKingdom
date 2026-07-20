using UnityEngine;

public class Room : MonoBehaviour
{
    public int column;
    public int line;


    private SpriteRenderer spriteRenderer;

    public RoomDataSO roomData;

    public RoomState roomstate;

    private void Awake()
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        SetupRoom(0, 0, roomData);
    }

    private void OnMouseDown()
    {
        // 处理点击事件
        Debug.Log("点击了房间：" + roomData.roomType);
    }


    /// <summary>
    /// 外部创建房间时调用此函数，初始化
    /// </summary>
    /// <param name="column"></param>
    /// <param name="line"></param>
    /// <param name="roomData"></param>
    public void SetupRoom(int column, int line, RoomDataSO roomData)
    {
        this.column = column;
        this.line = line;
        this.roomData = roomData;

        spriteRenderer.sprite = roomData.roomIcon;

    }

}
    
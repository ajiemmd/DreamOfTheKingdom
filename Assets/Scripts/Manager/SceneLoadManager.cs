using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneLoadManager : MonoBehaviour
{
    private AssetReference currentScene;
    public AssetReference map;



    /// <summary>
    /// 在房间加载事件中监听
    /// </summary>
    /// <param name="data"></param>
   public void OnLoadRoomEvent(object data)
    {
        if (data is RoomDataSO)
        {
            var currentData = (RoomDataSO)data;
            //Debug.Log(currentData.roomType);

            currentScene = currentData.SceneToLoad;
        }

        //加载房间
    }

   

}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public static class ExpendOptionUI
{
    private static readonly Dictionary<string, DialogueController> Dictionary = new();

    //如果触发某事就改
    private static readonly Dictionary<string, SeeDoorManDialogueController> Dictionary2 = new();

    private static CameraMove[] obj;

    private static List<EventData> _list;

    public static void FindScript()
    {
        var objectsWithScript = Resources.FindObjectsOfTypeAll<DialogueController>();

        foreach (var data in objectsWithScript)
        {
            Dictionary.Add(data.name, data);
        }

        var objectsWithScript2 = Resources.FindObjectsOfTypeAll<SeeDoorManDialogueController>();

        foreach (var data in objectsWithScript2)
        {
            Dictionary2.Add(data.name, data);
        }

        DeserializeFromJsonFile(Application.dataPath + "/Game Data/Dialogue Data/Level_1/eventJson.json");
    }

    public static void FindScript2()
    {
        obj = Resources.FindObjectsOfTypeAll<CameraMove>();
        //Debug.Log(obj[0].name);
    }

    public static void ChangeOptionHide(string scriptName, string nextPieceID)
    {
        if (_list == null) return;
        foreach (var eventData in _list)
        {
            if (!scriptName.Equals(eventData.对话名)) continue;
            if (!nextPieceID.Equals(eventData.下一个对话id)) continue;
            Dictionary[eventData.目标对象].currentData.dialoguePieces[eventData.对话块名称].options[eventData.选项索引].isHide = eventData.change;
        }
    }

    public static void ChangeGame(string scriptName, string nextPieceID, string currPieceID)
    {
        Debug.Log(scriptName + "," + nextPieceID + "," + currPieceID);
        //暂时是确定的
        if (!scriptName.Equals("幻方游戏对话") || !nextPieceID.Equals("") || !currPieceID.Equals("3")) return;
        Debug.Log("进去了");
        foreach (var camera in obj)
        {
            Debug.Log(camera.name);
            camera.startGame = true;
        }
    }

    public static void ChangeTalk(string objectName, bool isTalk)
    {
        if (!Dictionary2[objectName]) return;
        Dictionary2[objectName].isTalk = isTalk;
    }

    public delegate void OptionChange();

    public static void JsonSerialization(List<EventData> list)
    {
        // 创建包含列表的容器类
        var container = new MyListContainer(list);

        // 序列化容器类为JSON字符串
        var json = JsonUtility.ToJson(container);
        // 定义JSON文件路径
        var filePath = Application.dataPath + "/Game Data/Dialogue Data/Level_1/eventJson.json";
        // 将JSON字符串写入文件
        File.WriteAllText(filePath, json);

    }

    public static List<EventData> DeserializeFromJsonFile(string filePath)
    {
        // 读取JSON文件内容
        var json = File.ReadAllText(filePath);

        // 反序列化JSON字符串为 MyListContainer 对象
        var container = JsonUtility.FromJson<MyListContainer>(json);

        // 赋值给list
        _list = container.list;

        return container.list;
    }
}

[System.Serializable]
public class EventData
{
    [FormerlySerializedAs("scriptName")] public string 对话名;
    [FormerlySerializedAs("nextPieceID")] public string 下一个对话id;
    [FormerlySerializedAs("targetName")] public string 目标对象;
    [FormerlySerializedAs("dialoguePieceID")] public int 对话块名称;
    [FormerlySerializedAs("optionID")] public int 选项索引;
    public bool change;


    public EventData(string 对话名, string 下一个对话id, string 目标对象, int 对话块名称
        , int 选项索引, bool change)
    {
        this.对话名 = 对话名;
        this.下一个对话id = 下一个对话id;
        this.目标对象 = 目标对象;
        this.对话块名称 = 对话块名称;
        this.选项索引 = 选项索引;
        this.change = change;
    }
}

[System.Serializable]
class MyListContainer
{
    public List<EventData> list;

    public MyListContainer(List<EventData> list)
    {
        this.list = list;
    }
}

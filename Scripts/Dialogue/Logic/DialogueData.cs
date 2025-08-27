using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Event", menuName = "Dialogue/Dialogue Event")]
public class DialogueData: ScriptableObject
{
    public List<EventData> 事件列表  ;
    public  Dictionary<string, EventData> EventDataIndex = new Dictionary<string, EventData>();
    
    
#if UNITY_EDITOR
    void OnValidate()
    {
        EventDataIndex.Clear();
        foreach(var eventData in 事件列表)
        {
            EventDataIndex.TryAdd(eventData.对话名, eventData);
        }
        ExpendOptionUI.JsonSerialization(事件列表);
    }
    private void OnEnable()
    {
        事件列表 = ExpendOptionUI.DeserializeFromJsonFile(Application.dataPath +"/Game Data/Dialogue Data/Level_1/eventJson.json");
    }
#endif
}
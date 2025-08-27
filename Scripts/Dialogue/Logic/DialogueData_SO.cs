using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData_SO : ScriptableObject
{
    public List<DialoguePiece> dialoguePieces = new List<DialoguePiece>();
    public Dictionary<string, DialoguePiece> dialogueIndex = new Dictionary<string, DialoguePiece>();
#if UNITY_EDITOR
    //当编辑器中内容更新时，即更新字典中内容
    void OnValidate()
    {
        dialogueIndex.Clear();
        foreach(var piece in dialoguePieces)
        {
            if(!dialogueIndex.ContainsKey(piece.ID))
                dialogueIndex.Add(piece.ID,piece);
        }
    }
#endif    
}

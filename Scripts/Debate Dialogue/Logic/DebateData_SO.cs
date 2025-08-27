using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Debate/Dabate Data")]
public class DebateData_SO : ScriptableObject
{
    //主对话
    public List<DebateProbePiece> debatePieces = new List<DebateProbePiece>();

    //副对话
    public List<ProbePiece> probePieces_unSubmit = new List<ProbePiece>();//例如：【好像还不是解决问题的时候...】

    //public List<ProbePiece> probePieces_canSubmit = new List<ProbePiece>();//给出响应的提示

    //由于可能有多个问题需要回答，每个回答的追问不尽相同，所以形式为：多个关键句追问List<一个追问List<每句话>>
    public List<ProbePieceList> probePieces_canSubmit = new List<ProbePieceList>();//给出响应的提示
}

[System.Serializable]
public class DebateProbePiece : ProbePiece
{
    //public string ID;
    //public string Name;
    //[TextArea]
    //public string text;
    public bool isSubmit;//当前对话是否是可提交论据 [即当前是否为关键句]

    public bool isSuccess;//是否提交论据正确

    public string submitBookName;//如果当前为关键句，那么匹配这条关键句的数学知识名称是什么?
}

[System.Serializable]
public class ProbePiece
{
    public string ID;
    public string Name;

    [TextArea]
    public string text;
}

[System.Serializable]
public class ProbePieceList
{
    public List<ProbePiece> probeList;
}
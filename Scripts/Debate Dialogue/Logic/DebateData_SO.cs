using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Debate/Dabate Data")]
public class DebateData_SO : ScriptableObject
{
    //���Ի�
    public List<DebateProbePiece> debatePieces = new List<DebateProbePiece>();

    //���Ի�
    public List<ProbePiece> probePieces_unSubmit = new List<ProbePiece>();//���磺�����񻹲��ǽ�������ʱ��...��

    //public List<ProbePiece> probePieces_canSubmit = new List<ProbePiece>();//������Ӧ����ʾ

    //���ڿ����ж��������Ҫ�ش�ÿ���ش��׷�ʲ�����ͬ��������ʽΪ������ؼ���׷��List<һ��׷��List<ÿ�仰>>
    public List<ProbePieceList> probePieces_canSubmit = new List<ProbePieceList>();//������Ӧ����ʾ
}

[System.Serializable]
public class DebateProbePiece : ProbePiece
{
    //public string ID;
    //public string Name;
    //[TextArea]
    //public string text;
    public bool isSubmit;//��ǰ�Ի��Ƿ��ǿ��ύ�۾� [����ǰ�Ƿ�Ϊ�ؼ���]

    public bool isSuccess;//�Ƿ��ύ�۾���ȷ

    public string submitBookName;//�����ǰΪ�ؼ��䣬��ôƥ�������ؼ������ѧ֪ʶ������ʲô?
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
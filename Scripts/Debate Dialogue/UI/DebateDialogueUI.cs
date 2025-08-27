using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//...................................................................................��׺��ʾ�ô���δ����
public class DebateDialogueUI : SingletonMono<DebateDialogueUI>
{
    [Header("Basic Elements")]
    //�����������һ�䣩�İ�ť
    public Button btn_next;

    //����ṩ�۾ݵİ�ť��
    public Button btn_submit;

    //���׷�ʰ�ť��
    public Button btn_probe;

    //��ʾ��ǰ�Ի����Ƶ�Text:
    public Text txt_name;
    //��ʾ�ı���Text
    public Text txt_main;

    //��ʾ���۶Ի���Panel
    public GameObject debatePanel;

    [Header("TipPanelReletive")]//������Ӧ������д���룬��������Ϊ�˷������һ���ˣ������á�
    //��ʾ��ʾ���Panel
    public GameObject tipPanel;
    //��ʾ���Button��ť
    public Button btn_tipCommit;
    //��ʾ��ı���Text
    public Text txt_tipTitle;
    //��ʾ�������Text
    public Text txt_tipDescription;

    [Header("Data")]
    public DebateData_SO currentData;

    [HideInInspector]
    public int currentIndex = 0;   //��ǰ���Ի��б������

    int currentProbeIndex = 0; //��ǰ�ؼ���׷�ʶԻ�������

    [HideInInspector]
    public int currentWhichProbe = 0;//��ǰ����һ���ؼ����Ӧ��׷�ʵ�����
    void Start()
    {
        //���ύ��尴ť:
        btn_submit?.onClick.AddListener(() =>
        {
            //���ύ���
            SubmitCanvasUI.GetInstance().OpenSubmitPanel();
        });

        //׷�ʰ�ť��������жϵ�ǰ�Ի��Ƿ��׷��
        btn_probe?.onClick.AddListener(ContinueProbeDialogue);

        //��һ�䰴ť
        btn_next?.onClick.AddListener(ContinueDebate);

        //�����ʾ��ȷ�ϰ�ť�������䲻�ɼ�s
        btn_tipCommit?.onClick.AddListener(() =>
        {
            tipPanel.SetActive(false);
        });

    }

    private void ContinueProbeDialogue()
    {
        //���ڵ����׷�ʣ��ᴥ���¶Ի������Ի�֮��ĶԻ�������˵�������߼�Ҳ����仯

        //����ǰ�Ի�Ƭ��Ϊ�ؼ��䣬׷�ʺ󽫽�����ȫ�¶Ի�
        if (currentData.debatePieces[currentIndex].isSubmit == true)
        {
            btn_next?.onClick.RemoveAllListeners();
            btn_next?.onClick.AddListener(ContinueDebate_InProbe_canSubmit);
            UpdateMainDebate(currentData.probePieces_canSubmit[currentWhichProbe].probeList[0]);
        }else//�����ǹؼ��䣬�ҵ����׷�ʰ�ť����ʾ
        {
            btn_next?.onClick.RemoveAllListeners();
            btn_next?.onClick.AddListener(ContinueDebate_InProbe_unSubmit);
            UpdateMainDebate(currentData.probePieces_unSubmit[0]);//�ǹؼ�����׷��ֻ����ʾ������ֻ��Ҫһ�仰����
        }
    }

    private void ContinueDebate_InProbe_canSubmit()
    {
        //Ҫ��Ҫ�ܹ��ص����Ի�
        if(currentProbeIndex < currentData.probePieces_canSubmit.Count - 1)
        {
            currentProbeIndex++;
            UpdateMainDebate(currentData.probePieces_canSubmit[currentWhichProbe].probeList[currentProbeIndex]);
        }
        else
        {
            //����������������һ�ε��
            currentProbeIndex = 0;
            //��������Ҫʵ�ֵ��߼���ContinueDebate_InProbe_unSubmit������ͬ�����Ծ�ֱ��д
            ContinueDebate_InProbe_unSubmit();
        }
    }

    private void ContinueDebate_InProbe_unSubmit()
    {
        //������������Ĭ��һ�仰�����Ե���ͻص����Ի�
        btn_next?.onClick.RemoveAllListeners();
        btn_next?.onClick.AddListener(ContinueDebate);

        //�ص����Ի��߼�
        UpdateMainDebate(currentData.debatePieces[currentIndex]);
    }

    //�������һ��Ի�ʱ��
    public void ContinueDebate()
    {
        if (currentIndex < currentData.debatePieces.Count - 1)
        {
            //�����ǰΪ�ؼ���,��δ�����ǰ����
            if(currentData.debatePieces[currentIndex].isSubmit == true 
                && currentData.debatePieces[currentIndex].isSuccess == false)
            {
                //��ʾ�����Ե��������
                ErrorSubmitTip("��ʾ", "���Ƚ���ǰ��������!");
            }
            else//���ǹؼ��䣬���ǽ�������⡿ ���� ���þ䲻�ǹؼ��䡿����ʾ��һ�仰
            {
                currentIndex++;
                UpdateMainDebate(currentData.debatePieces[currentIndex]);
            }
        }
        else//����öԻ�Ϊ���һ�仰����ֱ��������������Ǳ��Ҫ�����һ��Ի������ǹؼ��䡿
        {
            //�л���Ϸ�ӽǵ�״̬
            PlayerCameraMgr.GetInstance().EnableMainCamera();
            PlayerCameraMgr.GetInstance().EnablePlayerMove();
            OperationStateMgr.GetInstance().DisableNowDialogueCamera();
            OperationStateMgr.GetInstance().SwitchCursorState(false);
            debatePanel.SetActive(false);
        }
    }

    public void UpdateDebateData(DebateData_SO data)
    {
        currentData = data;
        currentIndex = 0;
    }

    public void UpdateMainDebate(ProbePiece piece)//ProbePiece��DebatePiece�ĸ���
    {
        //��ʾ���
        debatePanel.SetActive(true);
        txt_name.text = piece.Name;//���Ի������ָ�ֵ
        txt_main.text = "";
        txt_main.DOText(piece.text, 1f);
    }

    //----------���ⲿ����
    //����ǰ�������óɹ��ύ
    public void UpdatePieceIsSuccess()
    {
        currentData.debatePieces[currentIndex].isSuccess = true;
    }
    
    //����������ʾ����
    public void ErrorSubmitTip(string title, string description)
    {
        tipPanel.SetActive(true);
        txt_tipTitle.text = title;
        txt_tipDescription.text = description;
    }
}

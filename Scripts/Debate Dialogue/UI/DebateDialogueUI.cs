using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//...................................................................................后缀表示该代码未完善
public class DebateDialogueUI : SingletonMono<DebateDialogueUI>
{
    [Header("Basic Elements")]
    //点击继续（下一句）的按钮
    public Button btn_next;

    //点击提供论据的按钮：
    public Button btn_submit;

    //点击追问按钮：
    public Button btn_probe;

    //显示当前对话名称的Text:
    public Text txt_name;
    //显示文本的Text
    public Text txt_main;

    //显示辩论对话的Panel
    public GameObject debatePanel;

    [Header("TipPanelReletive")]//【这里应该另外写代码，但是这里为了方便放在一起了，不够好】
    //显示提示框的Panel
    public GameObject tipPanel;
    //提示框的Button按钮
    public Button btn_tipCommit;
    //提示框的标题Text
    public Text txt_tipTitle;
    //提示框的内容Text
    public Text txt_tipDescription;

    [Header("Data")]
    public DebateData_SO currentData;

    [HideInInspector]
    public int currentIndex = 0;   //当前主对话列表的索引

    int currentProbeIndex = 0; //当前关键句追问对话的索引

    [HideInInspector]
    public int currentWhichProbe = 0;//当前是哪一个关键句对应的追问的索引
    void Start()
    {
        //打开提交面板按钮:
        btn_submit?.onClick.AddListener(() =>
        {
            //打开提交面板
            SubmitCanvasUI.GetInstance().OpenSubmitPanel();
        });

        //追问按钮：点击后判断当前对话是否可追问
        btn_probe?.onClick.AddListener(ContinueProbeDialogue);

        //下一句按钮
        btn_next?.onClick.AddListener(ContinueDebate);

        //点击提示的确认按钮，就让其不可见s
        btn_tipCommit?.onClick.AddListener(() =>
        {
            tipPanel.SetActive(false);
        });

    }

    private void ContinueProbeDialogue()
    {
        //由于点击了追问，会触发新对话【主对话之外的对话】，因此点击继续逻辑也必须变化

        //若当前对话片段为关键句，追问后将将触发全新对话
        if (currentData.debatePieces[currentIndex].isSubmit == true)
        {
            btn_next?.onClick.RemoveAllListeners();
            btn_next?.onClick.AddListener(ContinueDebate_InProbe_canSubmit);
            UpdateMainDebate(currentData.probePieces_canSubmit[currentWhichProbe].probeList[0]);
        }else//若不是关键句，且点击了追问按钮，显示
        {
            btn_next?.onClick.RemoveAllListeners();
            btn_next?.onClick.AddListener(ContinueDebate_InProbe_unSubmit);
            UpdateMainDebate(currentData.probePieces_unSubmit[0]);//非关键句点击追问只是提示，所以只需要一句话足矣
        }
    }

    private void ContinueDebate_InProbe_canSubmit()
    {
        //要求要能够回到主对话
        if(currentProbeIndex < currentData.probePieces_canSubmit.Count - 1)
        {
            currentProbeIndex++;
            UpdateMainDebate(currentData.probePieces_canSubmit[currentWhichProbe].probeList[currentProbeIndex]);
        }
        else
        {
            //重置索引，便于下一次点击
            currentProbeIndex = 0;
            //这里由于要实现的逻辑和ContinueDebate_InProbe_unSubmit函数相同，所以就直接写
            ContinueDebate_InProbe_unSubmit();
        }
    }

    private void ContinueDebate_InProbe_unSubmit()
    {
        //由于这里设置默认一句话，所以点击就回到主对话
        btn_next?.onClick.RemoveAllListeners();
        btn_next?.onClick.AddListener(ContinueDebate);

        //回到主对话逻辑
        UpdateMainDebate(currentData.debatePieces[currentIndex]);
    }

    //当点击下一句对话时：
    public void ContinueDebate()
    {
        if (currentIndex < currentData.debatePieces.Count - 1)
        {
            //如果当前为关键句,且未解决当前问题
            if(currentData.debatePieces[currentIndex].isSubmit == true 
                && currentData.debatePieces[currentIndex].isSuccess == false)
            {
                //提示不可以点击继续了
                ErrorSubmitTip("提示", "请先将当前的问题解决!");
            }
            else//【是关键句，但是解决了问题】 或者 【该句不是关键句】就显示下一句话
            {
                currentIndex++;
                UpdateMainDebate(currentData.debatePieces[currentIndex]);
            }
        }
        else//如果该对话为最后一句话，就直接跳出，【这里潜在要求：最后一句对话不能是关键句】
        {
            //切换游戏视角等状态
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

    public void UpdateMainDebate(ProbePiece piece)//ProbePiece是DebatePiece的父类
    {
        //显示面板
        debatePanel.SetActive(true);
        txt_name.text = piece.Name;//将对话的名字赋值
        txt_main.text = "";
        txt_main.DOText(piece.text, 1f);
    }

    //----------供外部调用
    //将当前句子设置成功提交
    public void UpdatePieceIsSuccess()
    {
        currentData.debatePieces[currentIndex].isSuccess = true;
    }
    
    //给出错误提示内容
    public void ErrorSubmitTip(string title, string description)
    {
        tipPanel.SetActive(true);
        txt_tipTitle.text = title;
        txt_tipDescription.text = description;
    }
}

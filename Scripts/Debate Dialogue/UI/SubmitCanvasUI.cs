using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitCanvasUI : SingletonMono<SubmitCanvasUI>
{
    // 是否显示的提交面板
    public GameObject submitPanel;
    //显示内容的面板
    /// <summary>
    /// 这里显示内容的面板 上有个脚本需要激活时调用，所以要在外部控制此面板的失活激活
    /// </summary>
    public GameObject submitContentPanel;

    //点击取消的按钮
    public Button btn_close;

    //点击提交的按钮
    public Button btn_submit;

    //当前确定提交的书籍名称----用于对比是否提交正确
    [HideInInspector]
    public string submitBookName;
    void Start()
    {
        btn_close.onClick.AddListener(() =>
        {
            ClosePanel();
        });

        //提交按钮的逻辑较为复杂
        btn_submit.onClick.AddListener(() =>
        {
            int index = DebateDialogueUI.GetInstance().currentIndex;
            if(DebateDialogueUI.GetInstance().currentData.debatePieces[index].isSubmit)
            {
                //如果当前点击的按钮对应的书籍名称 等于 当前对话所需的书籍名称
                if (submitBookName == DebateDialogueUI.GetInstance().currentData.debatePieces[index].submitBookName)
                {
                    DebateDialogueUI.GetInstance().currentData.debatePieces[index].isSuccess = true;
                    DebateDialogueUI.GetInstance().currentWhichProbe++;//通过了这次回答，就将下一次的关键句追问更改
                    DebateDialogueUI.GetInstance().ContinueDebate();
                    //再执行和关闭按钮一样的逻辑
                    ClosePanel();
                }
                else//如果提交论据失败
                {
                    //关闭提交界面
                    ClosePanel();
                    //显示错误提示
                    DebateDialogueUI.GetInstance().ErrorSubmitTip("提示", "这个知识点好像不是解决问题的关键...再仔细看看吧。");
                }
            }else//如果当前不是关键句但是点击了提交
            {
                ClosePanel();
                DebateDialogueUI.GetInstance().ErrorSubmitTip("提示", "好像还不是回答问题的时候...");
            }
        });
    }
    
    private void ClosePanel()
    {
        submitContentPanel.SetActive(false);
        submitPanel.SetActive(false);
        //还要将显示内容的Panel失活
        ReelPanelMgr.GetInstance().reelPanel.SetActive(false);
    }

    //供其他脚本调用,打开显示面板脚本 + 书本内容面板脚本
    public void OpenSubmitPanel()
    {
        submitPanel.SetActive(true);
        submitContentPanel.SetActive(true);
    }
}

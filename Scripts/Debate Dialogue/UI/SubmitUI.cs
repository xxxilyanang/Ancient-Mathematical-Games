using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


//废弃不用的代码
public class SubmitUI : SingletonMono<SubmitUI>
{
    //卷轴Panel
    public GameObject submitPanel;
    //点击取消的按钮
    public Button btn_close;
    //点击提交的按钮
    public Button btn_submit;
    //放置内容的Panel
    public GameObject contentPanel;
    
    //当前reel上挂载的UI的动画控制机
    private Animator canvas_animator;
    //关闭动画执行完后，该参数标志此时应该关闭该面板了
    private bool isCloseSubmitPanel;

    private bool isSubmitTrue;

    public Button submit_btn;
    void Start()
    {
        canvas_animator = submitPanel.GetComponent<Animator>();
        btn_close.onClick.AddListener(() =>
        {
            //画布执行关闭动画
            canvas_animator.SetBool("isClose", true);
        });
        btn_submit.onClick.AddListener(() =>
        {
            if (isSubmitTrue)//如果提交的正确
            {
                //更新当前关键句为成功提交
                DebateDialogueUI.GetInstance().UpdatePieceIsSuccess();
                //画布执行关闭动画
                canvas_animator.SetBool("isClose", true);
                DebateDialogueUI.GetInstance().ContinueDebate();
            }
        });
        submit_btn.onClick.AddListener(BtnEvent);
    }
    public void BtnEvent()
    {
        isSubmitTrue = true;

    }
    void Update()
    {
        //应该将自己失活了
        if (isCloseSubmitPanel)
        {
            submitPanel.SetActive(false);
        }
    }

    //动画:reel_close 的事件调用该方法，方便动画执行完后，失活面板
    public void SetSubmitPanelFalse()
    {
        isCloseSubmitPanel = true;
    }

    //供其他脚本调用,打开显示面板脚本
    public void OpenSubmitPanel()
    {
        submitPanel.SetActive(true);
    }
}

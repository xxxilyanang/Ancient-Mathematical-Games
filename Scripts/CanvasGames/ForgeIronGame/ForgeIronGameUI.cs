using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgeIronGameUI : SingletonMono<ForgeIronGameUI>
{
    //退出按钮
    [Tooltip("退出按钮")]
    public Button btn_exit;
    
    [Tooltip("控制结束的面板")]
    public GameObject EndPanel;

    [Tooltip("结束面板的确认按钮")]
    public Button btn_confirm;
    
    [Tooltip("结束面板的提示文本")]
    public Text txtTip;

    [Tooltip("游戏的条移动的显示面板")]
    public GameObject ShowPanel;

    [Tooltip("显示当前分数的文本")]
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        btn_confirm.onClick.AddListener(switchGameState);
        btn_exit.onClick.AddListener(switchGameState);
    }

    // Update is called once per frame
    void Update()
    {
        //如果当前游戏结束了
        if (ForgeIronGameMgr.GetInstance().isEnd) {
            ForgeIronGameMgr.GetInstance().isEnd = false;
            txtTip.text = "恭喜通关，获得典籍+1";
            ShowPanel.SetActive(false);
            EndPanel.SetActive(true);
        }
    }

    private void switchGameState()
    {
        //CanvasGameMgr.GetInstance().DisableNowGameCanvas();

        this.gameObject.SetActive(false);//不能直接，控制显影的函数在其他代码中
        print(this.gameObject.name);
        PlayerCameraMgr.GetInstance().EnableMainCamera();
        PlayerCameraMgr.GetInstance().EnablePlayerMove();
        OperationStateMgr.GetInstance().DisableNowDialogueCamera();
        OperationStateMgr.GetInstance().SwitchCursorState(false);
    }

    public void ResetGameState()
    {
        ForgeIronGameMgr.GetInstance().totalScore = 0;

        ShowPanel.SetActive(true);

        EndPanel.SetActive(false);

        text.text = "当前分数：0 / 50";
    }

    public void UpdateScore(string score)
    {
        text.text = score;
    }
}

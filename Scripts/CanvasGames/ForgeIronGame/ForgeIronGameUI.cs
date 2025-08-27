using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgeIronGameUI : SingletonMono<ForgeIronGameUI>
{
    //�˳���ť
    [Tooltip("�˳���ť")]
    public Button btn_exit;
    
    [Tooltip("���ƽ��������")]
    public GameObject EndPanel;

    [Tooltip("��������ȷ�ϰ�ť")]
    public Button btn_confirm;
    
    [Tooltip("����������ʾ�ı�")]
    public Text txtTip;

    [Tooltip("��Ϸ�����ƶ�����ʾ���")]
    public GameObject ShowPanel;

    [Tooltip("��ʾ��ǰ�������ı�")]
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
        //�����ǰ��Ϸ������
        if (ForgeIronGameMgr.GetInstance().isEnd) {
            ForgeIronGameMgr.GetInstance().isEnd = false;
            txtTip.text = "��ϲͨ�أ���õ伮+1";
            ShowPanel.SetActive(false);
            EndPanel.SetActive(true);
        }
    }

    private void switchGameState()
    {
        //CanvasGameMgr.GetInstance().DisableNowGameCanvas();

        this.gameObject.SetActive(false);//����ֱ�ӣ�������Ӱ�ĺ���������������
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

        text.text = "��ǰ������0 / 50";
    }

    public void UpdateScore(string score)
    {
        text.text = score;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


//�������õĴ���
public class SubmitUI : SingletonMono<SubmitUI>
{
    //����Panel
    public GameObject submitPanel;
    //���ȡ���İ�ť
    public Button btn_close;
    //����ύ�İ�ť
    public Button btn_submit;
    //�������ݵ�Panel
    public GameObject contentPanel;
    
    //��ǰreel�Ϲ��ص�UI�Ķ������ƻ�
    private Animator canvas_animator;
    //�رն���ִ����󣬸ò�����־��ʱӦ�ùرո������
    private bool isCloseSubmitPanel;

    private bool isSubmitTrue;

    public Button submit_btn;
    void Start()
    {
        canvas_animator = submitPanel.GetComponent<Animator>();
        btn_close.onClick.AddListener(() =>
        {
            //����ִ�йرն���
            canvas_animator.SetBool("isClose", true);
        });
        btn_submit.onClick.AddListener(() =>
        {
            if (isSubmitTrue)//����ύ����ȷ
            {
                //���µ�ǰ�ؼ���Ϊ�ɹ��ύ
                DebateDialogueUI.GetInstance().UpdatePieceIsSuccess();
                //����ִ�йرն���
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
        //Ӧ�ý��Լ�ʧ����
        if (isCloseSubmitPanel)
        {
            submitPanel.SetActive(false);
        }
    }

    //����:reel_close ���¼����ø÷��������㶯��ִ�����ʧ�����
    public void SetSubmitPanelFalse()
    {
        isCloseSubmitPanel = true;
    }

    //�������ű�����,����ʾ���ű�
    public void OpenSubmitPanel()
    {
        submitPanel.SetActive(true);
    }
}

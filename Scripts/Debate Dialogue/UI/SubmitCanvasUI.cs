using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitCanvasUI : SingletonMono<SubmitCanvasUI>
{
    // �Ƿ���ʾ���ύ���
    public GameObject submitPanel;
    //��ʾ���ݵ����
    /// <summary>
    /// ������ʾ���ݵ���� ���и��ű���Ҫ����ʱ���ã�����Ҫ���ⲿ���ƴ�����ʧ���
    /// </summary>
    public GameObject submitContentPanel;

    //���ȡ���İ�ť
    public Button btn_close;

    //����ύ�İ�ť
    public Button btn_submit;

    //��ǰȷ���ύ���鼮����----���ڶԱ��Ƿ��ύ��ȷ
    [HideInInspector]
    public string submitBookName;
    void Start()
    {
        btn_close.onClick.AddListener(() =>
        {
            ClosePanel();
        });

        //�ύ��ť���߼���Ϊ����
        btn_submit.onClick.AddListener(() =>
        {
            int index = DebateDialogueUI.GetInstance().currentIndex;
            if(DebateDialogueUI.GetInstance().currentData.debatePieces[index].isSubmit)
            {
                //�����ǰ����İ�ť��Ӧ���鼮���� ���� ��ǰ�Ի�������鼮����
                if (submitBookName == DebateDialogueUI.GetInstance().currentData.debatePieces[index].submitBookName)
                {
                    DebateDialogueUI.GetInstance().currentData.debatePieces[index].isSuccess = true;
                    DebateDialogueUI.GetInstance().currentWhichProbe++;//ͨ������λش𣬾ͽ���һ�εĹؼ���׷�ʸ���
                    DebateDialogueUI.GetInstance().ContinueDebate();
                    //��ִ�к͹رհ�ťһ�����߼�
                    ClosePanel();
                }
                else//����ύ�۾�ʧ��
                {
                    //�ر��ύ����
                    ClosePanel();
                    //��ʾ������ʾ
                    DebateDialogueUI.GetInstance().ErrorSubmitTip("��ʾ", "���֪ʶ������ǽ������Ĺؼ�...����ϸ�����ɡ�");
                }
            }else//�����ǰ���ǹؼ��䵫�ǵ�����ύ
            {
                ClosePanel();
                DebateDialogueUI.GetInstance().ErrorSubmitTip("��ʾ", "���񻹲��ǻش������ʱ��...");
            }
        });
    }
    
    private void ClosePanel()
    {
        submitContentPanel.SetActive(false);
        submitPanel.SetActive(false);
        //��Ҫ����ʾ���ݵ�Panelʧ��
        ReelPanelMgr.GetInstance().reelPanel.SetActive(false);
    }

    //�������ű�����,����ʾ���ű� + �鱾�������ű�
    public void OpenSubmitPanel()
    {
        submitPanel.SetActive(true);
        submitContentPanel.SetActive(true);
    }
}

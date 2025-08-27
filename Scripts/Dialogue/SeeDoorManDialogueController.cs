using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeDoorManDialogueController : MonoBehaviour
{
    //�ⲿ���������Ӧ�ĵ�ǰ�Ի�
    public DialogueData_SO currentData;

    //�����л��۲�NPC�������
    public GameObject camera_SeeNPC;

    public  bool isTalk = true; 
    
    //��ǰ�Ƿ��ڿɶԻ�״̬
    bool canTalk = false;
    void OnTriggerEnter(Collider other)
    {
        if (!isTalk||!other.CompareTag("Player") || currentData == null) return;
        //�򿪶Ի����
        OpenDialogue();
            
        //��ֹ�����ƶ�  �� ���������
        PlayerCameraMgr.GetInstance().DisablePlayerMove();
        PlayerCameraMgr.GetInstance().DisableMainCamera();

        //������ӦNPC�ϵ������������ԭ���������
        camera_SeeNPC.SetActive(true);

        //������������������ɼ�
        OperationStateMgr.GetInstance().SwitchCursorState(true);
        
        PlayerCameraMgr.GetInstance().PlayerBack(2,0,0);
    }
    
    void OpenDialogue()
    {
        //����Ի���Ϣ
        //�򿪶Ի����
        OperationStateMgr.GetInstance().currentCamera = camera_SeeNPC;
        //DialogueUI.GetInstance().SetCurrentNPCCamera(camera_SeeNPC);//����NPC�����
        DialogueUI.GetInstance().UpdateDialogueData(currentData);//��ʼ������
        DialogueUI.GetInstance().UpdateMainDialogue(currentData.dialoguePieces[0]);
        // ��ȡ���й������ض��ű���GameObject
        // StartCoroutine(ExpendDialogueController.Enumerator(this,gameObject));
    }
}

